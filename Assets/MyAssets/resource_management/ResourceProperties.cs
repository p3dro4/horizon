using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.resource_management
{
    [CreateAssetMenu(fileName = "ResourceProperties", menuName = "ScriptableObject/ResourceProperties", order = 4)]
    public class ResourceProperties : ScriptableObject
    {
        [SerializeField] private List<string> resourceNames = new();
        [SerializeField] private List<Sprite> resourceImage = new();

        public List<string> ResourceNames => resourceNames;

        public List<Sprite> ResourceImage => resourceImage;
    }
}