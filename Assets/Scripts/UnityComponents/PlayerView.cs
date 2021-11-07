using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace Shooter
{
    public class PlayerView : MonoBehaviour
    {

        public EcsEntity entity;

        public void Shoot()
        {
            entity.Get<HasWeapon>().Weapon.Get<Shoot>();
        }

        public void Reload()
        {
            entity.Get<HasWeapon>().Weapon.Get<ReloadingFinished>();
        }
    }
}
