using System;
namespace IONET.Collada.Enums
{
	[Serializable()]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.collada.org/2005/11/COLLADASchema" )]
	public enum Format_Hint_Precision
	{
		DEFAULT,
		LOW,
		MID,
		HIGH,
		MAX		
	}
}

