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
    
    (*processTrade {
        Symbol = "XX"
        Position = Long
        Date = new DateTime (2021, 1, 1)
        Price = 0M
        CostBasis = 0M
        Shares = 0M
        Cover = ""
        Credit = 0M
    }*)

    processTrade {
        Symbol = "ABBV"
        Position = Long
        Date = new DateTime (2021, 4, 13)
        Price = 108.779M
        CostBasis = 108.66726M
        Shares = 500.0M
        Cover = "ABBV_070921C121"
        Credit = 0.65M
    }

    processTrade {
        Symbol = "ENB"
        Position = Long
        Date = new DateTime (2021, 4, 13)
        Price = 37.55775M
        CostBasis = 37.64129M
        Shares = 2200.0M
        Cover = "ENB_071621C42.5"
        Credit = 0.22M
    }

    processTrade {
        Symbol = "PFE"
        Position = Long
        Date = new DateTime (2021, 4, 13)
        Price = 37.04569M
        CostBasis = 36.00505M
        Shares = 1300.0M
        Cover = "PFE_071621C41"
        Credit = 0.36M
    }

    processTrade {
        Symbol = "RIO"
        Position = Long
        Date = new DateTime (2021, 4, 27)
        Price = 86.45893M
        CostBasis = 84.30184M
        Shares = 800.0M
        Cover = "RIO_071621C89.07"
        Credit = 0.82M
    }

    processTrade {
        Symbol = "T"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 29.215M
        CostBasis = 29.03221M
        Shares = 1600.0M
        Cover = "T_072321C29.5"
        Credit = 0.26M
    }

    processTrade {
        Symbol = "VZ"
        Position = Long
        Date = new DateTime (2021, 6, 2)
        Price = 56.60M
        CostBasis = 56.40721M
        Shares = 800.0M
        Cover = "VZ_071621C57.5"
        Credit = 0.27M
    }

    processTrade {
        Symbol = "CVX"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 107.78M
        CostBasis = 107.00475M
        Shares = 400.0M
        Cover = "CVX_072321C111"
        Credit = 1.11M
    }

    processTrade {
        Symbol = "ED"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 77.34570M
        CostBasis = 77.34570M
        Shares = 600.0M
        Cover = "ED_072321C80"
        Credit = 0.44M
    }

    processTrade {
        Symbol = "XOM"
        Position = Long
        Date = new DateTime (2021, 3, 24)
        Price = 57.23644M
        CostBasis = 54.78797M
        Shares = 800.0M
        Cover = "XOM_082021C72.5"
        Credit = 0.39M
    }
