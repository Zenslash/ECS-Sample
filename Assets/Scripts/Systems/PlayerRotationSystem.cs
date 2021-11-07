using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<Player> _filter = null;
        private SceneData _sceneData = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);

                Plane playerPlane = new Plane(Vector3.up, player.PlayerTransform.position);
                Ray ray = _sceneData.MainCamera.ScreenPointToRay(Input.mousePosition);
                if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

                player.PlayerTransform.forward = ray.GetPoint(hitDistance) - player.PlayerTransform.position;
            }
        }
    }
}