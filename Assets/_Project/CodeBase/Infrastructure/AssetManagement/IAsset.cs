using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAsset : IService
    {
        public GameObject Instatiate(string path);

        public GameObject Instatiate(string path, Vector3 position);
    }
}
