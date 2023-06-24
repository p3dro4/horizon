using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace MyAssets.ui.build_menu
{
    public class OptionOnMouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI _text;
        private GameObject _requirements;
        private GameObject _enoughResources;
        private BuildMenuController _controller;
        private bool _mouseOver;

        private void Start()
        {
            _text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _requirements = transform.GetChild(2).gameObject;
            _enoughResources = transform.GetChild(3).gameObject;
            _controller = GetComponentInParent<BuildMenuController>();
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
            _enoughResources.SetActive(_mouseOver && !_controller.HasEnoughResources[transform.GetSiblingIndex()]);
        }
    }
}