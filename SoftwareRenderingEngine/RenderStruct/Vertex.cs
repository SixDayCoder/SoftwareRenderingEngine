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

        public Vertex(Vertex v) {

            this.position = v.position;
            this.normal = v.normal;
            this.u = v.u;
            this.v = v.v;
            this.color = v.color;
            this.rhw = v.rhw;
        }

        public override string ToString() {

            string pos = string.Format("position : ({0}, {1}, {2}, {3})\n", position.x, position.y, position.z, position.w);
            string uv = string.Format("uv : ({0}, {1})\n", u, v);
            string rhw = string.Format("rhw : {0}\n", this.rhw);
            string inz = string.Format("1/rhw : {0}\n", 1.0f / this.rhw);
            string normal = string.Format("normal : ({0}, {1}, {2}, {3})\n", this.normal.x, this.normal.y, this.normal.z, this.normal.w);
            string color = string.Format("color : [R : {0}, G : {1}, B : {2}, A : {3}]\n", this.color.R, this.color.G, this.color.B, this.color.A);

            return pos + uv + rhw + inz + normal + color;
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
