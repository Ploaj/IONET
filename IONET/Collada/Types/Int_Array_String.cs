using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Int_Array_String
	{
		[XmlText()]
	    public string Value_As_String;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[] GetValues()
        {
            if (string.IsNullOrEmpty(Value_As_String))
                return null;

            return Regex.Replace(Value_As_String.Trim(), @"\s+", " ").Split(' ').Select(e => int.Parse(e)).ToArray();
        }
	}
}

