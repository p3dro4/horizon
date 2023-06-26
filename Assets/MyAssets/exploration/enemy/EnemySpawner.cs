using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyAssets.exploration.enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly List<KeyValuePair<float, float>> _enemyLocations = new();
        [SerializeField] private List<GameObject> enemyPrefabs = new();

        private void Awake()
        {
            _enemyLocations.Add(new KeyValuePair<float, float>(-13.84f, 0.69f));
        }
    }
}