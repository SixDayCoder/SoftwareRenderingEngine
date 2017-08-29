using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SoftwareRenderingEngine.Math3D;
using SoftwareRenderingEngine.RenderStruct;

namespace SoftwareRenderingEngine.Utility {

    public static class RenderUtility {

        private static void DrawHrizontalLine(ref Bitmap buffer, int x1, int y1, int x2, int y2) {

            MathUtility.LhsLowerThanRhs(ref x1, ref x2);
            for (int x = x1; x <= x2; ++x)
                buffer.SetPixel(x, y1, Color.Red);
        }

        private static void DrawVerticalLine(ref Bitmap buffer, int x1, int y1, int x2, int y2) {

            MathUtility.LhsLowerThanRhs(ref y1, ref y2);

            for (int y = y1; y <= y2; ++y)
                buffer.SetPixel(x1, y, Color.Red);

        }

        public static void BresenhamDrawLine(ref Bitmap buffer, int x1, int y1, int x2, int y2) {

            //处理两种特殊的情况
            if (y1 == y2) {
                //水平线
                DrawHrizontalLine(ref buffer, x1, y1, x2, y2);
            }
            else if (x1 == x2) {
                //垂直线
                DrawVerticalLine(ref buffer, x1, y1, x2, y2);
            }
            else {

                //根据斜率的正负判断步进是增还是减 
                int stepy = (y2 > y1) ? 1 : -1;
                int stepx = (x2 > x1) ? 1 : -1;

                //gradient为true表示斜率大于等于1,否则表示斜率小于1
                bool gradient = Math.Abs(y2 - y1) >= Math.Abs(x2 - x1);

                int dx = Math.Abs(x2 - x1);
                int dy = Math.Abs(y2 - y1);
                int error = 0;

                //斜率>1,说明y变化较快,为了不让画出点稀疏,让y步进,计算x的值
                if (gradient) {

                    int x = Math.Min(x1, x2);
                    MathUtility.LhsLowerThanRhs(ref y1, ref y2);
                    error = -dy;

                    for (int y = y1; y <= y2; ++y) {

                        buffer.SetPixel(x, y, Color.Red);

                        error = error + dx + dx;
                        if (error >= 0) {
                            error = error - dy - dy;
                            x += stepx;
                        }

                    }
                }
                //斜率<1,说明x变换较快,为了不让画出的点稀疏,让x步进,计算y的值
                else {

                    int y = Math.Min(y1, y2);
                    MathUtility.LhsLowerThanRhs(ref x1, ref x2);
                    error = -dx;

                    for (int x = x1; x <= x2; ++x) {

                        buffer.SetPixel(x, y, Color.Red);

                        error = error + dy + dy;
                        if (error >= 0) {
                            error = error - dx - dx;
                            y += stepy;
                        }

                    }
                }
            }
        }

        //渲染管线的几何阶段
        public static void DrawTraingle(ref Bitmap buffer, Triangle triangle) {


            Point2 p1 = new Point2(triangle.top.position);
            Point2 p2 = new Point2(triangle.middle.position);
            Point2 p3 = new Point2(triangle.bottom.position);

            BresenhamDrawLine(ref buffer, (int)p1.x, (int)p1.y, (int)p2.x, (int)p2.y);
            BresenhamDrawLine(ref buffer, (int)p2.x, (int)p2.y, (int)p3.x, (int)p3.y);
            BresenhamDrawLine(ref buffer, (int)p1.x, (int)p1.y, (int)p3.x, (int)p3.y);

        }

    }

}
