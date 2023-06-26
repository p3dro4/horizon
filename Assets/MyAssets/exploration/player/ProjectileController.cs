using UnityEngine;

namespace MyAssets.exploration.player
{
    public class ProjectileController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy")) return;
            Destroy(gameObject);
        }
    }
}
