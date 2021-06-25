open System

[<EntryPoint>]
let main _ =
    // -------------------- HEADER --------------------
    let filepath = "/Volumes/LaCie/OptionData/"
    //let filepath = @"C:\Temp\"
    let filename = sprintf @"%s%i-%i-%i-Report.html" filepath DateTime.Now.Year DateTime.Now.Month DateTime.Now.Day

    Report.startReport filename

    LEAPS.processTrades filename
    Stocks.processTrades filename
    //Condors.processTrades filename
    Spreads.processTrades filename
    Singles.processTrades filename

    Report.endReport filename

    0 // return an integer exit code
