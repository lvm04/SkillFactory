using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using SF.Module18.Utils;

namespace SF.Module18
{
    /// <summary>
    /// Исполнитель команд (Receiver)
    /// </summary>
    internal class YoutubeLoader
    {
        private YoutubeClient youtube;
        private string outputDir;
        private string ffmpegDir;
        public string Url { get; set; }

        public YoutubeLoader(string outputDir, string ffmpegDir)
        {
            this.outputDir = outputDir;
            this.ffmpegDir = ffmpegDir;
            youtube = new YoutubeClient();
        }

        public async Task DisplayInfoAsync()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                Console.WriteLine("Ошибка! Укажите адрес видео");
                return;
            }

            try
            {
                var video = await youtube.Videos.GetAsync(Url);

                var title = video.Title;
                var author = video.Author.Title;
                var duration = video.Duration;
                var uploadDate = video.UploadDate;
                var viewCount = video.Engagement.ViewCount;

                Console.WriteLine("\r\n_______Информация о видео_______");
                Console.WriteLine($" Название   : {title}");
                Console.WriteLine($" Автор      : {author}");
                Console.WriteLine($" Продолж.   : {duration}");
                Console.WriteLine($" Дата загр. : {uploadDate:dd.MM.yyyy}");
                Console.WriteLine($" Просмотров : {viewCount:#,#}\r\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task LoadFileAsync()
        {
            string file = $"{outputDir}\\video_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.mp4";
            Console.WriteLine($"Загружается файл {file} ...");

            try
            {
                using (var progress = new InlineProgress())
                {
                    await youtube.Videos.DownloadAsync(Url, file,
                                o => o.SetFormat("webm")
                                    .SetPreset(ConversionPreset.UltraFast)
                                    .SetFFmpegPath($"{ffmpegDir}\\ffmpeg.exe"),
                                progress);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
