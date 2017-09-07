using System;
using System.Collections.Generic;
using System.Drawing;
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

        #region 变量


        private Graphics canvas = null;

        private Bitmap buffer = null; 

        private float[,] zbuffer = null;

        private Light light;

        private Camera camera = null;

        private List<Mesh> meshs = null;

        private RenderType renderType;

        #endregion

        public MainWindow() {

            InitializeComponent();

            //当前窗口创建graphics
            canvas = this.CreateGraphics();

            //当前窗口的宽高值
            buffer = new Bitmap(this.Width, this.Height);
            RenderUtility.SetFrameBuffer(buffer);

            //根据buffer的大小设定zbuffer
            zbuffer = new float[this.Width, this.Height];
            RenderUtility.SetZBuffer(zbuffer);

            //创建平行光,平行光的位置默认为(0, 20, 0),光照颜色为橘黄色,环境光颜色为白色
            light = new Light(new Vector3(0, 20, 0, 1), new Color4(Color.Orange), new Color4(Color.White));
            RenderUtility.SetLight(light);

            //创建摄像机, 摄像机位置默认为(0,0,0),朝向z轴方向(0,0,1),以(0,1,0)为单位up向量,y方向的视角为90度,zn = 1, zf = 500
            float aspect = (float)this.Width / (float)this.Height;
            camera = new Camera( new Vector3(0, 0, 0, 1), new Vector3(0, 0, 1, 0), new Vector3(0, 1, 0, 0),
                                 3.1415926f / 4, aspect,
                                 1.0f, 500.0f);

            //设定渲染的模式
            renderType = RenderType.WireFrame;
            renderType = RenderType.VertexColor;
            RenderUtility.SetRenderType(renderType);

            //加载模型
            //meshs,要渲染的网格列表
            meshs = new List<Mesh>();

            Mesh cube = new Mesh(Cube.positions, Cube.indices, Cube.colors);

            meshs.Add(cube);

            //开启计时器
            SetupTimer();
        }

        //在每一帧调用,通过在MainWindow的构造方法中设定定时器来启动Update
        public void Update(object sender, EventArgs e) {

            //1.根据输入更新变换矩阵   ProcessInput()
            //2.清除缓存              ClearBuffer()
            //3.开启渲染管线          RenderMesh()
            //4.屏幕绘制              canvas.DrawImage() 
            lock (buffer) {

                ProcessInput();
                ClearBuffer();
                RenderMesh();
                canvas.DrawImage(buffer, 0, 0);

            }

        }

        #region 开启计时器,设定FPS为60,每帧调用Update,在Update中进行渲染
        public void SetupTimer() {
            System.Timers.Timer timer = new System.Timers.Timer( 1000 / 60 );
            timer.Elapsed += new ElapsedEventHandler(Update);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }
        #endregion

        #region 渲染流程
        //根据键盘的输入调整摄像机等,确定各个变换矩阵
        public void ProcessInput() {

            Matrix4X4 scale = Matrix4X4.ScaleMatrix(1, 1, 1);
            Matrix4X4 rotation = Matrix4X4.RotateMatrix(new Vector3(-2, -2, -2, 0), 3.1415926f / 6.0f);
            //Matrix4X4 rotation = Matrix4X4.Identity();
            Matrix4X4 translate = Matrix4X4.TranslateMatrix(0, 0, 10);

            //1.设定world矩阵
            Transform.world = Matrix4X4.WorldMatrix(scale, rotation, translate);

            //2.设定view变换矩阵
            Transform.view = Matrix4X4.ViewMatrix(camera);

            //3.设定projection矩阵
            Transform.projection = Matrix4X4.PerspectiveMatrix(camera.fov, camera.aspect, camera.zn, camera.zf);

        }

        //清空缓存 
        public void ClearBuffer() {

            //清空缓存
            for (int x = 0; x < buffer.Width; ++x) {
                for (int y = 0; y < buffer.Height; ++y) {
                    buffer.SetPixel(x, y, Color.Black);
                }
            }

            //清空zbuffer
            Array.Clear(zbuffer, 0, zbuffer.Length);
        }

        //渲染Mesh网格
        public void RenderMesh() {

            foreach(Mesh mesh in meshs) {

                
                int triangles = mesh.indices.GetLength(0);
          
                for (int i = 0; i < triangles; ++i) {
                    
                    //应当保证p1 p2 p3是逆时针的顺序 
                    Vertex p1 = new Vertex( mesh.vertices[ mesh.indices[i, 0] ] );
                    Vertex p2 = new Vertex( mesh.vertices[ mesh.indices[i, 1] ] );
                    Vertex p3 = new Vertex( mesh.vertices[ mesh.indices[i, 2] ] );

                    RenderUtility.DrawTriangle(p1, p2, p3);

                }
            }
        
        }

        #endregion

    }
       
}
