using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum Alpha_Operator
	{
		REPLACE,
		MODULATE,
		ADD,
		ADD_SIGNED,
		INTERPOLATE,
		SUBTRACT		
	}
}

