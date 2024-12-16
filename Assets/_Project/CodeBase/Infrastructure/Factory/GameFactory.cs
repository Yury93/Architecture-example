
using CodeBase.Enemy; 
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services;
using CodeBase.Services.PersistantProgress;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IWindowService _windowService;

        public GameFactory(IAsset assets,
            Services.IStaticDataService staticDataService, 
            IRandomService randomService,
            IPersistentProgressService persistentProgressService,
            IWindowService windowService)
        {
            _assetProvider = assets;
            _staticData = staticDataService;
            _randomService = randomService;
            _persistentProgressService = persistentProgressService;
            _windowService = windowService;
        }
        public async Task Warmup()
        {
            await _assetProvider.Load<GameObject>(AssetsAddress.LOOT_SPAWNER);
            await _assetProvider.Load<GameObject>(AssetsAddress.LOOT);
            await _assetProvider.Load<GameObject>(AssetsAddress.ENEMY_SPAWN_POINT);
            await _assetProvider.Load<GameObject>(AssetsAddress.HERO_PATH);
        }
        public async Task<GameObject> CreateHeroAsync(LevelStaticData levelData)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetsAddress.HERO_PATH);

            _heroGameObject =  InstatiateRegisted(prefab, levelData.InitialHeroPosition); 
            return _heroGameObject;
        }
        public async Task<GameObject> InstatiateHUDAsync()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetsAddress.HUD_PATH);

            GameObject hud = InstatiateRegisted(prefab);
            hud.GetComponentInChildren<LootCounter>()
                .Construct(_persistentProgressService.Progress.WorldData);
            foreach (var openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
            {
                openWindowButton.Construct(windowService: _windowService);
            }
            return hud;
        }
        public async Task<GameObject> CreateMonster(MonsterTypeId monsterTypeId, Transform transformParent)
        {
            MonsterStaticData monsterData = _staticData.ForMonster(monsterTypeId);
        
            GameObject prefab = await _assetProvider.Load<GameObject>(monsterData.PrefabReference);

            GameObject monster = Object.Instantiate(prefab, transformParent.position, Quaternion.identity, transformParent);
            var health = monster.GetComponent<IHealth>();
            health.CurrentHp = monsterData.Hp;
            health.MaxHp = monsterData.Hp;


            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(_heroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterData.MoveSpeed;
             

            Attack attack = monster.GetComponentInChildren<Attack>();
            attack.Construct(_heroGameObject.transform);
            attack.Damage = (int)monsterData.Damage;
            attack.Cleavage = monsterData.AttackCleavege;
            attack.AttckCooldown = monsterData.AttackCooldown;
            attack.EffectiveDistance = monsterData.EffectiveDistance;

            monster.GetComponent<RotateToHero>()?.Construct(_heroGameObject.transform);

            return monster;
        }
        public async Task<SpawnPoint> CreateSpawnerAsync(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetsAddress.ENEMY_SPAWN_POINT);

            var spawner = InstatiateRegisted(prefab, position).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.MonsterTypeId = monsterTypeId;
            spawner.Id = spawnerId;
            return spawner;
        }
        public async Task<LootSpawner> CreateLootSpawnerAsync(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId, SpawnPoint spawnPoint)
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetsAddress.LOOT_SPAWNER);

            var spawner = InstatiateRegisted(prefab, position).GetComponent<LootSpawner>();
            spawner.Init(spawnerId,  monsterTypeId, position, _staticData);
            spawner.Construct(this, _randomService);
            spawner.SetEnemySpawner(spawnPoint);
            return spawner;
        }
        public async Task<LootPiece> CreateLootAsync()
        {
            var prefab = await _assetProvider.Load<GameObject>(AssetsAddress.LOOT);

            LootPiece loot = InstatiateRegisted(prefab).GetComponent<LootPiece>();

            loot.Construct(_persistentProgressService.Progress.WorldData);

            return loot;
        }
        private GameObject InstatiateRegisted(GameObject prefab, Vector3 position)
        {
            GameObject gameobject =UnityEngine.Object.Instantiate(prefab, position,Quaternion.identity);
            RegisterProgressWatchers(gameobject);
            return gameobject;
        }
        private GameObject InstatiateRegisted(GameObject prefab)
        {
            GameObject gameobject = UnityEngine.Object.Instantiate(prefab);
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

            _assetProvider.Cleanup();
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
