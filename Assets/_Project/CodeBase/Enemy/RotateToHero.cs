using CodeBase.Infrastructure.Factory;
using System;
using UnityEngine; 

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        public float Speed;

        private Transform _heroTransform; 
        private Vector3 _positionToLook;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        } 
        private void Update()
        {
            if (_heroTransform != null)
                RotateTowardsHero();
        } 
        private void RotateTowardsHero()
        { 
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }
        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);
        } 
        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        } 
        private float SpeedFactor()
        {
            return Speed * Time.deltaTime;
        }
        private Quaternion TargetRotation(Vector3 positionToLook)
        {
            return Quaternion.LookRotation(positionToLook);
        }

    }
}