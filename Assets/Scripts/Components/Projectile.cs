using UnityEngine;

namespace Shooter
{
    public struct Projectile
    {
        public int Damage;
        public Vector3 Dir;
        public float Radius;
        public float Speed;
        public Vector3 PrevPos;
        public GameObject GO;
    }
}