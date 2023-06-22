using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.module_management
{
    [CreateAssetMenu(fileName = "ModuleState", menuName = "ScriptableObject/ModuleState", order = 0)]
    public class ModuleState : ScriptableObject
    {
        [SerializeField] private List<int> modules = new();
        [SerializeField] private ModuleProperties moduleProperties;
        public List<int> Modules => modules;
        public int NumberOfModules { get; set; } = 9;

        public void SetModule(int index, int value)
        {
            if (index >= modules.Count)
                modules.Add(value);
            else
                modules[index] = value;
            CallDelegates(new KeyValuePair<int, int>(index, value));
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

        public void FillRemaining()
        {
            var rng = new System.Random();
            for (var i = modules.Count; i < NumberOfModules; i++)
                SetModule(i, rng.Next(1, moduleProperties.FirstUpgradeModule));
        }

        public delegate void ChangedValue(KeyValuePair<int, int> alteredValues);

        [SerializeField] private readonly List<ChangedValue> _listeners = new();

        public void AddListener(ChangedValue listener)
        {
            _listeners.Add(listener);
        }

        private void CallDelegates(KeyValuePair<int, int> alteredValues)
        {
            foreach (var listener in _listeners)
            {
                listener.Invoke(alteredValues);
            }
        }
    }
}