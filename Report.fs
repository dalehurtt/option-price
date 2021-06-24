module Report

open System
open System.IO
open Trade
open TDAmeritrade

let blue = "<span class=\"blue\">HOLD</span>"
let bluesell = "<span class=\"cyan\">SELL</span>"
let green = "<span class=\"green\">SELL</span>"
let red = "<span class=\"red\">SELL</span>"
let yellow = "<span class=\"yellow\">ASSESS</span>"


// -------------------- BASIC REPORT IO FUNCTIONS --------------------


let appendToReport filename lines =
    File.AppendAllLines (filename, lines)
    ()


let endReport filename =
    File.AppendAllText (filename, "</body></html>")


let startReport filename =
    File.WriteAllText (filename, (sprintf "<html><style>.black {color:black;} .blue {color:blue;} .cyan {color:#00ccff;} .red {color:red;} .green {color:green;} .yellow {color:gold;} body {background-color:#4d4d4d;} h1,h2,h3,p,blockquote {color:white;} .covered {color:#ccccff;}</style><body><h1>Options Positions Report %s </h1>" (DateTime.Now.ToShortDateString ())))
    ()


// -------------------- OPTION DATA CALCULATIONS --------------------


let calcPotentialPrice (json : OptionQuote.Root) =
    (json.BidPrice + json.AskPrice) / 2.0M


// -------------------- STRING REPRESENTATION OF OPTION DATA --------------------


(* Lists out the deltas *)
let showDeltas (deltas : List<decimal>) =
    let mutable ctr = 0
    List.fold (fun str (delta : decimal) ->
        ctr <- ctr + 1
        if ctr = deltas.Length then sprintf "%s and %.2f." str delta
        elif deltas.Length = 2 then sprintf "%s %.2f" str delta
        else sprintf "%s %.2f," str delta
    ) "The deltas are " deltas


// -------------------- XX --------------------


let getDelta (trade : OptionTrade) (quote : OptionQuote.Root) =
    let tradeDelta = trade.Delta
    let curDelta = quote.Delta
    match trade.OptionType, trade.Position with
    | Call, Long ->
        let midDelta = (0.50M + curDelta) / 2.0M
        if curDelta >= tradeDelta then
            sprintf "Delta | Current %.2f | Initial %.2f | Now higher. %s" curDelta tradeDelta blue
        elif curDelta <= 0.50M then
            sprintf "Delta | Current %.2f | Initial %.2f | Close out remaining position. %s" curDelta tradeDelta red
        elif curDelta <= midDelta then
            sprintf "Delta | Current %.2f | Initial %.2f | Close out half of position. %s" curDelta tradeDelta red
        else
            sprintf "Delta | Current %.2f | Initial %.2f | %.2f below when purchased. %s" curDelta tradeDelta (tradeDelta - curDelta) yellow

    | Call, Short -> ""

    | Put, Long -> ""

    | Put, Short ->
        let midDelta = (-0.50M + curDelta) / 2.0M
        if curDelta >= tradeDelta then
            sprintf "Delta | Current %.2f | Initial %.2f | Now lower and farther out of the money. %s" curDelta tradeDelta blue
        elif curDelta < tradeDelta && curDelta <= midDelta then
            sprintf "Delta | Current %.2f | Initial %.2f | Close out half of position. %s" curDelta tradeDelta red
        elif curDelta < tradeDelta && curDelta <= -0.50M then
            sprintf "Delta | Current %.2f | Initial %.2f | Close out remaining position. %s" curDelta tradeDelta red
        else
            sprintf "Delta | Current %.2f | Initial %.2f | %.2f below when purchased. %s" curDelta tradeDelta (curDelta - tradeDelta) yellow


let getTheta (trade : OptionTrade) (quote : OptionQuote.Root) =
    let mutable theta = 0.0M
    try
        theta <- Math.Abs (quote.Theta)
    with
        | _ -> ()
    match trade.OptionType, trade.Position with
    | Call, Long | Put, Long ->
        sprintf "The option is losing $%.2f value each day with %i days remaining until expiration. Time is working against me." theta quote.DaysToExpiration

    | Call, Short | Put, Short ->
        sprintf "The option is losing $%.2f value each day with %i days remaining until expiration. Time is working in my favor." theta quote.DaysToExpiration


let isDeltaTooHigh (delta : decimal) =
    let d =  Math.Abs (delta)
    d >= 0.20M


let isAnyDeltaTooHigh (quotes : List<OptionQuote.Root>) =
    List.fold (fun found (quote : OptionQuote.Root) ->
        found || (isDeltaTooHigh quote.Delta)
    ) false quotes


