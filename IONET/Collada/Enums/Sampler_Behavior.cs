using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum Sampler_Behavior
	{

		UNDEFINED,
		CONSTANT,
		GRADIENT, 
		CYCLE,
		OSCILLATE, 
		CYCLE_RELATIVE
	}
}

