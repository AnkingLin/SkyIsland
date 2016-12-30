using System.Collections.Generic;
using UnityEngine;

namespace SkyIsland
{
    public class ModelPart
    {
        //一大堆看得懂的变量
        public GameObject thisobj;
        public Ling ling;
        public float offsetX;
        public float offsetY;
        public Texture tex;
        public List<Vector3> vertices;
        public List<Vector2> uv;
        public List<int> triangles;

        public Vector3 rotPoint;
        public Mesh mesh;
        public MeshFilter mf;

        /// <summary>
        /// 创建一个模型块
        /// </summary>
        /// <param name="ling">灵指针</param>
        /// <param name="name">块名字</param>
        public ModelPart(Ling ling, string name)
        {
            thisobj = new GameObject(name);
            thisobj.transform.parent = ling.gameObject.gameObject.transform;
            thisobj.transform.position = ling.gameObject.transform.position;
            this.ling = ling;

            rotPoint = thisobj.transform.position;
            mf = thisobj.AddComponent<MeshFilter>();
            thisobj.AddComponent<MeshRenderer>().material = ling.gameObject.GetComponent<MeshRenderer>().material;
            tex = thisobj.GetComponent<MeshRenderer>().material.mainTexture;

            offsetX = 0;
            offsetY = 0;
            vertices = new List<Vector3>();
            uv = new List<Vector2>();
            triangles = new List<int>();
        }

        /// <summary>
        /// 这个有问题。。。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void setPos(int x, int y, int z)
        {
            thisobj.transform.position = ling.gameObject.transform.position + new Vector3(x / 32f, y / 32f, z / 32f);
        }
        
        /// <summary>
        /// 设置旋转坐标
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public void setRotPoint(int x,int y,int z)
        {
            rotPoint = new Vector3(x / 32f, (y / 32f), z / 32f);
        }

        /// <summary>
        /// 获取旋转坐标
        /// </summary>
        /// <returns>旋转坐标</returns>
        public Vector3 getRotPoint()
        {
            return ling.gameObject.transform.position + rotPoint;
        }

        /// <summary>
        /// 设置材质偏移值
        /// </summary>
        /// <param name="x">偏移值X</param>
        /// <param name="y">偏移值Y</param>
        public void setOffset(int x,int y)
        {
            offsetX = ((float)x / tex.width);
            offsetY = ((float)y / tex.height);
        }

        /// <summary>
        /// 创建网格
        /// </summary>
        public void createMesh()
        {
            //重新new个mesh
            mesh = new Mesh();

            //设置ing
            mesh.vertices = vertices.ToArray();
            mesh.uv = uv.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            mf.mesh = mesh;
        }

        /// <summary>
        /// 添加一个盒子到网格
        /// </summary>
        /// <param name="x">偏移值X</param>
        /// <param name="y">偏移值Y</param>
        /// <param name="z">偏移值Z</param>
        /// <param name="w">大小W</param>
        /// <param name="h">大小H</param>
        /// <param name="d">大小D</param>
        /// <param name="expand">放大倍数（这是个好东西）</param>
        public void addBoxToMesh(int x, int y, int z, int w, int h, int d, int expand)
        {
            addFaceToMesh((x / 32f), (y / 32f), (z / 32f), (w / 32f), (h / 32f), (d / 32f), 0, expand);
            addFaceToMesh((x / 32f), (y / 32f), (z / 32f), (w / 32f), (h / 32f), (d / 32f), 1, expand);
            addFaceToMesh((x / 32f), (y / 32f), (z / 32f), (w / 32f), (h / 32f), (d / 32f), 2, expand);
            addFaceToMesh((x / 32f), (y / 32f), (z / 32f), (w / 32f), (h / 32f), (d / 32f), 3, expand);
            addFaceToMesh((x / 32f), (y / 32f), (z / 32f), (w / 32f), (h / 32f), (d / 32f), 4, expand);
            addFaceToMesh((x / 32f), (y / 32f), (z / 32f), (w / 32f), (h / 32f), (d / 32f), 5, expand);
        }

