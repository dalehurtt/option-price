module Report

open System
open System.IO
open FSharp.Data
open Trade
open TDAmeritrade

let blue = "<span class=\"blue\">HOLD</span>"
let bluesell = "<span class=\"cyan\">SELL</span>"
let green = "<span class=\"green\">SELL</span>"
let red = "<span class=\"red\">SELL</span>"
let yellow = "<span class=\"yellow\">ASSESS</span>"

let appendToReport filename lines =
    File.AppendAllLines (filename, lines)
    ()


let endReport filename =
    File.AppendAllText (filename, "</body></html>")


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


let getPotentialPrice (json : OptionQuote.Root) =
    (json.BidPrice + json.AskPrice) / 2.0M


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
    if d >= 0.20M
    then true
    else false


let isAnyDeltaTooHigh (quotes : List<OptionQuote.Root>) =
    List.fold (fun found (quote : OptionQuote.Root) ->
        found || (isDeltaTooHigh quote.Delta)
    ) false quotes


let showDeltas (quotes : List<OptionQuote.Root>) =
    let mutable ctr = 0
    List.fold (fun str (quote : OptionQuote.Root) ->
        ctr <- ctr + 1
        if ctr = 4 then sprintf "%s and %.2f." str quote.Delta
        else sprintf "%s %.2f," str quote.Delta
    ) "The deltas are currently " quotes


let showPotentialPrices (quotes : List<OptionQuote.Root>) =
    let mutable ctr = 0
    List.fold (fun  str (quote : OptionQuote.Root)  ->
        ctr <- ctr + 1
        if ctr = 1 then
            sprintf "%s $ %.2f," str (getPotentialPrice quote)
        elif ctr = 4 then
            sprintf "%s and $ %.2f" str (getPotentialPrice quote)
        else
            sprintf "%s $ -%.2f," str (getPotentialPrice quote)
    ) "Current values are " quotes
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


