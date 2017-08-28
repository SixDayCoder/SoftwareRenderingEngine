using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.Math3D;

namespace SoftwareRenderingEngine.RenderStruct {

    //UVN模型的摄像机
    class Camera {

        //摄像机在世界空间下的坐标
        public Vector3 pos;
        
        //观察目标点
        public Vector3 target;

        //自定义的竖直方向,这里取(0,0,1)
        public Vector3 up;

        //视域,弧度
        public float fov;

        //宽高比
        public float aspect;

        //近平面
        public float zn;

        //远平面
        public float zf;

        public void SetUp(Vector3 pos, Vector3 target, Vector3 up, float fov, float aspect, float zn, float zf) {

            this.pos = pos;
            this.target = target;
            this.up = up;
            this.fov = fov;
            this.aspect = aspect;
            this.zn = zn;
            this.zf = zf;

        }

    }
}
