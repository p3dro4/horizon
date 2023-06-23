using System.Collections.Generic;
using UnityEngine;
using MyAssets.resource_management;

namespace MyAssets.module_management
{
    public class ModulesController : MonoBehaviour
    {
        private readonly List<KeyValuePair<float, float>> _modulesLocation = new();
        [SerializeField] private ModuleProperties moduleProperties;
        [SerializeField] private ModuleState moduleState;
        [SerializeField] private SelectedModule selectedModule;
        [SerializeField] private ResourceState powerConsumption;
        [SerializeField] private ResourceState resourceState;
        [SerializeField] private ResourceState resourceCapacity;
        private const int PowerIndex = 0;
        [SerializeField] private int minimumPowerConsumption = 10;
        [SerializeField] private int minimumPowerCapacity = 20;
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
        }

        // Start is called before the first frame update
        private void Start()
        {
            moduleState.AddListener((pair) =>
            {
                var oldNewPair = pair.Value;
                ChangeModule(oldNewPair.Value);
                CalculatePower();
            });
            for (var i = 0; i < _modulesLocation.Count; i++)
            {
                ChangeModule(i, moduleState.Modules[i]);
            }

            CalculatePower();
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

        private void CalculatePower()
        {
            resourceState.Resources[PowerIndex] = minimumPowerConsumption;
            resourceCapacity.Resources[PowerIndex] = minimumPowerCapacity;
            for (var i = 0; i < moduleState.NumberOfModules; i++)
            {
                var module = moduleState.Modules[i];
                if (powerConsumption.Resources[module] > 0)
                    resourceState.Resources[PowerIndex] += powerConsumption.Resources[module];
                else
                    resourceCapacity.Resources[PowerIndex] += Mathf.Abs(powerConsumption.Resources[module]);
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