using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Transforms;

namespace Unity1Week
{
    public sealed class AnimationSkillRenderSystem : ComponentSystem
    {
        private readonly Camera mainCamera;
		// 間違った英語であるが、Intellisenseとの相性の関係上これで押し通させてもらう。
		private readonly Matrix4x4[] matrixes = new Matrix4x4[1023];
		

        public AnimationSkillRenderSystem(Camera mainCamera)
        {
			this.mainCamera = mainCamera;
        }

        protected override void OnUpdate()
        {
        }
    }
}