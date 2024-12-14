 
using CodeBase.UI.Elements;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
 
        public EnemyHealth Health;
        public EnemyAnimator Animator;
        public HpBar hpBar;

        public GameObject DeathVfx;
        public string UniqId { get; private set; }

        public event Action Happened;
        private void Start()
        {
            Health.HealthChanged += HealthChanged;
        } 
        private void HealthChanged()
        {
            if (Health.CurrentHp <= 0) 
            {
                Die();
            }
        }
        private void Die()
        {
            Health.HealthChanged -= HealthChanged;
            Animator.PlayDeath();
            SpawnVfx();
            StartCoroutine(DestroyTimer());
            hpBar.gameObject.SetActive(false);
            Happened?.Invoke();
        } 
        private void SpawnVfx()
        {
            var vfx = Instantiate(DeathVfx, transform.position, Quaternion.identity);
            Destroy(vfx.gameObject, 2f);
        } 
        private IEnumerator DestroyTimer()
        {
             yield return new WaitForSecondsRealtime(3f);
             Destroy(gameObject);
        } 
        private void OnDestroy()
        {
            Health.HealthChanged -= HealthChanged;
        }

        public void SetUniqId(string uniqId)
        {
            UniqId = uniqId;
        }
    }
}