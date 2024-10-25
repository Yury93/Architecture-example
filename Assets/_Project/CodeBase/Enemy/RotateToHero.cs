using CodeBase.Infrastructure.Factory; 
using UnityEngine; 

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        public float Speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            if (HeroExists())
            {
                InitializeHeroTransform();
            }
            else
            {
                _gameFactory.HeroCreated += InitializeHeroTransform;
            }
        }
        private void Update()
        {
            if (Initilized())
                RotateTowardsHero();
        }
        private bool Initilized()
        {
            return _heroTransform != null;
        }
        private void InitializeHeroTransform()
        {
            _heroTransform = _gameFactory.HeroGameObject.transform;
        }

        private bool HeroExists()
        {
            return _gameFactory.HeroGameObject != null;
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