using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Size_2D
	{

		[XmlAttribute("width")]
		public int Width;	

		[XmlAttribute("height")]
		public int Height;	
	}
}

