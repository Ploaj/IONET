namespace IONET
{
    public class ImportSettings
    {
        /// <summary>
        /// Recalculates smooth normals for the vertices
        /// </summary>
        public bool SmoothNormals { get; set; } = false;

        // TODO: generate tangents and binormals
        /// <summary>
        /// Generates Tangents and Binormals for the vertices
        /// </summary>
        //public bool GenerateTangentsAndBinormals { get; set; } = false;
        
        /// <summary>
        /// Flips the face winding order, when enabled the mesh is automatically triangulated
        /// </summary>
        public bool FlipWindingOrder { get; set; } = false;
        
        /// <summary>
        /// Flips the Y UV Coordinate. Useful if textures appear upside down
        /// </summary>
        public bool FlipUVs { get; set; } = false;

        /// <summary>
        /// Enables the weight limit <see cref="WeightLimitAmt"/>
        /// </summary>
        public bool WeightLimit { get; set; } = false;

        /// <summary>
        /// The max number of influences for each vertex
        /// </summary>
        public int WeightLimitAmt { get; set; } = 4;

        /// <summary>
        /// Triangulates all the polygons
        /// </summary>
        public bool Triangulate { get; set; } = false;

        /// <summary>
        /// Optimizes number of vertices, very recommended
        /// </summary>
        public bool Optimize { get; set; } = true;
    }

    public class ExportSettings
    {
    }
}
