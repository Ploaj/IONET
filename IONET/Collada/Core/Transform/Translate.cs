using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Transform
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Translate : IONET.Collada.Types.SID_Float_Array_String
	{
	}
}

