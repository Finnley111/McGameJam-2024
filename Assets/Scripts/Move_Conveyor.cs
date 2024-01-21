using UnityEngine;

public class Move_Conveyor : MonoBehaviour
{
    private Vector3 targetPosition;
    private float gridSize = 1.0f; // Set this to the size of your grid
    private float moveSpeed = 1.0f; // Set this to the desired move speed

    void Update()
    {
        if (targetPosition != Vector3.zero)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                targetPosition = Vector3.zero;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Check if the player is touching a conveyor belt and is not currently moving
        if (targetPosition == Vector3.zero)
        {
            if (other.gameObject.CompareTag("cbu"))
            {
                targetPosition = transform.position + Vector3.up * gridSize;
            }
            else if (other.gameObject.CompareTag("cbl"))
            {
                targetPosition = transform.position + Vector3.left * gridSize;
            }
            else if (other.gameObject.CompareTag("cbr"))
            {
                targetPosition = transform.position + Vector3.right * gridSize;
            }
            else if (other.gameObject.CompareTag("cbd"))
            {
                targetPosition = transform.position + Vector3.down * gridSize;
            }
        }
    }

    Vector3 RoundToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x / gridSize) * gridSize,
            Mathf.Round(position.y / gridSize) * gridSize,
            position.z
        );
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is leaving a conveyor belt
        if (other.gameObject.CompareTag("cbu") || other.gameObject.CompareTag("cbl") || other.gameObject.CompareTag("cbr") || other.gameObject.CompareTag("cbd"))
        {
            targetPosition = Vector3.zero;
        }
    }
}
