using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(SpawnMarker))]
    public class SpawnerMarkerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
        public static void RenderCosumGizmo(SpawnMarker enemySpawner, GizmoType type)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemySpawner.transform.position, 1f);
        } 
    }
}