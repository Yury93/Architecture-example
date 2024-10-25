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