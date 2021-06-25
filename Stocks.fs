module Stocks

open System
open Trade

let processTrades filename =
    let processTrade = Report.onStock filename       // curry to simplify

    let intro =
        ["<h2>Stocks</h2>"] @
        ["<p>My strategy for stocks is simply to grow the value while collecting dividends and selling calls "] @
        ["to lower the cost basis. As the emphasis on this application is not on stocks, but on options, "] @
        ["this application only assesses the option in the covered call. (Also, because I have not shorted "] @
        ["stock in the past, I do not current assess covered puts on shorted stock. Maybe in the future.) "] @
        ["</p><p>When a stock's price is within 10-20% of covered option's strike price, this application "] @
        ["shows a warning. If the stock price is within 10% of the option's strike price, or higher, it "] @
        ["indicates that the option should be sold (or rolled out, up, or both). Note that this is the same " ] @
        ["strategy for covered options for LEAP Calls.</p>"]
    intro |> List.toArray |> Report.appendToReport filename

    // FIDELITY
    
    processTrade {
        Symbol = "ABBV"
        Position = Long
        Date = new DateTime (2021, 4, 13)
        Price = 108.779M
        Shares = 500.0M
        Cover =
            {
                Symbol = "ABBV_070921C121"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 16)
                Price = 0.65M
                Contracts = -4
                Delta = 0.17M
                Theta = -0.03M
                IV = 19.03M
                StockPrice = 115.53M
            }
    }

    processTrade {
        Symbol = "PFE"
        Position = Long
        Date = new DateTime (2021, 4, 13)
        Price = 37.04569M
        Shares = 1300.0M
        Cover =
            {
                Symbol = "PFE_071621C41"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 4)
                Price = 0.37M
                Contracts = -12
                Delta = 0.23M
                Theta = -0.01M
                IV = 17.63M
                StockPrice = 39.15M
            }
    }

    processTrade {
        Symbol = "RIO"
        Position = Long
        Date = new DateTime (2021, 4, 27)
        Price = 86.45893M
        Shares = 800.0M
        Cover =
            {
                Symbol = "RIO_071621C89.07"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 17)
                Price = 0.83M
                Contracts = -8
                Delta = 0.22M
                Theta = -0.04M
                IV = 30.56M
                StockPrice = 83.02M
            }
    }

    processTrade {
        Symbol = "T"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 29.215M
        Shares = 1600.0M
        Cover =
            {
                Symbol = "T_072321C29.5"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 18)
                Price = 0.27M
                Contracts = -16
                Delta = 0.27M
                Theta = -0.01M
                IV = 20.87M
                StockPrice = 28.65M
            }
    }

    processTrade {
        Symbol = "VZ"
        Position = Long
        Date = new DateTime (2021, 6, 2)
        Price = 56.60M
        Shares = 800.0M
        Cover =
            {
                Symbol = "VZ_071621C57.5"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 18)
                Price = 0.28M
                Contracts = -8
                Delta = 0.20M
                Theta = -0.01M
                IV = 14.86M
                StockPrice = 55.82M
            }
    }

    processTrade {
        Symbol = "CVX"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 107.78M
        Shares = 400.0M
        Cover =
            {
                Symbol = "CVX_072321C111"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 18)
                Price = 1.12M
                Contracts = -4
                Delta = 0.20M
                Theta = -0.03M
                IV = 27.22M
                StockPrice = 103.03M
            }
    }

    processTrade {
        Symbol = "XOM"
        Position = Long
        Date = new DateTime (2021, 3, 24)
        Price = 57.23644M
        Shares = 800.0M
        Cover =
            {
                Symbol = "XOM_082021C72.5"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 21)
                Price = 0.395M
                Contracts = -8
                Delta = 0.11M
                Theta = -0.01M
                IV = 30.02M
                StockPrice = 62.59M
            }
    }
