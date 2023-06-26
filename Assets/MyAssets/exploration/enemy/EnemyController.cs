using MyAssets.exploration.player;
using UnityEngine;

namespace MyAssets.exploration.enemy
{
    public class EnemyController : MonoBehaviour
    {
        private enum Direction
        {
            Left,
            Right
        }

        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private float runSpeed = 3f;
        [SerializeField] private Enemy enemy;
        private float _health;
        private Direction _direction;
        [SerializeField] private Player player;

        public Enemy Enemy
        {
            get => enemy;
            set => enemy = value;
        }

        public float Health
        {
            get => _health;
            set => _health = value;
        }

        public Player Player
        {
            get => player;
            set => player = value;
        }

        public Animator Animator { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _health = enemy.Health;
            Animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_health <= 0) return;
            var velocity = _rb.velocity;
            _spriteRenderer.flipX = _direction == Direction.Left;
            velocity = new Vector2(runSpeed, velocity.y);
            _rb.velocity = _direction == Direction.Left ? -velocity : velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player") || !(_health > 0)) return;
            player.TakeDamage(enemy.Damage);
            _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("EdgeTrigger")) return;
            _direction = _direction == Direction.Left ? Direction.Right : Direction.Left;
        }
    }
}