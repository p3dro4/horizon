using UnityEngine;

namespace MyAssets.ui.overlay
{
    public class OverlayController : MonoBehaviour
    {
        // Start is called before the first frame update
        private GameObject _buildMenu;
        private GameObject _overlay;

        void Start()
        {
            _buildMenu = transform.GetChild(1).gameObject;
            _overlay = transform.GetChild(2).gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            _overlay.SetActive(!_buildMenu.activeSelf);
        }
    }
}