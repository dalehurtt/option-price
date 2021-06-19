module Report

open System
open System.IO
open FSharp.Data
open Trade
open TDAmeritrade

let blue = "<span class=\"blue\">HOLD</span>"
let bluesell = "<span class=\"blue\">SELL</span>"
let green = "<span class=\"green\">SELL</span>"
let red = "<span class=\"red\">SELL</span>"
let yellow = "<span class=\"yellow\">ASSESS</span>"

let appendToReport filename lines =
    File.AppendAllLines (filename, lines)
    ()


let endReport filename =
    File.AppendAllText (filename, "</body></html>")


let getDelta (trade : OptionTrade) (json : OptionQuote.Root) =
    let tradeDelta = trade.Delta
    //let curDelta = Math.Abs (json.Delta)
    let curDelta = json.Delta
    match trade.OptionType, trade.Position with
    | Call, Long ->
        if curDelta >= tradeDelta then
            //sprintf "Current delta %.2f is higher than when I purchased (%.2f). <span class=\"blue\">HOLD</span> " curDelta tradeDelta
            sprintf "Delta | Current %.2f | Initial %.2f | Now higher. %s" curDelta tradeDelta blue
        elif tradeDelta <= 0.50M then
            //sprintf "This option is now no longer in the money. The delta has moved from %.2f to %.2f. <span class=\"red\">SELL</span> " tradeDelta curDelta
            sprintf "Delta | Current %.2f | Initial %.2f | No longer in the money. %s" curDelta tradeDelta red
        else
            //sprintf "Current delta is down to %.2f from %.2f, %.2f below when I purchased the LEAP. <span class=\"yellow\">ASSESS</span> " curDelta tradeDelta (tradeDelta - curDelta)
            sprintf "Delta | Current %.2f | Initial %.2f | %.2f below when purchased. %s" curDelta tradeDelta (tradeDelta - curDelta) yellow

    | Call, Short -> ""

    | Put, Long -> ""

    | Put, Short ->
        if curDelta >= tradeDelta then
            //sprintf "Current delta %.2f is lower than when I purchased (%.2f). The Put is farther out of the money. <span class=\"blue\">HOLD</span> " curDelta tradeDelta
            sprintf "Delta | Current %.2f | Initial %.2f | Now lower and farther out of the money. %s" curDelta tradeDelta blue
        elif curDelta <= -0.50M then
            //sprintf "This option is now no longer out of the money. The delta has moved from %.2f to %.2f. <span class=\"red\">SELL</span> " tradeDelta curDelta
            sprintf "Delta | Current %.2f | Initial %.2f | No longer out of the money. %s" curDelta tradeDelta red
        else
            //sprintf "Current delta is down to %.2f from %.2f, %.2f below when I purchased the LEAP. <span class=\"yellow\">ASSESS</span> " curDelta tradeDelta (curDelta - tradeDelta)
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
            sprintf "Profit/Loss is currently estimated at $ %.2f. %s" curPL red] @
        [(showPotentialPrices quotes)] @
        [(showDeltas quotes)] @
        [(sprintf "</p>")]
    lines |> List.toArray |> appendToReport filename


let showCallStockPrice curprice tradeprice position =
    match position with
    | Long ->
        if curprice >= tradeprice then
            //sprintf "The current stock price ($ %.2f) is more than when the Call was purchased ($ %.2f). <span class=\"blue\">HOLD</span> " curprice tradeprice
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Higher than when the Call was purchased. %s" curprice tradeprice blue
         else
            //sprintf "The current stock price ($ %.2f) is less than when the Call was purchased ($ %.2f). <span class=\"yellow\">ASSESS</span> " curprice tradeprice
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Lower than when the Call was purchased. %s" curprice tradeprice yellow

    | Short ->
        if curprice >= tradeprice then
            //sprintf "The current stock price ($ %.2f) is more than when the Call was sold ($ %.2f). <span class=\"yellow\">ASSESS</span> " curprice tradeprice
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Higher than when the Call was sold. %s" curprice tradeprice yellow
         else
            //sprintf "The current stock price ($ %.2f) is less than when the Call was sold ($ %.2f). <span class=\"blue\">HOLD</span> " curprice tradeprice
            sprintf "Stock Price | Current $ %.2f | Purchased $ %.2f | Lower than when the Call was purchased. %s" curprice tradeprice blue


