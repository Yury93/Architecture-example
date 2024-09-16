using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IGameFactory
    {
        public GameObject CreateHero(GameObject initialPoint);
        public GameObject InstatiateHUD();
    }
}
