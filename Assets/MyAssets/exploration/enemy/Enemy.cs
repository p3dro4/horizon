using UnityEngine;

namespace MyAssets.exploration.enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObject/Enemy", order = 15)]
    public class Enemy : ScriptableObject
    {
        [SerializeField] private float runSpeed = 3f;
        [SerializeField] private float health = 100f;
        [SerializeField] private float damage = 10f;

        public float RunSpeed => runSpeed;
        public float Health => health;
        public float Damage => damage;
    }
}