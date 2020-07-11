using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum FX_Opaque_Channel
	{
		A_ONE,
		RGB_ZERO,
		A_ZERO,
		RGB_ONE
	}
}

