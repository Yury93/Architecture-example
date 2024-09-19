using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        public GameObject CreateHero(GameObject initialPoint);
        public GameObject InstatiateHUD();
    }
}
