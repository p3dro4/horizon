using UnityEngine;

namespace MyAssets.main_menu
{
    public class RotateSun : MonoBehaviour

    {
        [SerializeField] private float rotationSpeed = 2.5f;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.left * (Time.deltaTime * rotationSpeed));
        }
    }
}