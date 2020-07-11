using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Array_Length
	{
		[XmlAttribute("length")]
		public int Length;	
				
	}
}

