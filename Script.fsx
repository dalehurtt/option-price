#r "nuget:FSharp.Data"

open System
open FSharp.Data

type OptionQuote = JsonProvider<"""{
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
     "settlementType": "string"
}""">

let webclient = new Net.WebClient ()
let url = "https://api.tdameritrade.com/v1/marketdata/MSFT_012023C170/quotes?apikey=SHIGOV3DSGWWNZ4NFGKYP9CXRGJJYOKM"
let response = webclient.DownloadString (url)

let modJson (jsonstr : string) =
     let idx = jsonstr.IndexOf (":")
     jsonstr.Substring (idx+1, jsonstr.Length-idx-2)

let moddedResponse = modJson response
let msft = OptionQuote.Parse (moddedResponse)
