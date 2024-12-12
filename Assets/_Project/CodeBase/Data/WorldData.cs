using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        private string _initialLevel;

        public WorldData(string initialLevel)
        {
            this._initialLevel = initialLevel;
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
        public LootData LootData = new LootData();
    }
    [Serializable]
    public class LootData
    { 
        public List<LootItemData> LootItems = new List<LootItemData>();
        public int Collected;
        public Action Changed;

        public void Collect(Loot collect)
        {
            Collected += collect.Value;
            Changed?.Invoke();
        }
    }
    [Serializable] 
    public class LootItemData
    {
        public string UniqId;
        public Vector3Data Position;
        public bool PickUp;
    }
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data Position;
        public PositionOnLevel(string level, Vector3Data position)
        {
            this.Level = level;
            this.Position = position;
        }
        public PositionOnLevel(string level) 
        {
        this.Level= level;
        }
    }
}