using System.ComponentModel;

namespace IONET
{
    public class ImportSettings
    {
        /// <summary>
        /// Recalculates smooth normals for the mesh
        /// </summary>
        [Category("Vertices"), DisplayName("Smooth Normals"), Description("Recalcuates smooth normals")]
        public bool SmoothNormals { get; set; } = false;

        /// <summary>
        /// Generates Tangents and Binormals for the vertices
        /// </summary>
        [Category("Vertices"), DisplayName("Generate Tangents and Binormals"), Description("")]
        public bool GenerateTangentsAndBinormals { get; set; } = false;

        /// <summary>
        /// Flips the face winding order, when enabled the mesh is automatically triangulated
        /// </summary>
        [Category("Vertices"), DisplayName("Flip Winding Order"), Description("Flips the direction of the faces. Useful if the model looks inverted")]
        public bool FlipWindingOrder { get; set; } = false;

        /// <summary>
        /// Flips the Y UV Coordinate. Useful if textures appear upside down
        /// </summary>
        [Category("Vertices"), DisplayName("Flip UVs"), Description("Flips UV coordinates on the Y Axis. Useful if textures appear upside down.")]
        public bool FlipUVs { get; set; } = false;

        /// <summary>
        /// Enables the weight limit <see cref="WeightLimitAmt"/>
        /// </summary>
        [Category("Vertices"), DisplayName("Weight Limit"), Description("Sets a limit on the amount of weights a vertex can have")]
        public bool WeightLimit { get; set; } = false;

        /// <summary>
        /// The max number of influences for each vertex
        /// </summary>
        [Category("Vertices"), DisplayName("Weight Limit Affect Count"), Description("The max number of weights each vertex can have")]
        public int WeightLimitAmt { get; set; } = 4;

        /// <summary>
        /// Triangulates all the polygons
        /// </summary>
        [Category("Vertices"), DisplayName("Triangulate"), Description("Converts all polygons to triangle lists with the exception of points and line")]
        public bool Triangulate { get; set; } = false;

        /// <summary>
        /// Optimizes number of vertices, very recommended
        /// </summary>
        [Category("Vertices"), DisplayName("Optimize"), Description("Reduces number of vertices by combining duplicates")]
        public bool Optimize { get; set; } = true;
    }

    public class ExportSettings
    {
        /// <summary>
        /// Recalculates smooth normals for the mesh
        /// </summary>
        [Category("Vertices"), DisplayName("Smooth Normals"), Description("Recalcuates smooth normals")]
        public bool SmoothNormals { get; set; } = false;

        /// <summary>
        /// Flips the face winding order, when enabled the mesh is automatically triangulated
        /// </summary>
        [Category("Vertices"), DisplayName("Flip Winding Order"), Description("Flips the direction of the faces. Useful if the model looks inverted")]
        public bool FlipWindingOrder { get; set; } = false;

        /// <summary>
        /// Flips the Y UV Coordinate. Useful if textures appear upside down
        /// </summary>
        [Category("Vertices"), DisplayName("Flip UVs"), Description("Flips UV coordinates on the Y Axis. Useful if textures appear upside down.")]
        public bool FlipUVs { get; set; } = false;

        /// <summary>
        /// Optimizes number of vertices, very recommended
        /// </summary>
        [Category("Vertices"), DisplayName("Optimize"), Description("Reduces number of vertices by combining duplicates")]
        public bool Optimize { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Category("Materials"), DisplayName("Export Texture Info"), Description("Blender tends not like dae's with texture info")]
        public bool ExportTextureInfo { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Category("Materials"), DisplayName("Export Material Info"), Description("Exports material info if format supports it")]
        public bool ExportMaterialInfo { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        [Category("Misc"), DisplayName("Blender Mode"), Description("Helps with blender compatibility (DAE ONLY)")]
        public bool BlenderMode { get; set; } = true;

    }
}
