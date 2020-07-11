using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Formula : IONET.Collada.Core.Extensibility.Technique_Common
	{
		/// <summary>
		/// Need to determine the type and value of the Object(s)
		/// </summary>
		[XmlAnyElement]
		public XmlElement[] Data;	
		
	}
}

