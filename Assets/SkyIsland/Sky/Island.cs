using UnityEngine;
using System;
using System.Collections.Generic;

namespace SkyIsland
{
	public class Island
	{
		//各种变量
		public GameObject obj;

        public Mesh mesh;
        public MeshFilter mf;
        public MeshRenderer mr;
        public MeshCollider mc;

        //依次为岛屿坐标X,Y，天空坐标X,Z
		public int ix, iz, sx, sz;

		public List<Vector3> vertices;
		public List<Vector2> uv;
        public List<int> triangles;

		public Clod[,,] clods;
        public Sky sky;
        //这个变量毫无用处。。我貌似要用它来做加载动画？
		public Vector3 pos;
        
        /// <summary>
        /// 创建一个岛屿
        /// </summary>
        /// <param name="sky">天空指针</param>
        /// <param name="x">岛屿坐标X</param>
        /// <param name="z">岛屿坐标Y</param>
		public Island (Sky sky, int x,int z)
		{
            //初始化
            this.sky = sky;

            ix = x;
            iz = z;
            sx = x * 16;
            sz = z * 16;

            //搞对象。。别歪
            obj = new GameObject (x + "_" + z);
            obj.transform.position = new Vector3(sx, 0, sz);
            obj.transform.parent = this.sky.thisobj.transform;

            //这个要不要加呢？貌似没什么变化
            //this.obj.isStatic = true;

            //网格网格！！
            mf = obj.AddComponent<MeshFilter> ();
            mr = obj.AddComponent<MeshRenderer> ();

            //设置Material，以后肯定不是这样QAQ
            mr.material = Materials.terrain;

            //加入网格碰撞箱
            mc = obj.AddComponent<MeshCollider> ();

            //初始化泥块
            clods = new Clod[16, 128, 16];

            //上文提到，毫无用处。。
            obj.transform.position = pos = new Vector3(sx, 0, sz);
		}

        /// <summary>
        /// 我抄的，待我研究一番。。
        /// </summary>
        /// <param name="pos">方块坐标（c++表示占空间。。）</param>
        /// <param name="offset">生成偏移值</param>
        /// <param name="scale">放大倍数（放大化世界233）</param>
        /// <returns></returns>
        public static float CalculateNoiseValue(Vector3 pos, Vector3 offset, float scale)
        {

            float noiseX = Mathf.Abs((pos.x + offset.x) * scale);
            float noiseY = Mathf.Abs((pos.y + offset.y) * scale);
            float noiseZ = Mathf.Abs((pos.z + offset.z) * scale);

            return Mathf.Max(0, Noise.Generate(noiseX, noiseY, noiseZ));

        }

        /// <summary>
        /// 依然抄的。。
        /// </summary>
        /// <param name="pos">方块坐标</param>
        /// <returns></returns>
        public static Clod GetTheoreticalByte(Vector3 pos)
        {
            UnityEngine.Random.seed = 720;

            Vector3 grain0Offset = new Vector3(UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000);
            Vector3 grain1Offset = new Vector3(UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000);
            Vector3 grain2Offset = new Vector3(UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000, UnityEngine.Random.value * 10000);

            return GetTheoreticalByte(pos, grain0Offset, grain1Offset, grain2Offset);

        }

        /// <summary>
        /// 还是抄的。。
        /// </summary>
        /// <param name="pos">依然是方块坐标</param>
        /// <param name="offset0">偏移量1</param>
        /// <param name="offset1">偏移量2</param>
        /// <param name="offset2">偏移量3</param>
        /// <returns></returns>
        public static Clod GetTheoreticalByte(Vector3 pos, Vector3 offset0, Vector3 offset1, Vector3 offset2)
        {

            float heightBase = 16;
            float maxHeight = 48;
            float heightSwing = maxHeight - heightBase;

            Clod clod = Clod.Air;

            float clusterValue = CalculateNoiseValue(pos, offset1, 0.01f);
            float blobValue = CalculateNoiseValue(pos, offset1, 0.03f);
            float mountainValue = CalculateNoiseValue(pos, offset0, 0.02f);
            if ((mountainValue == 0) && (blobValue < 0.72f))
                clod = Clod.Air;
            else if (clusterValue > 0.9f)
                clod = Clod.Grass;
            else if (clusterValue > 0.5f)
                clod = Clod.Soil;
            else if (clusterValue > 0.1f)
                clod = Clod.Stone;

            mountainValue = Mathf.Sqrt(mountainValue);

            mountainValue *= heightSwing;
            mountainValue += heightBase;

            mountainValue += (blobValue * 10) - 5f;



            if (mountainValue >= pos.y)
                return clod;
            return Clod.Air;
        }

