using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Parameters
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="usertype", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class UserType
	{
		[XmlAttribute("typename")]
		public string TypeName;	
		
		[XmlAttribute("source")]
		public string Source;			
		
	    [XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.Set_Param[] SetParam;		
	}
}

