#r "nuget:FSharp.Data"

#load "TDAmeritrade.fs"
#load "Trade.fs"
#load "Report.fs"

open System
open Trade
open Report

let filepath = "/Volumes/LaCie/OptionData/"
let filename = sprintf @"%s%i-%i-%i-Report.html" filepath DateTime.Now.Year DateTime.Now.Month DateTime.Now.Day

Report.startReport filename

// Check up on our MSFT LEAP Call
let msftTrade = {
    Symbol = "MSFT_012023C170"
    OptionType = Call
    Position = Long
    Date = new DateTime (2021, 5, 27, 10, 46, 21)
    Price = 87.50M
    CostBasis = 87.50M
    Contracts = 1
    Delta = 0.86M
    IV = 0.3076M
    StockPrice = 249.31M
}
let msft = TDAmeritrade.getOptionQuote msftTrade.Symbol
Report.onLEAPCall filename msftTrade msft

// Check up on our PYPL LEAP Call
let pyplTrade = {
    Symbol = "PYPL_012023C155"
    OptionType = Call
    Position = Long
    Date = new DateTime (2021, 5, 26, 10, 49, 50)
    Price = 115.00M
    CostBasis = 115.00M
    Contracts = 1
    Delta = 0.90M
    IV = 0.4069M
    StockPrice = 261.37M
}
let pypl = TDAmeritrade.getOptionQuote pyplTrade.Symbol
Report.onLEAPCall filename pyplTrade pypl

let zIcTrade = {
    Symbols = [| "Z_070921P90"; "Z_070921P95"; "Z_070921C124"; "Z_070921C129" |]
    Date = new DateTime (2021, 6, 15, 15, 35, 15)
    Price = [| 0.39M; 0.8M; 0.96M; 0.6M |]  
    Contracts = 10
    Delta = [| -0.07M; -0.14M; 0.16M; 0.09M |]  
    IV = [| 0.5252M; 0.5271M; 0.4515M; 0.4582M|]
    StockPrice = 109.28M
}

Report.endReport filename
