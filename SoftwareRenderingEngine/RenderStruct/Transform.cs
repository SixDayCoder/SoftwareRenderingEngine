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
        public static void TransformToWorld(ref Vertex v) {

            //顶点的世界空间坐标
            v.position = v.position * world;

            //顶点的世界空间法向量
            //v.normal = (v.normal * world.Inverse().Transpose()).Normalize();

        }

        /// <summary>
        /// 把V从世界坐标系变换到相机空间下
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static void TransformToView(ref Vertex v) {
 
            v.position = v.position * view;

            //在相机空间下对三角面片做BackCulling

        }

        /// <summary>
        /// 把V从相机空间变换到齐次裁剪空间下
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static void TransformToHomogeneous(ref Vertex v) {

            v.position = v.position * projection;

            //透视矫正
            float rhw = 1.0f / v.position.w;

            v.rhw = rhw;
            v.u *= rhw;
            v.v *= rhw;
            v.color *= rhw;

        }

        /// <summary>
        /// 把V从标准裁剪空间换到视口
        /// </summary>
        /// <param name="v"></param>
        /// <param name="width">视口的宽度</param>
        /// <param name="height">视口的高度</param>
        /// <returns></returns>
        public static void TransformToViewport(ref Vertex v, int width, int height) {

            if (v.position.w != 0) {

                //1.透视除法,完成从三维坐标到二维坐标的映射
                //reciprocal homogeneous w   reciprocal->倒数 homogeneous -> 齐次
                //float rhw = 1.0f / v.position.w;

                float rhw = v.rhw;

                v.position.x *= rhw;
                v.position.y *= rhw;
                v.position.z *= rhw;
                v.position.w = 1.0f;

                //2.屏幕映射
                //调整x,y到输出窗口 这里实际上也是利用了线性插值
                //x[-1,1] -> x'[0,width]      (x'-0)/(width-0) = (x -(-1))/(1-(-1))  整理得  x' = (x + 1) * 0.5 * width
                v.position.x = (v.position.x + 1) * 0.5f * width;

                //y变换时需要特别注意,在屏幕上y是向下增长的,需要倒转y轴
                //-y[-1,1] -> y'[0, height]     (y'-0)/(height-0) = (-y -(-1))/(1-(-1))  整理得  y' = (1 - y) * 0.5 * height
                v.position.y = (1 - v.position.y) * 0.5f * height;
                
            }
            else
                return;

        }

        /// <summary>
        /// 将局部坐标系的点一次性变换到屏幕坐标
        /// </summary>
        /// <param name="v">局部坐标系下的点</param>
        /// <param name="width">屏幕的宽度</param>
        /// <param name="height">屏幕的高度</param>
        public static void TransformAll (ref Vertex v, int width, int height) {

            if (v != null) {

                TransformToWorld(ref v);
                TransformToView(ref v);
                TransformToHomogeneous(ref v);
                TransformToViewport(ref v, width, height);

            }

            //v为空引用
            return;

        }
        
    }

    #endregion
}
