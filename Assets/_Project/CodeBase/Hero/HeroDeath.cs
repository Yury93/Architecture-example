using CodeBase.Hero;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeath : MonoBehaviour
{
    public HeroAttack HeroAttack;
    public HeroHealth HeroHealth;
    public HeroMove HeroMove;
    public HeroAnimator HeroAnimator;

    public GameObject DeathVfxPrefab;
    private bool IsDead;

    private void Start()
    {
        HeroHealth.HealthChanged += HealthChanged;
    }
    private void OnDestroy()
    {
        HeroHealth.HealthChanged -= HealthChanged;
    }
    private void HealthChanged()
    {
         if(HeroHealth.CurrentHp <= 0 && !IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
       
        HeroMove.enabled = false;
        HeroAttack.enabled = false;
        HeroAnimator.PlayDeath();
       var deathVfx = Instantiate(DeathVfxPrefab,transform.position,Quaternion.identity);
        Destroy(deathVfx.gameObject, 2f);
        IsDead = true;
    }
}
