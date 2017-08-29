using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.RenderStruct;

namespace SoftwareRenderingEngine.Math3D {

    public class Matrix4X4 {

        #region 构造方法

        private float[,] matrix = new float[4, 4];

        public float this[int i, int j] {
            get {
                return matrix[i, j];
            }
            set {
                matrix[i, j] = value;
            }
        }

        public Matrix4X4() {

            SetZero();

        }
        public Matrix4X4(float a1, float a2, float a3, float a4,
                         float b1, float b2, float b3, float b4,
                         float c1, float c2, float c3, float c4,
                         float d1, float d2, float d3, float d4) {
            matrix[0, 0] = a1; matrix[0, 1] = a2; matrix[0, 2] = a3; matrix[0, 3] = a4;
            //
            matrix[1, 0] = b1; matrix[1, 1] = b2; matrix[1, 2] = b3; matrix[1, 3] = b4;
            //
            matrix[2, 0] = c1; matrix[2, 1] = c2; matrix[2, 2] = c3; matrix[2, 3] = c4;
            //
            matrix[3, 0] = d1; matrix[3, 1] = d2; matrix[3, 2] = d3; matrix[3, 3] = d4;
        }

        #endregion

        public void SetIdentity() {
            matrix[0, 0] = 1; matrix[0, 1] = 0; matrix[0, 2] = 0; matrix[0, 3] = 0;
            //
            matrix[1, 0] = 0; matrix[1, 1] = 1; matrix[1, 2] = 0; matrix[1, 3] = 0;
            //
            matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 1; matrix[2, 3] = 0;
            //
            matrix[3, 0] = 0; matrix[3, 1] = 0; matrix[3, 2] = 0; matrix[3, 3] = 1;
        }

        public void SetZero() {

            matrix[0, 0] = 0; matrix[0, 1] = 0; matrix[0, 2] = 0; matrix[0, 3] = 0;
            //
            matrix[1, 0] = 0; matrix[1, 1] = 0; matrix[1, 2] = 0; matrix[1, 3] = 0;
            //
            matrix[2, 0] = 0; matrix[2, 1] = 0; matrix[2, 2] = 0; matrix[2, 3] = 0;
            //
            matrix[3, 0] = 0; matrix[3, 1] = 0; matrix[3, 2] = 0; matrix[3, 3] = 0;

        }

        #region 重载运算符

