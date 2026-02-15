using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainMove : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;            
    public float travelDistance = 10f;  

    private float distanceMoved = 0f;
    public bool isActive = false;

    void Update()
    {
        if (isActive)
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
                isActive = false;
                Debug.Log("train moving.");
            }
        }
    }
}
