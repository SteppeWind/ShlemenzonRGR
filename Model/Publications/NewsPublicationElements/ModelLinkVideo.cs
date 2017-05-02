using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Publications.NewsPublicationElements
{
    class ModelLinkVideo
    {
        public readonly string MainLinkForInsert = @"https://www.youtube.com/embed/";
        public string HTMLCode { get; private set; }
        public string LinkVideo { get; set; }

        public ModelLinkVideo()
        {
            CreateHTMLCode();
        }

        public ModelLinkVideo(string videoLink)
        {
            var arr = LinkVideo.Split('=');
            if (arr.Length >= 2) 
                LinkVideo = MainLinkForInsert + arr[1];
            CreateHTMLCode();
        }

        public void CreateHTMLCode()
        {
            HTMLCode = $@"<center><iframe width=""560"" height=""315"" src=""{LinkVideo}"" frameborder=""0"" allowfullscreen></iframe></center>";
        }
    }
}
