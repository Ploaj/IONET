using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Texturing
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="image", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Image
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;		

	    [XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;			
		
	    [XmlElement(ElementName = "renderable")]
		public IONET.Collada.FX.Custom_Types.Renderable_Share Renderable_Share;			
		
	    [XmlElement(ElementName = "init_from")]
		public IONET.Collada.FX.Texturing.Init_From Init_From;			
		
	    [XmlElement(ElementName = "create_2d")]
		public IONET.Collada.FX.Texturing.Create_2D Create_2D;			

	    [XmlElement(ElementName = "create_3d")]
		public IONET.Collada.FX.Texturing.Create_3D Create_3D;			

	    [XmlElement(ElementName = "create_cube")]
		public IONET.Collada.FX.Texturing.Create_Cube Create_Cube;			
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;			
	}
}

