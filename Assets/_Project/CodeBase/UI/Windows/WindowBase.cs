using UnityEngine;
using UnityEngine.UI;


namespace CodeBase.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button closeButton;
        private void Awake()
        {
            OnAwake();
        }
        public virtual void OnAwake()
        {
           closeButton.onClick.AddListener(()=>Destroy(gameObject));
        }
    }
}