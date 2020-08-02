using System;
using System.Numerics;

namespace IONET.Core.Model
{
    /// <summary>
    /// Phong Material Model
    /// </summary>
    public class IOMaterial
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        public string Label { get; set; }

        public Vector4 AmbientColor { get; set; } = new Vector4(0.2f, 0.2f, 0.2f, 1);
        public IOTexture AmbientMap { get; set; }

        public Vector4 DiffuseColor { get; set; } = Vector4.One;
        public IOTexture DiffuseMap { get; set; }

        public Vector4 SpecularColor { get; set; } = new Vector4(0.2f, 0.2f, 0.2f, 1);
        public IOTexture SpecularMap { get; set; }

        public Vector4 EmissionColor { get; set; } = new Vector4(0, 0, 0, 1);
        public IOTexture EmissionMap { get; set; }

        public Vector4 ReflectiveColor { get; set; } = new Vector4(1, 1, 1, 1);
        public IOTexture ReflectiveMap { get; set; }

        public float Reflectivity { get; set; } = 1;
        public float Shininess { get; set; } = 20;
        public float Alpha { get; set; } = 1;
    }
}
