using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.module_management
{
    [CreateAssetMenu(fileName = "ModuleState", menuName = "ScriptableObject/ModuleState", order = 0)]
    public class ModuleState : ScriptableObject
    {
        [SerializeField] private List<int> modules = new();
        [SerializeField] private List<int> initialModules = new();
        [SerializeField] private bool generateNewValues = true;
        [SerializeField] private bool keepInitialValues = true;
        [SerializeField] private ModuleProperties moduleProperties;
        public List<int> Modules => modules;
        public int NumberOfModules { get; set; } = 9;

        public void OnEnable()
        {
            if (generateNewValues)
            {
                ResetDefault();
                return;
            }

            modules = new(initialModules);
            FillRemaining();
        }

        public void OnDisable()
        {
            if (!keepInitialValues)
                initialModules = modules;
        }

        public void SetModule(int index, int value)
        {
            var previousValue = modules[index];
            if (index >= modules.Count)
                modules.Add(value);
            else
                modules[index] = value;
            CallDelegates(new KeyValuePair<int, KeyValuePair<int, int>>(index,
                new KeyValuePair<int, int>(previousValue, value)));
        }

        public void ResetDefault()
        {
            modules.Clear();
            FillRemaining();
        }

        public void ResetToEmpty()
        {
            modules.Clear();
            for (var i = 0; i < NumberOfModules; i++)
                SetModule(i, 0);
        }

        private void FillRemaining()
        {
            var rng = new System.Random();
            for (var i = modules.Count; i < NumberOfModules; i++)
                SetModule(i, rng.Next(1, moduleProperties.FirstUpgradeModule));
        }

        public delegate void ChangedValue(KeyValuePair<int, KeyValuePair<int, int>> alteredValues);

        [SerializeField] private readonly List<ChangedValue> _listeners = new();

        public void AddListener(ChangedValue listener)
        {
            _listeners.Add(listener);
        }

        private void CallDelegates(KeyValuePair<int, KeyValuePair<int,int>> alteredValues)
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke(alteredValues);
            }
        }
    }
}