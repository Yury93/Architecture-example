
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistantProgress;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory 
    { 
        public List<ISavedProgressReader> progressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private readonly IAsset _assetProvider;
        public GameFactory(IAsset assets)
        {
            _assetProvider = assets;
        }
        public GameObject CreateHero(GameObject initialPoint)
        { 
            return InstatiateRegisted(AssetPath.HeroPath, initialPoint.transform.position); 
        }
        public GameObject InstatiateHUD()
        {
            return InstatiateRegisted(AssetPath.HudPath);
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
                RegisterProgressReader(progressReader);
            }
        }
         
        public void Cleanup()
        {
            progressReaders.Clear();
            ProgressWriters.Clear();
        }
        private void RegisterProgressReader(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            progressReaders.Add(progressReader);
        }
    }
}
