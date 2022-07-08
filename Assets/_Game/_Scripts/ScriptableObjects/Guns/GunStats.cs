using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "Gun Stats", menuName = "ScriptableObjects/Stats/Gun")]
    public class GunStats : BaseScriptableObject
    {
        public float fireRate;
        public float bulletDamage;
    }
}