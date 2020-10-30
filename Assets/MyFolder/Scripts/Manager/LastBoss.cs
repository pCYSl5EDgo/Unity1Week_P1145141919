using System;

namespace Unity1Week
{
    internal partial class Manager
    {
        private void LastBossAppear()
        {
            StopManagersExcept(typeof(IRenderSystem));
        }

        private void StopManagersExcept(Type exceptType)
        {
        }

        private void RestartManagers()
        {
        }
    }
}