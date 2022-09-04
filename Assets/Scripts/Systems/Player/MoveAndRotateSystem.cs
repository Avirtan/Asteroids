using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class MoveAndRotateSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Rigidbody>().Inc<Transform>().Inc<MoveSpeed>().Inc<RotateSpeed>().End();
            var inputActionFilter = world.Filter<InputAction>().End();
            var inputActionPool = world.GetPool<InputAction>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var transformPool = world.GetPool<Transform>();
            var rotatePool = world.GetPool<RotateSpeed>();
            var moveSpeedPool = world.GetPool<MoveSpeed>();
            foreach (int entity in inputActionFilter)
            {
                ref InputAction input = ref inputActionPool.Get(entity);
                foreach (int playerEntity in playerFilter)
                {
                    ref Rigidbody rb = ref rigidbodyPool.Get(playerEntity);
                    ref Transform transform = ref transformPool.Get(playerEntity);
                    ref MoveSpeed moveSpeed = ref moveSpeedPool.Get(playerEntity);
                    ref RotateSpeed rotateSpeed = ref rotatePool.Get(playerEntity);
                    var value = input.Value.Player.Move.ReadValue<float>() * moveSpeed.Value;
                    rb.Value.AddForce(value  * transform.Value.up);
                    value = input.Value.Player.Rotate.ReadValue<UnityEngine.Vector2>().x * rotateSpeed.Value;
                    rb.Value.AddTorque(value);
                }
            }
        }
    }
}