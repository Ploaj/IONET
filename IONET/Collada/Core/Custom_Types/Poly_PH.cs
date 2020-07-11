using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Poly_PH
	{

	    [XmlElement(ElementName = "p")]
		public IONET.Collada.Types.Int_Array_String P;

		[XmlElement(ElementName = "h")]
		public IONET.Collada.Types.Int_Array_String[] H;		


	}
}

