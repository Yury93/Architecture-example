using UnityEngine;

namespace CodeBase.Infrastructer
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstraper BootstraperPrefab;
        private void Awake()
        {
            var bootstrapper = GameObject.FindAnyObjectByType<GameBootstraper>();
            if (bootstrapper == null)
            {
                Instantiate(BootstraperPrefab);
            }
            
        }
    }
}