let showCallValue curvalue costbasis  position =
    let longchange = (curvalue - costbasis) / costbasis * 100.0M
    let shortchange = (costbasis - curvalue) / costbasis * 100.0M
    match position with
    | Long ->
        (*if curvalue >= costbasis then
            if curvalue >= (costbasis * 1.8M) then
                //sprintf "The current value of the Call ($ %.2f) has more than 80%% profit ove what it was purchased for ($ %.2f). <span class=\"green\">SELL TO CLOSE</span> " curvalue costbasis
                sprintf "Call Value | Current $ %.2f | Purchased $ %.2f | Change %%age %.2f %% %s" curvalue costbasis longchange green
            else
                //sprintf "The current value of the Call ($ %.2f) is more than what it was purchased for ($ %.2f). <span class=\"blue\">HOLD</span> " curvalue costbasis
                sprintf "Call Value | Current $ %.2f | Purchased $ %.2f | Change %%age %.2f %% %s" curvalue costbasis longchange blue
        else
            //sprintf "The current value of the Call ($ %.2f) is less than what it was purchased for ($ %.2f). <span class=\"yellow\">ASSESS DELTA AND THETA TO SELL</span> " curvalue costbasis
            sprintf "Call Value | Current $ %.2f | Purchased $ %.2f | Change %%age %.2f %% %s" curvalue costbasis longchange yellow*)
        let color =
            if curvalue >= costbasis
            then
                if curvalue >= (costbasis * 1.8M)
                then green
                else blue
            else yellow
        sprintf "Call Value | Current $ %.2f | Purchased $ %.2f | Change %.2f %%. %s" curvalue costbasis longchange color
    | Short ->
        (*if curvalue <= costbasis then
            if (curvalue / costbasis) <= 0.50M then
                sprintf "The current value of the Call ($ %.2f) is less than half it was sold for ($ %.2f). <span class=\"green\">SELL TO CLOSE</span> " curvalue costbasis
            else
                sprintf "The current value of the Call ($ %.2f) is less than what it was sold for ($ %.2f). <span class=\"blue\">HOLD</span> " curvalue costbasis
        else
            sprintf "The current value of the Call ($ %.2f) is more than what it was sold for ($ %.2f). <span class=\"yellow\">ASSESS DELTA AND THETA TO SELL</span> " curvalue costbasis*)
        let color =
            if curvalue <= costbasis
            then
                if (curvalue / costbasis) <= 0.50M
                then green
                else blue
            else yellow
        sprintf "Call Value | Current $ %.2f | Purchased $ %.2f | Change %%age %.2f %%. %s" curvalue costbasis shortchange color


let onLEAPCall (trade : OptionTrade) (json : OptionQuote.Root) =
    let lines =
        [(sprintf "<h2>Report on %s LEAP %s %A Call</h2><p>" json.Underlying trade.Symbol trade.Position)] @
        [(showCallValue (getPotentialPrice json) trade.CostBasis) trade.Position] @
        ["<br />"] @
        [(showCallStockPrice  json.UnderlyingPrice trade.StockPrice trade.Position)] @
        ["<br />"] @
        [(getDelta trade json)] @
        ["<br />"] @
        [(getTheta trade json)] @
        [(sprintf "</p>")]
    lines |> List.toArray


// Put anlysis

let showPutStockPrice curprice tradeprice position =
    match position with
    | Long ->
        if curprice >= tradeprice then
            sprintf "The current stock price ($ %.2f) is more than when the Put was purchased ($ %.2f). %s" curprice tradeprice yellow
         else
            sprintf "The current stock price ($ %.2f) is less than when the Put was purchased ($ %.2f). %s" curprice tradeprice blue

    | Short ->
        if curprice >= tradeprice then
            sprintf "The current stock price ($ %.2f) is more than when the Put was sold ($ %.2f). %s" curprice tradeprice blue
         else
            sprintf "The current stock price ($ %.2f) is less than when the Put was sold ($ %.2f). %s" curprice tradeprice yellow


let showPutValue curvalue costbasis  position =
    match position with
    | Long -> ""

    | Short ->
        if curvalue <= costbasis then
            if (curvalue / costbasis) <= 0.50M then
                sprintf "The current value of the Put ($ %.2f) is less than half it was sold for ($ %.2f). %s" curvalue costbasis green
            else
                sprintf "The current value of the Put ($ %.2f) is less than what it was sold for ($ %.2f). %s" curvalue costbasis blue
        else
            sprintf "The current value of the Put ($ %.2f) is more than what it was sold for ($ %.2f). %s" curvalue costbasis yellow


let onLEAPPut (trade : OptionTrade) (json : OptionQuote.Root) =
    let lines =
        [(sprintf "<h2>Report on %s LEAP %s %A Put</h2><p>" json.Underlying trade.Symbol trade.Position)] @
        [(showPutValue (getPotentialPrice json) trade.CostBasis) trade.Position] @
        ["<br />"] @
        [(showPutStockPrice  json.UnderlyingPrice trade.StockPrice trade.Position)] @
        ["<br />"] @
        [(getDelta trade json)] @
        ["<br />"] @
        [(getTheta trade json)] @
        [(sprintf "</p>")]
    lines |> List.toArray


(*  Using the trade data, get the option symbol, fetch the data from TD Ameritrade, and append the data to
    the report.
 *)
let onLEAP filename (trade : OptionTrade) =
    let quote = TDAmeritrade.getOptionQuote trade.Symbol
    match trade.OptionType with
    | Call -> onLEAPCall trade quote
    | Put -> onLEAPPut trade quote
    |> appendToReport filename

let startReport filename =
    File.WriteAllText (filename, (sprintf "<html><style>.black {color:black;} .blue {color:blue;} .red {color:red;} .green {color:green;} .yellow {color:gold;} body {background-color:#4d4d4d;} h1,h2,p {color:white;}</style><body><h1>Options Positions Report %s </h1>" (DateTime.Now.ToShortDateString ())))
    ()
