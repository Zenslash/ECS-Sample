using Leopotam.Ecs;

namespace Shooter
{
    internal class WeaponShootSystem : IEcsRunSystem
    {
        private EcsFilter<Weapon, Shoot> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);
                
                ref var entity = ref _filter.GetEntity(i);
                entity.Del<Shoot>();

                //If we have an ammo
                if (weapon.CurrentInMagazine > 0)
                {
                    weapon.CurrentInMagazine--;
                    
                    ref var spawnProjectile = ref entity.Get<SpawnProjectile>();
                }
                else
                {
                    ref var reload = ref entity.Get<TryReload>();
                }
            }
        }
    }
}