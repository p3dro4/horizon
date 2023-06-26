using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.resource_management.resources_requirement
{
    [CreateAssetMenu(fileName = "ScriptableObjectStorage", menuName = "ScriptableObject/ScriptableObjectStorage",
        order = 7)]
    public class ScriptableObjectStorage : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> storage = new();

        [SerializeField] private List<ScriptableObject> initialStorage = new();


        public List<ScriptableObject> Storage => storage;

        public List<ScriptableObject> InitialStorage => initialStorage;

        private void OnEnable()
        {
            storage = new List<ScriptableObject>(initialStorage);
        }
    }
}