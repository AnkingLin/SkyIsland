using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EM
{
    public class Renderer
    {
        public static float offsetX = 0;
        public static float offsetY = 0;

        public static void setOffset(float x,float y)
        {
            offsetX = x;
            offsetY = y;
        }

        public static void addBoxToMesh(List<Vector3> vertices, List<Vector2> uv, List<int> triangles, Vector3 pos, Vector3 size)
        {
            addFaceToMesh(vertices, uv, triangles, pos.x, pos.y, pos.z, size.x, size.y, size.z, 0);

            addFaceToMesh(vertices, uv, triangles, pos.x, pos.y, pos.z, size.x, size.y, size.z, 1);

            addFaceToMesh(vertices, uv, triangles, pos.x, pos.y, pos.z, size.x, size.y, size.z, 2);

            addFaceToMesh(vertices, uv, triangles, pos.x, pos.y, pos.z, size.x, size.y, size.z, 3);

            addFaceToMesh(vertices, uv, triangles, pos.x, pos.y, pos.z, size.x, size.y, size.z, 4);

            addFaceToMesh(vertices, uv, triangles, pos.x, pos.y, pos.z, size.x, size.y, size.z, 5);
        }

        //从Island复制~粘贴~
        public static void addFaceToMesh(List<Vector3> vertices, List<Vector2> uv, List<int> triangles, float x, float y, float z, float w, float h, float d, int face)
        {
            int index = vertices.Count;

            if (face == 0)
            {
                uv.Add(new Vector2((x + 0) + offsetX, (y + 0) + offsetY));
                uv.Add(new Vector2((x + w) + offsetX, (y + 0) + offsetY));
                uv.Add(new Vector2((x + 0) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w) + offsetX, (y + h) + offsetY));

                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
            }
            if (face == 1)
            {
                uv.Add(new Vector2((x + w) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w + d) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w) + offsetX, (y + 0) + offsetY));
                uv.Add(new Vector2((x + w + d) + offsetX, (y + 0) + offsetY));

                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
            }
            if (face == 2)
            {
                uv.Add(new Vector2((x + w) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w + d) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w) + offsetX, (y + h + d) + offsetY));
                uv.Add(new Vector2((x + w + d) + offsetX, (y + h + d) + offsetY));

                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
            }
            if (face == 3)
            {
                uv.Add(new Vector2((x + w + d) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w + d + w) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + w + d) + offsetX, (y + h + d) + offsetY));
                uv.Add(new Vector2((x + w + d + w) + offsetX, (y + h + d) + offsetY));

                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(-(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
            }
            if (face == 4)
            {
                uv.Add(new Vector2((x + d + w) + offsetX, (y + 0) + offsetY));
                uv.Add(new Vector2((x + d + w + d) + offsetX, (y + 0) + offsetY));
                uv.Add(new Vector2((x + d + w) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + d + w + d) + offsetX, (y + h) + offsetY));

                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, -(h / 2) + y, +(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, -(d / 2) + z));
                vertices.Add(new Vector3(+(w / 2) + x, +(h / 2) + y, +(d / 2) + z));
            }
            if (face == 5)
            {
                uv.Add(new Vector2((x + d + w + d + w) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + d + w + d) + offsetX, (y + h) + offsetY));
                uv.Add(new Vector2((x + d + w + d + w) + offsetX, (y + 0) + offsetY));
                uv.Add(new Vector2((x + d + w + d) + offsetX, (y + 0) + offsetY));

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
