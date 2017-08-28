using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareRenderingEngine.Math3D {

    class Matrix4X4 {

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


        #region static方法,3D数学



        #endregion


        #region static方法,矩阵相关



        #endregion

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
    }

}

}
