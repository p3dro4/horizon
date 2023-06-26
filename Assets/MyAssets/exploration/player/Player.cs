using UnityEngine;

namespace MyAssets.exploration.player
{
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObject/Player", order = 16)]
    public class Player : ScriptableObject
    {
        [SerializeField] private float initialHealth = 100f;
        [SerializeField] private float currentHealth;
        [Space(5)] [SerializeField] private float damage = 10f;
        [SerializeField] private float fireRate = 2f;

        public float InitialHealth => initialHealth;
        public float Damage => damage;
        public float CurrentHealth => currentHealth;
        public float FireRate => fireRate;

        public void TakeDamage(float damageValue)
        {
            currentHealth -= damageValue;
        }

        private void OnEnable()
        {
            currentHealth = initialHealth;
        }
    }
}