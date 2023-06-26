using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.base_building.ui
{
    public class ButtonInvoker : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private KeyCode keyCode;

        private void Start()
        {
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                _button.onClick.Invoke();
            }
        }
    }
}