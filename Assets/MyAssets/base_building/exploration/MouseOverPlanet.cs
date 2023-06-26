using UnityEngine;
using UnityEngine.EventSystems;

namespace MyAssets.base_building.exploration
{
    public class MouseOverPlanet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}