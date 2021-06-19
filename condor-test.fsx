#r "nuget:FSharp.Data"

#load "TDAmeritrade.fs"
#load "Trade.fs"
#load "Report.fs"

open System
open Trade
open Report

let filepath = "/Volumes/LaCie/OptionData/"
let filename = sprintf @"%s%i-%i-%i-Condors.html" filepath DateTime.Now.Year DateTime.Now.Month DateTime.Now.Day

Report.startReport filename

let mutable condor = {
    Symbols = [ "FB_071621P295"; "FB_071621P300"; "FB_071621C365"; "FB_071621C370" ]
    Date = new DateTime (2021, 6, 14, 10, 0, 0)
    Price = [| 1.01M; 1.30M; 1.02M; 0.76M |]  
    Contracts = 10
    Delta = [| -0.06M; -0.08M; 0.12M; 0.09M |]  
    IV = [| 0.2973M; 0.2838M; 0.2244M; 0.2244M |]
    Credit = 552.58M
    TargetPL = 418.06M
    StockPrice = 336.77M
}
let mutable quotes4 = TDAmeritrade.getCondorQuotes condor.Symbols
Report.onCondor filename condor quotes4

condor <- {
    Symbols = [ "LOW_071621P165"; "LOW_071621P170"; "LOW_071621C200"; "LOW_071621C210" ]
    Date = new DateTime (2021, 6, 17, 15, 4, 35)
    Price = [| 0.31M; 0.52M; 0.71M; 0.2M |]  
    Contracts = 10
    Delta = [| -0.05M; -0.09M; 0.13M; 0.04M |]  
    IV = [| 0.2743M; 0.2472M; 0.2016M; 0.2289M |]
    Credit = 693.40M
    TargetPL = 418.06M
    StockPrice = 186.84M
}
quotes4 <- TDAmeritrade.getCondorQuotes condor.Symbols
Report.onCondor filename condor quotes4

condor <- {
    Symbols = [ "BA_071621P195"; "BA_071621P200"; "BA_071621C270"; "BA_071621C275" ]
    Date = new DateTime (2021, 6, 18, 15, 31, 57)
    Price = [| 0.71M; 0.87M; 1.11M; 0.87M |]  
    Contracts = 10
    Delta = [| -0.05M; -0.07M; 0.10M; 0.08M |]  
    IV = [| 0.4519M; 0.4245M; 0.3476M; 0.3593M |]
    Credit = 373.39M
    TargetPL = 298.71M
    StockPrice = 237.35M
}
quotes4 <- TDAmeritrade.getCondorQuotes condor.Symbols
Report.onCondor filename condor quotes4

Report.endReport filename
