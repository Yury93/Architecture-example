
using  CodeBase.StaticData.Windows;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Services
{
    public interface IStaticDataService : IService
    {
        LevelStaticData ForLevel(string sceneKey);
        MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
        WindowConfig ForWindow(WindowId windowId);
        void LoadMonsters();
    }
}
 