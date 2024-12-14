
 
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace CodeBase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string MonsterPath = "StaticData/Monsters";
        private const string LevelsPath = "StaticData/Levels";
        private const string WindowsPath = "StaticData/UI/Windows";
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        public void LoadMonsters()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(MonsterPath)
                .ToDictionary(x => x.MonsterTypeId, x => x);

            _levels = Resources.LoadAll<LevelStaticData>(LevelsPath)
            .ToDictionary(x => x.LevelKey, x => x);

            _windowConfigs= Resources.Load<WindowStaticData>(WindowsPath).Configs
          .ToDictionary(x => x.WindowId, x => x);
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

        public WindowConfig ForWindow(WindowId windowId)
        {
           return _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
                ? windowConfig : null;
        }
    }
}