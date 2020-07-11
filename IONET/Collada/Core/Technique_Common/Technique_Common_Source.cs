using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Source : IONET.Collada.Core.Extensibility.Technique_Common
	{
	
		
		
		[XmlElement(ElementName = "accessor")]
		public IONET.Collada.Core.Data_Flow.Accessor Accessor;	
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;	
	}
}

