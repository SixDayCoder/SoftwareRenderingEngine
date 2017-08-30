using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareRenderingEngine.Math3D;
using SoftwareRenderingEngine.RenderStruct;

namespace SoftwareRenderingEngine.TestData {

    /// <summary>
    /// 正方体测试数据
    /// </summary>
    public class Cube {

        //顶点坐标
        public static Vector3[] positions = {

            //0~3
            new Vector3(-1,  1,  1),
            new Vector3( 1,  1,  1),
            new Vector3( 1, -1,  1),
            new Vector3(-1, -1,  1),

            //4~7
            new Vector3(-1,  1, -1),
            new Vector3( 1,  1, -1),
            new Vector3( 1, -1, -1),
            new Vector3(-1, -1, -1)

        };

        //顶点索引,12个面
        public static int[,] indices = {
            //后面
            {0, 1, 2},
            {0, 2, 3},

            //前面
            {4, 5, 6},
            {4, 6, 7},

            //上面
            {0, 1, 5},
            {0, 5, 4},

            //下面
            {3, 2, 6},
            {3, 6, 7},

            //左面
            {0, 4, 7},
            {0, 7, 3},

            //右面
            {5, 1, 2},
            {5, 2, 6}
        };

    }

}
