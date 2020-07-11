using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Custom_Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="mass_frame", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Mass_Frame
	{

		[XmlElement(ElementName = "rotate")]
		public IONET.Collada.Core.Transform.Rotate[] Rotate;
		

		[XmlElement(ElementName = "translate")]
		public IONET.Collada.Core.Transform.Translate[] Translate;		
	}
}

