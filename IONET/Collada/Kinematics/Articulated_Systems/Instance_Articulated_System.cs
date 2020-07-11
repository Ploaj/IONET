using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Kinematics.Articulated_Systems
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="instance_articulated_system", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Instance_Articulated_System
	{
		[XmlAttribute("sid")]
		public string sID;

		[XmlAttribute("name")]
		public string Name;

		[XmlAttribute("url")]
		public string URL;	
		
	    [XmlElement(ElementName = "bind")]
		public IONET.Collada.Kinematics.Articulated_Systems.Bind[] Bind;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;
		
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "setparam")]
		public IONET.Collada.Core.Parameters.Set_Param[] Set_Param;		
		
	}
}

