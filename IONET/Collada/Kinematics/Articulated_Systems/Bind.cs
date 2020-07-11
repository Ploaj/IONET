using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Bind
	{
		[XmlAttribute("symbol")]
		public string Symbol;
		
		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;

		[XmlElement(ElementName = "float")]
		public float Float;

		[XmlElement(ElementName = "int")]
		public int Int;

		[XmlElement(ElementName = "bool")]
		public bool Bool;

		[XmlElement(ElementName = "SIDREF")]
		public string SIDREF;
		
	}
}

