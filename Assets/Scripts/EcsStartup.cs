using UnityEngine;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] Service.BulletPool bulletPool;
        private EcsWorld _world;
        private IEcsSystems _update;
        private IEcsSystems _fixedUpdate;

        private void Start()
        {
            var screenCoodinate = new Service.ScreenCoordinate(Camera.main);
            _world = new EcsWorld();
            _update = new EcsSystems(_world);
            _fixedUpdate = new EcsSystems(_world, bulletPool);
            _update
                .Add(new System.InitEntitySystem())
                .Add(new System.InitInputActionSystem())
                .Add(new System.UISystem())
                .Inject(screenCoodinate)
                .ConvertScene()
                .Init();

            _fixedUpdate
               .Add(new System.RotateSystem())
               .Add(new System.MoveSystem())
               .Add(new System.CheckPositionPlayerSystem())
               .Add(new System.CheckBulletPositionSystem())
               .Add(new System.AttackSystem())
               .Inject(screenCoodinate)
#if UNITY_EDITOR
               .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
               .Init();
        }

        private void Update()
        {
            _update?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdate?.Run();
        }

        private void OnDestroy()
        {
            if (_update != null)
            {
                _update.Destroy();
                _update = null;
            }
            if (_fixedUpdate != null)
            {
                _fixedUpdate.Destroy();
                _fixedUpdate = null;
            }
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}