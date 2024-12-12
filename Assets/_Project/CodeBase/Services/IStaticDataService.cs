
using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Services
{
    public interface IStaticDataService : IService
    {
        LevelStaticData ForLevel(string sceneKey);
        MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
        void LoadMonsters();
    }
}
 