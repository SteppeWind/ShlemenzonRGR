using Model.PublicationTypes;
using ModelDataBase.DBPublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelDataBase.VMPublicationTypes;

namespace ServerApp.Converting
{
    class SmallPublicationConverter<TDBPublication, TVMPublication>
        where TDBPublication : SmallPublication, new()
        where TVMPublication : SmallPublication, new()
    {
        public virtual TVMPublication ConvertPublication(TDBPublication publication)
        {
            TVMPublication result = new TVMPublication();
            result.Convert(result, publication);

            return result;
        }


        public virtual TDBPublication ConvertPublication(TVMPublication publication)
        {
            TDBPublication result = new TDBPublication();
            result.Convert(result, publication);

            return result;
        }
    }
}