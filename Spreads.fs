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
                IV = 22.98M
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
                Delta = 0.22M
                Theta = -0.43M
                IV = 53.11M
                StockPrice = 423.58M
            }
            {
                Symbol = "ROKU_071621C485"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 24)
                Price = 5.32M
                Contracts = 5
                Delta = 0.18M
                Theta = -0.32M
                IV = 53.94M
                StockPrice = 423.58M
            }
        ]
        SpreadType = VerticalCredit
        Net = 193.14M
        TargetPL = 154.51M
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
                Theta = -0.06M
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
                Theta = -0.06M
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
                Theta = -0.07M
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
                Theta = -0.06M
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
                Theta = -0.05M
                IV = 24.40M
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
                Theta = -0.05M
                IV = 23.55M
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
                Theta = -0.04M
                IV = 17.52M
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
                Theta = -0.03M
                IV = 17.96M
                StockPrice = 234.32M
            }
        ]
        SpreadType = IronCondor
        Net = 322.58M
        TargetPL = 258.06M
    }

    processTrade {
        Options = [
            {
                Symbol = "JNJ_082021P145"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 21)
                Price = 0.54M
                Contracts = 10
                Delta = -0.08M
                Theta = -0.02M
                IV = 21.99M
                StockPrice = 163.90M
            }
            {
                Symbol = "JNJ_082021P150"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 21)
                Price = 0.88M
                Contracts = -10
                Delta = -0.13M
                Theta = -0.02M
                IV = 19.72M
                StockPrice = 163.90M
            }
            {
                Symbol = "JNJ_082021C180"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 21)
                Price = 0.32M
                Contracts = -10
                Delta = 0.07M
                Theta = -0.01M
                IV = 15.45M
                StockPrice = 163.90M
            }
            {
                Symbol = "JNJ_082021C185"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 21)
                Price = 0.17M
                Contracts = 10
                Delta = 0.04M
                Theta = -0.01M
                IV = 16.38M
                StockPrice = 163.90M
            }
        ]
        SpreadType = IronCondor
        Net = 463.40M
        TargetPL = 370.72M
    }

    processTrade {
        Options = [
            {
                Symbol = "BBBY_071621P21"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 24)
                Price = 0.24M
                Contracts = 10
                Delta = -0.06M
                Theta = -0.02M
                IV = 106.35M
                StockPrice = 30.25M
            }
            {
                Symbol = "BBBY_071621P26"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 24)
                Price = 1.20M
                Contracts = -10
                Delta = -0.23M
                Theta = -0.05M
                IV = 106.35M
                StockPrice = 30.25M
            }
            {
                Symbol = "BBBY_071621C49"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 24)
                Price = 0.38M
                Contracts = -10
                Delta = 0.10M
                Theta = -0.04M
                IV = 129.80M
                StockPrice = 30.25M
            }
            {
                Symbol = "BBBY_071621C50"
                OptionType = Call
                Position = Long
                Date = new DateTime (2021, 6, 24)
                Price = 0.37M
                Contracts = 10
                Delta = 0.09M
                Theta = -0.03M
                IV = 129.45M
                StockPrice = 30.25M
            }
        ]
        SpreadType = IronCondor
        Net = 942.59M
        TargetPL = 754.07M
    }