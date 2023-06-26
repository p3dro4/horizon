using UnityEngine;
using UnityEngine.Serialization;

namespace MyAssets.base_building.ui.overlay
{
    public class OverlayController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject buildMenu;
        [SerializeField] private GameObject overlay;
        [SerializeField] private GameObject research;
        [SerializeField] private GameObject explore;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            overlay.transform.GetChild(1).gameObject.SetActive(!(buildMenu.activeSelf || research.activeSelf));
        }
    }
}