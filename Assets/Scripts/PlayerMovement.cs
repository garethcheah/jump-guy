using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static string runConditionName = "isRunning";
    private static string groundedConditionName = "isGrounded";
    private bool isGrounded = true;
    private float horizontalInput;
    private Animator playerAnimator;
    private SpriteRenderer playerSR;
    private Rigidbody2D playerRB;

    [SerializeField]
    private float runSpeed = 5f;

    [SerializeField]
    private float jumpPower = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerSR = GetComponent<SpriteRenderer>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        MovePlayer();


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            JumpPlayer();
        }
    }

    private void MovePlayer()
    {
        // Apply player movement
        playerRB.velocity = new Vector2(horizontalInput * runSpeed, playerRB.velocity.y);

        // Initiate running animation if key down is detected
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerSR.flipX = Input.GetKeyDown(KeyCode.LeftArrow);
            playerAnimator.SetBool(runConditionName, true);
        }

        // Stop running animation if key up is detected
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAnimator.SetBool(runConditionName, false);
        }
    }

    private void JumpPlayer()
    {
        playerRB.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        isGrounded = false;
        playerAnimator.SetBool(groundedConditionName, isGrounded);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        playerAnimator.SetBool(groundedConditionName, isGrounded);
    }
}
