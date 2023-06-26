using System;
using MyAssets.base_building.resource_management;
using MyAssets.resource_management;
using UnityEngine;

namespace MyAssets.exploration.resources
{
    public class ResourceController : MonoBehaviour
    {
        private enum ResourceType
        {
            Morkite,
            Bismor,
            Phazyonite,
        }


        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int amount = 50;
        [SerializeField] private ResourceState resourceState;

        private void Update()
        {
            //go up and down a little
            transform.position = new Vector3(transform.position.x,
                transform.position.y + Mathf.Sin(Time.time) * 0.0005f,
                transform.position.z);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            switch (resourceType)
            {
                case ResourceType.Morkite:
                    resourceState.SetResource(0, amount);
                    break;
                case ResourceType.Bismor:
                    resourceState.SetResource(1, amount);
                    break;
                case ResourceType.Phazyonite:
                    resourceState.SetResource(2, amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Destroy(gameObject);
        }
    }
}