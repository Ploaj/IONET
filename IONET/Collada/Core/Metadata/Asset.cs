using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Core.Metadata
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Asset
	{
		    [XmlElement(ElementName = "created")]
		    public System.DateTime Created;
		    
		    [XmlElement(ElementName = "modified")]
		    public System.DateTime Modified;
		
		    [XmlElement(ElementName = "unit")]
			public IONET.Collada.Core.Metadata.Asset_Unit Unit;
		
		    [XmlElement(ElementName = "up_axis")]
		    [System.ComponentModel.DefaultValueAttribute("Y_UP")]
			public string Up_Axis;
		
		    [XmlElement(ElementName = "contributor")]
			public IONET.Collada.Core.Metadata.Asset_Contributor[] Contributor;

		    [XmlElement(ElementName = "keywords")]
			public string Keywords;
		
		    [XmlElement(ElementName = "revision")]
			public string Revision;

			[XmlElement(ElementName = "subject")]
			public string Subject;
		    
			[XmlElement(ElementName = "title")]
			public string Title;	
		
		    [XmlElement(ElementName = "extra")]
			public IONET.Collada.Core.Extensibility.Extra[] Extra;
		
		
		    [XmlElement(ElementName = "coverage")]
			public IONET.Collada.Core.Metadata.Asset_Coverage Coverage;
			
		
	}
}

