using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    internal class ProjectileMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);

                var position = projectile.GO.transform.position;
                position += projectile.Dir * projectile.Speed * Time.fixedDeltaTime;
                projectile.GO.transform.position = position;

                //Handle hit
                var displacementSinceLastFrame = position - projectile.PrevPos;
                var hit = Physics.SphereCast(projectile.PrevPos, projectile.Radius,
                    displacementSinceLastFrame.normalized, out var hitInfo,
                    displacementSinceLastFrame.magnitude);
                if (hit)
                {
                    ref var entity = ref _filter.GetEntity(i);
                    ref var projectileHit = ref entity.Get<ProjectileHit>();
                    projectileHit.RaycastHit = hitInfo;
                }

                projectile.PrevPos = projectile.GO.transform.position;
            }
        }
    }
}