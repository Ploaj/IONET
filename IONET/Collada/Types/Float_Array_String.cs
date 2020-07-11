using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Float_Array_String
	{
		[XmlText()]
	    public string Value_As_String;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public void SetValues(float[] values)
        {
            Value_As_String = string.Join(" ", values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float[] GetValues()
        {
            if (string.IsNullOrEmpty(Value_As_String))
                return null;

            return Regex.Replace(Value_As_String.Trim(), @"\s+", " ").Split(' ').Select(e => float.Parse(e)).ToArray();
        }
    }
}

