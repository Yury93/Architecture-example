using CodeBase.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            var uniqueId = (UniqueId)target;
            if (IsPrefab(uniqueId))
                return;

            if (string.IsNullOrEmpty(uniqueId.id))
            {
                Generate(uniqueId);
            }
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
                if (uniqueIds.Any(other => other.id != uniqueId.id && uniqueId.id == other.id))
                {
                    Generate(uniqueId);
                }
            }
        }

        private bool IsPrefab(UniqueId uniqueId)
        {
            return uniqueId.gameObject.scene.rootCount == 0;
        }

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}
