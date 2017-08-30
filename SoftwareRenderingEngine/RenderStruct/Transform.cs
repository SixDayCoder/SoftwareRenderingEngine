using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SoftwareRenderingEngine.Math3D;

namespace SoftwareRenderingEngine.RenderStruct {

    public static class Transform {

        public static Matrix4X4 world;

        public static Matrix4X4 view;

        public static Matrix4X4 projection;

        public static Matrix4X4 transform {

            get {
                return world * view * projection;
            }

        }


        #region 空间变换操作

        /// <summary>
        /// 把V从局部坐标系变换到世界坐标系
        /// </summary>
        /// <param name="v">位于局部坐标系下的点</param>
        /// <returns></returns>
        public static Vertex TransformToWorld(Vertex v) {

            Vertex nv = new Vertex(v);
            //顶点的世界空间坐标
            nv.position = nv.position * world;

            //顶点的世界空间法向量
            //v.normal = (v.normal * world.Inverse().Transpose()).Normalize();

            return nv;

        }

        /// <summary>
        /// 把V从世界坐标系变换到相机空间下
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vertex TransformToView(Vertex v) {

            Vertex nv = new Vertex(v);

            nv.position = nv.position * view;

            return nv;
        }

        /// <summary>
        /// 把V从相机空间变换到齐次裁剪空间下
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vertex TransformToHomogeneous(Vertex v) {

            Vertex nv = new Vertex(v);

            nv.position = nv.position * projection;

            //透视矫正
            float rhw = 1.0f / nv.position.w;

            nv.rhw = rhw;
            nv.u *= rhw;
            nv.v *= rhw;
            nv.color *= rhw;
            
            return nv;

        }

        /// <summary>
        /// 把V从标准裁剪空间换到视口
        /// </summary>
        /// <param name="v"></param>
        /// <param name="width">视口的宽度</param>
        /// <param name="height">视口的高度</param>
        /// <returns></returns>
        public static Vertex TransformToViewport(Vertex v, int width, int height) {

            Vertex nv = new Vertex(v);

            if (nv.position.w != 0) {
                //1.透视除法,完成从三维坐标到二维坐标的映射
                //reciprocal homogeneous w   reciprocal->倒数 homogeneous -> 齐次
                //float rhw = 1.0f / v.position.w;

                float rhw = nv.rhw;
                nv.position.x *= rhw;
                nv.position.y *= rhw;
                nv.position.z *= rhw;
                nv.position.w = 1.0f;

                //2.屏幕映射
                //调整x,y到输出窗口 这里实际上也是利用了线性插值
                //x[-1,1] -> x'[0,width]      (x'-0)/(width-0) = (x -(-1))/(1-(-1))  整理得  x' = (x + 1) * 0.5 * width
                nv.position.x = (nv.position.x + 1) * 0.5f * width;

                //y变换时需要特别注意,在屏幕上y是向下增长的,需要倒转y轴
                //-y[-1,1] -> y'[0, height]     (y'-0)/(height-0) = (-y -(-1))/(1-(-1))  整理得  y' = (1 - y) * 0.5 * height
                nv.position.y = (1 - nv.position.y) * 0.5f * height;

                return nv;
            }
            else
                return null;

        }

        /// <summary>
        /// 将局部坐标系的点一次性变换到屏幕坐标
        /// </summary>
        /// <param name="v">局部坐标系下的点</param>
        /// <param name="width">屏幕的宽度</param>
        /// <param name="height">屏幕的高度</param>
        /// <returns>屏幕空间的点</returns>

        public static Vertex CompleteTransform(Vertex v, int width, int height) {

            Vertex nv = new Vertex(v);

            nv = TransformToWorld(nv);
            nv = TransformToView(nv);
            nv = TransformToHomogeneous(nv);
            nv = TransformToViewport(nv, width, height);

            return nv;

        }
        
    }

    #endregion
}
