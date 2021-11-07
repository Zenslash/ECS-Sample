using System;
using Leopotam.Ecs;
using MiscUtil.Collections.Extensions;
using UnityEngine;

namespace Shooter {
    sealed class EcsStartup : MonoBehaviour
    {
        EcsWorld _world;
        EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;

        public StaticData StaticData;
        public SceneData SceneData;

        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _fixedUpdateSystems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_fixedUpdateSystems);
#endif
            _systems
                // register your systems here, for example:
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new CameraFollowSystem())
                .Add(new WeaponShootSystem())
                .Add(new SpawnProjectileSystem())
                .Add(new ReloadingSystem())

                // register one-frame components (order is important), for example:
                .OneFrame<TryReload> ()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                .Inject (StaticData)
                .Inject(SceneData)
                // .Inject (new NavMeshSupport ())
                ;

            _fixedUpdateSystems
                // register your systems here
                .Add(new PlayerMoveSystem())
                .Add(new PlayerRotationSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new ProjectileMoveSystem())
                .Add(new ProjectileHitSystem())
                
                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()

                // inject service instances here (order doesn't important), for example:
                .Inject (StaticData)
                .Inject(SceneData)
                // .Inject (new NavMeshSupport ())
                ;
            
            _systems.Init();
            _fixedUpdateSystems.Init();
        }

        void Update () {
            _systems?.Run ();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}