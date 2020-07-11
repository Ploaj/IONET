using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace IONET.Core
{
    /// <summary>
    /// Bind information for bone to vertex
    /// </summary>
    public class IOEnvelope
    {
        /// <summary>
        /// Weights for this vertex
        /// </summary>
        public List<IOBoneWeight> Weights { get; set; } = new List<IOBoneWeight>();

        /// <summary>
        /// Indicates if the BindMatrix in <see cref="IOBoneWeight"/> is meant to be used
        /// </summary>
        public bool UseBindMatrix { get; set; } = false;

        /// <summary>
        /// Optimizes number of weights by removing weights with lesser influence
        /// </summary>
        public void Optimize(int maxWeights)
        {
            var optimizedWeights = Optimize(Weights, maxWeights);

            Weights.Clear();

            Weights.AddRange(optimizedWeights);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weights"></param>
        /// <param name="weightCount"></param>
        /// <returns></returns>
        private static IOBoneWeight[] Optimize(List<IOBoneWeight> weights, int maxWeights)
        {
            IOBoneWeight[] optimized = new IOBoneWeight[maxWeights];

            if (weights.Count > maxWeights)
            {
                int[] toRemove = new int[weights.Count - maxWeights];

                for (int i = 0; i < toRemove.Length; ++i)
                    for (int j = 0; j < weights.Count; ++j)
                        if (!toRemove.Contains(j + 1) &&
                            (toRemove[i] == 0 || weights[j].Weight < weights[toRemove[i] - 1].Weight))
                            toRemove[i] = j + 1;

                foreach (int k in toRemove)
                    weights.RemoveAt(k - 1);
            }

            for (int i = 0; i < weights.Count; ++i)
                optimized[i] = weights[i];

            Normalize(optimized);

            return optimized;
        }

        /// <summary>
        /// Makes sure all weights add up to 1.0f.
        /// Does not modify any locked weights.
        /// </summary>
        private static void Normalize(IEnumerable<IOBoneWeight> weights, int weightDecimalPlaces = 7)
        {
            float denom = 0.0f;
            foreach (IOBoneWeight b in weights)
                if (b != null)
                    denom += b.Weight;

            if (denom > 0.0f)
                foreach (IOBoneWeight b in weights)
                    if (b != null)
                        b.Weight = (float)Math.Round(b.Weight / denom, weightDecimalPlaces);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class IOBoneWeight
    {
        /// <summary>
        /// Name of Bone to be influenced by
        /// </summary>
        public string BoneName;

        /// <summary>
        /// 
        /// </summary>
        public Matrix4x4 BindMatrix = Matrix4x4.Identity;

        /// <summary>
        /// Amount of Weight to this bone
        /// </summary>
        public float Weight;

        public override string ToString()
        {
            return $"{BoneName} {Weight}";
        }
    }
}
