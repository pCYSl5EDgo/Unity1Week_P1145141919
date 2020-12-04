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
        
        [field:NonSerialized] public int ShiftCount { get; private set; }
        
        [field:NonSerialized] public float MinInclusive { get; private set; }
        
        [field:NonSerialized] public float MaxExclusive { get; private set; }

        [field: NonSerialized] public float ChipSize { get; private set; } = 32f;

        private static readonly int kinds = Shader.PropertyToID("kinds");
        private static readonly int widthCountShift = Shader.PropertyToID("_WidthCountShift");
        private static readonly int chipSize = Shader.PropertyToID("_ChipSize");

        public void Initialize(int shiftCount)
        {
            ShiftCount = shiftCount;
            MaxExclusive = (1 << shiftCount - 1) * ChipSize;
            MinInclusive = -MaxExclusive;
            ChipCount = 1 << (shiftCount << 1);
            Buffer?.Dispose();
            Buffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, ChipCount, sizeof(int));
            ChipMaterial.SetBuffer(kinds, Buffer);
            ChipMaterial.SetInt(widthCountShift, shiftCount);
            ChipMaterial.SetInt(chipSize, (int)ChipSize);
            Bounds = new Bounds(Vector3.zero, Vector3.one * MaxExclusive * 2f);
        }

        public void Dispose()
        {
            Buffer?.Dispose();
            Buffer = null;
        }
    }
}
