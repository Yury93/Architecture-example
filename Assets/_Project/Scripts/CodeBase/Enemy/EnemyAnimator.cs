using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public static readonly int Die = Animator.StringToHash("Die");
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();   
    }
    public void PlayDeath() => _animator.SetTrigger(Die);
}
