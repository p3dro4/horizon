using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyAssets.base_building.resource_management;
using MyAssets.module_management;
using MyAssets.resource_management;
using MyAssets.resource_management.resources_requirement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.base_building.ui.build_menu
{
    public class BuildMenuController : MonoBehaviour, IUiController
    {
        [SerializeField] private SelectedModule selectedModule;
        [SerializeField] private ModuleState moduleState;
        [SerializeField] private ModuleProperties moduleProperties;
        [Space(10)] [SerializeField] private ScriptableObjectStorage resourceRequirement;
        [SerializeField] private ScriptableObjectStorage resourceProduction;
        [SerializeField] private ResourceState resourceState;


        [SerializeField] private Sprite demolishSprite;

        private int _firstUpgradeModule;

        public List<int> DisplayedModules { get; } = new();

        public Dictionary<int, bool> HasEnoughResources { get; } = new();

        private void Awake()
        {
            _firstUpgradeModule = moduleProperties.FirstUpgradeModule;
        }

        private void Start()
        {
            InitMenus();
        }

        private void OnEnable()
        {
            InitMenus();
        }

        public void InitMenus()
        {
            var currentModule = moduleState.Modules[selectedModule.SelectedModuleValue];
            var upgraded = currentModule >= _firstUpgradeModule;
            var options = transform.GetChild(1);
            DisplayedModules.Clear();
            HasEnoughResources.Clear();
            for (var i = _firstUpgradeModule; i < _firstUpgradeModule + options.childCount; i++)
            {
                if (currentModule != i) DisplayedModules.Add(i);
            }

            if (upgraded) DisplayedModules.Add(-1);
            var y = 0;
            foreach (var module in DisplayedModules)
            {
                var option = options.GetChild(y);
                var button = option.GetComponent<Button>();
                button.GetComponentInChildren<TextMeshProUGUI>().text =
                    module == -1 ? "Demolish" : moduleProperties.ModuleNames[module];
                var offset = module == -1 ? -1 : module - _firstUpgradeModule;
                button.GetComponent<Image>().sprite = module != -1
                    ? moduleProperties.ModuleImage[offset]
                    : demolishSprite;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (!EnoughResources(offset)) return;
                    SubtractResources(offset);
                    moduleState.SetModule(selectedModule.SelectedModuleValue, module != -1 ? module : 0);
                    gameObject.SetActive(false);
                });
                var description = button.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                description.text =
                    module == -1
                        ? "Removes the current upgrade"
                        : moduleProperties.ModuleDescriptions[offset];
                switch (module)
                {
                    case 4:
                        var science = ((ModuleResources)resourceProduction.Storage[offset])
                            .GetResource("Science");
                        description.text = description.text.Replace("?", science.ToString());
                        break;
                    case 5:
                        var regex = new Regex(Regex.Escape("?"));
                        var morkite = ((ModuleResources)resourceProduction.Storage[offset])
                            .GetResource("Morkite");
                        var bismor = ((ModuleResources)resourceProduction.Storage[offset])
                            .GetResource("Bismor");
                        var phazyonite = ((ModuleResources)resourceProduction.Storage[offset])
                            .GetResource("Phazyonite");
                        description.text = regex.Replace(description.text, morkite.ToString(), 1);
                        description.text = regex.Replace(description.text, bismor.ToString(), 1);
                        description.text = regex.Replace(description.text, phazyonite.ToString(), 1);
                        break;
                    case 6:
                        var power = ((ModuleResources)resourceProduction.Storage[offset])
                            .GetResource("Power");
                        description.text = description.text.Replace("?", power.ToString());
                        break;
                    case 7:
                        var time = ((ModuleResources)resourceProduction.Storage[offset])
                            .GetResource("BonusTime");
                        description.text = description.text.Replace("?", time.ToString());
                        break;
                }

                var requirements = option.GetChild(2);
                var demolish = module == -1;
                var requirement = module == -1
                    ? (ModuleResources)resourceRequirement.Storage[currentModule - _firstUpgradeModule]
                    : (ModuleResources)resourceRequirement.Storage[offset];

                requirements.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = demolish
                    ? $"+{requirement.Power}"
                    : $"-{requirement.Power}";

                requirements.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = demolish
                    ? $"+{requirement.Morkite * 0.85}"
                    : $"-{requirement.Morkite}";

                requirements.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = demolish
                    ? $"+{requirement.Bismor * 0.85}"
                    : $"-{requirement.Bismor}";

                requirements.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = demolish
                    ? $"+{requirement.Phazyonite * 0.85}"
                    : $"-{requirement.Phazyonite}";
                HasEnoughResources[y] = EnoughResources(offset);
                y++;
            }
        }

        private void SubtractResources(int module)
        {
            if (module == -1)
            {
                var currentModule = moduleState.Modules[selectedModule.SelectedModuleValue];
                var requirements = (ModuleResources)resourceRequirement.Storage[currentModule - _firstUpgradeModule];
                var requiredMorkite = requirements.Morkite;
                var requiredBismor = requirements.Bismor;
                var requiredPhazyonite = requirements.Phazyonite;
                resourceState.AddAmount(resourceState.GetResourceIndex("Morkite"), (int)(requiredMorkite * 0.85));
                resourceState.AddAmount(resourceState.GetResourceIndex("Bismor"), (int)(requiredBismor * 0.85));
                resourceState.AddAmount(resourceState.GetResourceIndex("Phazyonite"),
                    (int)(requiredPhazyonite * 0.85));
            }
            else
            {
                var requirements = (ModuleResources)resourceRequirement.Storage[module];
                var requiredMorkite = requirements.Morkite;
                var requiredBismor = requirements.Bismor;
                var requiredPhazyonite = requirements.Phazyonite;
                resourceState.RemoveAmount(resourceState.GetResourceIndex("Morkite"), requiredMorkite);
                resourceState.RemoveAmount(resourceState.GetResourceIndex("Bismor"), requiredBismor);
                resourceState.RemoveAmount(resourceState.GetResourceIndex("Phazyonite"), requiredPhazyonite);
            }
        }


        public bool EnoughResources(int module)
        {
            if (module == -1) return true;
            var requirements = (ModuleResources)resourceRequirement.Storage[module];
            var requiredMorkite = requirements.Morkite;
            var requiredBismor = requirements.Bismor;
            var requiredPhazyonite = requirements.Phazyonite;

            var currentMorkite = resourceState.GetResourceValue("Morkite");
            var currentBismor = resourceState.GetResourceValue("Bismor");
            var currentPhazyonite = resourceState.GetResourceValue("Phazyonite");

            return requiredMorkite <= currentMorkite &&
                   requiredBismor <= currentBismor &&
                   requiredPhazyonite <= currentPhazyonite;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }
}