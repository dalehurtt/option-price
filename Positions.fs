module Positions

open System
open Trade

let processTrades stockpath =
    let processLEAPTrade = Report.onLEAP
    let processSingleTrade = Report.onLEAP
    let processSpreadTrade = Report.onSpread
    let processStockTrade = Report.onStock stockpath

    processStockTrade {
        Symbol = "ABBV"
        Position = Long
        Date = new DateTime (2021, 4, 13)
        Price = 108.779M
        Shares = 500.0M
        Cover = None (*Some
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
                Broker = "Fidelity"
            }*)
    }

    processStockTrade {
        Symbol = "AMCR"
        Position = Long
        Date = new DateTime (2021, 4, 14)
        Price = 11.73305M
        Shares = 4500.0M
        Cover = None
    }

    processSpreadTrade {
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
                Broker = "TD Ameritrade"
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
                Broker = "TD Ameritrade"
            }
        ]
        SpreadType = VerticalCredit
        Net = 146.70M
        TargetPL = 117.36M
    }

    processSpreadTrade {
        Options = [
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
                Broker = "TD Ameritrade"
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
                Broker = "TD Ameritrade"
            }
        ]
        SpreadType = VerticalCredit
        Net = 226.69M
        TargetPL = 181.35M
    }

    processSpreadTrade {
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 946.29M
        TargetPL = 757.03M
    }

    processSpreadTrade {
        Options = [
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalDebit
        Net = -3.71M
        TargetPL = 0.00M
    }

    processLEAPTrade {
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

    processSingleTrade {
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
        Broker = "Fidelity"
    }

    processStockTrade {
        Symbol = "CVX"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 107.78M
        Shares = 400.0M
        Cover = Some
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
                Broker = "Fidelity"
            }
    }

    processSingleTrade {
        Symbol = "DIA_121721C305"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 28)
        Price = 41.45M
        Contracts = 1
        Delta = 0.80M
        Theta = -0.03M
        IV = 21.24M
        StockPrice = 342.80M
        Broker = "Fidelity"
    }

    processLEAPTrade {
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

    (*processSingleTrade {
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
        Broker = "Fidelity"
    }*)

    processSingleTrade {
        Symbol = "EFA_111921C70"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 28)
        Price = 10.40M
        Contracts = 2
        Delta = 0.80M
        Theta = -0.01M
        IV = 25.71M
        StockPrice = 79.47M
        Broker = "Fidelity"
    }

    processStockTrade {
    Symbol = "ENB"
    Position = Long
    Date = new DateTime (2021, 4, 13)
    Price = 37.55775M
    Shares = 2200.0M
    Cover = None
    }

    processLEAPTrade {
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

    processSingleTrade {
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
        Broker = "Fidelity"
    }

    processSpreadTrade {
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 276.29M
        TargetPL = 221.03M
    }

    processSpreadTrade {
        Options = [
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 246.29M
        TargetPL = 197.03M
    }

    processStockTrade {
    Symbol = "FNCL"
    Position = Long
    Date = new DateTime (2021, 5, 12)
    Price = 53.109M
    Shares = 1800.0M
    Cover = None
    }

    processSingleTrade {
        Symbol = "GLW_111921C35"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 28)
        Price = 6.81M
        Contracts = 3
        Delta = 0.81M
        Theta = -0.01M
        IV = 32.04M
        StockPrice = 40.99M
        Broker = "Fidelity"
    }

    processSpreadTrade {
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
                Broker = "TD Ameritrade"
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
                Broker = "TD Ameritrade"
            }
        ]
        SpreadType = VerticalCredit
        Net = 326.70M
        TargetPL = 261.36M
    }

    processSpreadTrade {
        Options = [
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
                Broker = "TD Ameritrade"
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
                Broker = "TD Ameritrade"
            }
        ]
        SpreadType = VerticalCredit
        Net = 136.70M
        TargetPL = 109.36M
    }

    processStockTrade {
    Symbol = "KBWY"
    Position = Long
    Date = new DateTime (2021, 5, 24)
    Price = 22.825M
    Shares = 1000.0M
    Cover = None
    }

    processSingleTrade {
        Symbol = "KMI_121721C15"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 21)
        Price = 3.08M
        Contracts = 7
        Delta = 0.84M
        Theta = -0.00M
        IV = 31.64M
        StockPrice = 17.97M
        Broker = "Fidelity"
    }

    processSpreadTrade {
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
                Broker = "TD Ameritrade"
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
                Broker = "TD Ameritrade"
            }
        ]
        SpreadType = VerticalCredit
        Net = 196.70M
        TargetPL = 157.36M
    }
    
    processLEAPTrade {
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

    processSingleTrade {
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
        Broker = "TD Ameritrade"
    }

    processStockTrade {
    Symbol = "OGE"
    Position = Long
    Date = new DateTime (2021, 6, 2)
    Price = 34.0542M
    Shares = 1400.0M
    Cover = None
    }

    processStockTrade {
    Symbol = "OMF"
    Position = Long
    Date = new DateTime (2021, 4, 13)
    Price = 55.30M
    Shares = 1200.0M
    Cover = None
    }

    processStockTrade {
    Symbol = "PFE"
    Position = Long
    Date = new DateTime (2021, 4, 13)
    Price = 37.0458M
    Shares = 1300.0M
    Cover = None
    }

    processStockTrade {
    Symbol = "PFF"
    Position = Long
    Date = new DateTime (2021, 5, 24)
    Price = 38.669M
    Shares = 1000.0M
    Cover = None
    }

    processLEAPTrade {
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

    processLEAPTrade {
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

    processSingleTrade {
        Symbol = "PYPL_091721C320"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 25)
        Price = 6.00M
        Contracts = -2
        Delta = 0.26M
        Theta = -0.08M
        IV = 29.58M
        StockPrice = 289.60M
        Broker = "Fidelity"
    }

    processSingleTrade {
        Symbol = "PYPL_091721C320"
        OptionType = Call
        Position = Short
        Date = new DateTime (2021, 6, 25)
        Price = 6.10M
        Contracts = -1
        Delta = 0.26M
        Theta = -0.08M
        IV = 29.58M
        StockPrice = 289.60M
        Broker = "TD Ameritrade"
    }

    processStockTrade {
    Symbol = "QYLD"
    Position = Long
    Date = new DateTime (2021, 4, 13)
    Price = 22.76166M
    Shares = 3000.0M
    Cover = None
    }

    processStockTrade {
        Symbol = "RIO"
        Position = Long
        Date = new DateTime (2021, 4, 27)
        Price = 86.45893M
        Shares = 800.0M
        Cover = Some
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
                Broker = "Fidelity"
            }
    }

    processSpreadTrade {
        Options = [
            {
                Symbol = "RIO_071621P74.07"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 11)
                Price = 0.35M
                Contracts = 2
                Delta = -0.07M
                Theta = -0.02M
                IV = 36.48M
                StockPrice = 87.48M
                Broker = "Fidelity"
            }
            {
                Symbol = "RIO_071621P79.07"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 11)
                Price = 0.74M
                Contracts = -2
                Delta = -0.14M
                Theta = -0.03M
                IV = 30.99M
                StockPrice = 87.48M
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 79.25M
        TargetPL = 63.40M
    }

    processSpreadTrade {
        Options = [
            {
                Symbol = "ROKU_071621P325"
                OptionType = Put
                Position = Long
                Date = new DateTime (2021, 6, 22)
                Price = 2.01M
                Contracts = 5
                Delta = -0.05M
                Theta = -0.12M
                IV = 54.36M
                StockPrice = 403.50M
                Broker = "Fidelity"
            }
            {
                Symbol = "ROKU_071621P335"
                OptionType = Put
                Position = Short
                Date = new DateTime (2021, 6, 22)
                Price = 2.93M
                Contracts = -5
                Delta = -0.07M
                Theta = -0.15M
                IV = 51.79M
                StockPrice = 403.50M
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 453.15M
        TargetPL = 362.52M
    }

    processSpreadTrade {
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 193.14M
        TargetPL = 154.51M
    }

    processStockTrade {
    Symbol = "RYLD"
    Position = Long
    Date = new DateTime (2021, 6, 10)
    Price = 25.32702M
    Shares = 4000.0M
    Cover = None
    }

    processStockTrade {
    Symbol = "SHYG"
    Position = Long
    Date = new DateTime (2021, 5, 24)
    Price = 45.8382M
    Shares = 1000.0M
    Cover = None
    }

    processStockTrade {
    Symbol = "STAG"
    Position = Long
    Date = new DateTime (2021, 3, 22)
    Price = 33.28M
    Shares = 1400.0M
    Cover = None
    }

    processStockTrade {
        Symbol = "T"
        Position = Long
        Date = new DateTime (2021, 6, 7)
        Price = 29.215M
        Shares = 1600.0M
        Cover = Some
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
                Broker = "Fidelity"
            }
    }

    processSingleTrade {
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
        Broker = "Fidelity"
    }

    processSpreadTrade {
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 146.29M
        TargetPL = 117.03M
    }

    processSpreadTrade {
        Options = [
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
                Broker = "Fidelity"
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
                Broker = "Fidelity"
            }
        ]
        SpreadType = VerticalCredit
        Net = 176.29M
        TargetPL = 141.03M
    }

    processStockTrade {
        Symbol = "VZ"
        Position = Long
        Date = new DateTime (2021, 6, 2)
        Price = 56.60M
        Shares = 800.0M
        Cover = Some
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
                Broker = "Fidelity"
            }
    }

    processSingleTrade {
        Symbol = "WDC_101521C57.5"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 28)
        Price = 14.45M
        Contracts = 2
        Delta = 0.79M
        Theta = -0.03M
        IV = 54.47M
        StockPrice = 69.99M
        Broker = "Fidelity"
    }

    processSingleTrade {
        Symbol = "XLF_111921C32"
        OptionType = Call
        Position = Long
        Date = new DateTime (2021, 6, 28)
        Price = 5.05M
        Contracts = 5
        Delta = 0.85M
        Theta = 0.05M
        IV = 22.59M
        StockPrice = 36.66M
        Broker = "Fidelity"
    }

    processStockTrade {
        Symbol = "XOM"
        Position = Long
        Date = new DateTime (2021, 3, 24)
        Price = 57.23644M
        Shares = 800.0M
        Cover = Some
            {
                Symbol = "XOM_091721C70"
                OptionType = Call
                Position = Short
                Date = new DateTime (2021, 6, 25)
                Price = 1.26M
                Contracts = -8
                Delta = 0.26M
                Theta = -0.01M
                IV = 27.29M
                StockPrice = 64.66M
                Broker = "Fidelity"
            }
    }

    processStockTrade {
        Symbol = "XYLD"
        Position = Long
        Date = new DateTime (2021, 4, 29)
        Price = 47.9382M
        Shares = 2000.0M
        Cover = None
    }

