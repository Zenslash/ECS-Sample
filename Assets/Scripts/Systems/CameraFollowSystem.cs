using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Player> _filter;
        private SceneData _sceneData;
        private StaticData _staticData;

        //Used only in CameraFollow system
        private Vector3 _currentVelocity;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);

                var currentPos = _sceneData.MainCamera.transform.position;
                currentPos = Vector3.SmoothDamp(currentPos, player.PlayerTransform.position + _staticData.FollowOffset,
                    ref _currentVelocity, _staticData.SmoothTime);
                _sceneData.MainCamera.transform.position = currentPos;
            }
        }
    }
}