using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyAssets.base_building.resource_management;
using MyAssets.module_management;
using MyAssets.resource_management;
using MyAssets.resource_management.resources_requirement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.base_building.research
{
    public class ResearchController : MonoBehaviour, IUiController
    {
        [SerializeField] private ResearchState researchState;
        [SerializeField] private ResourceState resourceState;
        [SerializeField] private ScriptableObjectStorage resourceProduction;
        [SerializeField] private ModuleProperties moduleProperties;
        private GameObject _options;

        private void Awake()
        {
            _options = transform.GetChild(1).gameObject;
            InitMenus();
        }


        private void Update()
        {
            InitMenus();
        }


        public void InitMenus()
        {
            for (var i = 0; i < _options.transform.childCount; i++)
            {
                var offset = i + moduleProperties.FirstUpgradeModule;
                var child = _options.transform.GetChild(i);
                var header = child.GetChild(0).GetComponent<TextMeshProUGUI>();

                var (currentLvl, maxLvl) = ResearchLevels(child.name);
                var cost = researchState.BaseCost + researchState.BaseCost * currentLvl;
                child.GetChild(3).gameObject.SetActive(resourceState.GetResourceValue("Science") < cost);

                header.text = $"{moduleProperties.ModuleNames[offset]} {currentLvl}/{maxLvl}";

                var button = child.GetComponentInChildren<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    var (currentLevel, maxLevel) = ResearchLevels(child.name);
                    if (currentLevel >= maxLevel) return;
                    var upgradeCost = researchState.BaseCost + researchState.BaseCost * currentLevel;
                    if (!(resourceState.GetResourceValue("Science") >= upgradeCost)) return;
                    resourceState.RemoveAmount(resourceState.GetResourceIndex("Science"), (int)upgradeCost);
                    IncreaseResearchLevel(child.name);
                    InitMenus();
                });

                var description = child.GetChild(1).GetComponent<TextMeshProUGUI>();
                description.text = moduleProperties.ModuleDescriptions[i];
                switch (offset)
                {
                    case 4:
                        var science = ((ModuleResources)resourceProduction.Storage[i])
                            .GetResource("Science");
                        description.text = description.text.Replace("?", science.ToString());
                        break;
                    case 5:
                        var regex = new Regex(Regex.Escape("?"));
                        var morkite = ((ModuleResources)resourceProduction.Storage[i])
                            .GetResource("Morkite");
                        var bismor = ((ModuleResources)resourceProduction.Storage[i])
                            .GetResource("Bismor");
                        var phazyonite = ((ModuleResources)resourceProduction.Storage[i])
                            .GetResource("Phazyonite");
                        description.text = regex.Replace(description.text, morkite.ToString(), 1);
                        description.text = regex.Replace(description.text, bismor.ToString(), 1);
                        description.text = regex.Replace(description.text, phazyonite.ToString(), 1);
                        break;
                    case 6:
                        var power = ((ModuleResources)resourceProduction.Storage[i])
                            .GetResource("Power");
                        description.text = description.text.Replace("?", power.ToString());
                        break;
                    case 7:
                        var time = ((ModuleResources)resourceProduction.Storage[i])
                            .GetResource("BonusTime");
                        description.text = description.text.Replace("?", time.ToString());
                        break;
                }

                if (currentLvl >= maxLvl)
                {
                    button.gameObject.SetActive(false);
                    continue;
                }

                var requirement = child.GetChild(2).GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
                requirement.text =
                    $"-{researchState.BaseCost + researchState.BaseCost * researchState.GetModuleLvl(i)}";
            }
        }

        private void IncreaseResearchLevel(string option)
        {
            switch (option)
            {
                case "Option 1":
                    researchState.ResearchModuleLvl++;
                    break;
                case "Option 2":
                    researchState.MaterialProcessingModuleLvl++;
                    break;
                case "Option 3":
                    researchState.GeneratorModuleLvl++;
                    break;
                case "Option 4":
                    researchState.RelayModuleLvl++;
                    break;
            }
        }

        private KeyValuePair<int, int> ResearchLevels(string option)
        {
            var currentLvl = 0;
            var maxLvl = 0;
            switch (option)
            {
                case "Option 1":
                    currentLvl = researchState.ResearchModuleLvl;
                    maxLvl = researchState.RelayModuleLvlMax;
                    break;
                case "Option 2":
                    currentLvl = researchState.MaterialProcessingModuleLvl;
                    maxLvl = researchState.MaterialProcessingModuleLvlMax;
                    break;
                case "Option 3":
                    currentLvl = researchState.GeneratorModuleLvl;
                    maxLvl = researchState.GeneratorModuleLvlMax;
                    break;
                case "Option 4":
                    currentLvl = researchState.RelayModuleLvl;
                    maxLvl = researchState.RelayModuleLvlMax;
                    break;
            }

            return new KeyValuePair<int, int>(currentLvl, maxLvl);
        }


        public bool EnoughResources(int index)
        {
            var lvl = index switch
            {
                0 => researchState.RelayModuleLvl,
                1 => researchState.MaterialProcessingModuleLvl,
                2 => researchState.GeneratorModuleLvl,
                3 => researchState.RelayModuleLvl,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
            };
            var requirement = researchState.BaseCost + lvl * researchState.BaseCost;
            return requirement <= resourceState.GetResourceValue("Science");
        }
    }
}