using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Geographic_Location_Altitude
	{

		[XmlTextAttribute()]
		public float Altitude;
		
		[XmlAttribute("mode")]
		[System.ComponentModel.DefaultValueAttribute(IONET.Collada.Enums.Geographic_Location_Altitude_Mode.relativeToGround)]
		public IONET.Collada.Enums.Geographic_Location_Altitude_Mode Mode;	
		
	}
}