let showPotentialPrices (quotes : List<OptionQuote.Root>) =
    let mutable ctr = 0
    List.fold (fun  str (quote : OptionQuote.Root)  ->
        ctr <- ctr + 1
        if ctr = 1 then
            sprintf "%s $ %.2f," str (calcPotentialPrice quote)
        elif ctr = quotes.Length then
            sprintf "%s and $ %.2f" str (calcPotentialPrice quote)
        else
            sprintf "%s $ -%.2f," str (calcPotentialPrice quote)
    ) "Option values are " quotes
    |> sprintf "%s. "

let showStockPrice curprice tradeprice otype position =
    let change = (curprice - tradeprice) / tradeprice * 100.0M
    let color =
        match otype, position with
        | Call, Long | Put, Short ->
            if curprice >= tradeprice
            then blue
            else yellow
        | Call, Short | Put, Long ->
            if curprice >= tradeprice
            then yellow
            else blue
    sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Change %.2f %%. %s" curprice tradeprice change color


let showUnderlying (underlyingtostrike : decimal) otype =
    let absults = Math.Abs (underlyingtostrike) * 100.0M
    let ou =
        match otype with
        | Call ->
            if underlyingtostrike < -0.2M then "under"
            elif underlyingtostrike < -0.1M then "under"
            elif underlyingtostrike <= -0.0M then "under"
            else "over"
        | Put ->
            if underlyingtostrike > 0.2M  then "over"
            elif underlyingtostrike > 0.1M then "over"
            elif underlyingtostrike > 0.0M then "over"
            else "under"
    sprintf "Underlying is %.2f %% %s the strike price of the covering option." absults ou


(*  This always assumes that the covering Call or Put is always farther out of the money than the option
    being covered. Note: to assess a covered call for a stock, set the otype to Call.
*)
let showCoverStrike (quote : OptionQuote.Root) otype credit =
    let curvalue = calcPotentialPrice quote
    let underlyingtostrike = (quote.UnderlyingPrice - quote.StrikePrice) / quote.StrikePrice
    let delta = quote.Delta
    let color =
        match otype with
        | Call ->
            if delta >= 0.30M then red
            elif delta >= 0.20M then yellow
            else blue
        |Put ->
            if delta <= -0.30M then red
            elif delta <= -0.20M then yellow
            else  blue
    
    sprintf "Delta is %.2f. %s Current value is %.2f compared to the original credit of %.2f. %s" delta (showUnderlying underlyingtostrike otype) curvalue credit color


let showCoverOptionValue coversymbol otype credit =
    if coversymbol = ""
    then ["This position does not have a covering option."]
    else
        let quote = TDAmeritrade.getOptionQuote coversymbol
        let lines =
            //[sprintf "%s Delta: %.2f" (showCoverStrike quote otype) quote.Delta]
            [showCoverStrike quote otype credit]
        lines


let showOptionValue curvalue costbasis otype position =
    let (change, color) =
        match position with
        | Long ->
            let change = (curvalue - costbasis) / costbasis * 100.0M
            if curvalue >= costbasis
            then
                if curvalue >= (costbasis * 1.8M)
                then (change ,green)
                else (change, blue)
            else (change, yellow)
            
        | Short ->
            let change = (costbasis - curvalue) / costbasis * 100.0M
            if curvalue <= costbasis
            then
                if (curvalue / costbasis) <= 0.50M
                then (change, green)
                else (change, blue)
            else (change, yellow)
    sprintf "%A Value | Current $ %.2f | Purchased $ %.2f | Change %.2f %%. %s" otype curvalue costbasis change color


let showIV (trade : OptionTrade) (quote : OptionQuote.Root) =
    let curiv = quote.Volatility
    let tradeiv = trade.IV
    let (color, movement, result, action) =
        match trade.Position with
        | Long ->
            if curiv >= tradeiv
            then (blue, "expanded", "for", "sell")
            else (yellow, "contracted", "against", "sell")
        | Short -> 
            if curiv <= tradeiv
            then (blue, "contracted", "for",  "buy back")
            else (yellow, "expanded", "against", "buy back")
    sprintf "Volatility | Current %.2f | Initial %.2f | Volatility has %s, which will go %s me when I %s the option. %s" curiv tradeiv movement result action color


