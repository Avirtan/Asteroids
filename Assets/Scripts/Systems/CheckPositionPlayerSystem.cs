using Leopotam.EcsLite;
using Component;

namespace System
{
    sealed class CheckPositionPlayerSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            var playerFilter = world.Filter<PlayerTag>().Inc<Transform>().End();
            var cameraFilter = world.Filter<Camera>().End();
            var transformPool = world.GetPool<Transform>();
            var cameraPool = world.GetPool<Camera>();
            foreach (int cameraEntity in cameraFilter)
            {
                ref Camera camera = ref cameraPool.Get(cameraEntity);
                var bottomLeft = camera.BottomLeft;
                var topRight = camera.TopRight;
                foreach (int playerEntity in playerFilter)
                {
                    ref Transform transform = ref transformPool.Get(playerEntity);
                    var positionPlayer = transform.Value.position;
                    if (positionPlayer.x < bottomLeft.x)
                    {
                        transform.Value.position = new UnityEngine.Vector3(topRight.x, positionPlayer.y, positionPlayer.z);
                    }
                    else if (positionPlayer.x > topRight.x)
                    {
                        transform.Value.position = new UnityEngine.Vector3(bottomLeft.x, positionPlayer.y, positionPlayer.z);
                    }
                    else if (positionPlayer.y < bottomLeft.y)
                    {
                        transform.Value.position = new UnityEngine.Vector3(positionPlayer.x, topRight.y, positionPlayer.z);
                    }
                    else if (positionPlayer.y > topRight.y)
                    {
                        transform.Value.position = new UnityEngine.Vector3(positionPlayer.x, bottomLeft.y, positionPlayer.z);
                    }
                }
            }
        }
    }
}