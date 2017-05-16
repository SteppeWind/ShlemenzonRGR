using Model.PublicationTypes.NewsPublications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMElementLinkVideo : VMNewsElement
    {
        string attributeYTVideo = "https://www.youtube.com/watch";

        string link = @"https://www.youtube.com/embed/";

        public String HTMLCode { get; private set; }

        public String FullLinkForVideo { get; private set; }

        public string ResultCode { get; private set; }

        public VMElementLinkVideo()
        {
            TypeElement = TypeElementOfNews.LinkVideo;
        }

        /// <summary>
        /// Устанавливает html код для видео
        /// </summary>
        /// <param name="linkVideo">Ссылка с youtube</param>
        /// <returns>Результат выполнения</returns>
        public bool SetResultCode(string linkVideo)
        {
            var arr = linkVideo.Split('=');
            if (arr.Length >= 2)
            {
                if (arr[0].Contains(attributeYTVideo))
                {
                    FullLinkForVideo = link + arr[1];
                    HTMLCode = $@"<center><iframe width=""560"" height=""315"" src=""{FullLinkForVideo}"" frameborder=""0"" allowfullscreen></iframe></center>";
                    return true;
                }
            }
            return false;
        }
    }
}