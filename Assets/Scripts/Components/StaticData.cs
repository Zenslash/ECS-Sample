using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter
{
    
    [CreateAssetMenu]
    public class StaticData : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float PlayerSpeed;
        public float SmoothTime;        //Camera smooth time
        public Vector3 FollowOffset;    //Offset from player
    }
}
