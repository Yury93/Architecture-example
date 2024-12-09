
using CodeBase.Enemy;
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory 
    { 
        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject _heroGameObject;

        private readonly IAsset _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgressService;


        public GameFactory(IAsset assets,
            Services.IStaticDataService staticDataService, 
            IRandomService randomService,
            IPersistentProgressService persistentProgressService)
        {
            _assetProvider = assets;
            _staticData = staticDataService;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
        }
        public GameObject CreateHero(GameObject initialPoint)
        { 
            _heroGameObject =  InstatiateRegisted(AssetPath.HeroPath, initialPoint.transform.position); 
            return _heroGameObject;
        }
        public GameObject InstatiateHUD()
        {
            return InstatiateRegisted(AssetPath.HudPath);
        }
        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform transformParent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);
            GameObject monster = Object.Instantiate(monsterData.prefab, transformParent.position, Quaternion.identity, transformParent);
            var health = monster.GetComponent<IHealth>();
            health.CurrentHp = monsterData.Hp;
            health.MaxHp = monsterData.Hp;


            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.Construct(this,randomService: _randomService);
            lootSpawner.SetLoot(monsterData.MinLoot, monsterData.MaxLoot);

            Attack attack = monster.GetComponentInChildren<Attack>();
            attack.Construct(_heroGameObject.transform);
            attack.Damage = (int)monsterData.Damage;
            attack.Cleavage = monsterData.AttackCleavege;
            attack.AttckCooldown = monsterData.AttackCooldown;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

            return monster;
        }
        public LootPiece CreateLoot()
        {
            LootPiece loot = InstatiateRegisted(AssetPath.Loot).GetComponent<LootPiece>();

            loot.Construct(_persistentProgressService.Progress.WorldData);

            return loot;
        }
        private GameObject InstatiateRegisted(string prefabPath, Vector3 position)
        {
            GameObject gameobject = _assetProvider.Instatiate(prefabPath, position);
            RegisterProgressWatchers(gameobject);
            return gameobject;
        }
        private GameObject InstatiateRegisted(string prefabPath)
        {
            GameObject gameobject = _assetProvider.Instatiate(prefabPath);
            RegisterProgressWatchers(gameobject);
            return gameobject;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }
         
        public void Cleanup()
        {
            progressReaders.Clear();
            ProgressWriters.Clear();
        }
        public void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            progressReaders.Add(progressReader);
        }

      
    }
}
