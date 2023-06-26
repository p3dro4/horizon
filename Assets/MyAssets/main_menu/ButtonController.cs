using UnityEngine;
using UnityEngine.EventSystems;

namespace MyAssets
{
    public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
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