using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistantProgress;
using System;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public string Id { get; set; }
        public Enemy.EnemyDeath EnemyDeath { get;private set; }
        public MonsterTypeId MonsterTypeId;
        public bool _slane;
        private EnemyHealth _enemyHealth; 
        private IGameFactory _factory;
        internal Action<EnemyDeath> onCreateEnemy;

        public void Construct(IGameFactory gameFactory)
        {
            _factory = gameFactory;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slane)
            {
                progress.KillData.ClearedSpawners.Add(Id);
            }
        }
        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
            {
                _slane = true;
            }
            else 
            {
                Spawn();
            }
        } 
        private void Spawn()
        {
          GameObject monster = _factory.CreateMonster(MonsterTypeId,transform);
            _enemyHealth =  monster.GetComponent<EnemyHealth>();
            EnemyDeath = monster.GetComponentInChildren<EnemyDeath>();
            _enemyHealth.HealthChanged += Slay;
            onCreateEnemy?.Invoke(EnemyDeath);
        }

        private void Slay()
        { 
            _slane = true;

            if (_enemyHealth != null)
                _enemyHealth.HealthChanged -= Slay;
        } 
    }
}