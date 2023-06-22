using MyAssets.resource_management;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MyAssets.ui.overlay
{
    public class TopTextController : MonoBehaviour
    {
        [SerializeField] private ResourceState currentResource;
        [SerializeField] private ResourceState resourceCapacity;
        [SerializeField] private int resourceIndex;
        private TextMeshProUGUI _text;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            _text.text = $"{currentResource.Resources[resourceIndex]}/{resourceCapacity.Resources[resourceIndex]}";
        }
    }
}