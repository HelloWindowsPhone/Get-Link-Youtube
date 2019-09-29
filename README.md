
# Get Play Links from Youtube video
This library written in C#.NET Standard

## How to use: very simple

	  var videoLinks = await YoutubeHelper.GetVideoPlayLinks(idOrLink);

*Note: This will fail if the video is VEVO or country restricted or age restricted*
