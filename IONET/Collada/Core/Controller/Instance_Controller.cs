using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Instance_Controller
	{
		[XmlAttribute("sid")]
		public string sID;
		
		[XmlAttribute("name")]
		public string Name;	
		
		[XmlAttribute("url")]
		public string URL;

        [XmlElement(ElementName = "skeleton")]
        public IONET.Collada.Core.Controller.Skeleton[] Skeleton;

        [XmlElement(ElementName = "bind_material")]
		public IONET.Collada.FX.Materials.Bind_Material[] Bind_Material;				
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
	}
}

