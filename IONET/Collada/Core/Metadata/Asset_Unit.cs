using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Metadata
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Asset_Unit
	{
		[XmlAttribute("meter")]
	    [System.ComponentModel.DefaultValueAttribute(1.0)]
		public double Meter;

		[XmlAttribute("name")]
	    [System.ComponentModel.DefaultValueAttribute("meter")]
		public string Name;

		
	}
}

