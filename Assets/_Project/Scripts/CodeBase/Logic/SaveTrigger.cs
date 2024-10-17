using CodeBase.Services.SaveLoad;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private ISavedLoadService _saverLoadServer;
    public BoxCollider collider;
    private void Awake()
    {
        _saverLoadServer = AllServices.Container.Single<ISavedLoadService>();
 
    }
    private void OnTriggerEnter(Collider other)
    {
        _saverLoadServer.SaveProgress();
        Debug.Log("saved");
        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        if (collider == null) return;
        Gizmos.color =new Color32(30,200,30,130);
        Gizmos.DrawCube(transform.position + collider.center, collider.size);
    }
}
