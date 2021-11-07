using Leopotam.Ecs;
using UnityEngine;
using Object = System.Object;

namespace Shooter
{
    internal class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private StaticData _staticData;
        private SceneData _sceneData;
        
        public void Init()
        {
            EcsEntity playerEntity = _world.NewEntity();

            ref var player = ref playerEntity.Get<Player>();
            ref var inputData = ref playerEntity.Get<PlayerInputData>();
            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
            ref var animatorRef = ref playerEntity.Get<AnimatorRef>();
            
            GameObject playerGO = GameObject.Instantiate(_staticData.PlayerPrefab, _sceneData.PlayerSpawnPoint.position,
                Quaternion.identity);
            player.PlayerRigidbody = playerGO.GetComponent<Rigidbody>();
            player.PlayerSpeed = _staticData.PlayerSpeed;
            player.PlayerTransform = playerGO.transform;
            player.PlayerAnimator = playerGO.GetComponent<Animator>();
            animatorRef.Animator = player.PlayerAnimator;

            var weaponEntity = _world.NewEntity();
            var weaponView = playerGO.GetComponentInChildren<WeaponSettings>();
            ref var weapon = ref weaponEntity.Get<Weapon>();
            weapon.Owner = weaponEntity;
            weapon.ProjectilePrefab = weaponView.ProjectilePrefab;
            weapon.ProjectileRadius = weaponView.ProjectileRadius;
            weapon.ProjectileSocket = weaponView.ProjectileSocket;
            weapon.ProjectileSpeed = weaponView.ProjectileSpeed;
            weapon.TotalAmmo = weaponView.TotalAmmo;
            weapon.WeaponDamage = weaponView.WeaponDamage;
            weapon.CurrentInMagazine = weaponView.CurrentInMagazine;
            weapon.MaxInMagazine = weaponView.MaxInMagazine;
            
            hasWeapon.Weapon = weaponEntity;
            playerGO.GetComponent<PlayerView>().entity = playerEntity;
        }
    }
}