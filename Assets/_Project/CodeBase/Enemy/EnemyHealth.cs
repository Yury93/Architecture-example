using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.UI;
using System; 
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{ 
    [SerializeField] private int _currentHp;
    [SerializeField] private int _maxHp;
    public EnemyAnimator Animator;
    public Action HealthChanged { get; set; }
    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = value;
    }
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            if (_currentHp != value)
            {
                _currentHp = value;
                HealthChanged?.Invoke();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (CurrentHp <= 0)
        {
            return;
        }
        CurrentHp -= damage;
        if (Animator == null) Animator = GetComponentInChildren<EnemyAnimator>();
        Animator.PlayHit(); 
    }
}
