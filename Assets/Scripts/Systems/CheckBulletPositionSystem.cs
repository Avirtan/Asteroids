using Leopotam.EcsLite;
using Component;
using Service;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class CheckBulletPositionSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<ScreenCoordinate> _screenCoordinate = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<BulletTag>().Inc<Transform>().End();
            var transformPool = world.GetPool<Transform>();
            foreach (int bulletEntity in playerFilter)
            {
                ref Transform transform = ref transformPool.Get(bulletEntity);
                var bulletPosition = transform.Value.position;
                if(_screenCoordinate.Value.CheckOutScreen(bulletPosition))
                {
                    transform.Value.gameObject.SetActive(false);
                }
            }
        }
    }
}