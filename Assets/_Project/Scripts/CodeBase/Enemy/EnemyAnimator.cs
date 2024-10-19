using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.Enemy
{
   
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Die = Animator.StringToHash("Die");
        public static readonly int Win = Animator.StringToHash("Win");
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int Attack1 = Animator.StringToHash("Attack1");
        public static readonly int Attack2 = Animator.StringToHash("Attack2");
        public static readonly int Hit = Animator.StringToHash("Hit");
        private Animator _animator;

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExitered;
        public AnimatorState State { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        public void PlayHit() =>  _animator.SetTrigger(Hit);
        public void PlayDeath() => _animator.SetTrigger(Die);
        public void PlayAttack() => _animator.SetTrigger(Attack1);  
        public void StopMoving() => _animator.SetBool(IsMoving,false);
        public void Move(float speed)
        {
            _animator.SetBool(IsMoving,true);
            _animator.SetFloat(Speed,speed);
        }
        
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
            if(stateHash == Idle)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == Attack1)
            {
                state = AnimatorState.Attack;
            }
            else if(stateHash == IsMoving)
            {
                state = AnimatorState.Move;

            }
            else if(stateHash == Die)
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