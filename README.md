# 概述

使用C#语言实现的基于固定管线的软件渲染器。

* 标准 D3D 坐标模型，左手系加 WORLD / VIEW / PROJECTION 三矩阵。
* 线框模式的绘制使用Bresenham算法
* 相机基于UVN模型
* 灯光基于平行光，使用HalfLambert计算漫反射，BlinnPhong计算高光反射
* 实现简单背面消隐BackFaceCulling
* 深度缓冲
* 透视投影矫正


# 目录结构

直接从MainWindow.cs的代码开始读，有详细的注释。

Math3D文件夹:

 * Math3D
    * Color4 :       颜色类,可以方便的和System.Drawing.Color进行转换。
    * Matrix4X4 : 4X4矩阵，在该类的static方法中实现旋转,平移,放缩,透视投影,相机变换矩阵的构造。
    * Vector3 : 三维向量类.

RenderStruct文件夹:

 * RenderStruct
   * Camera : 基于UVN模型的相机类
   * Light : 基于平行光的光照类(未完成)
   * Mesh : 模型网格类,主要包含vertexList和indicesList
   * Transform : 静态类,包含坐标变换的变换算法
   * Vertex : 顶点类,包含顶点坐标、颜色、法线、UV坐标
   * Texture : 贴图类(待添加)。
 
Utility文件夹:

* Utility
  * MathUtility : 静态类,包含一些数学方法
  * RenderUtiltiy : 静态类, 包含所有的渲染算法，包括Bresenham绘线算法，光栅化算法，扫描线填充算法


TestData文件夹:

* TestData
  * Cube : 立方体测试数据
  * Primitive : 三角形测试数据
  * Quad ： 平面测试数据
