
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
        private Dictionary<string, LevelStaticData> _levels;
        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>("StaticData/Monsters")
                .ToDictionary(x => x.MonsterTypeId, x => x);
            _levels = Resources.LoadAll<LevelStaticData>("StaticData/Levels")
            .ToDictionary(x => x.LevelKey, x => x);
        }
        public MonsterStaticData ForMonster(MonsterTypeId monsterTypeId)
        {
            if (_monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData))
                return staticData;

            return null;
        }

        public LevelStaticData ForLevel(string sceneKey)
        {
            if (_levels.TryGetValue(sceneKey, out LevelStaticData staticData))
                return staticData;

            return null;
        }
    }
}