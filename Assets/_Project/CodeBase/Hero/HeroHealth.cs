using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistantProgress;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Hero
{

    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator Animator;
        private State _state;
        public Action HealthChanged { get; set; }
       
        public int MaxHp
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }
        public int CurrentHp
        {
            get => _state.CurrentHp;
            set
            {
                if (_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
          _state =  progress.HeroState;
             HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp =  CurrentHp;
            progress.HeroState.CurrentHp = MaxHp;
        }
        public void  TakeDamage(int damage)
        {
            if(CurrentHp <= 0)
            {
                return;
            }
            CurrentHp -= damage;
            if (Animator == null)  Animator = GetComponentInChildren<HeroAnimator>();
            Animator.PlayHit();
        }
    }
}
