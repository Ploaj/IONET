using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Effects
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="effect", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=true)]
	public partial class Effect
	{
		[XmlAttribute("id")]
		public string ID;
		
		[XmlAttribute("name")]
		public string Name;		
		
		[XmlElement(ElementName = "asset")]
		public IONET.Collada.Core.Metadata.Asset Asset;
		
	    [XmlElement(ElementName = "extra")]
		public IONET.Collada.Core.Extensibility.Extra[] Extra;	
		
		[XmlElement(ElementName = "annotate")]
		public IONET.Collada.FX.Effects.Annotate[] Annotate;
		
	    [XmlElement(ElementName = "newparam")]
		public IONET.Collada.Core.Parameters.New_Param[] New_Param;

		[XmlElement(ElementName = "profile_BRIDGE")]
		public IONET.Collada.FX.Profiles.BRIDGE.Profile_BRIDGE[] Profile_BRIDGE;
		
		[XmlElement(ElementName = "profile_CG")]
		public IONET.Collada.FX.Profiles.CG.Profile_CG[] Profile_CG;
		
		[XmlElement(ElementName = "profile_GLES")]
		public IONET.Collada.FX.Profiles.GLES.Profile_GLES[] Profile_GLES;
		
		[XmlElement(ElementName = "profile_GLES2")]
		public IONET.Collada.FX.Profiles.GLES2.Profile_GLES2[] Profile_GLES2;
		
		[XmlElement(ElementName = "profile_GLSL")]
		public IONET.Collada.FX.Profiles.GLSL.Profile_GLSL[] Profile_GLSL;
		
		[XmlElement(ElementName = "profile_COMMON")]
		public IONET.Collada.FX.Profiles.COMMON.Profile_COMMON[] Profile_COMMON;
				
	}
}

