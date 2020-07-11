using System;
using System.Collections.Generic;
using System.Numerics;

namespace IONET.Core.IOMath
{
    public class VertexTools
    {
        /// <summary>
        /// Calculates normalized, smooth normals for the given vertex positions.
        /// </summary>
        /// <param name="positions">The vertex positions</param>
        /// <param name="normals">The vertex normals</param>
        /// <param name="indices">The indices used to define the triangle faces</param>
        public static void CalculateSmoothNormals(IList<Vector3> positions, IList<int> indices, out Vector3[] normals)
        {
            normals = new Vector3[positions.Count];
            
            for (int i = 0; i < indices.Count; i += 3)
            {
                var normal = CalculateNormal(positions[indices[i]], positions[indices[i + 1]], positions[indices[i + 2]]);

                normals[indices[i]] += normal;
                normals[indices[i + 1]] += normal;
                normals[indices[i + 2]] += normal;
            }
            
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = Vector3.Normalize(normals[i]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns></returns>
        public static Vector3 CalculateNormal(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            Vector3 U = v2 - v1;
            Vector3 V = v3 - v1;
            
            return Vector3.Cross(U, V);
        }
    }
}
