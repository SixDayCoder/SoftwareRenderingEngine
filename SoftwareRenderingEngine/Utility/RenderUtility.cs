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


        public static Bitmap frameBuffer;

        public static RenderType renderType;

        public static void SetFrameBuffer(Bitmap buffer) {
            frameBuffer = buffer;
        }

        public static void SetRenderType(RenderType type) {
            renderType = type;
        }

        /// <summary>
        ///对三个顶点按照一定顺序排序
        /// 1.top是y值最小的点(因为屏幕的y轴是向下的)
        /// 2.bottom是y值最大的点
        /// 3.middle是y值第二的点
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <returns>返回Vertex的数组,长度为3 result[0] = top, result[1] = middle, result[2] = bottom</returns>
        public static Vertex[] RerangeVertex(Vertex v1, Vertex v2, Vertex v3) {

            Vertex[] result = new Vertex[3];

            Vertex top = new Vertex();
            Vertex middle = new Vertex();
            Vertex bottom = new Vertex();

            if (v1.position.y == v2.position.y && v2.position.y == v3.position.y) {
                //三点共线
                return null;
            }
            else {

                //1.v1 > v2 > v3
                if (v1.position.y >= v2.position.y && v2.position.y >= v3.position.y) {
                    top = v3;
                    middle = v2;
                    bottom = v1;
                }
                //2.v1 > v3 > v2
                else if (v1.position.y >= v3.position.y && v3.position.y >= v2.position.y) {
                    top = v2;
                    middle = v3;
                    bottom = v1;
                }
                //3.v2 > v1 > v3
                else if (v2.position.y >= v1.position.y && v1.position.y >= v3.position.y) {
                    top = v3;
                    middle = v1;
                    bottom = v2;
                }
                //4.v2 > v3 > v1
                else if (v2.position.y >= v3.position.y && v3.position.y >= v1.position.y) {
                    top = v1;
                    middle = v3;
                    bottom = v2;
                }
                //5.v3 > v1 > v2
                else if (v3.position.y >= v1.position.y && v1.position.y >= v2.position.y) {
                    top = v2;
                    middle = v1;
                    bottom = v3;
                }
                //6.v3 > v2 > v1
                else if (v3.position.y >= v2.position.y && v2.position.y >= v1.position.y) {
                    top = v1;
                    middle = v2;
                    bottom = v3;
                }
                else {

                }
            }

            result[0] = top;
            result[1] = middle;
            result[2] = bottom;

            return result;
        }

        public static void DrawHrizontalLine(int x1, int y1, int x2, int y2) {

            MathUtility.LhsLowerThanRhs(ref x1, ref x2);
            for (int x = x1; x <= x2; ++x)
                frameBuffer.SetPixel(x, y1, Color.Red);
        }

        public static void DrawVerticalLine(int x1, int y1, int x2, int y2) {

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

        public static void ScanlineFill(Vertex left, Vertex right, int yindex) {

            float minx = left.position.x;
            float maxx = right.position.x;

            for(float x = minx; x <= maxx; x += 1.0f) {

                int xindex = MathUtility.RoundToInt(x);
                float factor = (x - minx) / (maxx - minx);

                Vertex v = Vertex.Lerp(left, right, factor);
                float w = 1/ v.rhw;
                Color4 c = v.color * w;

                frameBuffer.SetPixel(xindex, yindex, c);
                
            }

        }


        //光栅化平底三角形
        public static void RasterizationTriangleBottom(Vertex bottomLeft, Vertex bottomRight, Vertex top) {

            float miny = top.position.y;
            float maxy = bottomLeft.position.y;

            //画顶点
            int topx = MathUtility.RoundToInt(top.position.x);
            int topy = MathUtility.RoundToInt(top.position.y);
            float topw = 1 / top.rhw;
            Color4 color = top.color * topw;
            frameBuffer.SetPixel(topx, topy, color);

            for (float y = miny + 1.0f ; y <= maxy; y += 1.0f) {

                float factor = (y - miny) / (maxy - miny);
                Vertex left = Vertex.Lerp(top, bottomLeft, factor);
                Vertex right = Vertex.Lerp(top, bottomRight, factor);
                int yindex = MathUtility.RoundToInt(y);

                ScanlineFill(left, right, yindex);

            }

        }

        //光栅化平顶三角形
        public static void RasterizationTriangleTop(Vertex topLeft, Vertex topRight, Vertex bottom) {

            float miny = topLeft.position.y;
            float maxy = bottom.position.y;

            for(float y = miny; y < maxy; y += 1.0f) {

                float factor = (y - miny) / (maxy - miny);
                Vertex left = Vertex.Lerp(topLeft, bottom, factor);
                Vertex right = Vertex.Lerp(topRight, bottom, factor);
                int yindex = MathUtility.RoundToInt(y);

                ScanlineFill(left, right, yindex);

            }

            //画底点
            int bottomx = MathUtility.RoundToInt(bottom.position.x);
            int bottomy = MathUtility.RoundToInt(bottom.position.y);
            float bottomw = 1 / bottom.rhw;
            Color4 color = bottom.color * bottomw;
            frameBuffer.SetPixel(bottomx, bottomy, color);


        }

        //光栅化三角形,任意一个三角形都可以被划分为平底三角形+平顶三角形
        public static void RasterizationTriangle(Vertex v1, Vertex v2, Vertex v3) {

            Vertex[] sortedVertices = RerangeVertex(v1, v2, v3);

            //三点共线
            if (sortedVertices == null)
                return;

            Vertex top = sortedVertices[0];
            Vertex middle = sortedVertices[1];
            Vertex bottom = sortedVertices[2];

            //平顶三角形
            if(top.position.y == middle.position.y) {

                if (top.position.x < middle.position.x)
                    RasterizationTriangleTop(top, middle, bottom);
                else
                    RasterizationTriangleTop(middle, top, bottom);

            }
            //平底三角形
            else if(middle.position.y == bottom.position.y) {

                if (middle.position.x < bottom.position.x)
                    RasterizationTriangleBottom(middle, bottom, top);
                else
                    RasterizationTriangleBottom(bottom, middle, top);

            }
            //普通的三角形,划分为一个平顶和一个平底
            else {

                
                float factor = (middle.position.y - top.position.y) / (bottom.position.y - top.position.y);
                //插值求新的middle
                Vertex newMiddle = Vertex.Lerp(top, bottom, factor);

                Vertex left;
                Vertex right;

                if(newMiddle.position.x < middle.position.x) {
                    left = newMiddle;
                    right = middle;
                }
                else {
                    left = middle;
                    right = newMiddle;
                }

                //绘制平底三角形
                RasterizationTriangleBottom(left, right, top);

                //绘制平顶三角形
                RasterizationTriangleTop(left, right, bottom);
            }

        }

        public static void DrawTriangle(Vertex p1, Vertex p2, Vertex p3) {

            

            Transform.TransformAll(ref p1, frameBuffer.Width, frameBuffer.Height);
            Transform.TransformAll(ref p2, frameBuffer.Width, frameBuffer.Height);
            Transform.TransformAll(ref p3, frameBuffer.Width, frameBuffer.Height);

            
            //线框模式
            if(renderType == RenderType.WireFrame) {

                BresenhamDrawLine(MathUtility.RoundToInt(p1.position.x), MathUtility.RoundToInt(p1.position.y),
                                  MathUtility.RoundToInt(p2.position.x), MathUtility.RoundToInt(p2.position.y));
                BresenhamDrawLine(MathUtility.RoundToInt(p2.position.x), MathUtility.RoundToInt(p2.position.y),
                                  MathUtility.RoundToInt(p3.position.x), MathUtility.RoundToInt(p3.position.y));
                BresenhamDrawLine(MathUtility.RoundToInt(p1.position.x), MathUtility.RoundToInt(p1.position.y),
                                  MathUtility.RoundToInt(p3.position.x), MathUtility.RoundToInt(p3.position.y));
            }
            //光栅化模式
            else {

                RasterizationTriangle(p1, p2, p3);

            }
            

        }

    }

}
