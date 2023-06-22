using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace MyAssets.module_management
{
    public class ModulesController : MonoBehaviour
    {
        private readonly List<KeyValuePair<float, float>> _modulesLocation = new();
        [SerializeField] private ModuleProperties moduleProperties;
        [SerializeField] private ModuleState moduleState;
        [SerializeField] private SelectedModule selectedModule;
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
            if (moduleState.Modules.Count >= _modulesLocation.Count) return;
            moduleState.FillRemaining();
        }

        // Start is called before the first frame update
        private void Start()
        {
            for (var i = 0; i < _modulesLocation.Count; i++)
            {
                var child = transform.GetChild(i);
                if (child != null)
                {
                    Destroy(child.gameObject);
                }
            }

            moduleState.AddListener((pair) => { ChangeModule(pair.Value); });
            for (var i = 0; i < _modulesLocation.Count; i++)
            {
                var module = Instantiate(_modules[moduleState.Modules[i]], transform, true);
                module.transform.localPosition = new Vector3(_modulesLocation[i].Key, _modulesLocation[i].Value, 0);
            }
        }

        private void ChangeModule(int prefab)
        {
            Destroy(transform.GetChild(selectedModule.SelectedModuleValue).gameObject);
            var module = Instantiate(_modules[prefab], transform, true);
            module.transform.localPosition = new Vector3(_modulesLocation[selectedModule.SelectedModuleValue].Key,
                _modulesLocation[selectedModule.SelectedModuleValue].Value, 0);
            module.transform.SetSiblingIndex(selectedModule.SelectedModuleValue);
        }
    }
}