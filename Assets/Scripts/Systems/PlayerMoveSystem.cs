using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Player, PlayerInputData> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var inputData = ref _filter.Get2(i);
                
                Vector3 direction = (player.PlayerTransform.forward * inputData.MoveInput.z + Vector3.right * inputData.MoveInput.x).normalized;
                player.PlayerRigidbody.AddForce(direction * player.PlayerSpeed);
            }
        }
    }
}