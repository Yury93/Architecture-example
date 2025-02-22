﻿using CodeBase.Logic;
using System;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class EnemySpawnerData 
    {
        public string Id;
        public MonsterTypeId MonsterTypeId;
        public Vector3 Position;
       public EnemySpawnerData(string id, MonsterTypeId monsterTypeId, Vector3 position)
        {
            Id = id;
            MonsterTypeId = monsterTypeId;
            Position = position;
        }
    }
}