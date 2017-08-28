using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace SoftwareRenderingEngine {



    /// <summary>
    /// MainWindow类是对运行时显示出来的窗体的一个抽象
    /// 窗体包含两个重要的属性
    /// 1.Graphics canvas      -->   Graphics封装了GDI,用于进行图形的绘制
    /// 2.Bitmap   buffer      -->   自定义的缓冲,渲染管线的每个像素的结果保存在这里
    /// </summary>
    
    public partial class MainWindow : Form {

        private Graphics canvas = null;

        private Bitmap buffer = null;

        #region 开启计时器,设定FPS为60,每帧调用Update,在Update中进行渲染
        private void SetupTimer() {
            System.Timers.Timer timer = new System.Timers.Timer(1000 / 60f);
            timer.Elapsed += new ElapsedEventHandler(Update);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }
        #endregion

        public MainWindow() {

            InitializeComponent();

            //当前窗口的宽高值
            buffer = new Bitmap(this.Width, this.Height);

            //当前窗口创建graphics
            canvas = this.CreateGraphics();

            //开启计时器
            SetupTimer();
        }


        //清空缓存 
        private void ClearBuffer() {

            for (int x = 0; x < buffer.Width; ++x) {
                for (int y = 0; y < buffer.Height; ++y) {
                    buffer.SetPixel(x, y, Color.Black);
                }
            }

        }

        //在每一帧调用,通过在MainWindow的构造方法中设定定时器来启动Update
        private void Update(object sender, EventArgs e) {

            lock (buffer) {
                ClearBuffer();
                BresenhamDrawLine(5, 5, 500, 5);
                BresenhamDrawLine(5, 5, 5, 500);
                BresenhamDrawLine(0, 0, 200, 100);
                BresenhamDrawLine(0, 0, 100, 200);
                
                canvas.DrawImage(buffer, 0, 0);
            }

        }


        //该函数保证lhs<rhs
        private void LhsLowerThanRhs(ref int lhs, ref int rhs) {
            if (lhs > rhs)
                swap(ref lhs, ref rhs);
        }

        private void swap<T>(ref T lhs, ref T rhs) {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        private void DrawHrizontalLine(int x1, int y1, int x2, int y2) {

            LhsLowerThanRhs(ref x1, ref x2);
            for (int x = x1; x <= x2; ++x)
                buffer.SetPixel(x, y1, Color.Red);
        }

        private void DrawVerticalLine(int x1, int y1, int x2, int y2) {

            LhsLowerThanRhs(ref y1, ref y2);
            for (int y = y1; y <= y2; ++y)
                buffer.SetPixel(x1, y, Color.Red);

        }

        void BresenhamDrawLine(int x1, int y1, int x2, int y2) {

            //处理两种特殊的情况
            if (y1 == y2) {
                //水平线
                DrawHrizontalLine(x1, y1, x2, y2);
            }
            else if (x1 == x2) {
                //垂直线
                DrawVerticalLine(x1, y1, x2, y2);
            }
            else {

                //根据斜率的正负判断步进是增还是减 
                int stepy = (y2 > y1) ? 1 : -1;
                int stepx = (x2 > x1) ? 1 : -1;

                //gradient为true表示斜率大于1,否则表示斜率小于1
                bool gradient = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);

                int dx = Math.Abs(x2 - x1);
                int dy = Math.Abs(y2 - y1);
                int error = 0;

                //斜率>1,说明y变化较快,为了不让画出点稀疏,让y步进,计算x的值
                if (gradient) {

                    int x = Math.Min(x1, x2);
                    LhsLowerThanRhs(ref y1, ref y2);
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
                    LhsLowerThanRhs(ref x1, ref x2);
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

    }
}
