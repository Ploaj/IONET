using System;
using System.Numerics;

namespace IONET.Core.IOMath
{
    public class MathExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static float DegToRad(float deg)
        {
            return deg * (float)Math.PI / 180;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rad"></param>
        /// <returns></returns>
        public static float RadToDeg(float rad)
        {
            return rad * 180 / (float)Math.PI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="z"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Quaternion FromEulerAngles(float x, float y, float z)
        {
            Quaternion xRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitX, x);
            Quaternion yRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, y);
            Quaternion zRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, z);

            Quaternion q = (zRotation * yRotation * xRotation);

            return q;
        }

        /// <summary>
        /// Converts quaternion into euler angles in ZYX order
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public static Vector3 ToEulerAngles(Quaternion q)
        {
            q = Quaternion.Inverse(q);

            Matrix4x4 mat = Matrix4x4.CreateFromQuaternion(q);
            float x, y, z;

            y = (float)Math.Asin(-Clamp(mat.M31, -1, 1));

            if (Math.Abs(mat.M31) < 0.99999)
            {
                x = (float)Math.Atan2(mat.M32, mat.M33);
                z = (float)Math.Atan2(mat.M21, mat.M11);
            }
            else
            {
                x = 0;
                z = (float)Math.Atan2(-mat.M12, mat.M22);
            }
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Clamps value between a minimum and maximum value
        /// </summary>
        /// <param name="v"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(float v, float min, float max)
        {
            if (v < min) return min;
            if (v > max) return max;
            return v;
        }

    }
}
