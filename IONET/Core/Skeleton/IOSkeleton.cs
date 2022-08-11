using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace IONET.Core.Skeleton
{
    /// <summary>
    /// 
    /// </summary>
    public class IOSkeleton
    {
        public List<IOBone> RootBones = new List<IOBone>();

        /// <summary>
        /// Gets a list of the bones in breath first order
        /// </summary>
        /// <returns></returns>
        public List<IOBone> BreathFirstOrder()
        {
            List<IOBone> bones = new List<IOBone>();
            
            foreach (var c in RootBones)
                BreathFirstWalk(c, ref bones);

            return bones;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bone"></param>
        /// <param name="bones"></param>
        private void BreathFirstWalk(IOBone bone, ref List<IOBone> bones)
        {
            bones.Add(bone);

            foreach (var c in bone.Children)
                BreathFirstWalk(c, ref bones);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IOBone GetBoneByName(string name)
        {
            return BreathFirstOrder().Find(e=>e.Name == name || e.AltID == name);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IOBone GetBoneByIndex(int index)
        {
            var bones = BreathFirstOrder();

            if (index < 0 || index > bones.Count)
                return null;

            return bones[index];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bone"></param>
        /// <returns></returns>
        public int IndexOf(IOBone bone)
        {
            return BreathFirstOrder().IndexOf(bone);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transform"></param>
        public void ApplyTransform(Matrix4x4 transform)
        {
            var bones = BreathFirstOrder();

            for(int i = 0; i < bones.Count; i++)
            {
                bones[i].Translation = Vector3.Transform(bones[i].Translation, transform);
                bones[i].RotationEuler = Vector3.TransformNormal(bones[i].RotationEuler, transform);
                //bones[i].Scale = Vector3.TransformNormal(bones[i].Scale, transform);
            }
        }
    }
}
