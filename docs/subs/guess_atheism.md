# r/atheism

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/guess_atheism_General.md)|FastTreeRegression|1,669|0.73|
|[SwapUsers](models/guess_atheism_SwapUsers.md)|FastForestRegression|1,267|0.57|
|[SwapBuzz_N+1](models/guess_atheism_SwapBuzz_N+1.md)|FastTreeTweedieRegression|1,267|0.65|
|[20minTrain](models/guess_atheism_20minTrain.md)|FastTreeTweedieRegression|783|0.26|
|[DropAuthor_N+1](models/guess_atheism_DropAuthor_N+1.md)|FastTreeTweedieRegression|783|0.27|
|[DropTitle](models/guess_atheism_DropTitle.md)|FastTreeTweedieRegression|474|-0.04|
|[AutoML](models/guess_atheism_AutoML.md)|FastTreeTweedieRegression|338|0.16|
|[Observe5](models/guess_atheism_Observe5.md)|LbfgsPoissonRegressionTrainer|150|0.56|
|[Prototype](models/guess_atheism_Prototype.md)||0|0.00|

## Subreddit Charts

![r/atheism Distributions](../images/guess_atheism_Distributions.png "r/atheism Distributions")

![r/atheism Categorical](../images/guess_atheism_Catagorical.png "r/atheism Categorical")

![r/atheism Correlation](../images/guess_atheism_Correlations.png "r/atheism Correlation")

