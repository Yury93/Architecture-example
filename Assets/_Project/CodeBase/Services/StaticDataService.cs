
using CodeBase.Logic;
using CodeBase.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace CodeBase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;

        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData")
                .ToDictionary(x => x.MonsterTypeId, x => x);
        }
        public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId)
        {
            if (_monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData))
                return staticData;

            return null;
        }
    }
}