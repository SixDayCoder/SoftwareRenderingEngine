using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.Math3D;

namespace SoftwareRenderingEngine.RenderStruct {

    public class Transform {

        public Matrix4X4 model;

        public Matrix4X4 view;

        public Matrix4X4 projection;

        public Matrix4X4 MVP {

            get {
                return model * view * projection;
            }

        }


        #region 空间变换操作

        /// <summary>
        /// 把V从局部坐标系变换到世界坐标系
        /// </summary>
        /// <param name="v">位于局部坐标系下的点</param>
        /// <returns></returns>
        public static Vertex TransformToWorld(Vertex v) {

            return new Vertex();

        }

        /// <summary>
        /// 把V从世界坐标系变换到相机空间下
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vertex TransformToView(Vertex v) {

            return new Vertex();

        }

        /// <summary>
        /// 把V从相机空间变换到标准裁剪空间下
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vertex TransformToCVV(Vertex v) {

            //透视矫正

            return new Vertex();

        }

        /// <summary>
        /// 把V从标准裁剪空间换到视口
        /// </summary>
        /// <param name="v"></param>
        /// <param name="width">视口的宽度</param>
        /// <param name="height">视口的高度</param>
        /// <returns></returns>
        public static Vertex TransformToViewport(Vertex v, int width, int height) {
            //1.透视除法
            //2.屏幕映射
            return new Vertex();

        }
        
    }

    #endregion
}
