# r/art

[Home](../index.md)

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/hunch_art_General.md)|FastTreeTweedieRegression|6,385|0.07|
|[DropAuthor_N+1](models/hunch_art_DropAuthor_N+1.md)|FastTreeTweedieRegression|6,385|0.07|
|[DropAuthor](models/hunch_art_DropAuthor.md)|SdcaRegression|4,955|0.05|
|[DropTitle](models/hunch_art_DropTitle.md)|SdcaRegression|4,955|0.05|
|[RawData](models/hunch_art_RawData.md)|FastForestRegression|4,294|0.05|
|[Full](models/hunch_art_Full.md)|OnlineGradientDescentTrainer|2,285|-0.01|
|[Prototype](models/hunch_art_Prototype.md)||0|0.00|

![r/art Distributions (hunch)](../images/hunch_art_Distributions.png "r/art Distributions (hunch)")

