using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EM
{
	public class Island
	{
		//下面这些看命名应该看得懂QAQ

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
		public Vector3 pos;

		public Island (Sky sky, int x,int z)
		{
            this.sky = sky;

            ix = x;
            iz = z;
            sx = x * 16;
            sz = z * 16;

            obj = new GameObject (x + "_" + z);
            obj.transform.position = new Vector3(sx, 0, sz);
            obj.transform.parent = this.sky.thisobj.transform;
            //this.obj.isStatic = true;
            mf = obj.AddComponent<MeshFilter> ();
            mr = obj.AddComponent<MeshRenderer> ();
            mr.material = Materials.mutou;
            mc = obj.AddComponent<MeshCollider> ();

            clods = new Clod[16, 128, 16];
            pos = new Vector3 (sx, 0, sz);
		}

		public void setClod(Clod newClod,int x,int y,int z){
            if (x < 0 || x >= 16 || y < 0 || y >= 128 || z < 0 || z >= 16)
                return;
            clods[x, y, z] = newClod;
            
            createMesh();
        }

		public Clod getClod(int x,int y,int z){
			if (x < 0 || x >= 16 || y < 0 || y >= 128 || z < 0 || z >= 16)
				return Clod.Air;
			return clods[x, y, z];
		}

        public void buildClod()
        {
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
            mesh = new Mesh();

            vertices = new List<Vector3>();
            uv = new List<Vector2>();

            for (int x = 0; x < 16; x++) {
				for (int y = 0; y < 16; y++) {
					for (int z = 0; z < 16; z++) {
                        clods[x, y, z].render(this, sx + x, y, sz + z);
					}
				}
			}

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

