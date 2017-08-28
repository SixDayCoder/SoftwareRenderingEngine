using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.Math3D;


namespace SoftwareRenderingEngine.RenderStruct {

    class Mesh {

        //顶点数组
        public Vertex[] vertices;

        //顶点索引数组
        public int[] indices;

        //三角面法线,由顶点法线叉积计算得来
        public Vector3[] normals;
    }

}
