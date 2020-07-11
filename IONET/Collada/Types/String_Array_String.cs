using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class String_Array_String
	{
		//TODO: cleanup to legit array
		[XmlText()]
	    public string Value_Pre_Parse;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetValues()
        {
            if (string.IsNullOrEmpty(Value_Pre_Parse))
                return null;

            return Regex.Replace(Value_Pre_Parse.Trim(), @"\s+", " ").Split(' ');
        }
    }
}
