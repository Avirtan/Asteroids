using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class MoveSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Rigidbody>().Inc<InputAction>().Inc<Transform>().End();
            var inputActionPool = world.GetPool<InputAction>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var transformPool = world.GetPool<Transform>();
            foreach (int playerEntity in playerFilter)
            {
                ref InputAction input = ref inputActionPool.Get(playerEntity);
                ref Rigidbody rb = ref rigidbodyPool.Get(playerEntity);
                ref Transform transform = ref transformPool.Get(playerEntity);
                var value = input.Value.Player.Move.ReadValue<float>();
                rb.Value.AddForce(value * transform.Value.up);
            }
        }
    }
}