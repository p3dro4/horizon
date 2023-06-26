using MyAssets.base_building.resource_management;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

namespace MyAssets.base_building.ui.build_menu
{
    public class OptionOnMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private MonoBehaviour controller;

        private TextMeshProUGUI _text;
        private GameObject _requirements;
        private GameObject _enoughResources;
        private bool _mouseOver;
        private IUiController _controller;

        private void Start()
        {
            _text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _requirements = transform.GetChild(2).gameObject;
            _enoughResources = transform.GetChild(3).gameObject;
            _controller = (IUiController)controller;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _mouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _mouseOver = false;
        }

        private void Update()
        {
            _text.gameObject.SetActive(_mouseOver);
            _requirements.SetActive(_mouseOver);
            _enoughResources.SetActive(_mouseOver && !_controller.EnoughResources(transform.GetSiblingIndex()));
        }
    }
}