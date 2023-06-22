using System;
using MyAssets.module_management;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MyAssets.ui.selection
{
    public class MouseoverSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool _mouseOver = false;
        private Image _image;
        private TextMeshProUGUI _textMeshProUGUI;

        [SerializeField] private GameObject buildMenu;
        [SerializeField] private SelectedModule selectedModule;
        [SerializeField] private ModuleProperties moduleProperties;
        [SerializeField] private ModuleState moduleState;

        // Start is called before the first frame update
        void Start()
        {
            _image = GetComponent<Image>();
            _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_mouseOver)
            {
                _image.color = Color.white;
                selectedModule.SelectedModuleValue = transform.GetSiblingIndex();
                var currentModule = moduleState.Modules[selectedModule.SelectedModuleValue];
                _textMeshProUGUI.text = moduleProperties.ModuleNames[currentModule];
                _textMeshProUGUI.color = moduleProperties.ModuleColor[currentModule];
                _textMeshProUGUI.outlineColor = moduleProperties.ModuleTextOutlineColor[currentModule];
            }
            else
            {
                _image.color = Color.clear;
                _textMeshProUGUI.color = Color.clear;
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseOver = false;
        }

        public void ShowBuildMenu()
        {
            buildMenu.SetActive(true);
        }
    }
}