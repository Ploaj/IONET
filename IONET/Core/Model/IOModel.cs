using IONET.Core.IOMath;
using IONET.Core.Skeleton;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace IONET.Core.Model
{
    public class IOModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "Model";

        /// <summary>
        /// 
        /// </summary>
        public List<IOMesh> Meshes { get; internal set; } = new List<IOMesh>();

        /// <summary>
        /// 
        /// </summary>
        public IOSkeleton Skeleton { get; set; } = new IOSkeleton();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        public void Transform(Matrix4x4 transform)
        {
            Skeleton.ApplyTransform(transform);

            foreach (var mesh in Meshes)
                foreach (var v in mesh.Vertices)
                {
                    // todo apply bind matrix before disabling
                    v.Envelope.UseBindMatrix = false;
                }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SmoothNormals()
        {
            List<Vector3> allpositions = new List<Vector3>();
            Dictionary<Vector3, int> vectorToIndex = new Dictionary<Vector3, int>();
            List<int> indices = new List<int>();

            foreach (var m in Meshes)
            {
                Dictionary<int, int> indexToIndex = new Dictionary<int, int>();

                int vi = 0;
                foreach (var v in m.Vertices)
                {
                    if (!vectorToIndex.ContainsKey(v.Position))
                    {
                        vectorToIndex.Add(v.Position, allpositions.Count);
                        allpositions.Add(v.Position);
                    }
                    indexToIndex.Add(vi++, vectorToIndex[v.Position]);
                }

                foreach (var poly in m.Polygons)
                    indices.AddRange(poly.Indicies.Select(e => indexToIndex[e]));
            }

            VertexTools.CalculateSmoothNormals(allpositions, indices, out Vector3[] smooth);

            Dictionary<Vector3, Vector3> posToNormal = new Dictionary<Vector3, Vector3>();

            for (int i = 0; i < allpositions.Count; i++)
                posToNormal.Add(allpositions[i], smooth[i]);

            
            foreach (var m in Meshes)
                foreach (var v in m.Vertices)
                    v.Normal = posToNormal[v.Position];
        }
    }
}
