using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAsset
    {
        public GameObject Instatiate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(heroPrefab);
        }
        public GameObject Instatiate(string path, Vector3 position)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return GameObject.Instantiate(heroPrefab, position, Quaternion.identity);
        }
    }
}
