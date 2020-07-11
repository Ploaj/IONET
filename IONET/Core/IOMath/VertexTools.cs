using System;
using System.Collections.Generic;
using System.Numerics;

namespace IONET.Core.IOMath
{
    public class VertexTools
    {
        /// <summary>
        /// The default value when the generated tangent would be a zero vector.
        /// </summary>
        public static Vector3 defaultTangent = new Vector3(1, 0, 0);

        /// <summary>
        /// The default value when the generated bitangent would be a zero vector.
        /// </summary>
        public static Vector3 defaultBitangent = new Vector3(0, 1, 0);

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


        /// <summary>
        /// Uses the Gran-Schmidt method for returning a normalized copy 
        /// of <paramref name="vectorToOrthogonalize"/> that is orthogonal to <paramref name="source"/>.
        /// </summary>
        /// <param name="vectorToOrthogonalize">The vector to normalize</param>
        /// <param name="source">The vector to normalize against</param>
        /// <returns><paramref name="vectorToOrthogonalize"/> orthogonalized to <paramref name="source"/></returns>
        public static Vector3 Orthogonalize(Vector3 vectorToOrthogonalize, Vector3 source)
        {
            return Vector3.Normalize(vectorToOrthogonalize - source * Vector3.Dot(source, vectorToOrthogonalize));
        }

        /// <summary>
        /// Calculates <paramref name="tangent"/> and <paramref name="bitangent"/> 
        /// for a triangle face. 
        /// <para></para><para></para>
        /// Zero vectors are set to <see cref="defaultTangent"/> and <see cref="defaultBitangent"/>.
        /// </summary>
        /// <param name="v1">The position of the first vertex</param>
        /// <param name="v2">The position of the second vertex</param>
        /// <param name="v3">The position of the third vertex</param>
        /// <param name="uv1">The UV coordinates of the first vertex</param>
        /// <param name="uv2">The UV coordinates of the second vertex</param>
        /// <param name="uv3">The UV coordinates of the third vertex</param>
        /// <param name="tangent">The generated tangent vector</param>
        /// <param name="bitangent">The generated bitangent vector</param>
        public static void GenerateTangentBitangent(Vector3 v1, Vector3 v2, Vector3 v3,
                                                    Vector2 uv1, Vector2 uv2, Vector2 uv3,
                                                    out Vector3 tangent, out Vector3 bitangent)
        {
            Vector3 posA = v2 - v1;
            Vector3 posB = v3 - v1;

            Vector2 uvA = uv2 - uv1;
            Vector2 uvB = uv3 - uv1;

            float div = (uvA.X * uvB.Y - uvB.X * uvA.Y);

            // Fix +/- infinity from division by zero.
            float r = 1.0f;
            if (div != 0)
                r = 1.0f / div;

            tangent = CalculateTangent(posA, posB, uvA, uvB, r);
            bitangent = CalculateBitangent(posA, posB, uvA, uvB, r);

            // Set zero vectors to arbitrarily chosen orthogonal vectors.
            // This prevents unwanted black areas when rendering.
            if (tangent.Length() == 0.0f)
                tangent = defaultTangent;
            if (bitangent.Length() == 0.0f)
                bitangent = defaultBitangent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posA"></param>
        /// <param name="posB"></param>
        /// <param name="uvA"></param>
        /// <param name="uvB"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static Vector3 CalculateBitangent(Vector3 posA, Vector3 posB, Vector2 uvA, Vector2 uvB, float r)
        {
            Vector3 bitangent;
            float tX = uvA.X * posB.X - uvB.X * posA.X;
            float tY = uvA.X * posB.Y - uvB.X * posA.Y;
            float tZ = uvA.X * posB.Z - uvB.X * posA.Z;
            bitangent = new Vector3(tX, tY, tZ) * r;
            return bitangent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="posA"></param>
        /// <param name="posB"></param>
        /// <param name="uvA"></param>
        /// <param name="uvB"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static Vector3 CalculateTangent(Vector3 posA, Vector3 posB, Vector2 uvA, Vector2 uvB, float r)
        {
            Vector3 tangent;
            float sX = uvB.Y * posA.X - uvA.Y * posB.X;
            float sY = uvB.Y * posA.Y - uvA.Y * posB.Y;
            float sZ = uvB.Y * posA.Z - uvA.Y * posB.Z;
            tangent = new Vector3(sX, sY, sZ) * r;
            return tangent;
        }
    }
}
