using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.module_management
{
    [CreateAssetMenu(fileName = "ModuleProperties", menuName = "ScriptableObject/ModuleProperties", order = 2)]
    public class ModuleProperties : ScriptableObject
    {
        [SerializeField] private List<GameObject> modulePrefabs = new();
        [SerializeField] private int firstUpgradeModule = 4;
        [SerializeField] private List<string> moduleNames = new();
        [SerializeField] private List<Sprite> moduleImage = new();
        [SerializeField] private List<Color> moduleTextColor = new();
        [SerializeField] private List<Color> moduleTextOutlineColor = new();

        public List<string> ModuleNames => moduleNames;

        public List<Sprite> ModuleImage => moduleImage;

        public List<Color> ModuleColor => moduleTextColor;

        public List<Color> ModuleTextOutlineColor => moduleTextOutlineColor;

        public List<GameObject> ModulePrefabs => modulePrefabs;

        public List<GameObject> ModulePrefabsUpgrades =>
            modulePrefabs.GetRange(firstUpgradeModule, modulePrefabs.Count - firstUpgradeModule);

        public int FirstUpgradeModule => firstUpgradeModule;
    }
}