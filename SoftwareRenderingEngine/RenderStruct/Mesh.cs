using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareRenderingEngine.Math3D;


namespace SoftwareRenderingEngine.RenderStruct {

    class Mesh {


        //顶点数组
        public Vertex[] vertices;

        //顶点索引数组,用来复用顶点
        public int[,] indices;

        /*
         *  以正方形为例
         *  
         *  (0)A -------------B(1)
         *      |             |
         *      |             |
         *      |             |
         *      |             |
         *   (2)C-------------D(3)
         *   
         *   那么 
         *   vertices = {A, B, C, D}
         *   indices  = { {0,1,2},
         *                {1,2,3} };
         *   可以根据indices来new Triangle
         * 
         * 
         */

        public Mesh() {

        }

        /*
        public Mesh(Vector3[] positions, int[,] indexs, Point2[] uvs, Color4[] colors, Vector3[] normals) {

            indices = indexs;

            vertices = new Vertex[positions.Length];

            for(int i = 0; i < positions.Length; ++i) {

                Vertex v = new Vertex();

                v.position = positions[i];
                v.u = uvs[i].x;
                v.v = uvs[i].y;
                v.color = colors[i];
                v.normal = normals[i];
                
                vertices[i] = v;
            }
        }

        */
    }

}
