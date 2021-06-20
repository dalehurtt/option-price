module Trade

open System

type OptionType =
    | Call
    | Put

type TradeType =
    | Long
    | Short

type OptionTrade = {
    Symbol : string
    OptionType : OptionType
    Position : TradeType
    Date : DateTime     // Purchase Date
    Price : decimal     // Purchase Price
    CostBasis : decimal // Potentially lowered by the sale of options against it
    Contracts : int
    Delta : decimal     // At the close of the purchase date
    IV : decimal        // At the close of the purchase date
    StockPrice : decimal    // At the close of the purchase date
    Cover : string      // Option covering this option trade
}

type CondorTrade = {
    Symbols : List<string> // long put, short put, short call, long call
    Date : DateTime     // Purchase Date
    Price : decimal []  // long put, short put, short call, long call
    Contracts : int
    Delta : decimal []  // long put, short put, short call, long call
    IV : decimal []     // long put, short put, short call, long call
    Credit : decimal    // Credit taken in
    TargetPL : decimal  // 80% of credit taken in
    StockPrice : decimal   // At the close of the purchase date
}
