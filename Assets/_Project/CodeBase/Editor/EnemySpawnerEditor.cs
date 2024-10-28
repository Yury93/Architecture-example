using CodeBase.Logic; 
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(EnemySpawner))]
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo( GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
        public static void RenderCosumGizmo(EnemySpawner enemySpawner, GizmoType type)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(enemySpawner.transform.position, 1f);
        }
    }
}