using Leopotam.EcsLite;
using Component;
using Leopotam.EcsLite.Di;
using Service;

namespace System
{
    sealed class UISystem : IEcsRunSystem
    {
        readonly EcsCustomInject<ScreenCoordinate> _screenCoordinate = default;

        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().Inc<Rigidbody>().End();
            var transformPool = world.GetPool<Transform>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var uiFilter = world.Filter<PanelState>().End();
            var textPool = world.GetPool<PanelState>();
            foreach (int entity in uiFilter)
            {
                ref PanelState panel = ref textPool.Get(entity);
                foreach (int playerEntity in playerFilter)
                {
                    ref Transform transform = ref transformPool.Get(playerEntity);
                    ref Rigidbody rigidbody = ref rigidbodyPool.Get(playerEntity);
                    panel.Rotate.text = transform.Value.eulerAngles.z.ToString("F1");
                    panel.Speed.text = rigidbody.Value.velocity.magnitude.ToString("F1");
                    panel.Coordinate.text = _screenCoordinate.Value.GetCurrentCoordinate(transform.Value.position);
                }
            }
        }
    }
}