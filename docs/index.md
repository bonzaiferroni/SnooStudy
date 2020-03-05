# Snoo Study 

Stay a while, and listen.

## Overview

### guess

|Subreddit|Trainer Name|Feature Set|n|R²|
|:---|:---|:---|---:|---:|
|[r/politics](subs/guess_politics.md)|LightGbm|AddSlump|10,288|0.67|
|[r/news](subs/guess_news.md)|FastTreeTweedie|AddSlump|1,281|0.46|
|[r/worldnews](subs/guess_worldnews.md)|FastForest|AddSlump|7,110|0.45|
|[r/television](subs/guess_television.md)|FastTreeTweedie|AddSlump|1,099|0.66|
|[r/movies](subs/guess_movies.md)|FastTreeTweedie|AddSlump|2,925|0.38|
|[r/the_donald](subs/guess_the_donald.md)|LightGbm|AddSlump|16,522|0.79|
|[r/pics](subs/guess_pics.md)|FastForest|AddSlump|15,429|0.63|
|[r/videos](subs/guess_videos.md)|FastTreeTweedie|AddSlump|6,328|0.17|
|[r/art](subs/guess_art.md)|FastTreeTweedie|AddSlump|7,748|0.46|
|[r/dataisbeautiful](subs/guess_dataisbeautiful.md)|FastTree|AddSlump|689|0.17|
|[r/outoftheloop](subs/guess_outoftheloop.md)|FastTreeTweedie|AddSlump|318|-0.03|
|[r/gifs](subs/guess_gifs.md)|FastForest|AddSlump|1,258|0.44|
|[r/funny](subs/guess_funny.md)|FastTree|AddSlump|14,140|0.64|
|[r/atheism](subs/guess_atheism.md)|FastTree|AddSlump|1,669|0.73|
|[r/music](subs/guess_music.md)|FastTreeTweedie|AddSlump|7,533|0.10|
|[r/aww](subs/guess_aww.md)|FastForest|AddSlump|26,717|0.64|
|[r/gaming](subs/guess_gaming.md)|FastTreeTweedie|AddSlump|10,115|0.61|

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

