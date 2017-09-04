using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SoftwareRenderingEngine.Math3D;
using SoftwareRenderingEngine.Utility;

namespace SoftwareRenderingEngine.RenderStruct {

    //平行光模型
    public class Light {
        //灯光在世界空间下的坐标
        public Vector3 worldPos;

        //灯光的颜色
        public Color4 lightColor;

        //环境光的颜色
        public Color4 ambient;

        public Light(Vector3 pos, Color4 color, Color4 namibent) {
            worldPos = pos;
            lightColor = color;
            ambient = namibent;
        }

        public Color4 Lighting(Vector3 view, Vertex v) {

            //根据视线向量,光照向量计算最终的光照

            Vector3 worldNormal = v.normal;
            Vector3 worldLightDir = worldPos - new Vector3(0, 0, 0);
            Vector3 wolrdViewDir = view;

            Color4 diffuce = DiffuseColor(worldNormal, worldLightDir);
            Color4 specular = SpecularColor(view, worldLightDir);

            return ambient + diffuce + specular;
        }

        //根据HalfLambert模型计算漫反射光
        private Color4 DiffuseColor(Vector3 normal, Vector3 lightDir) {

            /*
            为什么要使用半Lambert漫反射模型?
            传统的Lambert漫反射模型
            color = lightColor * diffuseColor * max(0, dot(normal,lightDir))
            有个问题，那就是背对我们的所有点的color结果都是(0,0,0),没有任何明暗变化,背光区域看起来像是平面。

            改进的半Lambert算法改进了公式
            color = lightColor * diffuseColor * ( A * dot(normal, lightDir) + B)
            通常A = 0.5 B = 0.5
            这样把dot(normal,lightDir)的值域从[-1,1]映射到了[0,1],原先所有背对我们的结果都是0,现在有了变化
            这个模型无任何物理依据,仅仅是视觉效果不同
            */

            normal.Normalize();
            lightDir.Normalize();

            //Mesh的Material的漫反射光颜色,这里为了测试方便随意给定了一种颜色 
            Color4 diffuseColor = new Color4(0.5f, 0.5f, 0.5f, 1.0f);
            float halfLambert = Vector3.Dot(normal, lightDir) * 0.5f + 0.5f;

            return lightColor * diffuseColor * halfLambert;
        }

        //根据BlinnPhong模型计算高光反射
        private Color4 SpecularColor(Vector3 viewDir, Vector3 lightDir) {

            /*
            传统的Phong高光反射模型
            specularColor = lightColor * specularColor * pow( max(dot(viewDir,reflectDir),0), glossiness)
            改进后的BlinnPhong模型不再使用反射向量,性能更好些,效果也差不多
            而是引入了一个新的向量 h = normalize(viewDir + lightDir)
            specularColor = lightColor * specularColor * pow( max(dot(h,view),0), glossiness)
            */

            viewDir.Normalize();
            lightDir.Normalize();

            //Mesh的Material的高光反射的颜色,这里为了测试方便随意给定了一种颜色  
            Color4 specularColor = new Color4(0.5f, 0.5f, 0.5f, 1.0f);
            //Mesh的Material的高光反射系数,这里为了测试方便随意给定了一种颜色
            float glossiness = 0.5f;

            Vector3 halfDir = (viewDir + lightDir).Normalize();

            float factor = (float)Math.Pow(Math.Max(0, Vector3.Dot(halfDir, viewDir)), glossiness);

            return lightColor * specularColor * factor;
        }


    }
}
