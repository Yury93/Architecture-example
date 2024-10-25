using CodeBase.Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(EnemyAnimator))]
public class AnimateAlongAgent : MonoBehaviour
{
    public NavMeshAgent Agent;
    public EnemyAnimator Animator;
    private const float MinimalVelocity = 0.1F;

    private void Update()
    {
        if (ShouldMove())
            Animator.Move(Agent.velocity.magnitude);
        else
            Animator.StopMoving();
    }

    private bool ShouldMove()
    {
      return  Agent.velocity.magnitude > MinimalVelocity && Agent.remainingDistance > Agent.radius;
    }
}
