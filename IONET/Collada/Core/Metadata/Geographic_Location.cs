using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Metadata
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Geographic_Location
	{


	    [XmlElement(ElementName = "longitude")]
		public float Longitude;
	    
		[XmlElement(ElementName = "latitude")]
		public float Latitude;
		
	    [XmlElement(ElementName = "altitude")]
		public IONET.Collada.Core.Custom_Types.Geographic_Location_Altitude Altitude;		
		
	}
}

