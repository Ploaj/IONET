using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum FX_Sampler_Common_Filter_Type
	{
		NONE,
		NEAREST,
		LINEAR,
		ANISOTROPIC
		
	}
}

