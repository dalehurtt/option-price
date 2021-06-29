(*
    This module models Yahoo Finance historical stock data as Skender.Stock.Indicators Quote data. It
    handles fetching, loading, saving, and updating data from Yahoo Finance to the disk and into memory.
*)
module StockQuoteData

open System
open System.IO
open System.Linq

open Skender.Stock.Indicators

open Common

type QuoteList = {
    Ticker : string
    StartDate : DateTime
    EndDate : DateTime
    High : decimal
    Low : decimal
    List : List<Quote>
}

let webclient = new Net.WebClient ()


(* Return the most recent weekday, given a date. *)
let lastweekday (date : DateTime) : DateTime =
    if date.DayOfWeek = DayOfWeek.Saturday then
        date.AddDays (-1.0)
    elif date.DayOfWeek = DayOfWeek.Sunday then
        date.AddDays (-2.0)
    else date


let ConvertToQuote datev openv highv lowv closev volumev : Quote =
    let mutable volNum = 0M
    try
        volNum <- Convert.ToDecimal (volumev : string)
    with
        | _ as ex -> printfn "%s on %s" ex.Message volumev
    new Quote (
        Date = Convert.ToDateTime (datev : string),
        Open = Convert.ToDecimal (openv : string),
        High = Convert.ToDecimal (highv : string),
        Low = Convert.ToDecimal (lowv : string),
        Close = Convert.ToDecimal (closev : string),
        Volume = volNum
    )


let CsvToQuote (csvLine : string) =
    let parsed = csvLine.Split [|','|]
    ConvertToQuote parsed.[0] parsed.[1] parsed.[2] parsed.[3] parsed.[4] parsed.[6]


let DownloadRangeCsvStockData ticker filename unixstart unixend =
    try
        let url = sprintf "https://query1.finance.yahoo.com/v7/finance/download/%s?period1=%i&period2=%i&interval=1d&events=history&includeAdjustedClose=true" ticker unixstart unixend
        webclient.DownloadFile (url, filename)
    with
    | ex -> printfn "%s %s" ticker ex.Message


let DownloadNewCsvStockData ticker filename =
    let unixstart = (DateTimeOffset.Parse ("1/1/2020")).ToUnixTimeSeconds ()
    let unixend = (DateTimeOffset (DateTime.Now)).ToUnixTimeSeconds ()
    DownloadRangeCsvStockData ticker filename unixstart unixend


let LinesToQuoteList ticker lines =
    let mutable list = List<Quote>.Empty
    Seq.iter (fun elem ->
        list <- List.append list [(CsvToQuote elem)]
    ) lines
    let startdate = (list.Head).Date
    let enddate = (list.Item (list.Length - 1)).Date
    let high = (List.fold (fun curhigh (elem : Quote) ->
        if elem.High > curhigh then elem.High else curhigh
    ) 0.0M list)
    let low = (List.fold (fun curlow (elem : Quote) ->
        if curlow = 0.0M || elem.Low < curlow then elem.Low else curlow
    ) 0.0M list)
    {
        Ticker = ticker
        StartDate = startdate
        EndDate = enddate
        High = high
        Low = low
        List = list
    }


let FileToLines (fileName : string) =
    File.ReadLines fileName
        

let FileToQuotes filepath ticker =
    let filename = sprintf "%s%s.csv" filepath ticker
    if not (File.Exists (filename)) then
        DownloadNewCsvStockData ticker filename

    try
        FileToLines filename
        |> Seq.skip 1
        |> LinesToQuoteList ticker
    with
    | ex ->
        printfn "Could not download %s %s" ticker ex.Message
        { Ticker = ticker; StartDate = DateTime.Now; EndDate = DateTime.Now; High = 0M; Low = 0M; List = List<Quote>.Empty }



let UpdateStockDataFile filepath ticker =
    let filename = sprintf "%s%s.csv" filepath ticker
    if (File.Exists (filename)) then
        let stocklist = (FileToLines filename
        |> Seq.skip 1
        |> LinesToQuoteList ticker)

        let lastdate = (List.last stocklist.List).Date
        let enddate = (lastweekday DateTime.Now)
        if not (lastdate.Month = enddate.Month && lastdate.Day = enddate.Day && lastdate.Year = enddate.Year) then
            let unixstart = (DateTimeOffset (lastdate.AddDays (1.0))).ToUnixTimeSeconds ()
            let unixend = (DateTimeOffset (enddate)).ToUnixTimeSeconds ()

            let newfile = sprintf "%s%s-addtl.csv" filepath ticker
            DownloadRangeCsvStockData ticker newfile unixstart unixend
            let lines = (FileToLines newfile
            |> Seq.skip 1)
            let sw = File.AppendText (filename)
            Seq.iter (fun (line : string) ->
                sw.Write ("\n")
                sw.Write (line)
            ) lines
            sw.Close ()

            File.Delete (newfile)


let GetStockMAAnalysis shortma longma stockpath ticker =
    let stockfile = sprintf "%s%s.csv" stockpath ticker
    UpdateStockDataFile stockpath ticker
    let quotes = FileToQuotes stockpath ticker
    if quotes.List.Length > 0 then
        let lastprice = ((quotes.List).Last ()).Close
        let longsma = ((quotes.List.GetSma (longma)).Last ()).Sma
        let shortsma = ((quotes.List.GetSma (shortma)).Last ()).Sma
        if longsma.HasValue && shortsma.HasValue then
            if lastprice >= longsma.Value then
                if lastprice >= shortsma.Value
                then sprintf "Price %.2f above %i and %i MA. %s" lastprice shortma longma blue
                else sprintf "Price %.2f below %i and above %i MA %s" lastprice shortma longma blue
            else
                if lastprice >= shortsma.Value
                then sprintf "Price %.2f above %i and below %i MA. %s" lastprice shortma longma yellow
                else sprintf "Price %.2f below %i and %i MA %s" lastprice shortma longma red
        else sprintf "Could not get MA. %s" yellow
    else sprintf "Could not get quotes for %s. %s" ticker yellow

