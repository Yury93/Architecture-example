using CodeBase.Data;
using CodeBase.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.UI;


namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button closeButton;
        protected IPersistentProgressService ProgressService;
        protected PlayerProgress PlayerProgress => ProgressService.Progress;
        public void Construct(IPersistentProgressService persistentProgressService)
        {
            ProgressService = persistentProgressService;
        }

        private void Awake()
        {
            OnAwake();
        }
        private void Start()
        {
            Initialize();
            SubscribeUpdate();
        }
        private void OnDestroy()
        {
            CleanUp();
        }

        protected virtual void OnAwake()
        {
           closeButton.onClick.AddListener(()=>Destroy(gameObject));
        }
        protected virtual void Initialize()
        { 
        }
        protected virtual void SubscribeUpdate()
        { 
        }
        protected virtual void CleanUp()
        { 
        }
    }
}