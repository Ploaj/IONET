using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Effects
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="bind_vertex_input", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Bind_Vertex_Input
	{
		[XmlAttribute("semantic")]
		public string Semantic;

		[XmlAttribute("imput_semantic")]
		public string Imput_Semantic;

		[XmlAttribute("input_set")]
		public int Input_Set;
		
	}
}

