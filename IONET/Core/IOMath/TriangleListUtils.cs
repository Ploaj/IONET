using System;
using System.Collections.Generic;
using System.Numerics;

namespace IONET.Core.IOMath
{
    public class TriangleListUtils
    {/// <summary>
     /// Calculates normalized, smooth tangents and bitangents for the given vertex data. Bitangents are adjusted to account for mirrored UVs.
     /// </summary>
     /// <param name="positions">The vertex positions</param>
     /// <param name="normals">The vertex normals</param>
     /// <param name="uvs">The vertex texture coordinates</param>
     /// <param name="indices">The indices used to define the triangle faces</param>
     /// <param name="tangents">The newly generated tangents</param>
     /// <param name="bitangents">The newly generated bitangents</param>
        public static void CalculateTangentsBitangents(IList<Vector3> positions, IList<Vector3> normals, IList<Vector2> uvs, IList<int> indices, out Vector3[] tangents, out Vector3[] bitangents)
        {
            if (normals.Count != positions.Count)
                throw new System.ArgumentOutOfRangeException(nameof(normals), "Vector source lengths do not match.");

            if (uvs.Count != positions.Count)
                throw new System.ArgumentOutOfRangeException(nameof(uvs), "Vector source lengths do not match.");

            tangents = new Vector3[positions.Count];
            bitangents = new Vector3[positions.Count];

            // Calculate the vectors.
            for (int i = 0; i < indices.Count; i += 3)
            {
                VertexTools.GenerateTangentBitangent(positions[indices[i]], positions[indices[i + 1]], positions[indices[i + 2]],
                    uvs[indices[i]], uvs[indices[i + 1]], uvs[indices[i + 2]], out Vector3 tangent, out Vector3 bitangent);

                tangents[indices[i]] += tangent;
                tangents[indices[i + 1]] += tangent;
                tangents[indices[i + 2]] += tangent;

                bitangents[indices[i]] += bitangent;
                bitangents[indices[i + 1]] += bitangent;
                bitangents[indices[i + 2]] += bitangent;
            }

            // Even if the vectors are not zero, they may still sum to zero.
            for (int i = 0; i < tangents.Length; i++)
            {
                if (tangents[i].Length() == 0.0f)
                    tangents[i] = VertexTools.defaultTangent;

                if (bitangents[i].Length() == 0.0f)
                    bitangents[i] = VertexTools.defaultBitangent;
            }

            // Account for mirrored normal maps.
            for (int i = 0; i < bitangents.Length; i++)
            {
                // The default bitangent may be parallel to the normal vector.
                if (Vector3.Cross(bitangents[i], normals[i]).Length() != 0.0f)
                    bitangents[i] = VertexTools.Orthogonalize(bitangents[i], normals[i]);
                bitangents[i] *= -1;
            }

            for (int i = 0; i < tangents.Length; i++)
            {
                tangents[i] = Vector3.Normalize(tangents[i]);
                bitangents[i] = Vector3.Normalize(bitangents[i]);
            }
        }
    }
}
