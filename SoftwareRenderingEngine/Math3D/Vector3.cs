using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.Utility;


namespace SoftwareRenderingEngine.Math3D {

    class Vector3 {

        public float x;
        public float y;
        public float z;
        public float w;


        public Vector3(float x = 0, float y = 0, float z = 0, float w = 0) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0;
        }

        public float Length {
            get {
                return (float) Math.Sqrt(SqrLength);
            }
        }
        
        public float SqrLength {
            get {
                return x * x + y * y + z * z;
            }
        }

        public Vector3 Normalize() {

            float length = Length;

            if (length != 0) {
                float s = 1 / length;
                x *= s;
                y *= s;
                z *= s;
            }
            return this;
        }

        #region 重载运算符
        public static Vector3 operator * (Vector3 vec, float factor) {
            Vector3 v = new Vector3();
            v.x = vec.x * factor;
            v.y = vec.y * factor;
            v.z = vec.z * factor;
            v.w = vec.w;
            return v;
        }

        public static Vector3 operator * (float factor, Vector3 vec) {
            return vec * factor;
        }

        public static Vector3 operator * (Vector3 lhs, Matrix4X4 rhs) {
            Vector3 v = new Vector3();
            v.x = lhs.x * rhs[0, 0] + lhs.y * rhs[1, 0] + lhs.z * rhs[2, 0] + lhs.w * rhs[3, 0];
            v.y = lhs.x * rhs[0, 1] + lhs.y * rhs[1, 1] + lhs.z * rhs[2, 1] + lhs.w * rhs[3, 1];
            v.z = lhs.x * rhs[0, 2] + lhs.y * rhs[1, 2] + lhs.z * rhs[2, 2] + lhs.w * rhs[3, 2];
            v.w = lhs.x * rhs[0, 3] + lhs.y * rhs[1, 3] + lhs.z * rhs[2, 3] + lhs.w * rhs[3, 3];
            return v;
        }

        public static Vector3 operator - (Vector3 lhs, Vector3 rhs) {
            Vector3 v = new Vector3();
            v.x = lhs.x - rhs.x;
            v.y = lhs.y - rhs.y;
            v.z = lhs.z - rhs.z;
            v.w = 0;
            return v;
        }

        public static Vector3 operator + (Vector3 lhs, Vector3 rhs) {
            Vector3 v = new Vector3();
            v.x = lhs.x + rhs.x;
            v.y = lhs.y + rhs.y;
            v.z = lhs.z + rhs.z;
            v.w = 0;
            return v;
        }
        #endregion

        #region static方法
       /// <summary>
       /// 求lhs在rhs上的投影向量
       /// </summary>
       /// <param name="lhs"></param>
       /// <param name="rhs"></param>
       /// <returns></returns>
        public static Vector3 Project(Vector3 lhs, Vector3 rhs) {
            float factor = Dot(lhs, rhs) / rhs.Length;
            return lhs.Normalize() * factor;
        }

        /// <summary>
        /// 求lhs和rhs之前按比例factor的插值
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 lhs, Vector3 rhs, float factor) {

            Vector3 vec = new Vector3();

            vec.x = MathUtility.Lerp(lhs.x, rhs.x, factor);
            vec.y = MathUtility.Lerp(lhs.y, rhs.y, factor);
            vec.z = MathUtility.Lerp(lhs.z, rhs.z, factor);
            vec.w = 0;

            return vec;
        }

        /// <summary>
        /// 将vec的长度限制到<maxlength
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Vector3 ClampMagnitude(Vector3 vec, float maxLength) {

            if (vec.Length <= maxLength)
                return vec;
            else {
                return vec.Normalize() * maxLength;
            }
        }

        public static Vector3 Zero(){
            return new Vector3(0, 0, 0, 0);
        }

        public static float Dot(Vector3 lhs, Vector3 rhs) {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs) {
            float x = lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.z * rhs.x - lhs.x * rhs.z;
            float z = lhs.x * rhs.y - lhs.y * rhs.x;
            return new Vector3(x, y, z, 0);
        }

        #endregion

    }
}
