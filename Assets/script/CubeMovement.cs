using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f; // You can change this in Inspector

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A & D
        float moveZ = Input.GetAxis("Vertical");   // W & S

        Vector3 move = new Vector3(moveX, 0f, moveZ);

        transform.Translate(move * speed * Time.deltaTime);
    }
}
