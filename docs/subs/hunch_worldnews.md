# r/worldnews

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/hunch_worldnews_General.md)|FastForestRegression|5,706|0.20|
|[DropAuthor_N+1](models/hunch_worldnews_DropAuthor_N+1.md)|FastForestRegression|5,706|0.20|
|[DropAuthor](models/hunch_worldnews_DropAuthor.md)|FastTreeTweedieRegression|4,387|0.14|
|[DropTitle](models/hunch_worldnews_DropTitle.md)|FastTreeTweedieRegression|4,387|0.16|
|[RawData](models/hunch_worldnews_RawData.md)|FastForestRegression|3,844|0.09|
|[Full](models/hunch_worldnews_Full.md)|SdcaRegressionTrainer|2,237|0.00|
|[Prototype](models/hunch_worldnews_Prototype.md)||0|0.00|

## Subreddit Charts

![r/worldnews Distributions](../images/hunch_worldnews_Distributions.png "r/worldnews Distributions")

![r/worldnews Categorical](../images/hunch_worldnews_Catagorical.png "r/worldnews Categorical")

![r/worldnews Correlation](../images/hunch_worldnews_Correlations.png "r/worldnews Correlation")

