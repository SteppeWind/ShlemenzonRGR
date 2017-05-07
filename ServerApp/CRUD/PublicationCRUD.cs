using ServerApp.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Text;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.Foundation;
using Model.PublicationTypes;

namespace ServerApp.CRUD
{
    public static class PublicationCRUD
    {
        private static NewsForumContext CurrentNewsForumContext = NewsForumContext.GetNewsForumContext;

        public static object IAsyncOperation { get; private set; }

        public static void CreatePublication(VMPublication publication)
        {
           // TextGetOptions tgo = TextGetOptions.FormatRtf;
            string newPath = Server.PathServerDirecotory + CurrentNewsForumContext.Publications.Last().PublicatoinId + 1;
            Directory.CreateDirectory(newPath);
            switch (publication.TypePublication)
            {
                case PublicationType.Game:
                    VMGamePublication gamePublic = publication as VMGamePublication;
                    break;
                case PublicationType.Music:
                   
                    break;
                case PublicationType.News:
                    break;
                case PublicationType.Film:
                    break;
                default:
                    break;
            }
        }
    }
}