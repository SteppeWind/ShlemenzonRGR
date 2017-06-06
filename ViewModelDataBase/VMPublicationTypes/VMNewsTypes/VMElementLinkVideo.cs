using Model.PublicationTypes.NewsPublications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMElementLinkVideo : NewsElement
    {
        string attributeYTVideo = "https://www.youtube.com/watch";

        string link = @"https://www.youtube.com/embed/";

        [JsonIgnore]
        public String HTMLCode { get; private set; }

        [JsonIgnore]
        public String FullLinkForVideo { get; private set; }

        private string linkYoutubeVideo;
        public string LinkYoutubeVideo
        {
            get => linkYoutubeVideo;
            set
            {
                if (CheckValidLink(value))
                    linkYoutubeVideo = value;
            }
        }

        public VMElementLinkVideo()
        {
            TypeElement = TypeElementOfNews.LinkVideo;
            if (linkYoutubeVideo != null)
            {
                SetResultCode(linkYoutubeVideo);
            }
        }

        /// <summary>
        /// Устанавливает html код для видео
        /// </summary>
        /// <param name="linkVideo">Ссылка с youtube</param>
        /// <returns>Результат выполнения</returns>
        public bool SetResultCode(string linkVideo)
        {
            if (CheckValidLink(linkVideo))
            {
                var arr = linkVideo.Split('=');
                LinkYoutubeVideo = linkVideo;
                FullLinkForVideo = link + arr[1];
                HTMLCode = $@"<center><iframe width=""560"" height=""315"" src=""{FullLinkForVideo}"" frameborder=""0"" allowfullscreen></iframe></center>";
                return true;
            }
            return false;
        }

        public bool SetResultCode()
        {
            var arr = LinkYoutubeVideo.Split('=');
            FullLinkForVideo = link + arr[1];
            HTMLCode = $@"<center><iframe width=""560"" height=""315"" src=""{FullLinkForVideo}"" frameborder=""0"" allowfullscreen></iframe></center>";
            return true;
        }

        private bool CheckValidLink(string linkVideo)
        {
            var arr = linkVideo.Split('=');
            return arr.Length >= 2 && arr[0].Contains(attributeYTVideo);
        }
    }
}