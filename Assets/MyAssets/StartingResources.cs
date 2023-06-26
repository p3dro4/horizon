using System;
using MyAssets.base_building.resource_management;
using UnityEngine;
using MyAssets.resource_management;

namespace MyAssets
{
    [CreateAssetMenu(fileName = "StartingResources", menuName = "ScriptableObject/StartingResources", order = 12)]
    public class StartingResources : ScriptableObject
    {
        [SerializeField] private int initialPowerConsumption;
        [SerializeField] private int initialPowerCapacity;
        [Space(5)] [SerializeField] private int initialScience;
        [SerializeField] private int initialScienceProduction;
        [Space(5)] [SerializeField] private int initialMorkite;
        [SerializeField] private int initialBismor;
        [SerializeField] private int initialPhazyonite;
        [Space(10)] [SerializeField] private ResourceState resourcesState;
        [SerializeField] private ResourceState resourcesIncome;
        [SerializeField] private ResourceState resourcesCapacity;

        private void OnEnable()
        {
            var powerIndex = resourcesState.GetResourceIndex("Power");
            resourcesState.InitialResources[powerIndex] = initialPowerConsumption;
            resourcesCapacity.InitialResources[powerIndex] = initialPowerCapacity;

            var scienceIndex = resourcesState.GetResourceIndex("Science");
            resourcesState.InitialResources[scienceIndex] = initialScience;
            resourcesIncome.InitialResources[scienceIndex] = initialScienceProduction;

            var morkiteIndex = resourcesState.GetResourceIndex("Morkite");
            resourcesState.InitialResources[morkiteIndex] = initialMorkite;

            var bismorIndex = resourcesState.GetResourceIndex("Bismor");
            resourcesState.InitialResources[bismorIndex] = initialBismor;

            var phazyoniteIndex = resourcesState.GetResourceIndex("Phazyonite");
            resourcesState.InitialResources[phazyoniteIndex] = initialPhazyonite;
        }
    }
}