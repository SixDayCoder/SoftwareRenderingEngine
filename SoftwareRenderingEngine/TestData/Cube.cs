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

                new Vector3(-1,  1, -1),
                new Vector3(-1, -1, -1),
                new Vector3(1, -1, -1),
                new Vector3(1, 1, -1),

                new Vector3( -1,  1, 1),
                new Vector3(-1, -1, 1),
                new Vector3(1, -1, 1),
                new Vector3(1, 1, 1)

        };

        //顶点索引,12个面
        public static int[,] indices = {

             { 0,1,2 },
             { 0,2,3 },

             { 7,6,5 },
             { 7,5,4 },

             { 0,4,5 },
             { 0,5,1 },

             { 1,5,6 },
             { 1,6,2 },

             { 2,6,7 },
             { 2,7,3 },

             { 3,7,4 },
             { 3,4,0 }
        };

    }

}
