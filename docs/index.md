# Snoo Study 

Stay a while, and listen.

## Overview

### guess

|Subreddit|Trainer Name|Feature Set|n|R²|
|:---|:---|:---|---:|---:|
|[r/politics](subs/guess_politics.md)|FastTree|20minTrain|5,354|0.64|
|[r/news](subs/guess_news.md)|FastTreeTweedie|20minTrain|733|0.54|
|[r/worldnews](subs/guess_worldnews.md)|FastTreeTweedie|20minTrain|3,521|0.40|
|[r/television](subs/guess_television.md)|FastTreeTweedie|20minTrain|559|0.53|
|[r/movies](subs/guess_movies.md)|LightGbm|20minTrain|1,634|0.72|
|[r/the_donald](subs/guess_the_donald.md)|LightGbm|20minTrain|13,363|0.82|
|[r/pics](subs/guess_pics.md)|LightGbm|20minTrain|8,059|0.47|
|[r/videos](subs/guess_videos.md)|FastTreeTweedie|20minTrain|3,465|0.20|
|[r/art](subs/guess_art.md)|Sdca|20minTrain|4,163|0.37|
|[r/dataisbeautiful](subs/guess_dataisbeautiful.md)|FastTreeTweedie|20minTrain|365|0.20|
|[r/outoftheloop](subs/guess_outoftheloop.md)|FastTreeTweedie|20minTrain|164|-0.11|
|[r/gifs](subs/guess_gifs.md)|LightGbm|20minTrain|665|0.43|
|[r/funny](subs/guess_funny.md)|LightGbm|20minTrain|7,251|0.63|
|[r/atheism](subs/guess_atheism.md)|FastTreeTweedie|20minTrain|783|0.26|
|[r/music](subs/guess_music.md)|FastTreeTweedie|20minTrain|3,912|0.12|
|[r/aww](subs/guess_aww.md)|FastTree|20minTrain|13,290|0.60|
|[r/gaming](subs/guess_gaming.md)|FastForest|20minTrain|5,060|0.63|

### hunch

|Subreddit|Trainer Name|Feature Set|n|R²|
|:---|:---|:---|---:|---:|
|[r/politics](subs/hunch_politics.md)|FastTree|DropAuthor_N+1|9,377|0.20|
|[r/news](subs/hunch_news.md)|FastTreeTweedie|DropAuthor_N+1|1,180|0.19|
|[r/worldnews](subs/hunch_worldnews.md)|FastForest|DropAuthor_N+1|5,706|0.20|
|[r/television](subs/hunch_television.md)|FastTree|DropAuthor_N+1|837|0.59|
|[r/movies](subs/hunch_movies.md)|LightGbm|DropAuthor_N+1|2,574|0.27|
|[r/the_donald](subs/hunch_the_donald.md)|LightGbm|DropAuthor_N+1|19,488|0.44|
|[r/pics](subs/hunch_pics.md)|FastForest|DropAuthor_N+1|12,360|0.11|
|[r/videos](subs/hunch_videos.md)|LightGbm|DropAuthor_N+1|5,279|0.05|
|[r/art](subs/hunch_art.md)|FastTreeTweedie|DropAuthor_N+1|6,385|0.07|
|[r/dataisbeautiful](subs/hunch_dataisbeautiful.md)|FastTreeTweedie|DropAuthor_N+1|412|-0.00|
|[r/outoftheloop](subs/hunch_outoftheloop.md)|FastTreeTweedie|DropAuthor_N+1|181|-0.10|
|[r/gifs](subs/hunch_gifs.md)|FastTreeTweedie|DropAuthor_N+1|764|0.14|
|[r/funny](subs/hunch_funny.md)|Sdca|DropAuthor_N+1|7,251|0.11|
|[r/atheism](subs/hunch_atheism.md)|FastTreeTweedie|DropAuthor_N+1|783|-0.03|
|[r/music](subs/hunch_music.md)|FastForest|DropAuthor_N+1|3,912|0.00|
|[r/aww](subs/hunch_aww.md)|FastTreeTweedie|DropAuthor_N+1|13,290|0.12|
|[r/gaming](subs/hunch_gaming.md)|FastForest|DropAuthor_N+1|5,060|0.11|

