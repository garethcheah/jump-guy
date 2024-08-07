using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private static string _runConditionName = "isRunning";
    private static string _groundedConditionName = "isGrounded";
    private bool _isGrounded = true;
    private float _horizontalInput;
    private float _verticalPosition;
    private Animator _playerAnimator;
    private SpriteRenderer _playerSR;
    private Rigidbody2D _playerRB;
    private GameObject _failIndicator;
    private GameManager _gameManager;

    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float jumpPower = 10.0f;
    [SerializeField] private float timeForActiveIndicators = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerSR = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
        _failIndicator = transform.Find("Damage Indicator").GameObject();
        _gameManager = FindObjectOfType<GameManager>();

        _failIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if player has fallen of the platform
        _verticalPosition = transform.position.y;

        if (_verticalPosition <= -6.0f)
        {
            // Restart player back at the start
            _gameManager.ResetGame();
            return;
        }

        _horizontalInput = Input.GetAxis("Horizontal");
        MovePlayer();


        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            JumpPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _playerAnimator.SetBool(_groundedConditionName, _isGrounded);
        }
        else if (collision.gameObject.CompareTag("Collectible"))
        {
            collision.GameObject().SetActive(false);
            _gameManager.UpdateCollectibleCount();
        }
        else if (collision.gameObject.CompareTag("PlatformStart"))
        {
            _gameManager.GenerateNextPlatform(collision.transform.parent.GameObject());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with obstacle detected.");

        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
        {
            TurnOnDamageIndicator();
            _gameManager.UpdateLastDamagedTime();
            Invoke("TurnOffDamageIndicator", timeForActiveIndicators);
        }
    }

    private void MovePlayer()
    {
        // Apply player movement
        _playerRB.velocity = new Vector2(_horizontalInput * runSpeed, _playerRB.velocity.y);

        // Initiate running animation if horizontal input is detected
        if (Mathf.Abs(_horizontalInput) > 0.0f)
        {
            _playerSR.flipX = Mathf.Sign(_horizontalInput) < 0;
            _playerAnimator.SetBool(_runConditionName, true);
        }

        // Stop running animation if no horizontal input
        if (Mathf.Abs(_horizontalInput) == 0.0f)
        {
            _playerAnimator.SetBool(_runConditionName, false);
        }
    }

    private void JumpPlayer()
    {
        _playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        _isGrounded = false;
        _playerAnimator.SetBool(_groundedConditionName, _isGrounded);
    }

    private void TurnOnDamageIndicator()
    {
        _failIndicator.SetActive(true);
        _playerSR.color = UnityEngine.Color.red;
    }

    private void TurnOffDamageIndicator()
    {
        _failIndicator.SetActive(false);
        _playerSR.color = UnityEngine.Color.white;
    }
}
