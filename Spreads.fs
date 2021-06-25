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

    processTrade {
        Options = [
            {
                Symbol = "BA_071621P195"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 18)
                Price = 0.71M
                Contracts = 10
                Delta = -0.05M
                Theta = -0.00M
                IV = 45.19M
                StockPrice = 237.35M
            }
            {
                Symbol = "BA_071621P200"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 18)
                Price = 0.87M
                Contracts = -10
                Delta = -0.07M
                Theta = -0.00M
                IV = 42.45M
                StockPrice = 237.35M
            }
            {
                Symbol = "BA_071621C270"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 18)
                Price = 1.11M
                Contracts = -10
                Delta = 0.10M
                Theta = -0.00M
                IV = 34.76M
                StockPrice = 237.35M
            }
            {
                Symbol = "BA_071621C275"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 18)
                Price = 0.87M
                Contracts = 10
                Delta = 0.08M
                Theta = -0.00M
                IV = 35.93M
                StockPrice = 237.35M
            }
        ]
        SpreadType = IronCondor
        Net = 373.39M
        TargetPL = 298.71M
    }

    processTrade {
        Options = [
            {
                Symbol = "V_071621P215"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 21)
                Price = 0.67M
                Contracts = 10
                Delta = -0.09M
                Theta = -0.00M
                IV = 24.33M
                StockPrice = 234.32M
            }
            {
                Symbol = "V_071621P217.5"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 21)
                Price = 0.83M
                Contracts = -10
                Delta = -0.11M
                Theta = -0.00M
                IV = 23.48M
                StockPrice = 234.32M
            }
            {
                Symbol = "V_071621C247.5"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 21)
                Price = 0.63M
                Contracts = -10
                Delta = 0.13M
                Theta = -0.00M
                IV = 17.60M
                StockPrice = 234.32M
            }
            {
                Symbol = "V_071621C250"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 21)
                Price = 0.44M
                Contracts = 10
                Delta = 0.09M
                Theta = -0.00M
                IV = 17.96M
                StockPrice = 234.32M
            }
        ]
        SpreadType = IronCondor
        Net = 322.58M
        TargetPL = 258.06M
    }