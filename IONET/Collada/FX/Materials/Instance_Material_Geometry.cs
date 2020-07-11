using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Materials
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_material", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Material_Geometry
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;		
		
		[XmlAttribute("target")]
		public string Target;	
		
		[XmlAttribute("symbol")]
		public string Symbol;	
		
	    [XmlElement(ElementName = "bind")]
		public IONET.Collada.FX.Materials.Bind_FX[] Bind;	
		
	    [XmlElement(ElementName = "bind_vertex_input")]
		public IONET.Collada.FX.Effects.Bind_Vertex_Input[] Bind_Vertex_Input;	
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

