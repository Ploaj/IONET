using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Mips_Attribute
	{
		
		[XmlAttribute("levels")]
		public int Levels;
		
		[XmlAttribute("auto_generate")]
		public bool Auto_Generate;	
	}
}

