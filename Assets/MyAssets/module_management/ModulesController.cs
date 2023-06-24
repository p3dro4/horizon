using System.Collections.Generic;
using UnityEngine;
using MyAssets.resource_management;
using MyAssets.resource_management.resources_requirement;

namespace MyAssets.module_management
{
    public class ModulesController : MonoBehaviour
    {
        private readonly List<KeyValuePair<float, float>> _modulesLocation = new();
        [SerializeField] private ModuleProperties moduleProperties;
        [SerializeField] private ModuleState moduleState;
        [SerializeField] private SelectedModule selectedModule;
        [SerializeField] private ResourceState resourceState;
        [SerializeField] private ResourceState resourceCapacity;
        [SerializeField] private ResourceState resourceIncome;
        [SerializeField] private ResourceProperties resourceProperties;
        [SerializeField] private ScriptableObjectStorage resourceProduction;
        [SerializeField] private ScriptableObjectStorage resourceRequirement;

        private Dictionary<string, int> _resourceIndex = new();

        private List<GameObject> _modules = new();

        private void Awake()
        {
            _modulesLocation.Add(new KeyValuePair<float, float>(-4.5f, 0));
            _modulesLocation.Add(new KeyValuePair<float, float>(0, 0));
            _modulesLocation.Add(new KeyValuePair<float, float>(4.5f, 0));
            _modulesLocation.Add(new KeyValuePair<float, float>(-4.5f, -3.49f));
            _modulesLocation.Add(new KeyValuePair<float, float>(0, -3.49f));
            _modulesLocation.Add(new KeyValuePair<float, float>(4.5f, -3.49f));
            _modulesLocation.Add(new KeyValuePair<float, float>(-4.5f, -6.94f));
            _modulesLocation.Add(new KeyValuePair<float, float>(0, -6.94f));
            _modulesLocation.Add(new KeyValuePair<float, float>(4.5f, -6.94f));
            _modules = moduleProperties.ModulePrefabs;
            moduleState.NumberOfModules = _modulesLocation.Count;
            if (moduleState.Modules.Count == 0) moduleState.ResetDefault();
            for (var i = 0; i < resourceProperties.ResourceNames.Count; i++)
                _resourceIndex.Add(resourceProperties.ResourceNames[i], i);
        }

        // Start is called before the first frame update
        private void Start()
        {
            moduleState.AddListener((pair) =>
            {
                var oldNewPair = pair.Value;
                RemoveAllProduction(oldNewPair.Key);
                RemoveAllRequirements(oldNewPair.Key);
                ChangeModule(oldNewPair.Value);
                AddAllRequirements(oldNewPair.Value);
                AddAllProduction(oldNewPair.Value);
            });
            for (var i = 0; i < _modulesLocation.Count; i++)
            {
                ChangeModule(i, moduleState.Modules[i]);
            }

            InitResources();
        }


        private void ChangeModule(int index, int prefab)
        {
            Destroy(transform.GetChild(index).gameObject);
            InstantiateModule(index, prefab);
        }


        private void ChangeModule(int prefab)
        {
            ChangeModule(selectedModule.SelectedModuleValue, prefab);
        }

        private void InitResources()
        {
            for (var i = 0; i < moduleState.NumberOfModules; i++)
            {
                var currentModule = moduleState.Modules[i];
                if (currentModule < moduleProperties.FirstUpgradeModule) continue;
                var offset = currentModule - moduleProperties.FirstUpgradeModule;
                AddAllProduction(currentModule);
                AddAllRequirements(currentModule);
            }
        }

        private void AddAllProduction(int module)
        {
            if (module < moduleProperties.FirstUpgradeModule) return;
            var offset = module - moduleProperties.FirstUpgradeModule;
            foreach (var keyValuePair in _resourceIndex)
            {
                var value = ((ModuleResources)resourceProduction.Storage[offset]).GetResource(keyValuePair.Key);
                AddProduction(keyValuePair.Key, value);
            }
        }

        private void RemoveAllProduction(int module)
        {
            if (module < moduleProperties.FirstUpgradeModule) return;
            var offset = module - moduleProperties.FirstUpgradeModule;
            foreach (var keyValuePair in _resourceIndex)
            {
                var value = ((ModuleResources)resourceProduction.Storage[offset]).GetResource(keyValuePair.Key);
                RemoveProduction(keyValuePair.Key, value);
            }
        }

        private void AddAllRequirements(int module)
        {
            if (module < moduleProperties.FirstUpgradeModule) return;
            var offset = module - moduleProperties.FirstUpgradeModule;
            foreach (var keyValuePair in _resourceIndex)
            {
                var value = ((ModuleResources)resourceRequirement.Storage[offset]).GetResource(keyValuePair.Key);
                AddRequirements(keyValuePair.Key, value);
            }
        }

        private void RemoveAllRequirements(int module)
        {
            if (module < moduleProperties.FirstUpgradeModule) return;
            var offset = module - moduleProperties.FirstUpgradeModule;
            foreach (var keyValuePair in _resourceIndex)
            {
                var value = ((ModuleResources)resourceRequirement.Storage[offset]).GetResource(keyValuePair.Key);
                RemoveRequirements(keyValuePair.Key, value);
            }
        }

        private void AddProduction(string resource, int value)
        {
            switch (resource)
            {
                case "Power":
                    resourceCapacity.Resources[_resourceIndex[resource]] += value;
                    break;
                default:
                    resourceIncome.Resources[_resourceIndex[resource]] += value;
                    break;
            }
        }

        private void RemoveProduction(string resource, int value)
        {
            switch (resource)
            {
                case "Power":
                    resourceCapacity.Resources[_resourceIndex[resource]] -= value;
                    break;
                default:
                    resourceIncome.Resources[_resourceIndex[resource]] -= value;
                    break;
            }
        }

        private void AddRequirements(string resource, int value)
        {
            switch (resource)
            {
                case "Power":
                    resourceState.Resources[_resourceIndex[resource]] += value;
                    break;
                default:
                    //resourceState.Resources[_resourceIndex[resource]] -= value;
                    break;
            }
        }

        private void RemoveRequirements(string resource, int value)
        {
            switch (resource)
            {
                case "Power":
                    resourceState.Resources[_resourceIndex[resource]] -= value;
                    break;
                default:
                    //resourceState.Resources[_resourceIndex[resource]] += value;
                    break;
            }
        }

        private void InstantiateModule(int index, int prefabIndex)
        {
            var module = Instantiate(_modules[prefabIndex], transform, true);
            module.transform.localPosition = new Vector3(_modulesLocation[index].Key,
                _modulesLocation[index].Value, 0);
            module.transform.SetSiblingIndex(index);
        }
    }
}