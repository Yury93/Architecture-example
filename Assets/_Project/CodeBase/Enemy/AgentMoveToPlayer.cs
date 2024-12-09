using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory;
using System;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AgentMoveToPlayer : Follow
{
    public NavMeshAgent Agent;
    private Transform _heroTransform;
    private const float MinimalDistance = 1;
    private IGameFactory gameFactory;
    public void Construct(Transform heroTransform)
    {
         _heroTransform = heroTransform;
    } 
    private void Update()
    {
        SetDestination();
    } 
    private void SetDestination()
    {
        if (_heroTransform)
            Agent.destination = _heroTransform.position;
    }
}
