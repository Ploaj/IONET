using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Metadata
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Asset_Coverage
	{
	    [XmlElement(ElementName = "geographic_location")]
		IONET.Collada.Core.Metadata.Geographic_Location Geographic_Location;
	}
}

