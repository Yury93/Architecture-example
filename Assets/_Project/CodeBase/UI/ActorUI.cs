using CodeBase.Hero;
using CodeBase.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        public HpBar HpBar;
        private IHealth _heroHealth;
        private void OnDestroy()
        {
            _heroHealth.HealthChanged -= UpdateHpBar;
        }
        public void Construct(IHealth heroHealth)
        {
            _heroHealth = heroHealth;
            _heroHealth.HealthChanged += UpdateHpBar;
        }
        public void UpdateHpBar()
        {
            HpBar.SetValue(_heroHealth.CurrentHp, _heroHealth.MaxHp); 
        }
         

    }
}