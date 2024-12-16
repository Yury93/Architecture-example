 using System.Collections.Generic;
using System.Threading.Tasks; 
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations; 

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAsset
    {
        private Dictionary<string,AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
        private Dictionary<string, List< AsyncOperationHandle>> _handles = new Dictionary<string, List< AsyncOperationHandle>>();
        public void Initialize()
        {
            Addressables.InitializeAsync();
        } 

        public async Task<T> Load<T>(AssetReference assetReference) where T : class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
        } 
        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), address);
        }
        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                _completedCache[cacheKey] = completeHandle;
            };
            AddHandle(cacheKey, handle);
            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }

            resourceHandles.Add(handle);
        }

        public void Cleanup()
        {
            foreach (var handle in _handles.Values)
            {
                foreach (var item in handle)
                {
                    Addressables.Release(item);
                }
            }
            _completedCache.Clear();
            _handles.Clear();
        }
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
         

        //public async Task<T> Load<T>(AssetReferenceGameObject assetReference)
        //{
        //    AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);
        //    return await handle.Task;
        //}
    }
}
