using IONET.Core.IOMath;
using System.Collections.Generic;
using System.Numerics;

namespace IONET.Core.Skeleton
{
    public enum BoneType
    {
        JOINT,
        NODE
    }

    /// <summary>
    /// 
    /// </summary>
    public class IOBone
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "Bone";

        public string AltID { get; set; } = "";

        public BoneType Type = BoneType.JOINT;

        /// <summary>
        /// This Node's Parent Node
        /// </summary>
        public IOBone Parent
        {
            get => _parent;
            set
            {
                if (_parent != null)
                    _parent._children.Remove(this);
                _parent = value;
                if (_parent != null)
                    _parent._children.Add(this);
            }
        }
        private IOBone _parent = null;
        
        /// <summary>
        /// Returns all children for this node
        /// </summary>
        public IOBone[] Children { get => _children.ToArray(); }
        private List<IOBone> _children = new List<IOBone>();
        
        public float TranslationX { get; set; }
        public float TranslationY { get; set; }
        public float TranslationZ { get; set; }

        public float ScaleX { get; set; } = 1;
        public float ScaleY { get; set; } = 1;
        public float ScaleZ { get; set; } = 1;

        /// <summary>
        /// Translation of bone
        /// </summary>
        public Vector3 Translation
        {
            get => new Vector3(TranslationX, TranslationY, TranslationZ);
            set
            {
                TranslationX = value.X;
                TranslationY = value.Y;
                TranslationZ = value.Z;
            }
        }

        /// <summary>
        /// Rotation of bone
        /// </summary>
        public Quaternion Rotation
        {
            get; set;
        } = new Quaternion(0, 0, 0, 1);

        /// <summary>
        /// Rotation Euler
        /// </summary>
        public Vector3 RotationEuler
        {
            get => MathExt.ToEulerAngles(Rotation);
            set => Rotation = MathExt.FromEulerAngles(value.X, value.Y, value.Z);
        }

        /// <summary>
        /// Scale of bone
        /// </summary>
        public Vector3 Scale
        {
            get => new Vector3(ScaleX, ScaleY, ScaleZ);
            set
            {
                ScaleX = value.X;
                ScaleY = value.Y;
                ScaleZ = value.Z;
            }
        }

        /// <summary>
        /// Transform of bone in local space
        /// </summary>
        public Matrix4x4 LocalTransform
        {
            get
            {
                return Matrix4x4.CreateScale(Scale) *
                    Matrix4x4.CreateFromQuaternion(Rotation) *
                    Matrix4x4.CreateTranslation(Translation);
            }
            set
            {
                Matrix4x4.Decompose(value, out Vector3 scale, out Quaternion rotation, out Vector3 translation);
                Translation = translation;
                Rotation = rotation;
                Scale = scale;
            }
        }

        /// <summary>
        /// Transform of Bone in world space
        /// </summary>
        public Matrix4x4 WorldTransform
        {
            get
            {
                return LocalTransform * (_parent == null ? Matrix4x4.Identity : _parent.WorldTransform);
            }
            set
            {
                if (_parent == null)
                    LocalTransform = value;
                else
                    if (Matrix4x4.Invert(_parent.WorldTransform, out Matrix4x4 invParent))
                    LocalTransform = invParent * value;
            }
        }

        /// <summary>
        /// Adds a new child node to this node
        /// </summary>
        public void AddChild(IOBone child)
        {
            child.Parent = this;
        }
    }
}
