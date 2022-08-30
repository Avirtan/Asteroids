using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class RotateSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Rigidbody>().Inc<InputAction>().End();
            var inputActionPool = world.GetPool<InputAction>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            foreach (int playerEntity in playerFilter)
            {
                ref InputAction input = ref inputActionPool.Get(playerEntity);
                ref Rigidbody rb = ref rigidbodyPool.Get(playerEntity);
                var value = input.Value.Player.Rotate.ReadValue<UnityEngine.Vector2>().x;
                rb.Value.AddTorque(value * 2);
            }
        }
    }
}