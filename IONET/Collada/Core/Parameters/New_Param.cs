using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Parameters
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class New_Param
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlElement(ElementName = "semantic")]
		public string Semantic;				
		
		[XmlElement(ElementName = "modifier")]
		public string Modifier;				
		
		[XmlElement("annotate")]
		public IONET.Collada.FX.Effects.Annotate[] Annotate;
	
		/// <summary>
		/// The element is the type and the element text is the value or space delimited list of values
		/// </summary>
		[XmlAnyElement]
		public XmlElement[] Data;	
	}
}

