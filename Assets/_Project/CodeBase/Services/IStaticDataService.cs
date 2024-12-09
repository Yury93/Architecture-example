
using CodeBase.Logic;
using CodeBase.StaticData;

namespace CodeBase.Services
{
    public interface IStaticDataService : IService
    {
        MonsterStaticData ForMonster(MonsterTypeId monsterTypeId);
        void LoadMonsters();
    }
}