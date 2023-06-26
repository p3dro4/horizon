using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace MyAssets.exploration.player
{
    public class PlayerController : MonoBehaviour
    {
        private enum Direction
        {
            Left,
            Right
        }

        private Rigidbody2D _rb;
        private bool _isGrounded;

        private float _playerScale;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Direction _direction;
        private Collider2D _collider2D;

        [SerializeField] private float jumpPower = 2f;

        [SerializeField] private float runSpeed = 0.5f;

        [SerializeField] private GameObject projectilePrefab;
        private bool _firing;

        private readonly List<ContactPoint2D> _contacts = new();
        private bool _collidingRight;
        private bool _collidingLeft;
        private static readonly int Running = Animator.StringToHash("running");
        private static readonly int Fire = Animator.StringToHash("fire");

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            var input = Input.GetAxis("Horizontal");

            if (_collidingRight && input > 0 || _collidingLeft && input < 0) input = 0;
            _rb.velocity = new Vector2(input * runSpeed, _rb.velocity.y);

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftShift) && !_firing)
            {
                _firing = true;
                _animator.SetTrigger(Fire);
                StartCoroutine(FireProjectile());
            }

            _animator.SetBool(Running, input != 0);

            if (input == 0) return;
            _direction = input > 0 ? Direction.Right : Direction.Left;
            _spriteRenderer.flipX = _direction == Direction.Left;
        }

        private IEnumerator FireProjectile()
        {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"));
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f);
            var offset = _direction == Direction.Left ? -1 : 1;
            var projectile = Instantiate(projectilePrefab, transform.position + new Vector3(offset, 0),
                Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity =
                new Vector2(_direction == Direction.Left ? -10 : 10, 0);
            Destroy(projectile, 5);
            _firing = false;
        }

        private void FixedUpdate()
        {
            _collider2D.GetContacts(_contacts);
            _collidingRight = false;
            _collidingLeft = false;
            _isGrounded = false;
            foreach (var contact in _contacts)
            {
                switch (contact.normal.x)
                {
                    case > 0:
                        if (_collidingRight) continue;
                        _collidingLeft = true;
                        break;
                    case < 0:
                        if (_collidingLeft) continue;
                        _collidingRight = true;
                        break;
                }

                if (_isGrounded) continue;
                _isGrounded = contact.normal.y > 0;
            }
        }
    }
}