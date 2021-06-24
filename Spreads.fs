module Spreads

open System
open Trade
open Report

let processTrades filename =
    let processTrade = Report.onSpread filename       // curry to simplify

    // -------------------- SPREADS --------------------

    let intro =
        ["<h2>Spreads</h2>"]
    intro |> List.toArray |> Report.appendToReport filename

    processTrade {
        Options = [
            {
                Symbol = "LOW_071621P165"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 17)
                Price = 0.31M
                Contracts = 10
                Delta = -0.05M
                Theta = -0.03M
                IV = 27.43M
                StockPrice = 186.84M
            }
            {
                Symbol = "LOW_071621P170"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 17)
                Price = 0.52M
                Contracts = -10
                Delta = -0.09M
                Theta = -0.03M
                IV = 24.72M
                StockPrice = 186.84M
            }
        ]
        SpreadType = VerticalCredit
        Net = 196.70M
        TargetPL = 157.36M
    }
    
    processTrade {
        Options = [
            {
                Symbol = "FB_071621P295"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 14)
                Price = 1.01M
                Contracts = 10
                Delta = -0.06M
                Theta = -0.06M
                IV = 29.73M
                StockPrice = 336.77M
            }
            {
                Symbol = "FB_071621P300"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 14)
                Price = 1.30M
                Contracts = -10
                Delta = -0.08M
                Theta = -0.06M
                IV = 28.38M
                StockPrice = 336.77M
            }
            {
                Symbol = "FB_071621C365"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 14)
                Price = 1.02M
                Contracts = -10
                Delta = 0.12M
                Theta = -0.07M
                IV = 22.44M
                StockPrice = 336.77M
            }
            {
                Symbol = "FB_071621C370"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 14)
                Price = 0.76M
                Contracts = 10
                Delta = 0.09M
                Theta = -0.06M
                IV = 22.44M
                StockPrice = 336.77M
            }
        ]
        SpreadType = IronCondor
        Net = 552.58M
        TargetPL = 418.06M
    }

    processTrade {
        Options = [
            {
                Symbol = "ROKU_070921C465"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 24)
                Price = 5.72M
                Contracts = -5
                Delta = -0.00M
                Theta = -0.00M
                IV = 00.00M
                StockPrice = 0.00M
            }
            {
                Symbol = "ROKU_070921C485"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 24)
                Price = 5.32M
                Contracts = 5
                Delta = -0.00M
                Theta = -0.00M
                IV = 0.00M
                StockPrice = 0.00M
            }
        ]
        SpreadType = VerticalCredit
        Net = 200.00M
        TargetPL = 160.00M
    }