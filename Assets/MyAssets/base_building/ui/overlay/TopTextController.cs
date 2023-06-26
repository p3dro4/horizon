using MyAssets.base_building.resource_management;
using MyAssets.resource_management;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.base_building.ui.overlay
{
    public class TopTextController : MonoBehaviour
    {
        private enum TopTextType
        {
            Capacity,
            Normal,
            Income,
            IncomePercentage
        }

        [SerializeField] private ResourceState currentResource;
        [SerializeField] private ResourceState resourceCapacity;
        [SerializeField] private ResourceState resourceIncome;
        [SerializeField] private ResourceProperties resourceProperties;
        [SerializeField] private int resourceIndex;
        [SerializeField] private TopTextType formatType = TopTextType.Normal;
        private Image _image;
        private TextMeshProUGUI _text;
        private Color _defaultColor;

        // Start is called before the first frame update
        private void Start()
        {
            _text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _image = GetComponentInChildren<Image>();
            _image.sprite = resourceProperties.ResourceImage[resourceIndex];
            _defaultColor = _text.color;
        }

        // Update is called once per frame
        private void Update()
        {
            _text.text = formatType switch
            {
                TopTextType.Capacity =>
                    $"{currentResource.Resources[resourceIndex]}/{resourceCapacity.Resources[resourceIndex]}",
                TopTextType.Normal => $"{currentResource.Resources[resourceIndex]}",
                TopTextType.Income =>
                    $"{currentResource.Resources[resourceIndex]}({resourceIncome.Resources[resourceIndex]:+#;-#;0})",
                TopTextType.IncomePercentage =>
                    $"{currentResource.Resources[resourceIndex]}({resourceIncome.Resources[resourceIndex]:+#;-#;0}%)",
                _ => _text.text
            };
            if (formatType == TopTextType.Capacity &&
                currentResource.Resources[resourceIndex] > resourceCapacity.Resources[resourceIndex])
                _text.color = Color.red;
            else
                _text.color = _defaultColor;
        }
    }
}