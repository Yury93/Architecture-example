
using CodeBase.Infrastructer.StateMachine;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string HeroPath = "Hero/Hero";
        private const string HudPath = "Controls/HUD";
        public GameObject CreateHero(GameObject initialPoint)
        {
            return GameFactory.Instatiate(HeroPath, initialPoint.transform.position);
        }
        public GameObject InstatiateHUD()
        {
            return Instatiate(HudPath);
        }
        private static GameObject Instatiate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(heroPrefab);
        }
        private static GameObject Instatiate(string path, Vector3 position)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(heroPrefab, position, Quaternion.identity);
        } 
    }
}
