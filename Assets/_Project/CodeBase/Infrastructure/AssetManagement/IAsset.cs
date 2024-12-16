using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAsset : IService
    {
        public void Initialize();
        public GameObject Instatiate(string path);

        public GameObject Instatiate(string path, Vector3 position);
        public Task<T> Load<T>(string address) where T : class;
        public Task<T> Load<T>(AssetReference assetReference) where T : class;
        public void Cleanup();
 
    }
}
