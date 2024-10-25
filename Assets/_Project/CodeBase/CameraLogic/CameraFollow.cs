using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _follow;
    [SerializeField] private float rotationX;
    [SerializeField] private float distance;
    [SerializeField] private float offsetY;
    public void Follow(GameObject following) =>
        _follow = following.transform;

    private void LateUpdate()
    {
        if (_follow == null) return;
        var rotation = Quaternion.Euler(rotationX, 0, 0);
        var position = rotation * new Vector3(0, 0, distance) + GetFollowingPointPosition();

        transform.position = position;
        transform.rotation = rotation;
    } 
    private Vector3 GetFollowingPointPosition()
    {
        var followPosition = _follow.position;
        followPosition.y = offsetY;
        return followPosition;
    }
}
