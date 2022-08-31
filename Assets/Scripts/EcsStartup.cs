using UnityEngine;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _update;
        private EcsSystems _fixedUpdate;

        private void Start()
        {
            _world = new EcsWorld();
            _update = new EcsSystems(_world);
            _fixedUpdate = new EcsSystems(_world);
            _update
                .Add(new System.InitInputActionSystem())
                .Add(new System.InitCameraSystem())
                .Add(new System.InitEntitySystem())
                .Add(new System.UISystem())
                .ConvertScene()
                .Init();

            _fixedUpdate
               .Add(new System.RotateSystem())
               .Add(new System.MoveSystem())
               .Add(new System.CheckPositionPlayerSystem())
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