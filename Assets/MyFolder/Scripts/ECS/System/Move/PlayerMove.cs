/*using Unity.Mathematics;
using UnityEngine;

namespace Unity1Week
{
    [UpdateBefore(typeof(MoveSystem))]
    public sealed class PlayerMoveSystem : ComponentSystem
    {
        private readonly Entity player;
        private readonly Transform mainCameraTransform;
        private float2 cached;

        public PlayerMoveSystem(Entity player, Transform mainCameraTransform)
        {
            this.player = player;
            this.mainCameraTransform = mainCameraTransform;
        }

        protected override void OnUpdate()
        {
            var delta = new float2(1, 1);
            var isW = Input.GetKey(KeyCode.W);
            var isA = Input.GetKey(KeyCode.A);
            var isS = Input.GetKey(KeyCode.S);
            var isD = Input.GetKey(KeyCode.D);
            if (!(isW ^ isS)) delta.y = 0;
            else if (isS) delta.y = -1;
            if (!(isA ^ isD)) delta.x = 0;
            else if (isA) delta.x = -1;
            delta = delta.x == 0 && delta.y == 0 ? default : math.normalize(delta);
            var manager = EntityManager;
            {
                var speed = manager.GetComponentData<MoveSpeed>(player).Value;
                var deltaTime = Time.deltaTime;
                var pos = mainCameraTransform.position;
                pos.x += delta.x * deltaTime * speed;
                pos.z += delta.y * deltaTime * speed;
                mainCameraTransform.position = pos;
            }
            if (cached.x == delta.x && cached.y == delta.y)
                return;
            manager.SetComponentData(player, new Heading2D(cached = delta));
        }
    }
}*/

