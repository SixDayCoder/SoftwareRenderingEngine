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

        public static void SetFrameBuffer(Bitmap buffer) {
            frameBuffer = buffer;
        }

        private static Bitmap frameBuffer;

        private static void DrawHrizontalLine(int x1, int y1, int x2, int y2) {

            MathUtility.LhsLowerThanRhs(ref x1, ref x2);
            for (int x = x1; x <= x2; ++x)
                frameBuffer.SetPixel(x, y1, Color.Red);
        }

        private static void DrawVerticalLine(int x1, int y1, int x2, int y2) {

            MathUtility.LhsLowerThanRhs(ref y1, ref y2);

            for (int y = y1; y <= y2; ++y)
                frameBuffer.SetPixel(x1, y, Color.Red);

        }

        public static void BresenhamDrawLine(int x1, int y1, int x2, int y2) {


            //处理三两种特殊的情况
            if (x1 == x2 && y1 == y2) {
                frameBuffer.SetPixel(x1, y1, Color.Red);
            }
            else if (y1 == y2) {
                //水平线
                DrawHrizontalLine(x1, y1, x2, y2);
            }
            else if (x1 == x2) {
                //垂直线
                DrawVerticalLine(x1, y1, x2, y2);
            }
            else {

                int x = 0, y = 0, error = 0;
                int dx = (x1 < x2) ? x2 - x1 : x1 - x2;
                int dy = (y1 < y2) ? y2 - y1 : y1 - y2;

                //此时,斜率的绝对值小于1,说明x的变化快,为了不让画出的点稀疏,让x步进,计算y的值
                if (dx >= dy) {

                    if (x2 < x1) {
                        x  = x1;   y  = y1;
                        x1 = x2;   y1 = y2;
                        x2 = x;    y2 = y;
                    }
                    for (x = x1, y = y1; x <= x2; x++) {
                        frameBuffer.SetPixel(x, y, Color.Red);
                        error += dy;
                        if (error >= dx) {
                            error -= dx;
                            y += (y2 >= y1) ? 1 : -1;
                            frameBuffer.SetPixel(x, y, Color.Red);
                        }
                    }

                    frameBuffer.SetPixel(x2, y2, Color.Red);
                }
                //此时,斜率的绝对值大于1,说明x的变化快,为了不让画出的点稀疏,让x步进,计算y的值
                else {

                    if (y2 < y1) {
                        x  = x1;  y  = y1;
                        x1 = x2;  y1 = y2;
                        x2 = x;   y2 = y;
                    }
                    for (x = x1, y = y1; y <= y2; y++) {
                        frameBuffer.SetPixel(x, y, Color.Red);
                        error += dx;
                        if (error >= dy) {
                            error -= dy;
                            x += (x2 >= x1) ? 1 : -1;
                            frameBuffer.SetPixel(x, y, Color.Red);
                        }
                    }

                    frameBuffer.SetPixel(x, y, Color.Red);
                }
            }

        }

        public static void DrawTriangle(Vertex p1, Vertex p2, Vertex p3) {

            Transform.TransformAll(ref p1, frameBuffer.Width, frameBuffer.Height);
            Transform.TransformAll(ref p2, frameBuffer.Width, frameBuffer.Height);
            Transform.TransformAll(ref p3, frameBuffer.Width, frameBuffer.Height);

                       
            BresenhamDrawLine( MathUtility.RoundToInt(p1.position.x), MathUtility.RoundToInt(p1.position.y),
                               MathUtility.RoundToInt(p2.position.x), MathUtility.RoundToInt(p2.position.y) );

            BresenhamDrawLine( MathUtility.RoundToInt(p2.position.x), MathUtility.RoundToInt(p2.position.y),
                               MathUtility.RoundToInt(p3.position.x), MathUtility.RoundToInt(p3.position.y));

            BresenhamDrawLine( MathUtility.RoundToInt(p1.position.x), MathUtility.RoundToInt(p1.position.y),
                               MathUtility.RoundToInt(p3.position.x), MathUtility.RoundToInt(p3.position.y));

        }

    }

}
