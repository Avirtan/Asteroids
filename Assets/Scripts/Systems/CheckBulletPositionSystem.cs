using Leopotam.EcsLite;
using Component;
using Service;

namespace System
{
    sealed class CheckBulletPositionSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<BulletTag>().Inc<Transform>().End();
            var cameraFilter = world.Filter<Camera>().End();
            var transformPool = world.GetPool<Transform>();
            var cameraPool = world.GetPool<Camera>();
            foreach (int cameraEntity in cameraFilter)
            {
                ref Camera camera = ref cameraPool.Get(cameraEntity);
                var bottomLeft = camera.BottomLeft;
                var topRight = camera.TopRight;
                foreach (int bulletEntity in playerFilter)
                {
                    ref Transform transform = ref transformPool.Get(bulletEntity);
                    var bulletPosition = transform.Value.position;
                    if (bulletPosition.x < bottomLeft.x || bulletPosition.x > topRight.x ||
                        bulletPosition.y < bottomLeft.y || bulletPosition.y > topRight.y)
                    {
                        transform.Value.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}