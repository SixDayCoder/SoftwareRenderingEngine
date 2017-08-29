using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareRenderingEngine.Math3D {

    public class Point2 {

        public float x;
        public float y;

        public Point2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public Point2(Vector3 vec) {

            x = vec.x;
            y = vec.y;
        }


    }
}
