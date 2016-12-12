using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
	public class Island
	{
		//各种变量
		public GameObject obj;

        public Mesh mesh;
        public MeshFilter mf;
        public MeshRenderer mr;
        public MeshCollider mc;

		public int ix, iz, sx, sz;

		public List<Vector3> vertices;
		public List<Vector2> uv;
		public int[] triangles;

		public Clod[,,] clods;
        public Sky sky;
        //这个变量毫无用处。。我貌似要用它来做加载动画？
		public Vector3 pos;
        
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
            mr.material = Materials.mutou;

            //加入网格碰撞箱
            mc = obj.AddComponent<MeshCollider> ();

            //初始化泥块
            clods = new Clod[16, 128, 16];

            //上文提到，毫无用处。。
            obj.transform.position = pos = new Vector3(sx, 0, sz);
		}

        //设置泥块
		public void setClod(Clod newClod,int x,int y,int z){
            //超出限制当然要踢出！当然这不可能！
            if (x < 0 || x >= 16 || y < 0 || y >= 128 || z < 0 || z >= 16)
                return;

            //设置。。
            clods[x, y, z] = newClod;

            //更新网格，貌似要删掉？
            createMesh();
        }

		public Clod getClod(int x,int y,int z){
            //同上
			if (x < 0 || x >= 16 || y < 0 || y >= 128 || z < 0 || z >= 16)
				return Clod.Air;
            //不多说。。
			return clods[x, y, z];
		}

        public void buildClod()
        {
            //又是一个小朋友都会的。。
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 128; y++)
                {
                    for (int z = 0; z < 16; z++)
                    {
                        if (y < 5)
                        {
                            clods[x, y, z] = Clod.Stone;
                        }
                        else
                        {
                            clods[x, y, z] = Clod.Air;
                        }
                    }
                }
            }
        }

		public void createMesh(){
            //重新new个mesh
            mesh = new Mesh();

            //同上
            vertices = new List<Vector3>();
            uv = new List<Vector2>();

            //添加到顶点数组
            for (int x = 0; x < 16; x++) {
				for (int y = 0; y < 16; y++) {
					for (int z = 0; z < 16; z++) {
                        clods[x, y, z].render(this, sx + x, y, sz + z);
					}
				}
			}

            //算三角
			int num = 0;
            triangles = new int[vertices.Count / 2 * 3];

			for (int i = 0; i < vertices.Count; i += 4)
			{
				triangles[num++] = i;
				triangles[num++] = i + 3;
				triangles[num++] = i + 1;
				triangles[num++] = i;
				triangles[num++] = i + 2;
				triangles[num++] = i + 3;
			}
            
            //设置ing
            mesh.vertices = vertices.ToArray();
            mesh.uv = uv.ToArray();
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mf.mesh = mesh;

            mc.sharedMesh = null;
            mc.sharedMesh = mesh;
            
		}

        public void addBoxToMesh(Vector3 pos, Vector3 size)
        {
            //近面剔除。。后来会改的
            if (!sky.getClod((int)pos.x, (int)pos.y, (int)pos.z + 1).isSolid)
                addFaceToMesh(pos.x - sx, pos.y, pos.z - sz, size.x, size.y, size.z, 0);

            if (!sky.getClod((int)pos.x, (int)pos.y, (int)pos.z - 1).isSolid)
                addFaceToMesh(pos.x - sx, pos.y, pos.z - sz, size.x, size.y, size.z, 1);

            if (!sky.getClod((int)pos.x, (int)pos.y + 1, (int)pos.z).isSolid)
                addFaceToMesh(pos.x - sx, pos.y, pos.z - sz, size.x, size.y, size.z, 2);

            if (!sky.getClod((int)pos.x, (int)pos.y - 1, (int)pos.z).isSolid)
                addFaceToMesh(pos.x - sx, pos.y, pos.z - sz, size.x, size.y, size.z, 3);

            if (!sky.getClod((int)pos.x + 1, (int)pos.y, (int)pos.z).isSolid)
                addFaceToMesh(pos.x - sx, pos.y, pos.z - sz, size.x, size.y, size.z, 4);

            if (!sky.getClod((int)pos.x - 1, (int)pos.y, (int)pos.z).isSolid)
                addFaceToMesh(pos.x - sx, pos.y, pos.z - sz, size.x, size.y, size.z, 5);
        }

		private void addFaceToMesh(float x, float y, float z, float w, float h, float d, int face)
		{
            //一大堆懒得缩写代码的东西。。
			if (face == 0)
			{
				uv.Add(new Vector2((x + 0) * 32f / 32f, (y + 0) * 32f / 32f));
				uv.Add(new Vector2((x + w) * 32f / 32f, (y + 0) * 32f / 32f));
				uv.Add(new Vector2((x + 0) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w) * 32f / 32f, (y + h) * 32f / 32f));

				vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
			}
			if (face == 1)
			{
				uv.Add(new Vector2((x + w) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w + d) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w) * 32f / 32f, (y + 0) * 32f / 32f));
				uv.Add(new Vector2((x + w + d) * 32f / 32f, (y + 0) * 32f / 32f));

				vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
			}
			if (face == 2)
			{
				uv.Add(new Vector2((x + w) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w + d) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w) * 32f / 32f, (y + h + d) * 32f / 32f));
				uv.Add(new Vector2((x + w + d) * 32f / 32f, (y + h + d) * 32f / 32f));

				vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
			}
			if (face == 3)
			{
				uv.Add(new Vector2((x + w + d) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w + d + w) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + w + d) * 32f / 32f, (y + h + d) * 32f / 32f));
				uv.Add(new Vector2((x + w + d + w) * 32f / 32f, (y + h + d) * 32f / 32f));

				vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
			}
			if (face == 4)
			{
				uv.Add(new Vector2((x + d + w) * 32f / 32f, (y + 0) * 32f / 32f));
				uv.Add(new Vector2((x + d + w + d) * 32f / 32f, (y + 0) * 32f / 32f));
				uv.Add(new Vector2((x + d + w) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + d + w + d) * 32f / 32f, (y + h) * 32f / 32f));

				vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
			}
			if (face == 5)
			{
				uv.Add(new Vector2((x + d + w + d + w) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + d + w + d) * 32f / 32f, (y + h) * 32f / 32f));
				uv.Add(new Vector2((x + d + w + d + w) * 32f / 32f, (y + 0) * 32f / 32f));
				uv.Add(new Vector2((x + d + w + d) * 32f / 32f, (y + 0) * 32f / 32f));

				vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
				vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
			}
		}
	}
}

