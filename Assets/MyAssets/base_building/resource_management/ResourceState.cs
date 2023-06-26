using System.Collections.Generic;
using MyAssets.resource_management;
using UnityEngine;

namespace MyAssets.base_building.resource_management
{
    [CreateAssetMenu(fileName = "ResourceState", menuName = "ScriptableObject/ResourceState", order = 3)]
    public class ResourceState : ScriptableObject
    {
        [SerializeField] private List<int> resources = new();
        [SerializeField] private List<int> initialResources = new();
        [SerializeField] private ResourceProperties resourceProperties;

        public void OnEnable()
        {
            resources = new List<int>(initialResources);
        }

        public List<int> Resources => resources;

        public List<int> InitialResources => initialResources;

        public void SetResource(int index, int value)
        {
            if (index >= resources.Count)
                resources.Add(value);
            else
                resources[index] = value;
            CallDelegates(new KeyValuePair<int, int>(index, value));
        }

        public void AddAmount(int index, int amount)
        {
            UpdateResource(index, amount);
        }

        public void RemoveAmount(int index, int amount)
        {
            UpdateResource(index, -amount);
        }

        private void UpdateResource(int index, int value)
        {
            resources[index] += value;
            CallDelegates(new KeyValuePair<int, int>(index, value));
        }

        public int GetResourceIndex(string resource)
        {
            return resourceProperties.ResourceNames.IndexOf(resource);
        }

        public int GetResourceValue(string resource)
        {
            return resources[GetResourceIndex(resource)];
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