        /// <summary>
        /// 添加一个面到网格
        /// </summary>
        /// <param name="x">偏移值X</param>
        /// <param name="y">偏移值Y</param>
        /// <param name="z">偏移值Z</param>
        /// <param name="w">大小W</param>
        /// <param name="h">大小H</param>
        /// <param name="d">大小D</param>
        /// <param name="face">面Index</param>
        /// <param name="expand">放大倍数（这是个好东西）</param>
        public void addFaceToMesh(float x, float y, float z, float w, float h, float d, float face, int expand)
        {
            int index = vertices.Count;
            float www = ((w * 32f) + (h * 32f)) * 2;
            www = www * (tex.width / www);
            float hhh = (d * 32f) + (h * 32f);
            hhh = hhh * (tex.height / hhh);

            float ww = (w * 32f / www);
            float hh = (h * 32f / hhh);
            float dd = (d * 32f / hhh);
            float ddd = (d * 32f / www);

            //up
            if (face == 0)
            {
                uv.Add(new Vector2(0f + offsetX, 0f + hh + offsetY));
                uv.Add(new Vector2(ww + offsetX, 0f + hh + offsetY));
                uv.Add(new Vector2(0f + offsetX, dd + hh + offsetY));
                uv.Add(new Vector2(ww + offsetX, dd + hh + offsetY));

                w *= expand;
                h *= expand;
                d *= expand;
                vertices.Add(new Vector3(w+x, h+y, d+z));
                vertices.Add(new Vector3(x, h+y, d+z));
                vertices.Add(new Vector3(w+x, h+y, z));
                vertices.Add(new Vector3(x, h+y, z));
            }
            //down
            else if (face == 1)
            {
                uv.Add(new Vector2(0f + ww + offsetX, dd + hh + offsetY));
                uv.Add(new Vector2(ww + ww + offsetX, dd + hh + offsetY));
                uv.Add(new Vector2(0f + ww + offsetX, 0f + hh + offsetY));
                uv.Add(new Vector2(ww + ww + offsetX, 0f + hh + offsetY));

                w *= expand;
                h *= expand;
                d *= expand;
                vertices.Add(new Vector3(w+x, y, z));
                vertices.Add(new Vector3(x, y, z));
                vertices.Add(new Vector3(w+x, y, d+z));
                vertices.Add(new Vector3(x, y, d+z));
            }
            //left
            else if (face == 2)
            {
                uv.Add(new Vector2(0f + offsetX, hh + offsetY));
                uv.Add(new Vector2(0f + ddd + offsetX, hh + offsetY));
                uv.Add(new Vector2(0f + offsetX, 0f + offsetY));
                uv.Add(new Vector2(0f + ddd + offsetX, 0f + offsetY));

                w *= expand;
                h *= expand;
                d *= expand;
                vertices.Add(new Vector3(x, h+y, z));
                vertices.Add(new Vector3(x, h+y, d+z));
                vertices.Add(new Vector3(x, y, z));
                vertices.Add(new Vector3(x, y, d+z));
            }
            //font
            else if (face == 3)
            {
                uv.Add(new Vector2(ddd + offsetX, 0f + offsetY));
                uv.Add(new Vector2(ddd + ww + offsetX, 0f + offsetY));
                uv.Add(new Vector2(ddd + offsetX, hh + offsetY));
                uv.Add(new Vector2(ddd + ww + offsetX, hh + offsetY));

                w *= expand;
                h *= expand;
                d *= expand;
                vertices.Add(new Vector3(w+x, y, d+z));
                vertices.Add(new Vector3(x, y, d+z));
                vertices.Add(new Vector3(w+x, h+y, d+z));
                vertices.Add(new Vector3(x, h+y, d+z));
            }
            //right
            else if (face == 4)
            {
                uv.Add(new Vector2(ddd + ww + ddd + offsetX, 0f + offsetY));
                uv.Add(new Vector2(ddd + ww + offsetX, 0f + offsetY));
                uv.Add(new Vector2(ddd + ww + ddd + offsetX, hh + offsetY));
                uv.Add(new Vector2(ddd + ww + offsetX, hh + offsetY));

                w *= expand;
                h *= expand;
                d *= expand;
                vertices.Add(new Vector3(w+x, y, z));
                vertices.Add(new Vector3(w+x, y, d+z));
                vertices.Add(new Vector3(w+x, h+y, z));
                vertices.Add(new Vector3(w+x, h+y, d+z));
            }
            //back
            else if (face == 5)
            {
                uv.Add(new Vector2(ddd + ww + ddd + offsetX, hh + offsetY));
                uv.Add(new Vector2(ddd + ww + ddd + ww + offsetX, hh + offsetY));
                uv.Add(new Vector2(ddd + ww + ddd + offsetX, 0f + offsetY));
                uv.Add(new Vector2(ddd + ww + ddd + ww + offsetX, 0f + offsetY));

                w *= expand;
                h *= expand;
                d *= expand;
                vertices.Add(new Vector3(w+x, h+y, z));
                vertices.Add(new Vector3(x, h+y, z));
                vertices.Add(new Vector3(w+x, y, z));
                vertices.Add(new Vector3(x, y, z));
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
