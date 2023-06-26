using TMPro;
using UnityEngine;

namespace MyAssets.base_building.ui.build_menu
{
    public class RequirementsController : MonoBehaviour
    {
        private void OnEnable()
        {
            foreach (Transform child in transform)
            {
                var text = child.GetComponentInChildren<TextMeshProUGUI>();
                child.gameObject.SetActive(float.Parse(text.text) != 0);
            }
        }
    }
}