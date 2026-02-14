 
using UnityEngine;
using System.Collections; // Required for Coroutines
public class PlayerTriggerHandler : MonoBehaviour
{
    private PlayerStat stats;

    [Header("Cooldown Settings")]
    public float cooldownTime = 0.5f;
    private float nextActionTime = 0f;

    // Drag the separate Traffic Light object here in the Inspector
    [Header("References")]
    public TrafficLightController trafficLight;

    void Start()
    {
        stats = GetComponent<PlayerStat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (Time.time >= nextActionTime)
            {
                // 1. Calculate Minutes and Seconds
                float currentTime = Time.time;
                int minutes = Mathf.FloorToInt(currentTime / 60); // Total seconds divided by 60
                int seconds = Mathf.FloorToInt(currentTime % 60); // The remainder using Modulo

                // 2. Format into a string (00:00 style)
                string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

                // 3. Logic: Add coin and Set cooldown
                stats.AddCoin(1);
                nextActionTime = Time.time + cooldownTime;

                // 4. Display the formatted time
                Debug.Log("Item Collected! Game Time: " + timeString);

                Destroy(other.gameObject);
            }





            
        }
        else if (other.CompareTag("getTimeTraf"))
        {
            other.enabled = false;
            float currentTime = Time.time;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

            // 2. GET THE SPEED (Improved search)
            // Get the Drivetrain script directly
            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
            float actualSpeed = 0f;

            if (drivetrain != null)
            {
                actualSpeed = drivetrain.speed; // This is the raw RigidBody speed * 3.6f
            }

            // Pass the actual physics speed, not the dashboard speed
            stats.SaveTrafficData(timeStringT, actualSpeed);

            //Debug.Log("Saved Time: " + timeStringT + " | Saved Speed: " + actualSpeed);


            // 3. Start the Traffic Light Sequence
            // 3. Get the controller from the object we hit (or its parent)
            // Talk to the specifically linked traffic light
            if (trafficLight != null)
            {
                trafficLight.StartSequence();
            }
            else
            {
                Debug.LogError("Assign the TrafficLight in the Inspector!");
            }




        }
    }
    

 
}