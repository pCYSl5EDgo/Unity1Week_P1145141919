using System;
using System.Collections.Generic;
using Unity.Entities;

namespace Unity1Week
{
    partial class Manager
    {
        sealed class EqualityComparer : IEqualityComparer<(World, Type)>
        {
            public EqualityComparer Instance => _instance;
            static readonly EqualityComparer _instance;
            static EqualityComparer(){
                // _instance
            }
            public bool Equals((World, Type) x, (World, Type) y)
            {
                throw new NotImplementedException();
            }

            public int GetHashCode((World, Type) obj)
            {
                throw new NotImplementedException();
            }
        }
        readonly Dictionary<(World, Type), bool> ComponentSystemEnabledStatusDictionary = new Dictionary<(World, Type), bool>();

        void LastBossAppear()
        {
            StopManagersExcept(typeof(IRenderSystem));
        }

        private void StopManagersExcept(Type exceptType)
        {
            foreach (var world in World.AllWorlds)
                foreach (var manager in world.BehaviourManagers)
                {
                    var type = manager.GetType();
                    if (!(manager is ComponentSystem system)) continue;
                    ComponentSystemEnabledStatusDictionary[(world, type)] = system.Enabled;
                    if (!exceptType.IsAssignableFrom(type))
                        system.Enabled = false;
                }
        }
        private void RestartManagers()
        {

        }
    }
}