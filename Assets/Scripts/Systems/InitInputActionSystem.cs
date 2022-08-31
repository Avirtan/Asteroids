using Leopotam.EcsLite;
using Component;

namespace System {
    sealed class InitInputActionSystem : IEcsInitSystem
    {
        private EcsWorld world;

        public void Init(EcsSystems systems)
        {
            world = systems.GetWorld();
            var entity = world.NewEntity();
            var inputAtionPool = world.GetPool<InputAction>();
            inputAtionPool.Add(entity);
            ref InputAction entityComponent = ref inputAtionPool.Get(entity);
            entityComponent.Value = new PlayerAction();
            entityComponent.Value.Enable();
        }
    }
}