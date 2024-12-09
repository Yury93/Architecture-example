using System;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        private string initialLevel;

        public WorldData(string initialLevel)
        {
            this.initialLevel = initialLevel;
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
        public LootData lootData;
    }
    [Serializable]
    public class LootData
    {
        public int Collected;

        public void Collect(Loot collect)
        {
            Collected += collect.Value;
        }
    }
    [Serializable]
    public class PositionOnLevel
    {
        public string Level;
        public Vector3Data position;
        public PositionOnLevel(string level, Vector3Data position)
        {
            this.Level = level;
            this.position = position;
        }
        public PositionOnLevel(string level) 
        {
        this.Level= level;
        }
    }
}