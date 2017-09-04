using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SoftwareRenderingEngine.Math3D;

namespace SoftwareRenderingEngine.Utility {

    public static class MathUtility {

        public static float Lerp(float min, float max, float factor){
            if (factor <= 0)
                return min;
            else if (factor >= 1)
                return max;
            else {
                return min + (max - min) * factor;
            }
        }

        public static Color Lerp(Color min, Color max, float factor) {

            if (factor <= 0)
                return min;
            else if (factor >= 1)
                return max;
            else {

                float r = Lerp(min.R, max.R, factor);
                float g = Lerp(min.G, max.G, factor);
                float b = Lerp(min.B, max.B, factor);

                return Color.FromArgb((int)r, (int)g, (int)b);

            }
        }

        public static float Min(float lhs, float rhs) {
            return lhs < rhs ? lhs : rhs;
        }

        public static int Min(int lhs, int rhs) {
            return lhs < rhs ? lhs : rhs;
        }

        /// <summary>
        /// 限制value的值为[min, max]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Range(float value, float min, float max) {

            if (value <= min)
                return min;
            else if (value >= max)
                return max;
            return value;

        }

        public static void Swap<T>(ref T lhs, ref T rhs) {

            T tmp = lhs;
            lhs = rhs;
            rhs = tmp;

        }

        //该函数保证Lhs<rhs
        public static void LhsLowerThanRhs(ref int lhs, ref int rhs) {
            if (lhs > rhs)
                Swap(ref lhs, ref rhs);
        }

        public static void LhsLowerThanRhs(ref float lhs, ref float rhs) {
            if (lhs > rhs)
                Swap(ref lhs, ref rhs);
        }

        //float四舍五入到int
        public static int RoundToInt(float f) {

            int x = (int)f;
            float frac = f - x;

            if (frac > 0.5f)
                return x + 1;

            return x;

        }

        /// <summary>
        /// 根据入射向量和法线计算全反射向量
        /// </summary>
        /// <param name="inVector">入射向量</param>
        /// <param name="normal">归一化的入射点法向量</param>
        /// <returns></returns>
        public static Vector3 ReflectVector(Vector3 inVector, Vector3 normal) {

            Vector3 projection = Vector3.Dot(inVector, normal) * normal;
            Vector3 refelct = inVector - 2.0f * projection;

            return refelct;
        }

    }
}
