using System;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Physics.Technique_Common
{

	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class Technique_Common_Physics_Scene : IONET.Collada.Core.Extensibility.Technique_Common
	{
		
		[XmlElement(ElementName = "gravity")]
		public IONET.Collada.Types.SID_Float_Array_String Gravity;	

		[XmlElement(ElementName = "time_step")]
		public IONET.Collada.Types.SID_Float Time_Step;		
	}
}

