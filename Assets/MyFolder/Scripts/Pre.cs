using Unity.Entities;
using UnityEngine;
static class Pre
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Preload() => PlayerLoopManager.RegisterDomainUnload(DestroyAll, 10000);

    static void DestroyAll()
    {
        World.DisposeAllWorlds();
        ScriptBehaviourUpdateOrder.UpdatePlayerLoop(System.Array.Empty<World>());
    }
}