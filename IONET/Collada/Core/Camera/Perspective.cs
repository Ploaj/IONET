using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Camera
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Perspective
	{
		[XmlElement(ElementName = "xfov")]
		public IONET.Collada.Types.SID_Float XFov;				
				
		[XmlElement(ElementName = "yfov")]
		public IONET.Collada.Types.SID_Float YFov;				

		[XmlElement(ElementName = "aspect_ratio")]
		public IONET.Collada.Types.SID_Float Aspect_Ratio;				

		[XmlElement(ElementName = "znear")]
		public IONET.Collada.Types.SID_Float ZNear;				

		[XmlElement(ElementName = "zfar")]
		public IONET.Collada.Types.SID_Float ZFar;	
	}
}

