using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



    }
}
