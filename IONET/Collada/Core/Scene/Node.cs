using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Scene
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Node
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("sid")]
		public string sID;

		[XmlAttribute("name")]
		public string Name;				

		[XmlAttribute("type")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Node_Type.NODE)]
		public IONET.Collada.Enums.Node_Type Type;				

		[XmlAttribute("layer")]
		public string Layer;				
		
		[XmlElement(ElementName = "lookat")]
		public IONET.Collada.Core.Transform.Lookat[] Lookat;

		[XmlElement(ElementName = "matrix")]
		public IONET.Collada.Core.Transform.Matrix[] Matrix;

		[XmlElement(ElementName = "rotate")]
		public IONET.Collada.Core.Transform.Rotate[] Rotate;

		[XmlElement(ElementName = "scale")]
		public IONET.Collada.Core.Transform.Scale[] Scale;

		[XmlElement(ElementName = "skew")]
		public IONET.Collada.Core.Transform.Skew[] Skew;

		[XmlElement(ElementName = "translate")]
		public IONET.Collada.Core.Transform.Translate[] Translate;
		
		[XmlElement(ElementName = "instance_camera")]
		public IONET.Collada.Core.Camera.Instance_Camera[] Instance_Camera;
		
		[XmlElement(ElementName = "instance_controller")]
		public IONET.Collada.Core.Controller.Instance_Controller[] Instance_Controller;
		
		[XmlElement(ElementName = "instance_geometry")]
		public IONET.Collada.Core.Geometry.Instance_Geometry[] Instance_Geometry;
		
		[XmlElement(ElementName = "instance_light")]
		public IONET.Collada.Core.Lighting.Instance_Light[] Instance_Light;
		
		[XmlElement(ElementName = "instance_node")]
		public IONET.Collada.Core.Scene.Instance_Node[] Instance_Node;

		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "node")]
		public IONET.Collada.Core.Scene.Node[] node;		
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		

	}
}

