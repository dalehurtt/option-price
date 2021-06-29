open System

[<EntryPoint>]
let main _ =
    // -------------------- HEADER --------------------
    //let reportpath = "/Volumes/LaCie/TradingData/"
    let optionpath = "/Volumes/LaCie/TradingData/OptionData/"
    let stockpath = "/Volumes/LaCie/TradingData/StockData/"
    //let filepath = @"C:\Temp\"
    let reportname = sprintf @"%s%i-%i-%i-positions.html" optionpath DateTime.Now.Year DateTime.Now.Month DateTime.Now.Day

    Report.startReport

    Positions.processTrades stockpath

    Report.endReport reportname

    0 // return an integer exit code
