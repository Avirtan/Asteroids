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
            entityComponent.Value.Player.Laser.performed += Laser_performed; ;
        }

        private void Laser_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                var entity = _world.NewEntity();
                var inputAtionPool = _world.GetPool<AttackEvent>();
                inputAtionPool.Add(entity);
                ref AttackEvent attackEvent = ref inputAtionPool.Get(entity);
                attackEvent.Value = Util.TypeAttack.LASER;
            }
        }

        private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            if(ctx.performed)
            {
                var entity = _world.NewEntity();
                var inputAtionPool = _world.GetPool<AttackEvent>();
                inputAtionPool.Add(entity);
                ref AttackEvent attackEvent = ref inputAtionPool.Get(entity);
                attackEvent.Value = Util.TypeAttack.BULLET;
            }
        }
    }
}