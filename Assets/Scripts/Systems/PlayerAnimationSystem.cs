using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class PlayerAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Player, PlayerInputData> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);
                
                float vertical = Vector3.Dot(input.MoveInput.normalized, player.PlayerTransform.forward);
                float horizontal = Vector3.Dot(input.MoveInput.normalized, player.PlayerTransform.right);
                player.PlayerAnimator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
                player.PlayerAnimator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
                player.PlayerAnimator.SetBool("Shooting", input.ShootInput);
            }
        }
    }
}