using System;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace IONET.Collada.Types
{
	[Serializable()]
	[XmlType(AnonymousType=true)]
	public partial class SID_Float_Array_String
	{
		[XmlAttribute("sid")]
		public string sID;
        
		[XmlText()]
	    public string Value_As_String;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float[] GetValues()
        {
            if (string.IsNullOrEmpty(Value_As_String))
                return null;

            return Regex.Replace(Value_As_String.Trim(), @"\s+", " ").Split(' ').Select(e => float.Parse(e)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 ToMatrix()
        {
            var m = GetValues();

            if (m.Length < 16)
                throw new IndexOutOfRangeException("Float array is not a matrix");

            return new Matrix4x4(
                    m[0], m[4], m[8], m[12],
                    m[1], m[5], m[9], m[13],
                    m[2], m[6], m[10], m[14],
                    m[3], m[7], m[11], m[15]);
        }

        /// <summary>
        /// 
        /// </summary>
        public void FromMatrix(Matrix4x4 mat)
        {
            Value_As_String = string.Join(" ", new float[] {
            mat.M11, mat.M21, mat.M31, mat.M41,
            mat.M12, mat.M22, mat.M32, mat.M42,
            mat.M13, mat.M23, mat.M33, mat.M43,
            mat.M14, mat.M24, mat.M34, mat.M44
            });
        }
    }
}

