using CodeBase.Enemy;
using CodeBase.Logic;
using CodeBase.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public List<ISavedProgressReader> progressReaders { get; } 
        public List<ISavedProgress> ProgressWriters { get; } 
        public GameObject CreateHero(GameObject initialPoint);
        public GameObject InstatiateHUD();
        public void Cleanup();
        public void Register(ISavedProgressReader savedProgress);
        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform transformParent);
        public LootPiece CreateLoot();
    }
}
