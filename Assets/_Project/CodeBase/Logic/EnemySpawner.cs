using CodeBase.Data;
using CodeBase.Services.PersistantProgress;
using System;
using UnityEngine;

namespace CodeBase.Logic
{
    public class EnemySpawner : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;

        private string _id;
        public bool _slane;
        private void Start()
        {
            _id = GetComponent<UniqueId>().id;
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