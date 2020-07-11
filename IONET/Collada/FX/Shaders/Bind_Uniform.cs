using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Shaders
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind_uniform", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Bind_Uniform
	{
		[XmlAttribute("symbol")]
		public string Symbol;

		[XmlElement(ElementName = "param")]
		public IONET.Collada.Core.Parameters.Param Param;	
		
		/// <summary>
		/// The element is the type and the element text is the value or space delimited list of values
		/// </summary>
		[XmlAnyElement]
		public XmlElement[] Data;	
	}
}

