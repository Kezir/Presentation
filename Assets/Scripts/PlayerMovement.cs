using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    left,
    middle,
    right
}
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;

    public Side side = Side.middle;

    private bool left, right, up;

    public Vector3 directionRay;
    private RaycastHit hit;

    float moveToX = 0f;

    public float moveWidth;

    private CharacterController player;

    private float tempX, tempY;

    public float dodgeSpeed;
    public float jumpForce;
    public float speed;
    private Vector3 moveVector;

    void Start()
    {
        player = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
    }

    void Update()
    {
        Inputs();

        if(player.isGrounded)
        {
            MoveSides();
        }
        Jump();
        moveVector = new Vector3(tempX - transform.position.x, tempY * Time.deltaTime, speed * Time.deltaTime);
        tempX = Mathf.Lerp(tempX, moveToX, Time.deltaTime * dodgeSpeed);
        player.Move(moveVector);
    }

    private void Jump()
    {
        if(player.isGrounded)
        {
            if (up)
            {
                tempY = jumpForce;              
            }
        }
        else
        {
            tempY -= jumpForce * 2 * Time.deltaTime;
        }
        
    }

    private void Inputs()
    {
        left = Input.GetKeyDown(KeyCode.LeftArrow);
        right = Input.GetKeyDown(KeyCode.RightArrow);
        up = Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool CheckForCollisions(Side _side)
    {

        // Only move if we wouldn't hit something 
        if(_side == Side.left )
        {
            directionRay = Vector3.left;
        }
        else
        {
            directionRay = Vector3.right;
        }

        if (playerRb.SweepTest(directionRay, out hit, moveWidth))
        {
            //Debug.Log("true");
            directionRay = Vector3.zero;
            return true;
        }
        else
        {
            //Debug.Log("false");
            directionRay = Vector3.zero;
            return false;
        }

    }

    private void MoveSides()
    {

        if (left)
        {
            if(CheckForCollisions(Side.left))
            {
                return;
            }

            if (side == Side.middle)
            {
                moveToX -= moveWidth;
                side = Side.left;
            }
            else if (side == Side.right)
            {
                moveToX = 0;
                side = Side.middle;
            }
        }
        else if (right)
        {
            if(CheckForCollisions(Side.right))
            {
                return;
            }

            if (side == Side.middle)
            {
                moveToX = moveWidth;
                side = Side.right;
            }
            else if (side == Side.left)
            {
                moveToX = 0;
                side = Side.middle;
            }
        }

    }
}
