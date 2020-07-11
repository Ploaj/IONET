using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Skin
	{
		[XmlAttribute("sid")]
		public string sID;

        [XmlAttribute("source")]
        public string sourceid;

        [XmlElement(ElementName = "bind_shape_matrix")]
		public IONET.Collada.Types.Float_Array_String Bind_Shape_Matrix;		
				
	    [XmlElement(ElementName = "source")]
		public IONET.Collada.Core.Data_Flow.Source[] Source;		

	    [XmlElement(ElementName = "joints")]
		public IONET.Collada.Core.Controller.Joints Joints;		

	    [XmlElement(ElementName = "vertex_weights")]
		public IONET.Collada.Core.Controller.Vertex_Weights Vertex_Weights;		
				
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;		
	}
}