        /// <summary>
        /// 加载泥块
        /// </summary>
        public void buildClod()
        {
            //又是一个小朋友都会的。。
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        clods[x, y, z] = GetTheoreticalByte(new Vector3(x, y, z) + new Vector3(sx, 0, sz));
                    }
                }
            }
        }

        /// <summary>
        /// 创建网格
        /// </summary>
		public void createMesh(){
            //重新new个mesh
            mesh = new Mesh();

            //同上
            vertices = new List<Vector3>();
            uv = new List<Vector2>();
            triangles = new List<int>();

            //添加到顶点数组
            for (int x = 0; x < 16; x++) {
				for (int y = 0; y < 128; y++) {
					for (int z = 0; z < 16; z++) {
                        clods[x, y, z].createMesh(this, sx + x, y, sz + z);
					}
				}
			}

            //设置ing
            mesh.vertices = vertices.ToArray();
            mesh.uv = uv.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mf.mesh = mesh;

            mc.sharedMesh = null;
            mc.sharedMesh = mesh;
            
		}

        //以下代码以后会移动到IslandRenderer去。。

        //材质偏移值
        private float offsetX = 0f;
        private float offsetY = 0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setOffset(float x,float y)
        {
            offsetX = x;
            offsetY = y;
        }

        //材质6个面
        private string texa = "Stone";
        private string texb = "Stone";
        private string texc = "Stone";
        private string texd = "Stone";
        private string texe = "Stone";
        private string texf = "Stone";

        /// <summary>
        /// 设置材质
        /// </summary>
        /// <param name="tex">6个面的材质</param>
        public void setTexture(string tex)
        {
            texa = tex;
            texb = tex;
            texc = tex;
            texd = tex;
            texe = tex;
            texf = tex;
        }

        /// <summary>
        /// 设置材质
        /// </summary>
        /// <param name="tex1">前后面材质</param>
        /// <param name="tex2">上下面材质</param>
        /// <param name="tex3">右左面材质</param>
        public void setTexture(string tex1,string tex2,string tex3)
        {
            texa = tex1;
            texb = tex1;
            texc= tex2;
            texd= tex2;
            texe = tex3;
            texf = tex3;
        }

        /// <summary>
        /// 设置材质
        /// </summary>
        /// <param name="tex1">前面材质</param>
        /// <param name="tex2">后面材质</param>
        /// <param name="tex3">上面材质</param>
        /// <param name="tex4">下面材质</param>
        /// <param name="tex5">右面材质</param>
        /// <param name="tex6">左面材质</param>
        public void setTexture(string tex1, string tex2, string tex3,string tex4,string tex5,string tex6)
        {
            texa = tex1;
            texb = tex2;
            texc = tex3;
            texd = tex4;
            texe = tex5;
            texf = tex6;
        }

        /// <summary>
        /// 添加一个盒子进岛屿的网格（近面剔除）
        /// </summary>
        /// <param name="x">偏移X</param>
        /// <param name="y">偏移Y</param>
        /// <param name="z">偏移Z</param>
        /// <param name="w">大小W</param>
        /// <param name="h">大小H</param>
        /// <param name="d">大小D</param>
        public void addBoxToMesh(float x, float y, float z, float w, float h, float d)
        {
            if (sky.getClod((int)x, (int)y, (int)z).isNormal)
            {
                if (!sky.getClod((int)x, (int)y, (int)z + 1).isNormal)
                {
                    setOffset(Materials.getClodTextureOffsetX(texa), Materials.getClodTextureOffsetY(texa));
                    addFaceToMesh(x - sx, y, z - sz, w, h, d, 0);
                }

                if (!sky.getClod((int)x, (int)y, (int)z - 1).isNormal)
                {
                    setOffset(Materials.getClodTextureOffsetX(texb), Materials.getClodTextureOffsetY(texb));
                    addFaceToMesh(x - sx, y, z - sz, w, h, d, 1);
                }

                if (!sky.getClod((int)x, (int)y + 1, (int)z).isNormal)
                {
                    setOffset(Materials.getClodTextureOffsetX(texc), Materials.getClodTextureOffsetY(texc));
                    addFaceToMesh(x - sx, y, z - sz, w, h, d, 2);
                }

                if (!sky.getClod((int)x, (int)y - 1, (int)z).isNormal)
                {
                    setOffset(Materials.getClodTextureOffsetX(texd), Materials.getClodTextureOffsetY(texd));
                    addFaceToMesh(x - sx, y, z - sz, w, h, d, 3);
                }

                if (!sky.getClod((int)x + 1, (int)y, (int)z).isNormal)
                {
                    setOffset(Materials.getClodTextureOffsetX(texe), Materials.getClodTextureOffsetY(texe));
                    addFaceToMesh(x - sx, y, z - sz, w, h, d, 4);
                }

                if (!sky.getClod((int)x - 1, (int)y, (int)z).isNormal)
                {
                    setOffset(Materials.getClodTextureOffsetX(texf), Materials.getClodTextureOffsetY(texf));
                    addFaceToMesh(x - sx, y, z - sz, w, h, d, 5);
                }
            }else
            {
                setOffset(Materials.getClodTextureOffsetX(texa), Materials.getClodTextureOffsetY(texa));
                addFaceToMesh(x - sx, y, z - sz, w, h, d, 0);

                setOffset(Materials.getClodTextureOffsetX(texb), Materials.getClodTextureOffsetY(texb));
                addFaceToMesh(x - sx, y, z - sz, w, h, d, 1);

                setOffset(Materials.getClodTextureOffsetX(texc), Materials.getClodTextureOffsetY(texc));
                addFaceToMesh(x - sx, y, z - sz, w, h, d, 2);

                setOffset(Materials.getClodTextureOffsetX(texd), Materials.getClodTextureOffsetY(texd));
                addFaceToMesh(x - sx, y, z - sz, w, h, d, 3);

                setOffset(Materials.getClodTextureOffsetX(texe), Materials.getClodTextureOffsetY(texe));
                addFaceToMesh(x - sx, y, z - sz, w, h, d, 4);

                setOffset(Materials.getClodTextureOffsetX(texf), Materials.getClodTextureOffsetY(texf));
                addFaceToMesh(x - sx, y, z - sz, w, h, d, 5);
            }
        }

        //这个不同于我改过的ModelPart，它是居中的

        /// <summary>
        /// 添加一个面进岛屿网格
        /// </summary>
        /// <param name="x">偏移X</param>
        /// <param name="y">偏移Y</param>
        /// <param name="z">偏移Z</param>
        /// <param name="w">大小W</param>
        /// <param name="h">大小H</param>
        /// <param name="d">大小D</param>
        /// <param name="face">面Index</param>
        public void addFaceToMesh(float x, float y, float z, float w, float h, float d, int face)
        {
            int index = vertices.Count;
            float a = ((float)Materials.clodWidth / Materials.clodTextureWidth);
            float b = ((float)Materials.clodHeight / Materials.clodTextureHeight);
            float i = Materials.clodWidth;
            float j = Materials.clodHeight;
            float e = Materials.clodTextureWidth;
            float f = Materials.clodTextureHeight;

            if (face == 0)
            {
                uv.Add(new Vector2(((x + offsetX)), ((y + offsetY))));
                uv.Add(new Vector2(((x + (a * w) + offsetX)), ((y + offsetY))));
                uv.Add(new Vector2(((x + offsetX)), ((y + (b * w) + offsetY))));
                uv.Add(new Vector2(((x + (a * w) + offsetX)), ((y + (b * w) + offsetY))));

                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
            }
            if (face == 1)
            {
                
                uv.Add(new Vector2(((x + offsetX)), ((y + (b * w) + offsetY))));
                uv.Add(new Vector2(((x + (a * w) + offsetX)), ((y + (b * w) + offsetY))));
                uv.Add(new Vector2(((x + offsetX)), ((y + offsetY))));
                uv.Add(new Vector2(((x + (a * w) + offsetX)), ((y + offsetY))));

                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
            }
            if (face == 2)
            {
                uv.Add(new Vector2((x + offsetX), (y + offsetY)));
                uv.Add(new Vector2((x + (a * w) + offsetX), (y + offsetY)));
                uv.Add(new Vector2((x + offsetX), (y + (a * d) + offsetY)));
                uv.Add(new Vector2((x + (a * w) + offsetX), (y + (a * d) + offsetY)));

                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
            }
            if (face == 3)
            {
                uv.Add(new Vector2((x + offsetX), (y + (a * d) + offsetY)));
                uv.Add(new Vector2((x + (a * w) + offsetX), (y + (a * d) + offsetY)));
                uv.Add(new Vector2((x + offsetX), (y + offsetY)));
                uv.Add(new Vector2((x + (a * w) + offsetX), (y + offsetY)));

                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
            }
            if (face == 4)
            {
                uv.Add(new Vector2((x + offsetX), (y + offsetY)));
                uv.Add(new Vector2((x + (a * d) + offsetX), (y + offsetY)));
                uv.Add(new Vector2((x + offsetX), (y + (a * h) + offsetY)));
                uv.Add(new Vector2((x + (a * d) + offsetX), (y + (a * h) + offsetY)));

                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
            }
            if (face == 5)
            {
                uv.Add(new Vector2((x + offsetX), (y + (a * h) + offsetY)));
                uv.Add(new Vector2((x + (a * d) + offsetX), (y + (a * h) + offsetY)));
                uv.Add(new Vector2((x + offsetX), (y + offsetY)));
                uv.Add(new Vector2((x + (a * d) + offsetX), (y + offsetY)));

                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
            }

            triangles.Add(index + 0);
            triangles.Add(index + 3);
            triangles.Add(index + 1);
            triangles.Add(index + 0);
            triangles.Add(index + 2);
            triangles.Add(index + 3);

        }
    }
}

