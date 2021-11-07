using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class SpawnProjectileSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Weapon, SpawnProjectile> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);

                var projectileGO = GameObject.Instantiate(weapon.ProjectilePrefab,
                    weapon.ProjectileSocket.transform.position, Quaternion.identity);
                var projectileEntity = _world.NewEntity();

                ref var projectile = ref projectileEntity.Get<Projectile>();

                projectile.Damage = weapon.WeaponDamage;
                projectile.GO = projectileGO;
                projectile.Radius = weapon.ProjectileRadius;
                projectile.Speed = weapon.ProjectileSpeed;
                projectile.Dir = weapon.ProjectileSocket.forward;
                projectile.PrevPos = projectileGO.transform.position;

                ref var entity = ref _filter.GetEntity(i);
                entity.Del<SpawnProjectile>();
            }
        }
    }
}