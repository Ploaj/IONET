using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum Argument_Source
	{
		TEXTURE,
		CONSTANT,
		PRIMARY,
		PREVIOUS
	}
}

