using Model.PublicationTypes.NewsPublications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMTypes;

namespace ViewModelDataBase.VMPublicationTypes.VMNewsTypes
{
    public class VMNewsPublication : VMPublication, INewsPublication<NewsElement>
    {
        public List<NewsElement> ListElements { get; set; } = new List<NewsElement>();

        [JsonIgnore]
        public override List<VMFile> ListFiles
        {
            get => (from el in ListElements
                    let item = el as VMNewsElementFile
                    where item != null
                    select new VMFile()
                    {
                        FullPath = item.FullPath,
                        Bytes = item.Bytes,
                        Name = item.Name,
                        Type = item.Type
                    }).ToList();
            set { }
        }

        public VMNewsPublication()
        {
            TypePublication = Model.PublicationTypes.PublicationType.News;
        }
    }
}