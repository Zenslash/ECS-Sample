using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    public class WeaponSettings : MonoBehaviour
    {
        public GameObject ProjectilePrefab;
        public Transform ProjectileSocket;
        public float ProjectileSpeed;
        public float ProjectileRadius;
        public int WeaponDamage;
        public int CurrentInMagazine;
        public int MaxInMagazine;
        public int TotalAmmo;
    }
}
