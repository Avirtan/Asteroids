using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class UISystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().Inc<Rigidbody>().End();
            var transformPool = world.GetPool<Transform>();
            var rigidbodyPool = world.GetPool<Rigidbody>();

            var cameraFilter = world.Filter<Camera>().End();
            var cameraPool = world.GetPool<Camera>();

            var uiFilter = world.Filter<PanelState>().End();
            var textPool = world.GetPool<PanelState>();
            foreach (int cameraEntity in cameraFilter)
            {
                ref Camera camera = ref cameraPool.Get(cameraEntity);
                var topRight = camera.TopRight;
                foreach (int entity in uiFilter)
                {
                    ref PanelState panel = ref textPool.Get(entity);
                    foreach (int playerEntity in playerFilter)
                    {
                        ref Transform transform = ref transformPool.Get(playerEntity);
                        ref Rigidbody rigidbody = ref rigidbodyPool.Get(playerEntity);
                        panel.Rotate.text = transform.Value.eulerAngles.z.ToString("F1");
                        panel.Speed.text = rigidbody.Value.velocity.magnitude.ToString("F1");
                        panel.Coordinate.text = String.Format("X:{0:#.0} Y:{1:#.0}", transform.Value.position.x + topRight.x, transform.Value.position.y + topRight.y);
                    }
                }
            }
        }
    }
}