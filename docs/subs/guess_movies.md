# r/movies

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/guess_movies_General.md)|FastTreeTweedieRegression|2,336|0.53|
|[SwapUsers](models/guess_movies_SwapUsers.md)|FastTreeTweedieRegression|2,336|0.53|
|[SwapBuzz_N+1](models/guess_movies_SwapBuzz_N+1.md)|FastTreeRegression|2,336|0.42|
|[20minTrain](models/guess_movies_20minTrain.md)|LightGbmRegression|1,634|0.72|
|[DropAuthor_N+1](models/guess_movies_DropAuthor_N+1.md)|LightGbmRegression|1,634|0.64|
|[DropAuthor](models/guess_movies_DropAuthor.md)|LightGbmRegression|1,083|0.12|
|[DropTitle](models/guess_movies_DropTitle.md)|LightGbmRegression|1,083|0.22|
|[AutoML](models/guess_movies_AutoML.md)|SdcaRegression|853|0.23|
|[Observe1](models/guess_movies_Observe1.md)|FastForestRegressionTrainer|1,706|0.43|
|[Observe10](models/guess_movies_Observe10.md)|FastTreeTweedieTrainer|1,233|0.47|
|[Observe5](models/guess_movies_Observe5.md)|GamRegressionTrainer|977|0.47|
|[Prototype](models/guess_movies_Prototype.md)||0|0.00|

## Subreddit Charts

![r/movies Distributions](../images/guess_movies_Distributions.png "r/movies Distributions")

![r/movies Categorical](../images/guess_movies_Catagorical.png "r/movies Categorical")

![r/movies Correlation](../images/guess_movies_Correlations.png "r/movies Correlation")

