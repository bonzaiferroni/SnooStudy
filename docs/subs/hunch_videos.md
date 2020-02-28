# r/videos

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/hunch_videos_General.md)|LightGbmRegression|5,279|0.05|
|[DropAuthor_N+1](models/hunch_videos_DropAuthor_N+1.md)|LightGbmRegression|5,279|0.05|
|[DropAuthor](models/hunch_videos_DropAuthor.md)|FastTreeTweedieRegression|3,906|0.01|
|[DropTitle](models/hunch_videos_DropTitle.md)|FastTreeTweedieRegression|3,906|0.05|
|[RawData](models/hunch_videos_RawData.md)|FastTreeTweedieRegression|3,440|-0.01|
|[Full](models/hunch_videos_Full.md)|FastForestRegressionTrainer|1,868|0.01|
|[Prototype](models/hunch_videos_Prototype.md)||0|0.00|

## Subreddit Charts

![r/videos Distributions](../images/hunch_videos_Distributions.png "r/videos Distributions")

![r/videos Categorical](../images/hunch_videos_Catagorical.png "r/videos Categorical")

![r/videos Correlation](../images/hunch_videos_Correlations.png "r/videos Correlation")

