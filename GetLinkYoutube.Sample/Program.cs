using System;
using System.Threading.Tasks;

namespace GetLinkYoutube.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Input Youtube video id or link: ");
            var idOrLink = Console.ReadLine();

            var videoLinks = await YoutubeHelper.GetVideoPlayLinks(idOrLink);
            if (videoLinks == null || videoLinks.Count == 0)
            {
                Console.WriteLine("Error occurred! May be the video is VEVO or age | country restricted");
            }
            else
            {
                foreach(var video in videoLinks)
                {
                    Console.WriteLine($"\n{video.Title} | {video.Link}\n");
                }
            }
            Console.Read();
        }
    }
}
