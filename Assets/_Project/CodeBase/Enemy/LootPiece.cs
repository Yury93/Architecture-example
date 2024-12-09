using CodeBase.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        public GameObject Model;
        public GameObject PickupFxPrefab;
        public TextMeshProUGUI LootText;
        public GameObject PickupPopup;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

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
            if (_picked) return;

            _picked = true;
            UpdateWorldData();
            HideSkull();
            PlayPickupFx();
            ShowText(); 
            StartCoroutine(StartDestroyTime());
        }

        private void UpdateWorldData()
        {
            _worldData.lootData.Collect(_loot);
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
    }
}
