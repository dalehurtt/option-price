module Condors

open System
open Trade
open Report

let processTrades filename =
    let processTrade = Report.onCondor filename       // curry to simplify

    // -------------------- IRON CONDORS --------------------

    let intro =
        ["<h2>Iron Condors</h2>"]
    intro |> List.toArray |> Report.appendToReport filename

    (*processTrade {
        Symbols = [ "FB_071621P295"; "FB_071621P300"; "FB_071621C365"; "FB_071621C370" ]
        Date = new DateTime (2021, 6, 14, 10, 0, 0)
        Price = [| 1.01M; 1.30M; 1.02M; 0.76M |]  
        Contracts = 10
        Delta = [| -0.06M; -0.08M; 0.12M; 0.09M |]  
        IV = [| 0.2973M; 0.2838M; 0.2244M; 0.2244M |]
        Credit = 552.58M
        TargetPL = 418.06M
        StockPrice = 336.77M
    }*)

    (*processTrade {
        Symbols = [ "LOW_071621P165"; "LOW_071621P170"; "LOW_071621C200"; "LOW_071621C210" ]
        Date = new DateTime (2021, 6, 17, 15, 4, 35)
        Price = [| 0.31M; 0.52M; 0.71M; 0.2M |]  
        Contracts = 10
        Delta = [| -0.05M; -0.09M; 0.13M; 0.04M |]  
        IV = [| 0.2743M; 0.2472M; 0.2016M; 0.2289M |]
        Credit = 693.40M
        TargetPL = 418.06M
        StockPrice = 186.84M
    }*)
    
    processTrade {
        Symbols = [ "BA_071621P195"; "BA_071621P200"; "BA_071621C270"; "BA_071621C275" ]
        Date = new DateTime (2021, 6, 18, 15, 31, 57)
        Price = [| 0.71M; 0.87M; 1.11M; 0.87M |]  
        Contracts = 10
        Delta = [| -0.05M; -0.07M; 0.10M; 0.08M |]  
        IV = [| 0.4519M; 0.4245M; 0.3476M; 0.3593M |]
        Credit = 373.39M
        TargetPL = 298.71M
        StockPrice = 237.35M
    }
    
    processTrade {
        Symbols = [ "V_071621P215"; "V_071621P217.5"; "V_071621C247.5"; "V_071621C250" ]
        Date = new DateTime (2021, 6, 21, 15, 0, 0)
        Price = [| 0.67M; 0.83M; 0.63M; 0.44M |]  
        Contracts = 10
        Delta = [| -0.09M; -0.11M; 0.13M; 0.09M |]  
        IV = [| 0.2433M; 0.2348M; 0.1760M; 0.1796M |]
        Credit = 322.58M
        TargetPL = 258.06M
        StockPrice = 234.32M
    }
    
    processTrade {
        Symbols = [ "JNJ_082021P145"; "JNJ_082021P150"; "JNJ_082021C180"; "JNJ_082021C185" ]
        Date = new DateTime (2021, 6, 21, 13, 26, 16)
        Price = [| 0.54M; 0.88M; 0.32M; 0.17M |]  
        Contracts = 10
        Delta = [| -0.08M; -0.13M; 0.07M; 0.04M |]  
        IV = [| 0.2147M; 0.1913M; 0.1595M; 0.1682M |]
        Credit = 490.00M
        TargetPL = 392.00M
        StockPrice = 163.90M
    }