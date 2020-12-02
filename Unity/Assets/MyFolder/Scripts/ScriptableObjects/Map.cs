using System;
using UnityEngine;

namespace Unity1Week.ScriptableObjects
{
    public class Map : ScriptableObject, IDisposable
    {
        public Material ChipMaterial;
        public float[] ChipTemperatures;

        [field:NonSerialized] public GraphicsBuffer Buffer { get; private set; }
        [field:NonSerialized] public int ChipCount { get; private set; }
        [field:NonSerialized] public Bounds Bounds { get; private set; }

        private static readonly int kinds = Shader.PropertyToID("kinds");
        private static readonly int widthCountShift = Shader.PropertyToID("_WidthCountShift");

        public void Initialize(int shiftCount)
        {
            ChipCount = 1 << (shiftCount << 1);
            Buffer?.Dispose();
            Buffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, ChipCount, sizeof(int));
            ChipMaterial.SetBuffer(kinds, Buffer);
            ChipMaterial.SetInt(widthCountShift, shiftCount);
            Bounds = new Bounds(Vector3.zero, Vector3.one * (1 << (5 + shiftCount)));
        }

        public void Dispose()
        {
            Buffer?.Dispose();
            Buffer = null;
        }
    }
}