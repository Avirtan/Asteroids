using UnityEngine;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;
using Leopotam.EcsLite.Di;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] Data.GameData _gameData;
        [SerializeField] Service.BulletPool _bulletPool;
        [SerializeField] Service.UFOPool _saucerPool;
        [SerializeField] Service.MeteorPool _meteorPool;
        [SerializeField] Service.PartMeteorPool _partMeteorPool;

        private EcsWorld _world;
        private IEcsSystems _update;
        private IEcsSystems _fixedUpdate;

        private void Start()
        {
            var screenCoodinate = new Service.ScreenCoordinate(Camera.main);
            _world = new EcsWorld();
            _update = new EcsSystems(_world);
            _fixedUpdate = new EcsSystems(_world);
            _update
                .Add(new System.InitEntitySystem())
                .Add(new System.InitInputActionSystem())
                .Add(new System.UISystem())
                .Add(new System.TimeSystem())
                .Add(new System.CheckMeteorPositionSystem())
                .Add(new System.CheckPositionPlayerSystem())
                .Add(new System.CheckBulletPositionSystem())
                .Add(new System.UpdateGameStateSystem())
                .Add(new System.StartGameSystem())
                .Add(new System.GameOverSystem())
                .Inject(screenCoodinate, _gameData)
                .ConvertScene()
                .Init();

            _fixedUpdate
               .Add(new System.MoveAndRotateSystem())
               .Add(new System.AttackSystem())
               .Add(new System.UFOSpawnSystem())
               .Add(new System.MeteorSpawnSystem())
               .Add(new System.FollowSystem())
               .Add(new System.DestroyEnemySystem())
               .Inject(screenCoodinate, _bulletPool, _saucerPool, _meteorPool, _partMeteorPool, _gameData)
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