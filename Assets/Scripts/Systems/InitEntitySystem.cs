using Leopotam.EcsLite;
using Component;

namespace System {
    sealed class InitEntitySystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<InitEntityEvent>().Inc<Entity>().End();
            var entityPool = world.GetPool<Entity>();
            var initPool = world.GetPool<InitEntityEvent>();
            foreach (int entity in filter)
            {
                ref Entity entityComponent = ref entityPool.Get(entity);
                entityComponent.Value.IdEntity = entity;
                initPool.Del(entity);
            }
        }
    }
}