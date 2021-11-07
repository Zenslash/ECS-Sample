using Leopotam.Ecs;

namespace Shooter
{
    internal class ReloadingSystem : IEcsRunSystem
    {
        private EcsFilter<TryReload, AnimatorRef>.Exclude<Reloading> _tryReloadFilter;
        private EcsFilter<Weapon, ReloadingFinished> _reloadingFinishedFilter;
        
        public void Run()
        {
            foreach (var i in _tryReloadFilter)
            {
                ref var animatorRef = ref _tryReloadFilter.Get2(i);

                animatorRef.Animator.SetTrigger("Reload");

                ref var entity = ref _tryReloadFilter.GetEntity(i);
                entity.Get<Reloading>();
            }
            foreach (var i in _reloadingFinishedFilter)
            {
                ref var weapon = ref _reloadingFinishedFilter.Get1(i);
                
                //How much ammo do we need?
                var needAmmo = weapon.MaxInMagazine - weapon.CurrentInMagazine;
                weapon.CurrentInMagazine = (weapon.TotalAmmo >= needAmmo)
                    ? weapon.MaxInMagazine
                    : weapon.CurrentInMagazine + weapon.TotalAmmo;
                weapon.TotalAmmo -= needAmmo;
                weapon.TotalAmmo = (weapon.TotalAmmo < 0) ? 0 : weapon.TotalAmmo;

                ref var entity = ref _reloadingFinishedFilter.GetEntity(i);
                weapon.Owner.Del<Reloading>();
                entity.Del<ReloadingFinished>();
            }
        }
    }
}