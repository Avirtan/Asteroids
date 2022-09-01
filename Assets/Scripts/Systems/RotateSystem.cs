using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class RotateSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Rigidbody>().Inc<RotateSpeed>().End();
            var inputActionFilter = world.Filter<InputAction>().End();
            var inputActionPool = world.GetPool<InputAction>();
            var rotatePool = world.GetPool<RotateSpeed>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            foreach (int entity in inputActionFilter)
            {
                ref InputAction input = ref inputActionPool.Get(entity);
                foreach (int playerEntity in playerFilter)
                {
                    ref Rigidbody rb = ref rigidbodyPool.Get(playerEntity);
                    ref RotateSpeed rotateSpeed = ref rotatePool.Get(playerEntity);
                    var value = input.Value.Player.Rotate.ReadValue<UnityEngine.Vector2>().x * rotateSpeed.Value;
                    rb.Value.AddTorque(value);
                }
            }
        }
    }
}