using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> progressReaders { get; } 
        public List<ISavedProgress> ProgressWriters { get; }
        public Task<GameObject> CreateHeroAsync(LevelStaticData levelData);
        public Task<GameObject> InstatiateHUDAsync();
        public void Cleanup(); 
        public Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform transformParent);
        public Task<LootSpawner> CreateLootSpawnerAsync(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId,SpawnPoint spawnPoint);
        public Task<LootPiece> CreateLootAsync();
        public Task<SpawnPoint> CreateSpawnerAsync(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId);
       public Task Warmup();
    }
}
