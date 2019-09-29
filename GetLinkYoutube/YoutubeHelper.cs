using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetLinkYoutube
{
    public static class YoutubeHelper
    {
        const string QUALITY_REGEX = @"(?<=quality=)[\w]+";
        const string VIDEO_LINK_REGEX = @"(?<=url=)[a-zA-Z0-9_\-\.\%]+";
        const string LINK_HOME = "https://www.youtube.com/watch?v=";

        public async static Task<List<YoutubeVideo>> GetVideoPlayLinks(string idOrLink)
        {
            var list = new List<YoutubeVideo>();

            if (!idOrLink.StartsWith("http"))
                idOrLink = LINK_HOME + idOrLink;
            var html = await HttpHelper.GetHTML(idOrLink, 5000);
            var raw = html.GetRegexMatchValue("(?<=url_encoded_fmt_stream_map\":\")[^\"]+");
            var qualities = raw.GetMatchItemsRegex(QUALITY_REGEX);
            var links = raw.GetMatchItemsRegex(VIDEO_LINK_REGEX);
            var size = qualities.Count > links.Count ? links.Count : qualities.Count;

            for (int i = 0; i < size; i++)
            {
                var video = new YoutubeVideo { Title = qualities[i], Link = links[i].UrlDecode() };
                if (await HttpHelper.RemoteFileExistsWithTimeout(video.Link, 5000))
                    list.Add(video);
            }

            return list;
        }
    }
}
