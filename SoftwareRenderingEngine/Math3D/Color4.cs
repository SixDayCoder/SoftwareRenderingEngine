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
                return r; 
            }
            set {
                r = MathUtility.Range(value, 0, 1);
            }
        }

        public float G {
            get {
                return g;
            }
            set {
                g = MathUtility.Range(value, 0, 1);
            }
        }

        public float B {
            get {
                return b;
            }
            set {
                b = MathUtility.Range(value, 0, 1);
            }
        }

        public float A {
            get {
                return a;
            }
            set {
                a = MathUtility.Range(value, 0, 1);
            }
        }


        public Color4(float r = 0, float g = 0, float b = 0, float a = 1) {

            R = r;
            G = g;
            B = b;
            A = a;

        }

        public Color4(Color c) {

            R = c.R / 255;
            G = c.G / 255;
            B = c.B / 255;
            A = c.A / 255;

        }


        #region 隐式转换为System.Color

        public static implicit operator Color(Color4 c) {

            float r = c.R * 255;
            float g = c.G * 255;
            float b = c.B * 255;
            float a = c.A * 255;

            return Color.FromArgb((int)r, (int)g, (int)b, (int)a);
        }

        #endregion


        #region 重载运算符

        public static Color4 operator* (float factor, Color4 c) {
            return new Color4(c.R * factor, c.G * factor, c.B * factor, c.A);
        }

        public static Color4 operator* (Color4 c, float factor) {
            return factor * c;
        }

        //颜色混合
        public static Color4 operator *(Color4 lhs, Color rhs) {
            return new Color4(lhs.R * rhs.R, lhs.G * rhs.G, lhs.B * rhs.B, lhs.A * rhs.A);
        }

        //颜色叠加
        public static Color4 operator +(Color4 lhs, Color rhs) {
            return new Color4(lhs.R + rhs.R, lhs.G + rhs.G, lhs.B + rhs.B, 1.0f);
        }

        #endregion

        public static Color4 Lerp(Color4 min, Color4 max, float factor) {

            Color4 c = new Color4();

            c.R = MathUtility.Lerp(min.R, max.R, factor);
            c.G = MathUtility.Lerp(min.G, max.G, factor);
            c.B = MathUtility.Lerp(min.B, max.B, factor);
            //c.A = MathUtility.Lerp(min.A, max.A, factor);
            c.A = 1;

            return c;
        }

    }
}
