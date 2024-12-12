using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        private void Start()
        {
            //_uniqId = GetComponent<UniqueId>().id;
            //_factory = AllServices.Container.Single<IGameFactory>();
            //var staticDataService = AllServices.Container.Single<IStaticDataService>();
            //_monsterData = staticDataService.ForMonster(GetComponent<SpawnPoint>().MonsterTypeId);
            //var randomService = AllServices.Container.Single<IRandomService>();
            //Construct(_factory, randomService);
        }
        public void SetEnemy(EnemyDeath enemyDeath)
        {
          //  enemyDeath.Happened += SpawnLoot;
        }
        public void Construct(IGameFactory factory, IRandomService randomService)
        {
            this._factory = factory;
            this._randomService = randomService;
        } 
        private void SpawnLoot()
        {
            _loot = _factory.CreateLoot();
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
        public void UpdateProgress(PlayerProgress progress)
        {
            if (_loot == null) return;
            LootItemData itemData = progress.WorldData.LootData.LootItems.FirstOrDefault(l => l.UniqId == _uniqId);

            if (itemData == null)
            {
                itemData = new LootItemData() { UniqId = _uniqId };
                progress.WorldData.LootData.LootItems.Add(itemData);
            }
            itemData.PickUp = _loot.Picked;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            LootItemData itemData = progress.WorldData.LootData.LootItems.FirstOrDefault(l => l.UniqId == _uniqId);
            if (itemData != null && itemData.PickUp == false)
            {
                SpawnLoot();
            }
        }
    }
}