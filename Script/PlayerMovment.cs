using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private Vector3 currentDirection = Vector3.right;
    public float rotationSpeed;
    public float speed;

    private void Update()
    {
        // Check for mouse input
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = (mousePosition - transform.position).normalized;

        // Only update the direction if there's non-zero input
        if (directionToMouse != Vector3.zero)
        {
            currentDirection = directionToMouse;
        }

        MoveSnake();
    }

    public void MoveSnake()
    {
        // Calculate the target rotation based on the current direction
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, currentDirection);

        // Smoothly interpolate between the current rotation and the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 3);

        // Move the snake continuously in the current direction
        Vector3 movement = transform.up * speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
    }
}
