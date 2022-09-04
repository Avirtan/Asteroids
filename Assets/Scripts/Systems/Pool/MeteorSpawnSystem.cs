using Leopotam.EcsLite;
using Component;
using Service;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class MeteorSpawnSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<MeteorPool> _meteorPool = default;
        readonly EcsCustomInject<ScreenCoordinate> _screenCoordinate = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var saucerSpawnerFilter = world.Filter<MeteorSpawnerTag>().Inc<TimeDelay>().Exc<IsPause>().End();
            var timeDelayPool = world.GetPool<TimeDelay>();
            foreach (int entity in saucerSpawnerFilter)
            {
                ref var timeDelay = ref timeDelayPool.Get(entity);
                timeDelay.Value += UnityEngine.Time.deltaTime;
                if(timeDelay.Value > 1f)
                {
                    timeDelay.Value = 0;
                    var meteor = _meteorPool.Value.GetPooledObject() as MonoBeh.Meteor;
                    meteor.gameObject.SetActive(true);
                    var (position, side) = _screenCoordinate.Value.RandomCoordinateOutScreen();
                    meteor.transform.position = position;
                    meteor.MoveMeteor(side, _screenCoordinate.Value);
                }
            }
        }
    }
}