using Leopotam.EcsLite;
using Component;

namespace System {
    sealed class InitCameraSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var filter = world.Filter<InitEntityEvent>().Inc<Camera>().End();
            var cameraPool = world.GetPool<Camera>();
            var initPool = world.GetPool<InitEntityEvent>();
            foreach (int entity in filter)
            {
                ref Camera camera = ref cameraPool.Get(entity);
                float width = camera.Value.pixelWidth;
                float height = camera.Value.pixelHeight;
                camera.BottomLeft = camera.Value.ScreenToWorldPoint(new UnityEngine.Vector2(0, 0));
                camera.TopRight = camera.Value.ScreenToWorldPoint(new UnityEngine.Vector2(width, height));
                initPool.Del(entity);
            }
        }
    }
}