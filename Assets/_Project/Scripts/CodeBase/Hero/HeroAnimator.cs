using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.Hero
{
    public class HeroAnimator : MonoBehaviour , IAnimationStateReader
    {
        public Animator _animator;
        public CharacterController CharacterController;
        public static readonly int Idle = Animator.StringToHash("Idle"); 
        public static readonly int AttackLeft = Animator.StringToHash("AttackLeft");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Run = Animator.StringToHash("Run");
        public static readonly int Die = Animator.StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExitered;
        public AnimatorState State { get; private set; }
         
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void Update()
        {
            _animator.SetFloat(Walk, CharacterController.velocity.sqrMagnitude, 0.1f, Time.deltaTime);
        }
         
        //public void PlayHit() => _animator.SetTrigger(Hit);
        //public void PlayDeath() => _animator.SetTrigger(Die);
        //public void PlayAttack() => _animator.SetTrigger(Attack1);
        //public void StopMoving() => _animator.SetBool(IsMoving, false);
        //public void Move(float speed)
        //{
        //    _animator.SetBool(IsMoving, true);
        //    _animator.SetFloat(Speed, speed);
        //}

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }
        public void ExitedState(int stateHash)
        {
            StateExitered?.Invoke(State);
        }
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == Idle)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == AttackLeft)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == Walk)
            {
                state = AnimatorState.Move;

            }
            else if (stateHash == Die)
            {
                state = AnimatorState.Die;
            }
            else
            {
                state = AnimatorState.Unknow;
            }
            return state;
        }
    }
}