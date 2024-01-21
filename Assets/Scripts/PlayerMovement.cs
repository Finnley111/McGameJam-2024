using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.5f;
    public float dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    public bool isDead = true;
    public int originalLayer;
    public int dashingLayer;

    private Vector2 dashDirection;
    private Vector2 lastMovementDirection;


    public void killPlayer()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    void Start()
    {
        activeMoveSpeed = moveSpeed;
        rb = this.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        originalLayer = gameObject.layer;
        dashingLayer = LayerMask.NameToLayer("Dashing");
    }

    void Update()
    {
        if (GameManager.pegsActive["start"]) {
            PlayerMove();
        }
    }

    void PlayerMove() {
        if (GameManager.pegsActive["right"] && Input.GetAxis("Horizontal") > 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");

        }
        else if (GameManager.pegsActive["left"] && Input.GetAxis("Horizontal") < 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
        }
        //movement.x = Input.GetAxisRaw("Horizontal");

        if (GameManager.pegsActive["up"] && Input.GetAxis("Vertical") > 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");

        }
        else if (GameManager.pegsActive["down"] && Input.GetAxis("Vertical") < 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }

        if (movement.x != 0 || movement.y != 0)
        {
            lastMovementDirection = movement.normalized; // Store the last movement direction
        }

        movement.Normalize();

        rb.velocity = movement * activeMoveSpeed;

        // Update the player's facing direction based on input
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }


        if (Input.GetKeyDown(KeyCode.Space) && GameManager.pegsActive["dash"])
        {
            if (dashCoolCounter <= 0 && dashCoolCounter <= 0)
            {
                //Vector2 dashDirection = (movement.x != 0 || movement.y != 0) ? movement.normalized : lastMovementDirection;
                gameObject.layer = dashingLayer;
                Debug.Log("Current Layer: " + LayerMask.LayerToName(gameObject.layer));
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                rb.velocity = dashDirection * dashSpeed; // Apply velocity in the dash direction
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                gameObject.layer = originalLayer;
                activeMoveSpeed = moveSpeed;
                rb.velocity = Vector2.zero;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("obstacle"))
        {

            // Player collided with the DeathObstacle
            // if (gameObject != null)
            // {
            Debug.Log("Player is dead");
            killPlayer();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.pegsActive = new Dictionary<string, bool>(){
            {"up", false},
            {"down", false},
            {"right", false},
            {"left", false},
            {"dash", false},
            {"start", false},
            };
            // }
        }

        if (other.gameObject.CompareTag("goal"))
        {

            // Player collided with the DeathObstacle
            // if (gameObject != null)
            // {
            Debug.Log("Next Level");
            LoadNextLevel();
            // }
        }

        // check if touching a conveyor belt labelled cbu, cbl, cbr, cbd and mvove accordingly and stop when not on it
      

    }
    void OnCollisionStay(Collision other)
    {
        Debug.Log("Player is on conveyor belt");
        // Check if the player is touching a conveyor belt
        if (other.gameObject.CompareTag("cbu"))
        {
            // Move the player up
            Debug.Log("Player is on conveyor belt");
            movement.y = 1;
            transform.position += Vector3.up * Time.deltaTime;
        }
        else if (other.gameObject.CompareTag("cbl"))
        {
            // Move the player left
            transform.position += Vector3.left * Time.deltaTime;
        }
        else if (other.gameObject.CompareTag("cbr"))
        {
            // Move the player right
            transform.position += Vector3.right * Time.deltaTime;
        }
        else if (other.gameObject.CompareTag("cbd"))
        {
            // Move the player down
            transform.position += Vector3.down * Time.deltaTime;
        }
    }


    private void LoadNextLevel()
    {
        // Assuming you want to load the next scene in the build order
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the range of available scenes
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            GameManager.pegsActive = new Dictionary<string, bool>(){
            {"up", false},
            {"down", false},
            {"right", false},
            {"left", false},
            {"dash", false},
            {"start", false},
            };
        }
        else
        {
            Debug.Log("No more levels to load");
            // Optionally, you could load a 'game completed' scene or return to the main menu
        }
    }
}
