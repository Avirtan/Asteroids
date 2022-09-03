using Leopotam.EcsLite;
using Component;
using Service;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class CheckMeteorPositionSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<ScreenCoordinate> _screenCoordinate = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var meteorFilter = world.Filter<MeteorTag>().Inc<Transform>().End();
            var transformPool = world.GetPool<Transform>();
            foreach (int meteorEntity in meteorFilter)
            {
                ref Transform transform = ref transformPool.Get(meteorEntity);
                var meteorPosition = transform.Value.position;
                if(_screenCoordinate.Value.CheckOutScreen(meteorPosition, true))
                {
                    transform.Value.gameObject.SetActive(false);
                }
            }
        }
    }
}