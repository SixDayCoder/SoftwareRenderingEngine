using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SoftwareRenderingEngine.Math3D;
using SoftwareRenderingEngine.Utility;

namespace SoftwareRenderingEngine.RenderStruct {

    public class Vertex {

        //顶点坐标
        public Vector3 position;

        //顶点法线
        public Vector3 normal;

        //顶点纹理坐标
        public float u, v;

        //顶点颜色
        public Color4 color;

        //rhw ->  reciprocal homogeneous w   reciprocal->倒数 homogeneous -> 齐次
        public float rhw;

        public Vertex() {
            position = new Vector3();
            normal = new Vector3();
            u = v = 0;
            rhw = 1f;
            color = new Color4();
        }

        #region static方法

        public static Vertex Lerp(Vertex min, Vertex max, float factor) {

            Vertex v = new Vertex();

            v.position = Vector3.Lerp(min.position, max.position, factor);
            v.rhw = MathUtility.Lerp(min.rhw, max.rhw, factor);
            v.u = MathUtility.Lerp(min.u, max.u, factor);
            v.v = MathUtility.Lerp(min.v, max.v, factor);
            v.color = Color4.Lerp(min.color, max.color, factor);

            return v;


        }

        #endregion

    }
}
