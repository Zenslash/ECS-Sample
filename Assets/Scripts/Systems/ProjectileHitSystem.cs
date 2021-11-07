using Leopotam.Ecs;

namespace Shooter
{
    internal class ProjectileHitSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile,ProjectileHit> _filter;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectile = ref _filter.Get1(i);
                
                //Deal damage and other stuff...
                projectile.GO.SetActive(false);
            }
        }
    }
}