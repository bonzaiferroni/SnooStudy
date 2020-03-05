# r/videos

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/guess_videos_General.md)|FastTreeTweedieRegression|6,328|0.17|
|[SwapActivity_N+1](models/guess_videos_SwapActivity_N+1.md)|SdcaRegression|6,328|0.20|
|[SwapActivity](models/guess_videos_SwapActivity.md)|FastForestRegression|5,034|0.12|
|[SwapUsers](models/guess_videos_SwapUsers.md)|FastForestRegression|5,034|0.12|
|[SwapBuzz_N+1](models/guess_videos_SwapBuzz_N+1.md)|FastForestRegression|5,034|0.12|
|[SwapBuzz](models/guess_videos_SwapBuzz.md)|FastTreeTweedieRegression|3,465|0.09|
|[20minTrain](models/guess_videos_20minTrain.md)|FastTreeTweedieRegression|3,465|0.20|
|[DropAuthor_N+1](models/guess_videos_DropAuthor_N+1.md)|FastTreeTweedieRegression|3,465|0.15|
|[DropAuthor](models/guess_videos_DropAuthor.md)|LightGbmRegression|2,092|0.02|
|[DropTitle](models/guess_videos_DropTitle.md)|LightGbmRegression|2,092|0.02|
|[AutoML](models/guess_videos_AutoML.md)|FastTreeTweedieRegression|1,626|0.01|
|[Observe5](models/guess_videos_Observe5.md)|GamRegressionTrainer|2,454|0.13|
|[Observe2](models/guess_videos_Observe2.md)|FastTreeTweedieTrainer|2,126|0.16|
|[Observe10](models/guess_videos_Observe10.md)|LbfgsPoissonRegressionTrainer|1,868|0.20|
|[Prototype](models/guess_videos_Prototype.md)||0|0.00|

## Subreddit Charts

![r/videos Distributions](../images/guess_videos_Distributions.png "r/videos Distributions")

![r/videos Categorical](../images/guess_videos_Catagorical.png "r/videos Categorical")

![r/videos Correlation](../images/guess_videos_Correlations.png "r/videos Correlation")

