using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week
{
    public static class CreateMeshHelper
    {
        private static readonly List<Vector3> vertList = new List<Vector3>(16);
        private static readonly List<Vector2> uvList = new List<Vector2>(16);

        public static Mesh FromSprite(this Sprite sprite, float height = 0.01f)
        {
            var mesh = new Mesh();
            var verts = sprite.vertices;
            vertList.Clear();
            for (var j = 0; j < verts.Length; j++)
                vertList.Add(new Vector3(verts[j].x, height, verts[j].y));
            mesh.SetVertices(vertList);
            var tris = sprite.triangles;
            var triangles = new int[tris.Length];
            for (var j = 0; j < tris.Length; j++)
                triangles[j] = tris[j];
            mesh.SetTriangles(triangles, 0);
            var uvs = sprite.uv;
            uvList.Clear();
            for (var j = 0; j < uvs.Length; j++)
                uvList.Add(new Vector2(uvs[j].x, uvs[j].y));
            mesh.SetUVs(0, uvList);
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}