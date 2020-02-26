# r/music

[Home](../index.md)

|Model|Trainer|n|R²|
|:---|:---|---:|---:|
|[General](models/hunch_music_General.md)|FastForestRegression|3,912|0.00|
|[DropAuthor_N+1](models/hunch_music_DropAuthor_N+1.md)|FastForestRegression|3,912|0.00|
|[DropAuthor](models/hunch_music_DropAuthor.md)|FastTreeTweedieRegression|2,257|-0.01|
|[DropTitle](models/hunch_music_DropTitle.md)|FastTreeTweedieRegression|2,257|-0.02|
|[RawData](models/hunch_music_RawData.md)|FastTreeTweedieRegression|1,675|-0.02|
|[Full](models/hunch_music_Full.md)|FastTreeTweedieTrainer|185|0.05|
|[Prototype](models/hunch_music_Prototype.md)||0|0.00|

![r/music Distributions (hunch)](../images/hunch_music_Distributions.png "r/music Distributions (hunch)")

