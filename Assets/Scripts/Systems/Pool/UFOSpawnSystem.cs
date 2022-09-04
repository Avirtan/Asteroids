using Leopotam.EcsLite;
using Component;
using Service;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class UFOSpawnSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<UFOPool> _saucerPool = default;
        readonly EcsCustomInject<ScreenCoordinate> _screenCoordinate = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var saucerSpawnerFilter = world.Filter<UFOSpawnerTag>().Inc<TimeDelay>().Exc<IsPause>().End();
            var timeDelayPool = world.GetPool<TimeDelay>();
            foreach (int entity in saucerSpawnerFilter)
            {
                ref var timeDelay = ref timeDelayPool.Get(entity);
                timeDelay.Value += UnityEngine.Time.deltaTime;
                if(timeDelay.Value > 3f)
                {
                    timeDelay.Value = 0;
                    var saucer = _saucerPool.Value.GetPooledObject();
                    saucer.gameObject.SetActive(true);
                    saucer.transform.position = _screenCoordinate.Value.RandomCoordinateOutScreen().Item1;
                }
            }
        }
    }
}