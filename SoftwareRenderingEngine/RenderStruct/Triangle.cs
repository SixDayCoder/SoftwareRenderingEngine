using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.Math3D;
using SoftwareRenderingEngine.Utility;

namespace SoftwareRenderingEngine.RenderStruct {

    //该数据结构用于位于屏幕坐标系下的点
    public class Triangle {

        //三角形的三个顶点,考虑到屏幕是的y轴是反转的
        //top    y值最小的点 
        //middl
        //bottom y值最大的点
        public Vertex top;

        public Vertex middle;

        public Vertex bottom;

        //三角形面的法线
        public Vector3 normal;

        public Triangle(Vertex v1, Vertex v2, Vertex v3) {

            RerangeVertex(v1, v2, v3);
            CalculateNormal();               

        }

        //左手系,按照一定的顺序计算该三角形的面法向量,保证法向量指向观察者的方向
        private void CalculateNormal() {

            Vector3 v1 = middle.position - top.position;
            Vector3 v2 = bottom.position - top.position;

            normal = Vector3.Cross(v1, v2);
        }

        //重新排布结点
        private void RerangeVertex(Vertex v1, Vertex v2, Vertex v3) {

            if (v1.position.y == v2.position.y && v2.position.y == v3.position.y) {
                //三点共线
                return;
            }
            else {

                //1.v1 > v2 > v3
                if (v1.position.y >= v2.position.y && v2.position.y >= v3.position.y) {
                    top = v1;
                    middle = v2;
                    bottom = v3;
                }
                //2.v1 > v3 > v2
                else if (v1.position.y >= v3.position.y && v3.position.y >= v2.position.y) {
                    top = v1;
                    middle = v3;
                    bottom = v2;
                }
                //3.v2 > v1 > v3
                else if (v2.position.y >= v1.position.y && v1.position.y >= v3.position.y) {
                    top = v2;
                    middle = v1;
                    bottom = v3;
                }
                //4.v2 > v3 > v1
                else if (v2.position.y >= v3.position.y && v3.position.y >= v1.position.y) {
                    top = v2;
                    middle = v3;
                    bottom = v1;
                }
                //5.v3 > v1 > v2
                else if (v3.position.y >= v1.position.y && v1.position.y >= v2.position.y) {
                    top = v3;
                    middle = v1;
                    bottom = v2;
                }
                //6.v3 > v2 > v1
                else if (v3.position.y >= v2.position.y && v2.position.y >= v1.position.y) {
                    top = v3;
                    middle = v2;
                    bottom = v1;
                }
                else {

                }
            }
        }
    }

}
