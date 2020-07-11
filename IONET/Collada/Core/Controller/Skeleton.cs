using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Controller
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Skeleton
	{
	    [XmlTextAttribute()]
	    public string Value;
	}
}

