using System;
using UnityEngine;
using System.Collections.Generic;
using MyAssets.module_management;
using TMPro;
using UnityEngine.UI;

namespace MyAssets.ui.build_menu
{
    public class BuildMenuController : MonoBehaviour
    {
        [SerializeField] private SelectedModule selectedModule;
        [SerializeField] private ModuleState moduleState;
        [SerializeField] private ModuleProperties moduleProperties;
        [SerializeField] private Sprite demolishSprite;

        private int _firstUpgradeModule;


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

        private void InitMenus()
        {
            //Gets the current module
            var currentModule = moduleState.Modules[selectedModule.SelectedModuleValue];
            //Checks if the current module is upgraded
            var upgraded = currentModule >= _firstUpgradeModule;
            //Iterates through all the upgrade modules and creates an option for each one 
            var options = transform.GetChild(1);
            List<int> availableModules = new();

            for (var i = _firstUpgradeModule; i < _firstUpgradeModule + options.childCount; i++)
            {
                if (currentModule != i) availableModules.Add(i);
            }

            if (upgraded) availableModules.Add(-1);
            var y = 0;
            foreach (var module in availableModules)
            {
                var button = options.GetChild(y++).GetComponent<Button>();
                button.GetComponentInChildren<TextMeshProUGUI>().text =
                    module == -1 ? "Demolish" : moduleProperties.ModuleNames[module];
                button.GetComponent<Image>().sprite = module != -1
                    ? moduleProperties.ModuleImage[module - _firstUpgradeModule]
                    : demolishSprite;
                button.onClick.AddListener(() =>
                {
                    moduleState.SetModule(selectedModule.SelectedModuleValue, module != -1 ? module : 0);
                    gameObject.SetActive(false);
                });
            }
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