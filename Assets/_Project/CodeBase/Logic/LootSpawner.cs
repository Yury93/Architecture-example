using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData; 
using System.Linq; 
using UnityEngine;

namespace CodeBase.Logic
{

    public class LootSpawner : MonoBehaviour,ISavedProgress
    {
        private IGameFactory _factory;
        private IRandomService _randomService;
        private MonsterStaticData _monsterData;
        private LootPiece _loot;
        private string _uniqId;
        private Vector3 _position;
        private SpawnPoint _spawnPoint;
        public void Init(string uniqueId, 
            MonsterTypeId monsterTypeId,
            Vector3 position, 
            IStaticDataService staticDataService) 
        {
            _uniqId = uniqueId; 
            _monsterData = staticDataService.ForMonster(monsterTypeId); 
        } 
        public void Construct(IGameFactory factory, IRandomService randomService)
        {
            _factory = factory;
            this._randomService = randomService;
        }
        public void SetEnemySpawner(SpawnPoint spawnEnemyPoint)
        {
            _spawnPoint = spawnEnemyPoint;
            _spawnPoint.onCreateEnemy += SubscribeOnEnemyDeath;
        } 
        private async void SpawnLoot()
        {
            _loot = await _factory.CreateLootAsync();
            _loot.transform.position = transform.position;
            Loot lootItem = GenerateLoot(); 
            _loot.Initialized(lootItem); 
        }
        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _randomService.Next(_monsterData.MinLoot, _monsterData.MaxLoot)
            };
        }
        private void SubscribeOnEnemyDeath(EnemyDeath enemyDeath)
        {
            _spawnPoint.onCreateEnemy -= SubscribeOnEnemyDeath;
            enemyDeath.Happened += SpawnLoot;
        }
        public void UpdateProgress(PlayerProgress progress)
        {
            if (_loot == null) return;
            LootItemData itemData = progress.WorldData.LootData.LootItems.FirstOrDefault(l => l.UniqId == _uniqId );

            if (itemData == null)
            {
                itemData = new LootItemData() { UniqId = _uniqId };
                progress.WorldData.LootData.LootItems.Add(itemData);
            }
            itemData.PickUp = _loot.Picked;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LootItemData itemData = progress.WorldData.LootData.LootItems.FirstOrDefault(l => l.UniqId == _uniqId );
            if (itemData != null && itemData.PickUp == false)
            {
                SpawnLoot();
            }
        }
    }
}