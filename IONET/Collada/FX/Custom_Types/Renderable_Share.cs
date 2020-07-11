using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.FX.Custom_Types
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Renderable_Share
	{

		[XmlAttribute("share")]
		public bool Share;	
	}
}

