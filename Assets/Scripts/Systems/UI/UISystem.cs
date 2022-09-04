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
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().Inc<Rigidbody>().Inc<LaserComponent>().End();
            var updateScoreFilter = world.Filter<UpdateScoreUIEvent>().End();
            var updateScoreEventPool = world.GetPool<UpdateScoreUIEvent>();
            var transformPool = world.GetPool<Transform>();
            var rigidbodyPool = world.GetPool<Rigidbody>();
            var laserComponentPool = world.GetPool<LaserComponent>();
            var uiFilter = world.Filter<PanelState>().End();
            var textPool = world.GetPool<PanelState>();
            foreach (int entity in uiFilter)
            {
                ref PanelState panel = ref textPool.Get(entity);
                foreach (int playerEntity in playerFilter)
                {
                    ref Transform transform = ref transformPool.Get(playerEntity);
                    ref Rigidbody rigidbody = ref rigidbodyPool.Get(playerEntity);
                    ref LaserComponent laser = ref laserComponentPool.Get(playerEntity);
                    panel.Rotate.text = transform.Value.eulerAngles.z.ToString("F1");
                    panel.Speed.text = rigidbody.Value.velocity.magnitude.ToString("F1");
                    panel.Coordinate.text = _screenCoordinate.Value.GetCurrentCoordinate(transform.Value.position);
                    panel.Laser.text = laser.Count.ToString();
                }
                foreach (int eventEntity in updateScoreFilter)
                {
                    ref UpdateScoreUIEvent updateScoreEvent = ref updateScoreEventPool.Get(eventEntity);
                    panel.Score.text = updateScoreEvent.Value.ToString();
                    world.DelEntity(eventEntity);
                }
            }
        }
    }
}