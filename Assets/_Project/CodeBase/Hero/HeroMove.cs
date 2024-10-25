using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.InputService;
using CodeBase.Services.PersistantProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(CharacterController))]
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        private CharacterController _characterController;
        public float MovementSpeed;
        private IInputService _inputService;
        private Camera _camera;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.WorldData.PositionOnLevel.Level == CurrentLevel())
            {
                var savedPosition = progress.WorldData.PositionOnLevel.position;
                if (savedPosition != null)
                { 
                    Warp(savedPosition);
                }
            }
        }

        private void Warp(Vector3Data savedPosition)
        {
            _characterController.enabled = false;
            transform.position = savedPosition.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }

        private void Awake()
        { 
            _characterController = GetComponent<CharacterController>();  
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
            _characterController.Move(movementDirection * Time.deltaTime * MovementSpeed);
        }
        
    }
}