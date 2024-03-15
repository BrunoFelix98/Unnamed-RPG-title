using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Players' health
    public float health = 100.0f;

    //Moving vector of the character
    private Vector2 move;

    //Characters' rigidbody
    private Rigidbody2D rb;

    //Movement speed of the character
    public float movementSpeed;

    //Running speed of the character
    public float runningSpeed;

    //Check if the character if facing right or not based on mouse position
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        //Get the characters' rigidbody
        rb = GetComponent<Rigidbody2D>();

        movementSpeed = 100.0f;
        runningSpeed = movementSpeed * 2;
    }

    // Update is called once per frame (based on hardware performance, inconsistent)
    void Update()
    {
        //Keep track of mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Get direction of movement based on input
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Flip the character to face mouse position
        if (mousePos.x < transform.position.x && facingRight)
        {
            flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            flip();
        }

        //Testing the "TakeDamage" script
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1.0f);
        }*/
    }

    //Fixed update is called once every physics update (not based on hardware performance, consistent)
    private void FixedUpdate()
    {
        //Normalize the direction to prevent diagonal movement from being faster than linear movement
        Vector2 directionNormal = move.normalized;

        //Add velocity based on speed and direction, if holding "left shift" the speed is doubled, and the character sprints/runs
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(directionNormal.x * runningSpeed * Time.fixedDeltaTime, directionNormal.y * runningSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = new Vector2(directionNormal.x * movementSpeed * Time.fixedDeltaTime, directionNormal.y * movementSpeed * Time.fixedDeltaTime);
        }
    }

    //Flip character
    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //Handle player taking damage
    public void TakeDamage(float amount)
    {
        //If the current health of the player is still enough to survive, the player takes the damage, however, if it isn't, the player dies
        if (health - amount > 0)
        {
            health -= amount;
            print("Player took " + amount + " hitpoints of damage.");
        }
        else
        {
            health = 0;
            print("Player died!");
        }
    }
}
