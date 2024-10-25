using CodeBase.Enemy;
using CodeBase.Infrastructure.Factory; 
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class AgentMoveToPlayer : Follow
{
    public NavMeshAgent Agent;
    private Transform _heroTransform;
    private const float MinimalDistance = 1;
    private IGameFactory gameFactory;
    private void Start()
    {
        gameFactory = AllServices.Container.Single<IGameFactory>();
        if (gameFactory.HeroGameObject != null)
        {
            InitializeHeroTransform();
        }
        else
        {
            gameFactory.HeroCreated += HeroCreated;
        }
    }
    private void Update()
    {
        if (Initialized() && HeroNotReached())
            Agent.destination = _heroTransform.position;
    }

    private bool Initialized()
    {
        return _heroTransform != null;
    }

    private void InitializeHeroTransform()
    {
        _heroTransform = gameFactory.HeroGameObject.transform;
    }
    private void HeroCreated()
    {
        InitializeHeroTransform();
    }
    private bool HeroNotReached()
    {
        return Vector3.Distance(_heroTransform.position, transform.position) > MinimalDistance;
    }
}
