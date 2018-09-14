using System;
using System.Collections.Generic;
using Unity.Entities;

namespace Unity1Week
{
    partial class Manager
    {
        readonly Dictionary<Type, bool> ComponentSystemEnabledStatusDictionary = new Dictionary<Type, bool>();
        void LastBossAppear()
        {
            foreach (var world in World.AllWorlds)
                foreach (var manager in world.BehaviourManagers)
                {
                    if (manager is ComponentSystem system)
                    {
                        ComponentSystemEnabledStatusDictionary[system.GetType()] = system.Enabled;
                        if (!(system is IRenderSystem))
                            system.Enabled = false;
                    }
                }
        }
    }
}