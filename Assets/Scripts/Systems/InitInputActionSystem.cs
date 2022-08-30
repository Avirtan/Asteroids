using Leopotam.EcsLite;
using Component;

namespace System {
    sealed class InitInputActionSystem : IEcsRunSystem
    {
        private EcsWorld world;
        public void Run(EcsSystems systems)
        {
            world = systems.GetWorld();
            var filter = world.Filter<InitInputActionEvent>().Inc<InputAction>().End();
            var inputAtionPool = world.GetPool<InputAction>();
            var initPool = world.GetPool<InitInputActionEvent>();
            foreach (int entity in filter)
            {
                ref InputAction entityComponent = ref inputAtionPool.Get(entity);
                entityComponent.Value = new PlayerAction();
                entityComponent.Value.Enable();
                entityComponent.Value.Player.Move.performed += Move_performed;
                initPool.Del(entity);
            }
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                //UnityEngine.Debug.Log(context.phase);
            }
        }
    }
}