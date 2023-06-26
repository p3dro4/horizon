using MyAssets.exploration.player;
using UnityEngine;
using UnityEngine.UI;

namespace MyAssets.exploration.HealthBar
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite emptyHeart;

        // Start is called before the first frame update

        // Update is called once per frame
        private void Update()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                var image = child.GetComponent<Image>();
                image.sprite = i < player.CurrentHealth ? fullHeart : emptyHeart;
            }
        }
    }
}