let onCondor filename (trade : CondorTrade) =
    let quotes = TDAmeritrade.getCondorQuotes trade.Symbols
    let underlying = quotes.Head.Underlying
    let expdays = quotes.Head.DaysToExpiration

    let mutable ctr = 0
    let mutable curPL =
        List.fold (fun price (quote : OptionQuote.Root)  ->
            ctr <- ctr + 1
            if ctr = 1 || ctr = 4 then
                price + (calcPotentialPrice quote)
            else
                price - (calcPotentialPrice quote)
        ) 0.0M quotes
    curPL <- curPL * (decimal) trade.Contracts * 100.0M

    let lines =
        [sprintf "<h3>Report on %s Iron Condor</h3><p>" underlying] @
        [if curPL >= trade.TargetPL then
            sprintf "This trade has reached its target profit of $ %.2f. You are aiming to get $ %.2f when closing this position. %s" trade.TargetPL curPL green
        elif expdays <= 7 then
            sprintf "This position is within %i days of expiration. %s" expdays bluesell
        elif isAnyDeltaTooHigh quotes then
            sprintf "At least one leg in this position is over the target delta of .20/-.20. %s" red
        else
            sprintf "Profit/Loss is estimated at $ %.2f. %s" curPL blue] @
        [showPotentialPrices quotes] @
        [showDeltas (List.map (fun (quote : OptionQuote.Root) -> quote.Delta) quotes)] @
        ["</p>"]
    lines |> List.toArray |> appendToReport filename


(*  Using the trade data, get the option symbol, fetch the data from TD Ameritrade, and append the data to
    the report.
 *)
let onLEAP filename (trade : OptionTrade) =
    let quote = TDAmeritrade.getOptionQuote trade.Symbol
    let lines =
        [sprintf "<h3>Report on %s LEAP %s %A %A</h3>" quote.Underlying trade.Symbol trade.Position trade.OptionType] @
        [sprintf "<p>"] @
        [showOptionValue (calcPotentialPrice quote) trade.CostBasis trade.OptionType trade.Position] @
        ["<br />"] @
        [showStockPrice  quote.UnderlyingPrice trade.StockPrice trade.OptionType trade.Position] @
        ["<br />"] @
        [getDelta trade quote] @
        ["<br />"] @
        [showIV trade quote] @
        ["<br />"] @
        [getTheta trade quote] @
        ["</p><p class=\"covered\">"] @
        (showCoverOptionValue trade.Cover trade.OptionType trade.Credit) @
        ["</p>"]
    lines |> List.toArray |> appendToReport filename


let showOptionPositionAndType (options : List<OptionTrade>) =
    let opstr =
        List.fold (fun str (option : OptionTrade) ->
            sprintf "%s%A %A/" str option.Position option.OptionType
        ) "(" options
    sprintf "%s)" (opstr.Substring (0, opstr.Length-1))


let onSpread filename (trade : SpreadTrade) =
    let quotes =
        List.map (fun (option : OptionTrade) ->
            option.Symbol
        ) trade.Options
        |> TDAmeritrade.getMultipleQuotes
    let underlying = quotes.Head.Underlying
    let expdays = quotes.Head.DaysToExpiration
    let contracts = Math.Abs (trade.Options.Head.Contracts)

    let curPL =
        List.fold2 (fun price (quote : OptionQuote.Root) trade ->
            price + ((calcPotentialPrice quote) * (decimal) trade.Contracts * 100.0M)
        ) 0.0M quotes trade.Options

    let lines =
        [sprintf "<h3>Report on %s %A %A Spread</h3><p>" underlying trade.Options.Head.OptionType trade.SpreadType] @
        [showOptionPositionAndType trade.Options] @
        [showDeltas (List.map (fun (quote : OptionQuote.Root) -> quote.Delta) quotes)] @
        [showPotentialPrices quotes] @
        [if curPL >= trade.TargetPL then
            sprintf "This trade has reached its target profit of $ %.2f. You are aiming to get $ %.2f when closing this position. %s" trade.TargetPL curPL green
        elif expdays <= 7 then
            sprintf "This position is within %i days of expiration. %s" expdays bluesell
        elif isAnyDeltaTooHigh quotes then
            sprintf "At least one leg in this position is over the target delta of .20/-.20. %s" red
        else
            sprintf "Profit/Loss is estimated at $ %.2f. %s" curPL blue] @
        ["</p>"]
    lines |> List.toArray |> appendToReport filename


let onStock filename (trade : StockTrade) =
    let lines =
        [sprintf "<h3> Report on Covered Call %s for %s</h3>" trade.Cover trade.Symbol] @
        ["<p class=\"covered\">"] @
        (showCoverOptionValue trade.Cover Call trade.Credit) @   // Having Stock is equivalent to having Long Call
        ["</p>"]
    lines |> List.toArray |> appendToReport filename


