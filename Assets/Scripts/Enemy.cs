using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    private SpriteRenderer enemySR;

    [SerializeField] private float moveSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRB.velocity.x == 0f)
        {
            MoveEnemy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBoundary"))
        {
            FlipEnemy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            FlipEnemy();
        }
    }

    private void MoveEnemy()
    {
        // Set move direction to left (default)
        float moveDirection = -1.0f;

        if (enemySR.flipX)
        {
            // Set move direction to right
            moveDirection = 1.0f;
        }

        // Apply enemy movement
        enemyRB.velocity = new Vector2(moveDirection * moveSpeed, enemyRB.velocity.y);
    }

    private void FlipEnemy()
    {
        enemyRB.velocity = Vector2.zero;
        enemySR.flipX = !enemySR.flipX;
    }

}
