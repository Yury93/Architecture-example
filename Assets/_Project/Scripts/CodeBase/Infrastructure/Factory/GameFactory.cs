
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Infrastructure.AssetManagement;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory 
    { 
       
        private readonly IAsset _assetProvider;
        public GameFactory(IAsset assets)
        {
            _assetProvider = assets;
        }
        public GameObject CreateHero(GameObject initialPoint)=>
            _assetProvider.Instatiate(AssetPath.HeroPath, initialPoint.transform.position);
  
        public GameObject InstatiateHUD()=>
            _assetProvider.Instatiate(AssetPath.HudPath); 
    }
}
