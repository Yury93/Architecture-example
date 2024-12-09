
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName ="MonsterData", menuName = "StaticData/Monster")]
    public class MonsterStaticData : ScriptableObject
    {
        public MonsterTypeId MonsterTypeId;
        [Range(1,30)]public int Hp = 5;
        [Range(1, 30)] public float Damage = 10;
        [Range(0.5f, 1)] public float EffectiveDistance;
        [Range(0.5f, 1)] public float AttackCleavege;
        [Range(1f, 30f)] public float MoveSpeed = 2f;
        [Range(0.1f, 10)] public float AttackCooldown = 1f;
        public int MinLoot;
        public int MaxLoot;
        public GameObject prefab;
       
    }
}
