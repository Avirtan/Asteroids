using Leopotam.EcsLite;
using Component;
using Leopotam.EcsLite.Di;

namespace System
{
    sealed class CheckPositionPlayerSystem : IEcsRunSystem
    {
        readonly EcsCustomInject<Service.ScreenCoordinate> _screenCoordinate = default;
        public void Run(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().End();
            var transformPool = world.GetPool<Transform>();
            foreach (int playerEntity in playerFilter)
            {
                ref Transform transform = ref transformPool.Get(playerEntity);
                var positionPlayer = _screenCoordinate.Value.CheckOutScreenPlayer(transform.Value.position);
                if (positionPlayer != UnityEngine.Vector3.zero)
                {
                    transform.Value.position = positionPlayer;
                }
            }
        }
    }
}