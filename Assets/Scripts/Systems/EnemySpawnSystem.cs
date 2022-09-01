using Leopotam.EcsLite;
using Component;
using Service;

namespace System
{
    sealed class EnemySpawnSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var bulletPool = systems.GetShared<BulletPool>();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().End();
            var attackEventFilter = world.Filter<AttackEvent>().End();
            var transformPool = world.GetPool<Transform>();
            var attackEventPool = world.GetPool<AttackEvent>();
            foreach (int entity in attackEventFilter)
            {
                
            }
        }
    }
}