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

using SoftwareRenderingEngine.Math3D;
using SoftwareRenderingEngine.RenderStruct;
using SoftwareRenderingEngine.Utility;
using SoftwareRenderingEngine.TestData;

namespace SoftwareRenderingEngine {



    /// <summary>
    /// MainWindow类是对运行时显示出来的窗体的一个抽象
    /// 窗体包含两个重要的属性
    /// 1.Graphics canvas      -->   Graphics封装了GDI,用于进行图形的绘制
    /// 2.Bitmap   buffer      -->   自定义的缓冲,渲染管线的每个像素的结果保存在这里
    /// 3.float[,] zbuffer     -->   深度缓冲
    /// </summary>

    public partial class MainWindow : Form {

        private Graphics canvas = null;

        private Bitmap buffer = null; 

        private float[,] zbuffer = null;

        private Camera camera = null;

        private List<Mesh> meshs = null;

        public MainWindow() {

            InitializeComponent();

            //当前窗口创建graphics
            canvas = this.CreateGraphics();

            //当前窗口的宽高值
            buffer = new Bitmap(this.Width, this.Height);

            //根据buffer的大小设定zbuffer
            zbuffer = new float[this.Width, this.Height];

            //创建摄像机, 摄像机位置默认为(0,0,0),朝向z轴方向(0,0,1),以(0,1,0)为单位up向量,y方向的视角为90度,zn = 1, zf = 500
            camera = new Camera(new Vector3(0, 0, 0, 1), new Vector3(0, 0, 1, 0), new Vector3(0, 1, 0, 0),
                                 3.1415926f / 4, this.Width / this.Height,
                                 1.0f, 500.0f);
            //加载模型
            LoadMesh();

            //开启计时器
            SetupTimer();
        }

        #region  测试mesh,待改进

        private void LoadMesh() {


            //meshs,要渲染的网格列表
            meshs = new List<Mesh>();

            Mesh primitive = new Mesh(Primitive.positions, Primitive.indices);
            Mesh quad = new Mesh(Quad.positions, Quad.indices);
            Mesh cube = new Mesh(Cube.positions, Cube.indices);

            meshs.Add(primitive);
            //meshs.Add(quad);
            //meshs.Add(cube);
        }

        #endregion

        #region 开启计时器,设定FPS为60,每帧调用Update,在Update中进行渲染
        private void SetupTimer() {
            System.Timers.Timer timer = new System.Timers.Timer( 1000 / 60 );
            timer.Elapsed += new ElapsedEventHandler(Update);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }
        #endregion

        #region 渲染流程
        //根据键盘的输入调整摄像机等,确定各个变换矩阵
        private void UpdateTransform() {

            #region 根据输入来设定模型的scale rotation translate矩阵

            //必须先缩放再旋转再平移

            Matrix4X4 scale = Matrix4X4.ScaleMatrix(1, 1, 1);
            Matrix4X4 rotation = Matrix4X4.Identity();//不做旋转
            Matrix4X4 translate = Matrix4X4.TranslateMatrix(0, 0, 5);

            #endregion

            //1.设定world矩阵
            Transform.world = Matrix4X4.WorldMatrix(scale, rotation, translate);

            //2.设定view变换矩阵
            Transform.view = Matrix4X4.ViewMatrix(camera);

            //3.设定projection矩阵
            Transform.projection = Matrix4X4.PerspectiveMatrix(camera.fov, camera.aspect, camera.zn, camera.zf);

        }

        //清空缓存 
        private void ClearBuffer() {

            //清空缓存
            for (int x = 0; x < buffer.Width; ++x) {
                for (int y = 0; y < buffer.Height; ++y) {
                    buffer.SetPixel(x, y, Color.Black);
                }
            }

            //清空zbuffer
            Array.Clear(zbuffer, 0, zbuffer.Length);

            //清除整个绘图面并以黑色填充
            //canvas.Clear(Color.Black);
        }

        //渲染Mesh网格
        private void RenderMesh() {

            int rows = 0;
            int index1 = 0, index2 = 0, index3 = 0;

            foreach(Mesh mesh in meshs) {

                rows = mesh.indices.GetLength(0);

                for (int i = 0; i < rows; ++i) {

                    index1 = mesh.indices[i, 0];
                    index2 = mesh.indices[i, 1];
                    index3 = mesh.indices[i, 2];
                    
                    Vertex p1 = Transform.CompleteTransform(mesh.vertices[index1], buffer.Width, buffer.Height);
                    Vertex p2 = Transform.CompleteTransform(mesh.vertices[index2], buffer.Width, buffer.Height);
                    Vertex p3 = Transform.CompleteTransform(mesh.vertices[index3], buffer.Width, buffer.Height);
 

                    RenderUtility.BresenhamDrawLine(ref buffer, (int)p1.position.x, (int)p1.position.y, (int)p2.position.x, (int)p2.position.y);
                    RenderUtility.BresenhamDrawLine(ref buffer, (int)p2.position.x, (int)p2.position.y, (int)p3.position.x, (int)p3.position.y);
                    RenderUtility.BresenhamDrawLine(ref buffer, (int)p1.position.x, (int)p1.position.y, (int)p3.position.x, (int)p3.position.y);
                    

                }
            }
        
        }

        #endregion

        //在每一帧调用,通过在MainWindow的构造方法中设定定时器来启动Update
        private void Update(object sender, EventArgs e) {

            /*
             lock(object){
                code
             } 
             1. object被lock了吗？没有则由我来lock，否则一直等待，直至object被释放。
             2. lock以后在执行code的期间其他线程不能调用code，也不能使用object。
             3. 执行完codeB之后释放object，并且code可以被其他线程访问。
            */

            lock (buffer) {

                //1.根据输入更新变换矩阵  UpdateTransform()
                //2.清除缓存             ClearBuffer()
                //3.开启渲染管线         RenderMesh()
                //4.屏幕绘制             canvas.DrawImage() 
                
                UpdateTransform();
                ClearBuffer();
                RenderMesh();
                canvas.DrawImage(buffer, 0, 0);

            }

        }

    }
       
}
