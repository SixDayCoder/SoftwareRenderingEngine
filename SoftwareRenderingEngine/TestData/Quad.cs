using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareRenderingEngine.Math3D;

namespace SoftwareRenderingEngine.TestData {

    public class Quad {

        //顶点坐标
        public static Vector3[] positions = {

            new Vector3(-1,  1,  1),
            new Vector3( 1,  1,  1),
            new Vector3( 1, -1, -1),
            new Vector3(-1, -1, -1),

        };

        public static int[,] indices = {
            {0, 2, 3},
            {0, 1, 2}
        };

    }
}
