module TDAmeritrade

open System
open FSharp.Data

//Option:
(*type OptionQuote = JsonProvider<"""{
     "symbol": "string",
     "description": "string",
     "bidPrice": 0.01,
     "bidSize": 0,
     "askPrice": 0.01,
     "askSize": 0,
     "lastPrice": 0.01,
     "lastSize": 0,
     "openPrice": 0.01,
     "highPrice": 0.01,
     "lowPrice": 0.01,
     "closePrice": 0.01,
     "netChange": 0.01,
     "totalVolume": 0,
     "quoteTimeInLong": 0,
     "tradeTimeInLong": 0,
     "mark": 0.01,
     "openInterest": 0.01,
     "volatility": 0.01,
     "moneyIntrinsicValue": 0.01,
     "multiplier": 0.01,
     "strikePrice": 0.01,
     "contractType": "string",
     "underlying": "string",
     "timeValue": 0.01,
     "deliverables": "string",
     "delta": 0.01,
     "gamma": 0.01,
     "theta": 0.01,
     "vega": 0.01,
     "rho": 0.01,
     "securityStatus": "string",
     "theoreticalOptionValue": 0,
     "underlyingPrice": 0.01,
     "uvExpirationType": "string",
     "exchange": "string",
     "exchangeName": "string",
     "settlementType": "string",
     "daysToExpiration": 0
}""">*)

type OptionQuote = JsonProvider<"""{
    "assetType": "OPTION",
    "assetMainType": "OPTION",
    "cusip": "0ENB..AL20032500",
    "symbol": "ENB_012122C32.5",
    "description": "ENB Jan 21 2022 32.5 Call",
    "bidPrice": 8.2,
    "bidSize": 12,
    "askPrice": 8.4,
    "askSize": 12,
    "lastPrice": 8.6,
    "lastSize": 2,
    "openPrice": 8.6,
    "highPrice": 8.6,
    "lowPrice": 8.6,
    "closePrice": 8.4477,
    "netChange": 0.1523,
    "totalVolume": 3,
    "quoteTimeInLong": 1623873582302,
    "tradeTimeInLong": 1623855472545,
    "mark": 8.3,
    "openInterest": 563,
    "volatility": 23.6543,
    "moneyIntrinsicValue": 8.19,
    "multiplier": 100,
    "digits": 2,
    "strikePrice": 32.5,
    "contractType": "C",
    "underlying": "ENB",
    "expirationDay": 21,
    "expirationMonth": 1,
    "expirationYear": 2022,
    "daysToExpiration": 219,
    "timeValue": 0.41,
    "deliverables": "",
    "delta": 0.9606,
    "gamma": 0.0363,
    "theta": -0.0006,
    "vega": 0.0205,
    "rho": 0.0252,
    "securityStatus": "Normal",
    "theoreticalOptionValue": 8.2,
    "underlyingPrice": 40.69,
    "uvExpirationType": "R",
    "exchange": "o",
    "exchangeName": "OPR",
    "lastTradingDay": 1642813200000,
    "settlementType": " ",
    "netPercentChangeInDouble": 1.8029,
    "markChangeInDouble": -0.1477,
    "markPercentChangeInDouble": -1.7484,
    "impliedYield": 0.0384,
    "isPennyPilot": false,
    "delayed": true,
    "realtimeEntitled": false
}""">

let webclient = new Net.WebClient ()
let baseurl = "https://api.tdameritrade.com/v1/marketdata/"
let parameters = sprintf "/quotes?apikey=%s" API.consumerkey

let trimOptionJson (jsonstr : string) =
     let idx = jsonstr.IndexOf (":")
     jsonstr.Substring (idx+1, jsonstr.Length-idx-2)

let getOptionQuote symbol =
     let url = sprintf "%s%s%s" baseurl symbol parameters
     webclient.DownloadString (url)
     |> trimOptionJson
     |> OptionQuote.Parse

let getCondorQuotes symbols =
    List.map (fun symbol ->
        getOptionQuote symbol
    ) symbols

let getMultipleQuotes symbols =
    List.map (fun symbol ->
        getOptionQuote symbol
    ) symbols
