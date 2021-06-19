open System
open Trade
open Report
open LEAPS

[<EntryPoint>]
let main _ =
    // -------------------- HEADER --------------------
    let filepath = "/Volumes/LaCie/OptionData/"
    let filename = sprintf @"%s%i-%i-%i-Report.html" filepath DateTime.Now.Year DateTime.Now.Month DateTime.Now.Day

    Report.startReport filename

    LEAPS.processTrades filename

    Report.endReport filename

    0 // return an integer exit code
