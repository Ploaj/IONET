using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Diagnostics;
using IONET.Collada.Core.Animation;
using IONET.Collada.Physics.Physics_Scene;
using IONET.Collada.Physics.Physics_Material;
using IONET.Collada.Core.Scene;
using IONET.Collada.Core.Lighting;
using IONET.Collada.Core.Geometry;
using IONET.Collada.Core.Mathematics;
using IONET.Collada.Core.Controller;
using IONET.Collada.Physics.Physics_Model;
using IONET.Collada.FX.Effects;
using IONET.Collada.FX.Texturing;
using IONET.Collada.Kinematics.Articulated_Systems;
using IONET.Collada.FX.Materials;
using IONET.Collada.Kinematics.Kinematics_Scenes;
using IONET.Collada.Kinematics.Joints;
using IONET.Collada.Kinematics.Kinematics_Models;
using IONET.Collada.Core.Camera;
using IONET.Collada.Core.Metadata;
using IONET.Collada.Core.Extensibility;

namespace IONET.Collada
{
	[Serializable()]
	[DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[XmlType(AnonymousType=true)]
	[XmlRoot(ElementName="COLLADA", Namespace="http://www.collada.org/2005/11/COLLADASchema", IsNullable=false)]
	public partial class Collada
	{
		[XmlAttribute("version")]
		public string Version;
		
		[XmlElement(ElementName = "asset")]
		public Asset Asset;

		//Core Elements
		[XmlElement(ElementName = "library_animation_clips")]
		public Library_Animation_Clips Library_Animation_Clips;

		[XmlElement(ElementName = "library_animations")]
		public Library_Animations Library_Animations;

		[XmlElement(ElementName = "library_cameras")]
		public Library_Cameras Library_Cameras;


        //FX Elements
        [XmlElement(ElementName = "library_images")]
        public Library_Images Library_Images;

        [XmlElement(ElementName = "library_materials")]
        public Library_Materials Library_Materials;

        [XmlElement(ElementName = "library_effects")]
        public Library_Effects Library_Effects;


        //Core Elements Ext
        [XmlElement(ElementName = "library_formulas")]
		public Library_Formulas Library_Formulas;

		[XmlElement(ElementName = "library_geometries")]
		public Library_Geometries Library_Geometries;
        
        [XmlElement(ElementName = "library_controllers")]
        public Library_Controllers Library_Controllers;

        [XmlElement(ElementName = "library_lights")]
		public Library_Lights Library_Lights;

		[XmlElement(ElementName = "library_nodes")]
		public Library_Nodes Library_Nodes;

		[XmlElement(ElementName = "library_visual_scenes")]
		public Library_Visual_Scenes Library_Visual_Scene;
				
		//Physics Elements

		[XmlElement(ElementName = "library_force_fields")]
		public Library_Force_Fields Library_Force_Fields;
		
		[XmlElement(ElementName = "library_physics_materials")]
		public Library_Physics_Materials Library_Physics_Materials;
		
		[XmlElement(ElementName = "library_physics_models")]
		public Library_Physics_Models Library_Physics_Models;
		
		[XmlElement(ElementName = "library_physics_scenes")]
		public Library_Physics_Scenes Library_Physics_Scenes;


        //Kinematics
        [XmlElement(ElementName = "library_articulated_systems")]
		public Library_Articulated_Systems Library_Articulated_Systems;
		
		[XmlElement(ElementName = "library_joints")]
		public Library_Joints Library_Joints;
		
		[XmlElement(ElementName = "library_kinematics_models")]
		public Library_Kinematics_Models Library_Kinematics_Models;
		
		[XmlElement(ElementName = "library_kinematics_scenes")]
		public Library_Kinematics_Scene Library_Kinematics_Scene;
		
		
		
		[XmlElement(ElementName = "scene")]
		public Scene Scene;

		[XmlElement(ElementName = "extra")]
		public Extra[] Extra;

        /// <summary>
        /// Save to XML file
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveToFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;

                XmlSerializer serializer = new XmlSerializer(typeof(Collada));
                serializer.Serialize(writer, this);
            }
        }

        /// <summary>
        /// Loads Collada Data from File
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Collada LoadFromFile(string fileName)
        {
            //try
            {
                Collada collada = null;

                XmlSerializer serializer = new XmlSerializer(typeof(Collada));
                using (StreamReader stream = new StreamReader(fileName))
                    collada = (Collada)(serializer.Deserialize(stream));

                return collada;
            }
            //catch (Exception ex)
            {
                //Debug.WriteLine(ex.ToString());
                //return null;
            }
        }
    }
}

