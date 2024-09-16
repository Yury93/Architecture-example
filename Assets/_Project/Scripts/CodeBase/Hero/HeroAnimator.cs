using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        public Animator Animator;
        public CharacterController CharacterController;
        public static readonly int Idle = Animator.StringToHash("Idle"); 
        public static readonly int AttackLeft = Animator.StringToHash("AttackLeft");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Run = Animator.StringToHash("Run");
        private void Update()
        {
            Animator.SetFloat(Walk, CharacterController.velocity.sqrMagnitude, 0.1f, Time.deltaTime);
        }

    }
}