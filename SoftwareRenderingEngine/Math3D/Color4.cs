using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using SoftwareRenderingEngine.Utility;

namespace SoftwareRenderingEngine.Math3D {

    public class Color4 {

        private float r;
        private float g;
        private float b;
        private float a;

        public float R {
            get {
                r = MathUtility.Range(r, 0, 1f);
                return r; 
            }
            set {
                r = MathUtility.Range(value, 0, 1f);
            }
        }

        public float G {
            get {
                g = MathUtility.Range(g, 0, 1f);
                return g;
            }
            set {
                g = MathUtility.Range(value, 0, 1f);
            }
        }

        public float B {
            get {
                b = MathUtility.Range(b, 0, 1f);
                return b;
            }
            set {
                b = MathUtility.Range(value, 0, 1f);
            }
        }

        public float A {
            get {
                a = MathUtility.Range(a, 0, 1f);
                return a;
            }
            set {
                a = MathUtility.Range(value, 0, 1f);
            }
        }


        public Color4(float r = 0, float g = 0, float b = 0, float a = 1) {

            R = r;
            G = g;
            B = b;
            A = a;

        }

        public Color4(Color c) {

            R = c.R / 255f;
            G = c.G / 255f;
            B = c.B / 255f;
            A = c.A / 255f;

        }

        public override string ToString() {
            return string.Format("R {0}, G {1}, B {2}, A {3}", R, G, B, A);
        }

        public static Color4 Lerp(Color4 min, Color4 max, float factor) {

            Color4 c = new Color4();

            c.R = MathUtility.Lerp(min.R, max.R, factor);
            c.G = MathUtility.Lerp(min.G, max.G, factor);
            c.B = MathUtility.Lerp(min.B, max.B, factor);
            //c.A = MathUtility.Lerp(min.A, max.A, factor);
            c.A = 1;

            return c;
        }

        public static implicit operator Color(Color4 c) {

            float r = c.R * 255f;
            float g = c.G * 255f;
            float b = c.B * 255f;
            float a = c.A * 255f;

            return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
        }



        #region 重载运算符

        public static Color4 operator* (float factor, Color4 c) {
            return new Color4(c.r * factor, c.g * factor, c.b * factor, 1.0f);
        }

        public static Color4 operator* (Color4 c, float factor) {
            return factor * c;
        }

        //颜色混合
        public static Color4 operator *(Color4 lhs, Color4 rhs) {
            return new Color4(lhs.r * rhs.r, lhs.g * rhs.g, lhs.b * rhs.b, 1.0f);
        }

        //颜色叠加
        public static Color4 operator +(Color4 lhs, Color4 rhs) {
            return new Color4(lhs.r + rhs.r, lhs.g + rhs.g, lhs.b + rhs.b, 1.0f);
        }

        #endregion

    }
}
