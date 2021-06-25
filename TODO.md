# To Do List

## Model Covered Calls Properly

There are two types of covered calls I deal with: selling a Call and covering it with stock; and selling a call and covering it with a LEAP or other long-term option I am using as a substitution for stock.

Although you could look at the latter as a Calendar Call Debit Spread, I don't think it should be modeled as such. Spreads are modeled as an atomic unit, whereas covered calls are generally not as you tend to stay in the Long Call as long as possible and will buy back the sold Call separately from selling the LEAP. Essentially buying back the Call and selling the LEAP are two separate decisions whereas closing a spread is a single decision.

## Target Delta Strategy

Right now if an option has a delta greater than 0.20 or less than -0.20 it is marked as a SELL action. For those options that are purchased at higher positive or lower negative deltas, the trade should be able to reflect that alternative target delta so it can be used in the analysis of whether the position should be exited or not.

> The primary strategy this impacts is spreads (and all variations, such as Iron Condors) where the goal is to create credit spreads with low deltas and exit them when the target profit is hit, you are nearing expiration, or the option deltas stray too far from where they were sold.
