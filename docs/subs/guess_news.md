# r/news

[Home](../index.md)

## Models

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/guess_news_General.md)|FastForestRegression|1,044|0.40|
|[20minTrain](models/guess_news_20minTrain.md)|FastTreeTweedieRegression|733|0.54|
|[DropAuthor_N+1](models/guess_news_DropAuthor_N+1.md)|FastTreeTweedieRegression|733|0.46|
|[AutoML](models/guess_news_AutoML.md)|FastTreeTweedieRegression|377|0.29|
|[Observe10](models/guess_news_Observe10.md)|FastTreeTweedieTrainer|463|0.73|
|[Prototype](models/guess_news_Prototype.md)||0|0.00|

## Subreddit Charts

![r/news Distributions](../images/guess_news_Distributions.png "r/news Distributions")

![r/news Categorical](../images/guess_news_Catagorical.png "r/news Categorical")

![r/news Correlation](../images/guess_news_Correlations.png "r/news Correlation")

