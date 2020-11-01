using UnityEngine;

namespace Unity1Week.Render
{
    public sealed class PlayerRenderSystem
    {
        private readonly Mesh playerMesh;

        public PlayerRenderSystem(Mesh playerMesh)
        {
            this.playerMesh = playerMesh;
        }

        public void Render(Camera camera)
        {
        }
    }
}