using MyAssets.exploration.player;
using UnityEngine;

namespace MyAssets.exploration.enemy
{
    public class HitBox : MonoBehaviour
    {
        private Enemy _enemy;
        private Player _player;
        private EnemyController _enemyController;
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int Death = Animator.StringToHash("dead");

        // Start is called before the first frame update
        private void Start()
        {
            _enemyController = GetComponentInParent<EnemyController>();
            _enemy = _enemyController.Enemy;
            _player = _enemyController.Player;
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Projectile")) return;
            Destroy(other.gameObject);
            _enemyController.Health -= _player.Damage;
            _enemyController.Animator.SetTrigger(Hit);
            if (!(_enemyController.Health <= 0)) return;
            _enemyController.Animator.SetTrigger(Death);
            Destroy(_enemyController.gameObject, 1f);
        }
    }
}