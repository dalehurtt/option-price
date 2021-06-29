module LEAPS

open System
open Trade
open Report

let processTrades filename =
    let processTrade = Report.onLEAP filename       // curry to simplify

    // -------------------- LEAP CALLS --------------------

    let leapIntro =
        ["<h2>LEAPs</h2>"] @
        ["<p>My strategy for LEAPs is simply to grow the value until it reaches 90 days to expiration and "] @
        ["then sell. There are two exceptions to that: selling for strong profit; and selling against strong "] @
        ["loss.</p><blockquote>Strong profit is hit when the option reaches 5 times its initial purchase "] @
        ["price, if Long, or 80% of the original credit, if Short.</blockquote><blockquote>Strong loss is "] @
        ["reached when the option's delta reaches critical points, whether Long or Short. When a Call's delta "] @
        ["is reduced to the midpoint between the initial delta and 0.50, or between the initial delta and "] @
        ["-0.50 of a Put, one-half of the position will be sold. If the delta reaches to 0.50/-0.50 the " ] @
        ["remaining position will be sold.</blockquote>"]
    leapIntro |> List.toArray |> Report.appendToReport filename

    // TD AMERITRADE

    // Check up on our MSFT LEAP Call
    processTrade {
        Symbol = "MSFT_012023C170"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 27)
        Price = 87.50M
        Contracts = 1
        Delta = 0.86M
        Theta = 0.0M
        IV = 30.76M
        StockPrice = 249.31M
        Broker = "TD Ameritrade"
    }
    
    // Check up on our PYPL LEAP Call
    processTrade {
        Symbol = "PYPL_012023C155"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 26)
        Price = 115.00M
        Contracts = 1
        Delta = 0.90M
        Theta = 0.0M
        IV = 40.69M
        StockPrice = 261.37M
        Broker = "TD Ameritrade"
    }

    // FIDELITY
    
    // Check up on our CSCO LEAP Call
    processTrade {
        Symbol = "CSCO_012023C35"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 2)
        Price = 18.15M
        Contracts = 10
        Delta = 0.89M
        Theta = 0.0M
        IV = 32.21M
        StockPrice = 52.98M
        Broker = "Fidelity"
    }

    // Check up on our DFS LEAP Call
    processTrade {
        Symbol = "DFS_012023C72.5"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 1)
        Price = 52.11M
        Contracts = 2
        Delta = 0.85M
        Theta = 0.0M
        IV = 45.01M
        StockPrice = 102.88M
        Broker = "Fidelity"
    }

    // Check up on our ENB LEAP Call
    processTrade {
        Symbol = "ENB_012122C32.5"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 6)
        Price = 7.28M
        Contracts = 10
        Delta = 1.0M
        Theta = 0.0M
        IV = 25.10M
        StockPrice = 39.79M
        Broker = "Fidelity"
    }

    // Check up on our FB LEAP Call
    processTrade {
        Symbol = "FB_012023C220"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 2)
        Price = 124.00M
        Contracts = 1
        Delta = 0.86M
        Theta = 0.0M
        IV = 37.64M
        StockPrice = 329.15M
        Broker = "Fidelity"
    }

    // Check up on our Fidelity PYPL LEAP Call
    processTrade {
        Symbol = "PYPL_012122C220"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 5, 24)
        Price = 49.49M
        Contracts = 2
        Delta = 0.75M
        Theta = 0.0M
        IV = 36.05M
        StockPrice = 257.17M
        Broker = "Fidelity"
    }
    