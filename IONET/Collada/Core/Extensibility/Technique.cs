using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Extensibility
{
	
	/// <summary>
	/// This is the core <technique>
	/// </summary>
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique
	{
		[XmlAttribute("profile")]
		public string profile;
		[XmlAttribute("xmlns")]
		public string xmlns;

		[XmlAnyElement]
		public XmlElement[] Data;	

	}
}

