
using CodeBase.Data;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.Services.InputService;
using CodeBase.Services.PersistantProgress;
using UnityEngine;

public class HeroAttack : MonoBehaviour, ISavedProgressReader
{
    public CharacterController CharacterController;
    public HeroAnimator HeroAnimator;
    private Stats _stats;
    private IInputService _inputService;
    private int _layerMask;
    private Collider[] _hits = new Collider[3];

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
        _layerMask = 1 << LayerMask.NameToLayer("Hittable");

    }
    private void Update()
    { 
        if(_inputService.IsAttackButtonUp() && !HeroAnimator.IsAttacking)
        {
            HeroAnimator.PlayAttack();
        }
    }
    public int Hit()
    {
        int countColliders = Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.RadiusDamage, _hits, _layerMask);
        return countColliders;
    }
    public void OnAttack()
    {
        for (int i = 0; i < Hit(); i++)
        {
            _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
        }
    }
    private Vector3 StartPoint()
    {
      return new Vector3(  transform.position.x, CharacterController.center.y/2, transform.position.z);
    }

    public void LoadProgress(PlayerProgress progress)
    {
        _stats = progress.HeroStats;
    }
}
