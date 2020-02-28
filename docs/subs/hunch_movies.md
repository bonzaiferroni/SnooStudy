# r/movies

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/hunch_movies_General.md)|LightGbmRegression|2,574|0.27|
|[DropAuthor_N+1](models/hunch_movies_DropAuthor_N+1.md)|LightGbmRegression|2,574|0.27|
|[DropAuthor](models/hunch_movies_DropAuthor.md)|LightGbmRegression|2,023|0.17|
|[DropTitle](models/hunch_movies_DropTitle.md)|FastForestRegression|2,023|0.16|
|[RawData](models/hunch_movies_RawData.md)|FastTreeTweedieRegression|1,793|0.20|
|[Full](models/hunch_movies_Full.md)|FastForestRegressionTrainer|977|0.05|
|[Prototype](models/hunch_movies_Prototype.md)||0|0.00|

## Subreddit Charts

![r/movies Distributions](../images/hunch_movies_Distributions.png "r/movies Distributions")

![r/movies Categorical](../images/hunch_movies_Catagorical.png "r/movies Categorical")

![r/movies Correlation](../images/hunch_movies_Correlations.png "r/movies Correlation")

