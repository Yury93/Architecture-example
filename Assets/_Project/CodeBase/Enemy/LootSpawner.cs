
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootSpawner : MonoBehaviour
    {
        public EnemyHealth enemyHealth;
        private IGameFactory _factory;
        private IRandomService _randomService;
        private int _lootMin;
        private int _lootMax;
        public void Construct(IGameFactory factory,IRandomService randomService)
        {
            this._factory = factory;
            this._randomService = randomService;
        }
        void Start()
        {
            enemyHealth.HealthChanged += SpawnLoot;
        }

        private void SpawnLoot()
        {
            if (enemyHealth.CurrentHp <= 0)
            {
                LootPiece loot = _factory.CreateLoot();
                loot.transform.position = transform.position;
                Loot lootItem = GenerateLoot();
                loot.Initialized(lootItem);
            }
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                Value = _randomService.Next(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            this._lootMin = min;
            this._lootMax = max;
        }
    }
}
