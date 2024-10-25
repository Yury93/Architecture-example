
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver TriggerObserver;
        public AgentMoveToPlayer Follow;

        public float CoodDown;
       private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            TriggerObserver.TriggerEnter += TriggerEnter;
            TriggerObserver.TriggerExit += TriggerExit;


            SwitchFollowOff();
        }
        private void TriggerEnter(Collider collider)
        {
            if (!_hasAggroTarget)
            {
                _hasAggroTarget = true;

                StopAgroCoroutine();
                SwitchFollowOn();
            }
        }

        private void TriggerExit(Collider collider)
        {
            if (_hasAggroTarget)
            {
                _hasAggroTarget = false;
                 
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(CoodDown);

            SwitchFollowOff();
        }


        private void StopAgroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void SwitchFollowOn()
        {
            Follow.enabled = true;
        }

        private void SwitchFollowOff()
        {
            Follow.enabled = false;
        }
    }
}