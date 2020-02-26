# r/movies

[Home](../index.md)

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/hunch_movies_General.md)|LightGbmRegression|2,574|0.27|
|[DropAuthor_N+1](models/hunch_movies_DropAuthor_N+1.md)|LightGbmRegression|2,574|0.27|
|[DropAuthor](models/hunch_movies_DropAuthor.md)|LightGbmRegression|2,023|0.17|
|[DropTitle](models/hunch_movies_DropTitle.md)|FastForestRegression|2,023|0.16|
|[RawData](models/hunch_movies_RawData.md)|FastTreeTweedieRegression|1,793|0.20|
|[Full](models/hunch_movies_Full.md)|FastForestRegressionTrainer|977|0.05|
|[Prototype](models/hunch_movies_Prototype.md)||0|0.00|

![r/movies Distributions (hunch)](../images/hunch_movies_Distributions.png "r/movies Distributions (hunch)")

