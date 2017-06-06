using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.PublicationTypes
{
    public class ConvertProperty
    {
        public virtual List<PropertyInfo> GetProperties(object @object) =>
                     (from prop in @object.GetType().GetRuntimeProperties()
                      let customProperty = prop.GetCustomAttribute(typeof(Property))
                      where customProperty != null
                      select prop)
                     .ToList();

        public virtual List<PropertyInfo> GetProperties(object @object, object value)
        {
            var objectProperties = GetProperties(@object);
            var valueProperties = GetProperties(value);
            
            return null;
        }


        /// <summary>
        /// Конвертирует текущие свойства класса с указанным атрибутом Property
        /// </summary>
        /// <param name="value">Объект, от которого берем значения</param>
        public virtual void Convert(object value)
        {
            Convert(this, value);
        }

        /// <summary>
        /// Конвертирует текущие свойства класса с указанным атрибутом Property
        /// </summary>
        /// <param name="object">Объект, которому меняем значения</param>
        /// <param name="value">Объект, от которого берем значения</param>
        public virtual void Convert(object @object, object value)
        {
            var thisProperties = GetProperties(@object);
            var newProperties = GetProperties(value);
            List<PropertyInfo> thisProperties1 = new List<PropertyInfo>();
            List<PropertyInfo> newProperties1 = new List<PropertyInfo>();

            foreach (var item in thisProperties)
            {
                var currProp1 = item.GetCustomAttribute<Property>();
                foreach (var item2 in newProperties)
                {
                    var currProp2 = item2.GetCustomAttribute<Property>();
                    if (currProp1 != null && currProp2 != null && currProp1.Name == currProp2.Name)
                    {
                        thisProperties1.Add(item);
                        newProperties1.Add(item2);
                    }
                    
                }
            }
            int length = thisProperties1.Count();
            
            for (int i = 0; i < length; i++)
            {
                thisProperties1[i].SetValue(@object, newProperties1[i].GetValue(value));
            }
        }
    }
}