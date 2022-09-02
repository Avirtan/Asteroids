using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class DestroyEnemySystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var attackEventFilter = world.Filter<DestroyEnemyEvent>().End();
            var destroyEnemyEventPool = world.GetPool<DestroyEnemyEvent>();
            var transformPool = world.GetPool<Transform>();
            foreach (int entity in attackEventFilter)
            {
                ref DestroyEnemyEvent destroyEvent = ref destroyEnemyEventPool.Get(entity);
                if(transformPool.Has(destroyEvent.Entity.IdEntity))
                {
                    ref Transform transform = ref transformPool.Get(destroyEvent.Entity.IdEntity);
                    transform.Value.gameObject.SetActive(false);
                }
                world.DelEntity(entity);
            }
        }
    }
}