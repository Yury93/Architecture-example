using CodeBase.Infrastructer.StateMachine;
using UnityEngine;

namespace CodeBase.Logic
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private const string PLAYER_TAG = "Player";
        public string TransferTo;
        private IGameStateMachine _stateMachine;
        private bool _triggered;
        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (_triggered) return;

            if (other.CompareTag(PLAYER_TAG))
            {
                _stateMachine.Enter<LoadLevelState, string>(TransferTo);
                _triggered = true;
            }
        }
    }
}
