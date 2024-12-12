using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.PersistantProgress;
using System;
using System.Collections;
using System.Linq; 
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour /*, ISavedProgress*/
    {
        public GameObject Model;
        public GameObject PickupFxPrefab;
        public TextMeshProUGUI LootText;
        public GameObject PickupPopup;
        private string _uniqId;
        private Loot _loot;
        public bool Picked { get;private set; }
        private WorldData _worldData;
        public Action OnPickUp;
        public void Construct(WorldData worldData)
        {
            _worldData = worldData;
        }
        public void Initialized(Loot loot)
        {
            _loot = loot;

        }
        
        private void OnTriggerEnter(Collider other)
        {
            PickUp();
        }

        private void PickUp()
        {
            if (Picked) return;

            Picked = true;
            UpdateWorldData();
            HideSkull();
            PlayPickupFx();
            ShowText(); 
            StartCoroutine(StartDestroyTime());
           
        }

        private void UpdateWorldData()
        {
            _worldData.LootData.Collect(_loot);
        }

        private void HideSkull()
        {
            Model.SetActive(false);
        }

        private IEnumerator StartDestroyTime()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            Destroy(gameObject);
        }

        private void PlayPickupFx()
        {
            Instantiate(PickupFxPrefab, transform.position, Quaternion.identity);
        }

        private void ShowText()
        {
            LootText.text = $"{_loot.Value}";
            PickupPopup.SetActive(true);
        }

        //public void UpdateProgress(PlayerProgress progress)
        //{
        //    LootItemData itemData = progress.WorldData.lootData.lootItems.FirstOrDefault(l => l.UniqId == _uniqId);

        //    if (itemData == null)
        //    {
        //        itemData = new LootItemData() { UniqId = _uniqId };
        //        progress.WorldData.lootData.lootItems.Add(itemData);
        //    }
        //    itemData.pickUp =Picked;
        //}

        //public void LoadProgress(PlayerProgress progress)
        //{
        //    LootItemData itemData = progress.WorldData.lootData.lootItems.FirstOrDefault(l => l.UniqId == _uniqId);
        //    if (itemData.pickUp == false)
        //    {
        //        Debug.LogError("spawn loot");
        //        //SpawnLoot();
        //    }
        //}
    }
}
