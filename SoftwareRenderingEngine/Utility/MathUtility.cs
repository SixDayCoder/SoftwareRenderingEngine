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

    }
}
