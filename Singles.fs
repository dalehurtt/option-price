module Singles

open System
open Trade

let processTrades filename =
    let processTrade = Report.onLEAP filename       // curry to simplify

    (*processTrade {
        Symbol = "xxx"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 0)
        Price = 0M
        Contracts = -0
        Delta = 0M
        Theta = -0M
        IV = 0M
        StockPrice = 0M
    }*)

    processTrade {
        Symbol = "CSCO_071621C55"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 17)
        Price = 0.31M
        Contracts = -10
        Delta = 0.20M
        Theta = -0.01M
        IV = 16.20M
        StockPrice = 53.13M
    }

    processTrade {
        Symbol = "DFS_071621C130"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 16)
        Price = 1.09M
        Contracts = -2
        Delta = 0.24M
        Theta = -0.05M
        IV = 28.48M
        StockPrice = 122.07M
    }

    processTrade {
        Symbol = "FB_071621C360"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 21)
        Price = 0.95M
        Contracts = -1
        Delta = 0.10M
        Theta = -0.07M
        IV = 23.00M
        StockPrice = 331.08M
    }

    processTrade {
        Symbol = "PYPL_072321C300"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 18)
        Price = 4.39M
        Contracts = -2
        Delta = 0.28M
        Theta = -0.12M
        IV = 29.53M
        StockPrice = 283.38M
    }

    processTrade {
        Symbol = "T_072321C27.5"
        OptionType = Put
        Position = Short
        Date = new DateTime (2021, 6, 24)
        Price = 0.35M
        Contracts = -4
        Delta = -0.27M
        Theta = -0.01M
        IV = 24.62M
        StockPrice = 28.79M
    }

    // TD AMERITRADE

    processTrade {
        Symbol = "PYPL_071621C300"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 17)
        Price = 1.79M
        Contracts = -1
        Delta = 0.17M
        Theta = -0.09M
        IV = 26.57M
        StockPrice = 278.11M
    }

    processTrade {
        Symbol = "MSFT_071621C275"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 17)
        Price = 0.95M
        Contracts = -1
        Delta = 0.14M
        Theta = -0.05M
        IV = 16.73M
        StockPrice = 260.90M
    }