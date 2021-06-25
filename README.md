# Option Price
This application and set of functions allows you to monitor your option positions, allowing you to handle more trades.

Although you can use the Program.fs as a start, you will definitely have to replace the data in LEAPS.fs, Stocks.fs, Condors.fs, and Spreads.fs with your own.

## Rules annd Strategy

Over time I will replace the hardcoding of rules and strategy as configuration variables, but this is a work in progress and it is how I work. I iterate from hardcoding to variables to configuration over time as I refactor code to make it more generic and functional.

Speaking of which, this is written completely in F# and for me is a learning process. So some functions will seem more _refined_ than others. Eventually I will go back and replace the older functions with the newer techniques I learned later. A good example if the use of _fold2_ to iterate through two lists simultaneously.