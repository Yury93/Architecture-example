using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.StaticData;
using System.Linq; 
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDatEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var level = (LevelStaticData)target;

            if(GUILayout.Button("Collect"))
            {
                level.EnemySpawners = FindObjectsOfType<SpawnMarker>()
                    .Select(x=> new EnemySpawnerData(x.GetComponent<UniqueId>().id,x.MonsterTypeId,x.transform.position))
                    .ToList();
                level.LevelKey = SceneManager.GetActiveScene().name;
            }
            EditorUtility.SetDirty(level);
        }
    }
}