        public static Matrix4X4 operator* (Matrix4X4 lhs, Matrix4X4 rhs) {

            Matrix4X4 result = new Matrix4X4();
            
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    result[j, i] = (lhs[j, 0] * rhs[0, i]) + (lhs[j, 1] * rhs[1, i]) +
                                   (lhs[j, 2] * rhs[2, i]) + (lhs[j, 3] * rhs[3, i]);
                }
            }
            return result;
        }

        public static Matrix4X4 operator* (Matrix4X4 matrix, float factor) {

            for(int i = 0; i < 4; ++i) {
                for(int j = 0; j < 4; ++j) {
                    matrix[i, j] *= factor;
                }
            }
            return matrix;

        }

        public static Matrix4X4 operator *(float factor, Matrix4X4 matrix) {
            return matrix * factor;
        }


        #endregion  


        #region static方法,旋转,平移,放缩,透视投影,相机变换矩阵

        /// <summary>
        /// 获取放缩矩阵
        /// </summary>
        /// <param name="x">x方向放缩系数</param>
        /// <param name="y">y方向放缩系数</param>
        /// <param name="z">z方向放缩系数</param>
        /// <returns></returns>
        public static Matrix4X4 ScaleMatrix(float x, float y, float z) {

           return new Matrix4X4( x, 0, 0, 0,
                                 0, y, 0, 0,
                                 0, 0, z, 0,
                                 0, 0, 0, 1 );
                 
        }

        /// <summary>
        /// 获取平移矩阵
        /// </summary>
        /// <param name="x">x方向位移量</param>
        /// <param name="y">y方向位移量</param>
        /// <param name="z">z方向位移量</param>
        /// <returns></returns>
        public static Matrix4X4 TranslateMatrix(float x, float y, float z) {

            return new Matrix4X4( 1, 0, 0, 0,
                                  0, 1, 0, 0,
                                  0, 0, 1, 0,
                                  x, y, z, 1 );

        }

        /// <summary>
        /// 绕着向量vec逆时针旋转theta弧度
        /// </summary>
        /// <param name="vec">用vec表示旋转轴</param>
        /// <param name="theta">旋转角,弧度制</param>
        /// <returns></returns>
        public static Matrix4X4 RotateMatrix(Vector3 vec, float theta) {

            //单位化旋转向量,计算方便
            vec.Normalize();
            float x = vec.x;
            float y = vec.y;
            float z = vec.z;
            float sin = (float)Math.Sin(theta);
            float cos = (float)Math.Cos(theta);

            Matrix4X4 matrix = new Matrix4X4();

            matrix[0, 0] = cos + x * x * (1 - cos);
            matrix[0, 1] = x * y * (1 - cos) - (z * sin);
            matrix[0, 2] = x * z * (1 - cos) + (y * sin);

            matrix[1, 0] = x * y * (1 - cos) + (z * sin);
            matrix[1, 1] = cos + y * y * (1 - cos);
            matrix[1, 2] = y * z * (1 - cos) - (x * sin);

            matrix[2, 0] = x * z * (1 - cos) - (y * sin);
            matrix[2, 1] = y * z * (1 - cos) + (x * sin);
            matrix[2, 2] = cos + z * z * (1 - cos);

            matrix[3, 3] = 1;

            return matrix;
        }


        /// <summary>
        /// 左手系,绕着x轴逆时针旋转theta弧度
        /// </summary>
        /// <param name="theta">旋转角,弧度制</param>
        /// <returns></returns>
        public static Matrix4X4 RotateX(float theta) {

            float sin = (float)Math.Sin(theta);
            float cos = (float)Math.Cos(theta);

            return new Matrix4X4( 1,  0,    0,    0,
                                  0,  cos,  sin,  0,
                                  0,  -sin, cos,  0,
                                  0,  0,    0,    1);
        }

        /// <summary>
        /// 左手系,绕着y轴逆时针旋转theta弧度
        /// </summary>
        /// <param name="theta">旋转角,弧度制</param>
        /// <returns></returns>
        public static Matrix4X4 RotateY(float theta) {

            float sin = (float)Math.Sin(theta);
            float cos = (float)Math.Cos(theta);

            return new Matrix4X4( cos, 0, -sin, 0,
                                  0,   1, 0,    0,
                                  sin, 0, cos,  0,
                                  0,   0, 0,    1);

        }

        /// <summary>
        /// 左手系,绕着z轴逆时针旋转theta弧度
        /// </summary>
        /// <param name="theta">旋转角,弧度制</param>
        /// <returns></returns>
        public static Matrix4X4 RotateZ(float theta) {

            float sin = (float)Math.Sin(theta);
            float cos = (float)Math.Cos(theta);

            return new Matrix4X4( cos, sin, 0,  0,
                                 -sin, cos, 0,  0,
                                  0,   0,   1,  0,
                                  0,   0,   0,  1);
        }

        /// <summary>
        /// D3DXMatrixPerspectiveFovLH,获取投影矩阵
        /// </summary>
        /// <param name="fov">摄像机视角</param>
        /// <param name="aspect">屏幕宽高比</param>
        /// <param name="zn">近平面</param>
        /// <param name="zf">远平面</param>
        /// <returns></returns>
        public static Matrix4X4 PerspectiveMatrix(float fov, float aspect, float zn, float zf) {

            Matrix4X4 matrix = new Matrix4X4();
            float tanHalfFov = (float)Math.Tan(fov * 0.5f);

            matrix[0, 0] = 1 / ( tanHalfFov * aspect );
            matrix[1, 1] = 1 / tanHalfFov;
            matrix[2, 2] = zf / (zf - zn);
            matrix[2, 3] = 1.0f;
            matrix[3, 2] = -(zn * zf) / (zf - zn);

            return matrix;

            //近平面作为视平面
            //x[-1,1] y[-1,1] z[0,1]
            //投影结束后,w保存着原来的z信息,可以用来进行深度检测
        }


        /// <summary>
        /// 根据camera的属性建立world->view变换矩阵
        /// </summary>
        /// <param name="camera">UVN系统的摄像机</param>
        /// <returns></returns>
        public static Matrix4X4 ViewMatrix(Camera camera) {

            Vector3 axisZ = (camera.target - camera.pos).Normalize();
            Vector3 axisX = Vector3.Cross(camera.up, axisZ).Normalize();
            Vector3 axisY = Vector3.Cross(axisZ, axisX).Normalize();

            Matrix4X4 view = new Matrix4X4(axisX.x, axisX.y, axisX.z, 0,
                                           axisY.x, axisY.y, axisY.z, 0,
                                           axisZ.x, axisZ.y, axisZ.z, 0,
                                           0, 0, 0, 1);
            view *= TranslateMatrix(-camera.pos.x, -camera.pos.y, -camera.pos.z);

            return view;

        }

        public static Matrix4X4 WorldMatrix(Matrix4X4 scale, Matrix4X4 rotate, Matrix4X4 translate) {
            return scale * rotate * translate;
        }

        #endregion


        #region 转置,行列式,逆矩阵

        public Matrix4X4 Transpose() {
            for (int i = 0; i < 4; i++) {
                for (int j = i; j < 4; j++) {
                    //MathHelper.Swap<float>(ref matrix[i, j], ref matrix[j, i]);
                }
            }
            return this;
        }

        public float Determinate() {
            return Determinate(matrix, 4);
        }

        private float Determinate(float[,] m, int n)//递归求行列式
        {
            if (n == 1) {//递归出口
                return m[0, 0];
            }
            else {
                float result = 0;
                float[,] tempM = new float[n - 1, n - 1];
                for (int i = 0; i < n; i++) {
                    //求代数余子式A
                    for (int j = 0; j < n - 1; j++)//行
                    {
                        for (int k = 0; k < n - 1; k++)//列
                        {
                            int x = j + 1;//原矩阵行
                            int y = k >= i ? k + 1 : k;//原矩阵列
                            tempM[j, k] = m[x, y];
                        }
                    }

                    result += (float)System.Math.Pow(-1, 1 + (1 + i)) * m[0, i] * Determinate(tempM, n - 1);
                }
                return result;
            }
        }

        public Matrix4X4 GetAdjoint() {
            int x, y;
            float[,] tempM = new float[3, 3];
            Matrix4X4 result = new Matrix4X4();
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    for (int k = 0; k < 3; k++) {
                        for (int t = 0; t < 3; ++t) {
                            x = k >= i ? k + 1 : k;
                            y = t >= j ? t + 1 : t;

                            tempM[k, t] = matrix[x, y];
                        }
                    }
                    result.matrix[i, j] = (float)System.Math.Pow(-1, (1 + j) + (1 + i)) * Determinate(tempM, 3);
                }
            }
            return result.Transpose();
        }

        public Matrix4X4 Inverse() {
            float a = Determinate();
            if (a == 0) {
                Console.WriteLine("矩阵不可逆");
                return null;
            }
            Matrix4X4 adj = GetAdjoint();//伴随矩阵
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    adj.matrix[i, j] = adj.matrix[i, j] / a;
                }
            }
            return adj;
        }

        #endregion

    }
   
}