(*let showCallStockPrice curprice tradeprice position =
    match position with
    | Long ->
        if curprice >= tradeprice then
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Higher than when the Call was purchased. %s" curprice tradeprice blue
         else
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Lower than when the Call was purchased. %s" curprice tradeprice yellow

    | Short ->
        if curprice >= tradeprice then
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Higher than when the Call was sold. %s" curprice tradeprice yellow
         else
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Lower than when the Call was purchased. %s" curprice tradeprice blue*)


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


(*let showCallValue curvalue costbasis position =
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
    sprintf "Call Value | Current $ %.2f | Purchased $ %.2f | Change %.2f %%. %s" curvalue costbasis change color*)


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


(*let private onLEAPCall (trade : OptionTrade) (quote : OptionQuote.Root) =
    let lines =
        [(sprintf "<h2>Report on %s LEAP %s %A Call</h2><p>" quote.Underlying trade.Symbol trade.Position)] @
        [(showCallValue (getPotentialPrice quote) trade.CostBasis) trade.Position] @
        ["<br />"] @
        [(showStockPrice  quote.UnderlyingPrice trade.StockPrice trade.OptionType trade.Position)] @
        ["<br />"] @
        [(getDelta trade quote)] @
        ["<br />"] @
        [(showIV trade quote)] @
        ["<br />"] @
        [(getTheta trade quote)] @
        [(sprintf "</p>")]
    lines |> List.toArray*)


// Put anlysis

(*let showPutStockPrice curprice tradeprice position =
    match position with
    | Long ->
        if curprice >= tradeprice then
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Higher than when the Put was purchased. %s" curprice tradeprice blue
        else
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Lower than when the Put was purchased. %s" curprice tradeprice yellow

    | Short ->
        if curprice >= tradeprice then
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Higher than when the Put was sold. %s" curprice tradeprice yellow
        else
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Lower than when the Put was purchased. %s" curprice tradeprice blue*)


(*let showPutValue curvalue costbasis  position =
    let longchange = (curvalue - costbasis) / costbasis * 100.0M
    let shortchange = (costbasis - curvalue) / costbasis * 100.0M
    match position with
    | Long ->
        let color =
            if curvalue >= costbasis
            then
                if curvalue >= (costbasis * 1.8M)
                then green
                else blue
            else yellow
        sprintf "Put Value | Current $ %.2f | Purchased $ %.2f | Change %.2f %%. %s" curvalue costbasis longchange color
    | Short ->
        let color =
            if curvalue <= costbasis
            then
                if (curvalue / costbasis) <= 0.50M
                then green
                else blue
            else yellow
        sprintf "Put Value | Current $ %.2f | Purchased $ %.2f | Change %.2f %%. %s" curvalue costbasis shortchange color*)


(*let private onLEAPPut (trade : OptionTrade) (quote : OptionQuote.Root) =
    let lines =
        [(sprintf "<h2>Report on %s LEAP %s %A Put</h2><p>" quote.Underlying trade.Symbol trade.Position)] @
        [(showOptionValue (getPotentialPrice quote) trade.CostBasis) trade.OptionType trade.Position] @
        ["<br />"] @
        [(showStockPrice  quote.UnderlyingPrice trade.StockPrice trade.OptionType trade.Position)] @
        ["<br />"] @
        [(getDelta trade quote)] @
        ["<br />"] @
        [(showIV trade quote)] @
        ["<br />"] @
        [(getTheta trade quote)] @
        [(sprintf "</p>")]
    lines |> List.toArray*)


let onCondor filename (trade : CondorTrade) (quotes : List<OptionQuote.Root>) =
    let underlying = quotes.Head.Underlying
    let expdays = quotes.Head.DaysToExpiration

    let mutable ctr = 0
    let mutable curPL =
        List.fold (fun price (quote : OptionQuote.Root)  ->
            ctr <- ctr + 1
            if ctr = 1 || ctr = 4 then
                price + (getPotentialPrice quote)
            else
                price - (getPotentialPrice quote)
        ) 0.0M quotes
    curPL <- curPL * (decimal) trade.Contracts * 100.0M

    let lines =
        [(sprintf "<h2>Report on %s Iron Condor</h2><p>" underlying)] @
        [if curPL >= trade.TargetPL then
            sprintf "This trade has reached its target profit of $ %.2f. You are aiming to get $ %.2f when closing this position. %s" trade.TargetPL curPL green
        elif expdays <= 7 then
            sprintf "This position is within %i days of expiration. %s" expdays bluesell
        elif isAnyDeltaTooHigh quotes then
            sprintf "At least one leg in this position is over the target delta of .20/-.20. %s" red
        else
            sprintf "Profit/Loss is currently estimated at $ %.2f. %s" curPL blue] @
        [(showPotentialPrices quotes)] @
        [(showDeltas quotes)] @
        [(sprintf "</p>")]
    lines |> List.toArray |> appendToReport filename


(*  Using the trade data, get the option symbol, fetch the data from TD Ameritrade, and append the data to
    the report.
 *)
let onLEAP filename (trade : OptionTrade) =
    let quote = TDAmeritrade.getOptionQuote trade.Symbol
    (*match trade.OptionType with
    | Call -> onLEAPCall trade quote
    | Put -> onLEAPPut trade quote*)
    let lines =
        [(sprintf "<h3>Report on %s LEAP %s %A %A</h3>" quote.Underlying trade.Symbol trade.Position trade.OptionType)] @
        [(sprintf "<p>")] @
        [(showOptionValue (getPotentialPrice quote) trade.CostBasis) trade.OptionType trade.Position] @
        ["<br />"] @
        [(showStockPrice  quote.UnderlyingPrice trade.StockPrice trade.OptionType trade.Position)] @
        ["<br />"] @
        [(getDelta trade quote)] @
        ["<br />"] @
        [(showIV trade quote)] @
        ["<br />"] @
        [(getTheta trade quote)] @
        [(sprintf "</p>")]
    lines |> List.toArray
    |> appendToReport filename

let startReport filename =
    File.WriteAllText (filename, (sprintf "<html><style>.black {color:black;} .blue {color:blue;} .cyan {color:#00ccff;} .red {color:red;} .green {color:green;} .yellow {color:gold;} body {background-color:#4d4d4d;} h1,h2,h3,p,blockquote {color:white;}</style><body><h1>Options Positions Report %s </h1>" (DateTime.Now.ToShortDateString ())))
    ()

