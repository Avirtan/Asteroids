using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class InitInputActionSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            var entity = _world.NewEntity();
            var inputAtionPool = _world.GetPool<InputAction>();
            inputAtionPool.Add(entity);
            ref InputAction entityComponent = ref inputAtionPool.Get(entity);
            entityComponent.Value = new PlayerAction();
            entityComponent.Value.Enable();
            entityComponent.Value.Player.Attack.performed += Attack_performed;
        }

        private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if(ctx.performed)
            {
                var entity = _world.NewEntity();
                var inputAtionPool = _world.GetPool<AttackEvent>();
                inputAtionPool.Add(entity);
            }
        }
    }
}