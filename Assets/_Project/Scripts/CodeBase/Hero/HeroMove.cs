using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.InputService;
using CodeBase.Services.PersistantProgress;
using UnityEngine;

namespace CodeBase.Hero
{ 
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        public CharacterController CharacterController;
        public float MovementSpeed;
        private IInputService _inputService;
        private Camera _camera;

        public void LoadProgress(PlayerProgress progress)
        {
            progress.WorldData.position = transform.position.AsVectorData();
        }
        public void UpdateProgress(PlayerProgress progress)
        {
             
        }

        private void Awake()
        { 
            _inputService = AllServices.Container.Single<IInputService>();
            _camera = Camera.main;
        } 
        private void Update()
        {
            Vector3 movementDirection = Vector3.zero;
         
            if ( _inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementDirection = _camera.transform.TransformDirection(_inputService.Axis);
                movementDirection.y = 0;
                movementDirection.Normalize();

                transform.forward = movementDirection;
            }
            movementDirection += Physics.gravity;
            CharacterController.Move(movementDirection * Time.deltaTime * MovementSpeed);
        }
        
    }
}