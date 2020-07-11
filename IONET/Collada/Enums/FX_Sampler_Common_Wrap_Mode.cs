using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum FX_Sampler_Common_Wrap_Mode
	{
		WRAP,
		MIRROR,
		CLAMP,
		BORDER,
		MIRROR_ONCE,
		
		REPEAT,
		CLAMP_TO_EDGE,
		MIRRORED_REPEAT
	}
}

