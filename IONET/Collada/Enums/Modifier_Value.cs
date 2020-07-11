using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum Modifier_Value
	{
		CONST,
		UNIFORM,
		VARYING,
		STATIC,
		VOLATILE,
		EXTERN,
		SHARED
	}
}

