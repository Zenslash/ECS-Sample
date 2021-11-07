using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputData, HasWeapon> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var hasWeapon = ref _filter.Get2(i);
                
                //TODO Implement new Input System
                input.MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                input.ShootInput = Input.GetMouseButton(0);
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ref var weapon = ref hasWeapon.Weapon.Get<Weapon>();

                    if (weapon.CurrentInMagazine < weapon.MaxInMagazine)
                    {
                        ref var entity = ref _filter.GetEntity(i);
                        entity.Get<TryReload>();
                    }
                }
            }
        }
    }
}