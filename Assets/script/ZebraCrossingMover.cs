using UnityEngine;

public class ZebraCrossingMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;           // Speed of the cube (pedestrian/car)
    public float travelDistance = 10f; // Width of the road/crossing

    private float distanceMoved = 0f;
    public bool isCrossing = false;

    void Update()
    {
        if (isCrossing)
        {
            // 1. Calculate how much to move this frame
            float step = speed * Time.deltaTime;

            // 2. Move the cube forward
            transform.Translate(Vector3.forward * step);

            // 3. Track the total distance
            distanceMoved += step;

            // 4. Check if we have crossed the zebra crossing
            if (distanceMoved >= travelDistance)
            {
                isCrossing = false;
                Debug.Log("Object has finished crossing the Zebra Crossing.");
            }
        }
    }
}