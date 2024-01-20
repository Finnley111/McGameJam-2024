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

        movement.Normalize();

        rb.velocity = movement * activeMoveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.pegsActive["dash"])
        {
            if (dashCoolCounter <= 0 && dashCoolCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
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
        }
        else
        {
            Debug.Log("No more levels to load");
            // Optionally, you could load a 'game completed' scene or return to the main menu
        }
    }
}
