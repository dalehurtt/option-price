module LEAPS

open System
open Trade
open Report

let processTrades filename =
    let processTrade = Report.onLEAP filename       // curry to simplify

    // -------------------- LEAP CALLS --------------------
    
    // Check up on our MSFT LEAP Call
    processTrade {
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
    
    // Check up on our PYPL LEAP Call
    processTrade {
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
    
    // Check up on our CSCO LEAP Call
    processTrade {
        Symbol = "CSCO_012023C35"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 2, 10, 0, 0)
        Price = 18.15M
        CostBasis = 17.99405M  // Commissions added, sale subtracted
        Contracts = 10
        Delta = 0.89M
        IV = 0.3221M
        StockPrice = 52.98M
    }

    // Check up on our DFS LEAP Call
    processTrade {
        Symbol = "DFS_012023C72.5"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 1, 10, 0, 0)
        Price = 52.11M
        CostBasis = 52.11M
        Contracts = 2
        Delta = 0.85M
        IV = 0.4501M
        StockPrice = 102.88M
    }

    // Check up on our ENB LEAP Call
    processTrade {
        Symbol = "ENB_012122C32.5"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 6, 10, 0, 0)
        Price = 7.28M
        CostBasis = 7.28M
        Contracts = 10
        Delta = 1.0M
        IV = 0.2510M
        StockPrice = 39.79M
    }

    // Check up on our FB LEAP Call
    processTrade {
        Symbol = "FB_012023C220"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 2, 10, 0, 0)
        Price = 124.00M
        CostBasis = 124.00M
        Contracts = 1
        Delta = 0.86M
        IV = 0.3764M
        StockPrice = 329.15M
    }

    // Check up on our Fidelity PYPL LEAP Call
    processTrade {
        Symbol = "PYPL_012122C220"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 24, 10, 0, 0)
        Price = 49.49M
        CostBasis = 49.49M
        Contracts = 2
        Delta = 0.75M
        IV = 0.3605M
        StockPrice = 257.17M
    }
    
    // Check up on our MMM LEAP Call
    processTrade {
        Symbol = "MMM_012122C175"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 10, 10, 0, 0)
        Price = 34.50M
        CostBasis = 34.50M
        Contracts = 10
        Delta = 0.83M
        IV = 0.2350M
        StockPrice = 261.37M
    }

    // -------------------- LEAP PUTS --------------------

    // Check up on our COIN LEAP Put
    processTrade {
        Symbol = "COIN_012023P160"
        OptionType = Put
        Position = Short
        Date = new DateTime (2021, 6, 4, 10, 0, 0)
        Price = 34.50M
        CostBasis = 32.30M
        Contracts = 3
        Delta = -0.20M
        IV = 0.6491M
        StockPrice = 228.79M
    }
