
using CodeBase.Infrastructer;
using CodeBase.Infrastructer.StateMachine;
using CodeBase.Services;
using CodeBase.Services.InputService;
using UnityEngine;

namespace CodeBase.Hero
{ 
    public class HeroMove : MonoBehaviour
    {
        public CharacterController CharacterController;
        public float MovementSpeed;
        private IInputService _inputService;
        private Camera _camera;
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