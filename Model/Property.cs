using Model.PublicationTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class Property : Attribute
    {
        /// <summary>
        /// Имя свойства
        /// </summary>
        public string Name { get; private set; }

        public Property()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Имя свойства</param>
        public Property(string name)
        {
            Name = name;
        }
    }
}