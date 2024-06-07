using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static string runConditionName = "isRunning";
    private Animator playerAnimator;

    [SerializeField]
    private float runSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Gets input from axis
        float input = Input.GetAxis("Horizontal");

        // Calculate movement
        Vector2 movement = new Vector2(input * Time.deltaTime * runSpeed, 0f);

        // Applies movement
        transform.Translate(movement);

        // Initiate running animation if key down is detected
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerAnimator.SetBool(runConditionName, true);
        }

        // Stop running animation if key up is detected
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerAnimator.SetBool(runConditionName, false);
        }
    }
}
