# r/music

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/guess_music_General.md)|FastTreeRegression|5,873|0.08|
|[SwapUsers](models/guess_music_SwapUsers.md)|FastTreeRegression|5,873|0.05|
|[SwapBuzz_N+1](models/guess_music_SwapBuzz_N+1.md)|FastTreeTweedieRegression|5,873|0.09|
|[SwapBuzz](models/guess_music_SwapBuzz.md)|FastTreeTweedieRegression|3,912|0.13|
|[20minTrain](models/guess_music_20minTrain.md)|FastTreeTweedieRegression|3,912|0.12|
|[DropAuthor_N+1](models/guess_music_DropAuthor_N+1.md)|FastTreeTweedieRegression|3,912|0.05|
|[DropAuthor](models/guess_music_DropAuthor.md)|FastTreeTweedieRegression|2,257|-0.00|
|[DropTitle](models/guess_music_DropTitle.md)|FastTreeTweedieRegression|2,257|0.03|
|[AutoML](models/guess_music_AutoML.md)|FastTreeTweedieRegression|1,675|0.08|
|[Observe5](models/guess_music_Observe5.md)|SdcaRegressionTrainer|850|0.04|
|[Observe2](models/guess_music_Observe2.md)|FastTreeTweedieTrainer|185|0.94|
|[Prototype](models/guess_music_Prototype.md)||0|0.00|

## Subreddit Charts

![r/music Distributions](../images/guess_music_Distributions.png "r/music Distributions")

![r/music Categorical](../images/guess_music_Catagorical.png "r/music Categorical")

![r/music Correlation](../images/guess_music_Correlations.png "r/music Correlation")

