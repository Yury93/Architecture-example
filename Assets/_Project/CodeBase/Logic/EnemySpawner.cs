using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services.PersistantProgress;
using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        public bool _slane;
        private EnemyHealth _enemyHealth;
        private string _id;
        private IGameFactory _factory;
 
        private void Start()
        {
            _id = GetComponent<UniqueId>().id;
            _factory = AllServices.Container.Single<IGameFactory>();
        }
        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(_id))
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

            _enemyHealth.HealthChanged += Slay;
        }

        private void Slay()
        { 
            _slane = true;

            if (_enemyHealth != null)
                _enemyHealth.HealthChanged -= Slay;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slane) 
            {
                progress.KillData.ClearedSpawners.Add(_id);
            }
        }

      
    }
}