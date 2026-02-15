
//using System.Collections; // Required for Coroutines
//using UnityEngine;
//using UnityEngine.SceneManagement;
//public class PlayerTriggerHandler : MonoBehaviour
//{
//    private PlayerStat stats;

//    [Header("Cooldown Settings")]
//    public float cooldownTime = 0.5f;
//    private float nextActionTime = 0f;

//    // Drag the separate Traffic Light object here in the Inspector
//    [Header("References")]
//    public TrafficLightController trafficLight;

//    void Start()
//    {
//        stats = GetComponent<PlayerStat>();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Collectible"))
//        {
//            if (Time.time >= nextActionTime)
//            {
//                // 1. Calculate Minutes and Seconds
//                float currentTime = Time.time;
//                int minutes = Mathf.FloorToInt(currentTime / 60); // Total seconds divided by 60
//                int seconds = Mathf.FloorToInt(currentTime % 60); // The remainder using Modulo

//                // 2. Format into a string (00:00 style)
//                string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

//                // 3. Logic: Add coin and Set cooldown
//                stats.AddCoin(1);
//                nextActionTime = Time.time + cooldownTime;

//                // 4. Display the formatted time
//                Debug.Log("Item Collected! Game Time: " + timeString);

//                Destroy(other.gameObject);
//            }






//        }
//        else if (other.CompareTag("getTimeTraf"))
//        {
//            other.enabled = false;
//            float currentTime = Time.time;
//            int minutes = Mathf.FloorToInt(currentTime / 60);
//            int seconds = Mathf.FloorToInt(currentTime % 60);
//            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

//            // 2. GET THE SPEED (Improved search)
//            // Get the Drivetrain script directly
//            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
//            float actualSpeed = 0f;

//            if (drivetrain != null)
//            {
//                actualSpeed = drivetrain.speed; // This is the raw RigidBody speed * 3.6f
//            }

//            // Pass the actual physics speed, not the dashboard speed
//            stats.SaveTrafficData(timeStringT, actualSpeed);

//            //Debug.Log("Saved Time: " + timeStringT + " | Saved Speed: " + actualSpeed);


//            // 3. Start the Traffic Light Sequence
//            // 3. Get the controller from the object we hit (or its parent)
//            // Talk to the specifically linked traffic light
//            if (trafficLight != null)
//            {
//                trafficLight.StartSequence();
//            }
//            else
//            {
//                Debug.LogError("Assign the TrafficLight in the Inspector!");
//            }




//        }

//        // 4. Check for Round Triggers
//        if (other.CompareTag("roundOne")) { stats.CheckPathPatern(1);
//            other.enabled = false;
//        }
//        else if (other.CompareTag("roundTwo")){ stats.CheckPathPatern(2);
//            other.enabled = false;
//        }
//        else if (other.CompareTag("roundThree")){ stats.CheckPathPatern(3);
//            other.enabled = false;
//        }
//        else if (other.CompareTag("roundFour")){ stats.CheckPathPatern(4);
//            other.enabled = false;
//        }


//        if (other.CompareTag("humanTrigger"))
//        {

//            other.enabled = false;
//            float currentTime = Time.time;
//            int minutes = Mathf.FloorToInt(currentTime / 60);
//            int seconds = Mathf.FloorToInt(currentTime % 60);
//            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

//            // 2. GET THE SPEED (Improved search)
//            // Get the Drivetrain script directly
//            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
//            float actualSpeed = 0f;

//            if (drivetrain != null)
//            {
//                actualSpeed = drivetrain.speed; // This is the raw RigidBody speed * 3.6f
//            }

//            // Pass the actual physics speed, not the dashboard speed
//            stats.SaveChildCrossRoadData(timeStringT, actualSpeed);

//            //move the zebra crossing
//            ZebraCrossingMover mover = other.GetComponentInChildren<ZebraCrossingMover>();


//            // Generate a random number between 0 and 1
//            float chance = Random.value;

//            if (chance <= 0.7f) // 70% chance
//            {
//                mover.isCrossing = true;
//                Debug.Log("70% Chance hit: Pedestrian is CROSSING.");
//            }
//            else // 30% chance
//            {
//                mover.isCrossing = false;
//                Debug.Log("30% Chance hit: Pedestrian is WAITING.");
//            }

//        }

//        if (other.CompareTag("child"))
//        {
//            ZebraCrossingMover mover = other.GetComponent<ZebraCrossingMover>();
//            other.enabled = false;
//            if (mover.isCrossing)
//            {
//                stats.SaveChildCrossState(false);
//             //   Debug.LogError("child hit");
//            }
//            else {
//                stats.SaveChildCrossState(true);
//               // Debug.LogError("child not hit");
//            }

//        }

//        if (other.CompareTag("checkEnter"))
//        {
//            other.enabled = false;
//            stats.checkPoint(true);

//        }





//        //train scene
//        if (other.CompareTag("trainTrigger"))
//        {

//            other.enabled = false;
//            float currentTime = Time.time;
//            int minutes = Mathf.FloorToInt(currentTime / 60);
//            int seconds = Mathf.FloorToInt(currentTime % 60);
//            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

//            // 2. GET THE SPEED (Improved search)
//            // Get the Drivetrain script directly
//            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
//            float actualSpeed = 0f;

//            if (drivetrain != null)
//            {
//                actualSpeed = drivetrain.speed; // This is the raw RigidBody speed * 3.6f
//            }

//            // Pass the actual physics speed, not the dashboard speed
//            stats.SaveTrainPassData(timeStringT, actualSpeed);

//            //move the zebra crossing
//            trainMove mover = other.GetComponentInChildren<trainMove>();


//            // Generate a random number between 0 and 1
//            float chance = Random.value;

//            if (chance <= 0.8f) // 70% chance
//            {
//                mover.isActive = true;
//                Debug.Log("80% chance to train active.");
//            }
//            else // 30% chance
//            {
//                mover.isActive = false;
//                Debug.Log("20% chance to train not active..");
//            }

//        }

//        if (other.CompareTag("train"))
//        {
//            trainMove mover = other.GetComponent<trainMove>();
//            other.enabled = false;
//            if (mover.isActive)
//            {
//                stats.SafeFromTrainState(false);
//                //   Debug.LogError("train hit");
//            }
//            else
//            {
//                stats.SafeFromTrainState(true);
//                // Debug.LogError("train not hit");
//            }

//        }



//        // Inside your OnTriggerEnter
//        if (other.CompareTag("endGame"))
//        {
//            other.enabled = false;

//            float currentTime = Time.time;
//            int minutes = Mathf.FloorToInt(currentTime / 60);
//            int seconds = Mathf.FloorToInt(currentTime % 60);
//            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

//            // 2. GET THE SPEED (Improved search)
//            // Get the Drivetrain script directly
//            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
//            float actualSpeed = 0f;

//            if (drivetrain != null)
//            {
//                actualSpeed = drivetrain.speed; // This is the raw RigidBody speed * 3.6f
//            }

//            // Pass the actual physics speed, not the dashboard speed
//            stats.checkPointEnd(true,timeStringT, actualSpeed);

//            Debug.Log("Game Finished! Restarting in 5 seconds...");
//            StartCoroutine(RestartTimer());
//        }

//        // Add this Coroutine method at the bottom of your class
//        IEnumerator RestartTimer()
//        {
//            yield return new WaitForSeconds(5f);

//            // This reloads the currently active scene
//            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//        }

//    }



//}


using System.Collections; // Required for Coroutines
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriggerHandler : MonoBehaviour
{
    private PlayerStat stats;

    [Header("Cooldown Settings")]
    public float cooldownTime = 0.5f;
    private float nextActionTime = 0f;

    // Drag the separate Traffic Light object here in the Inspector
    [Header("References")]
    public TrafficLightController trafficLight;


    [Header("Respawn System")]
    public Transform currentCheckpoint;
    private Rigidbody rb; // To stop the car's physics

    void Start()
    {
        stats = GetComponent<PlayerStat>();
        rb = GetComponentInParent<Rigidbody>();
 
    }

    public void SetCurrentCheckpoint(Transform newPoint)
    {
        currentCheckpoint = newPoint;
    }

    public void RespawnPlayer()
    {
        if (currentCheckpoint != null && stats != null && stats.isSpawnNeeded)
        {
            // Stop physics
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Move THIS object (since script is on parent)
            transform.position = currentCheckpoint.position;
            transform.rotation = currentCheckpoint.rotation;

            Debug.Log("Player respawned due to rule violation.");
            stats.SpawnStateSet(false);
        }
        else
        {
            Debug.Log("Checkpoint or stats missing.");
        }
    }


     
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (Time.time >= nextActionTime)
            {
                // 1. Calculate Minutes and Seconds (Session-based)
                float currentTime = Time.timeSinceLevelLoad;
                int minutes = Mathf.FloorToInt(currentTime / 60);
                int seconds = Mathf.FloorToInt(currentTime % 60);

                // 2. Format into a string (00:00 style)
                string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

                // 3. Logic: Add coin and Set cooldown
                stats.AddCoin(1);
                nextActionTime = Time.time + cooldownTime;

                // 4. Display the formatted time
                Debug.Log("Item Collected! Session Time: " + timeString);

                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("getTimeTraf"))
        {
            other.enabled = false;
            float currentTime = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

            // 2. GET THE SPEED
            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
            float actualSpeed = 0f;

            if (drivetrain != null)
            {
                actualSpeed = drivetrain.speed;
            }

            stats.SaveTrafficData(timeStringT, actualSpeed);

            if (trafficLight != null)
            {
                trafficLight.StartSequence();
            }
            else
            {
                Debug.LogError("Assign the TrafficLight in the Inspector!");
            }
        }

        // 4. Check for Round Triggers
        if (other.CompareTag("roundOne"))
        {
            stats.CheckPathPatern(1);
            other.enabled = false;
        }
        else if (other.CompareTag("roundTwo"))
        {
            stats.CheckPathPatern(2);
            other.enabled = false;
        }
        else if (other.CompareTag("roundThree"))
        {
            stats.CheckPathPatern(3);
            other.enabled = false;
        }
        else if (other.CompareTag("roundFour"))
        {
            stats.CheckPathPatern(4);
            other.enabled = false;
        }

        if (other.CompareTag("humanTrigger"))
        {
            other.enabled = false;
            float currentTime = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
            float actualSpeed = 0f;

            if (drivetrain != null)
            {
                actualSpeed = drivetrain.speed;
            }

            stats.SaveChildCrossRoadData(timeStringT, actualSpeed);

            ZebraCrossingMover mover = other.GetComponentInChildren<ZebraCrossingMover>();

            float chance = Random.value;

            if (chance <= 0.7f) // 70% chance
            {
                mover.isCrossing = true;
                Debug.Log("70% Chance hit: Pedestrian is CROSSING.");
            }
            else // 30% chance
            {
                mover.isCrossing = false;
                Debug.Log("30% Chance hit: Pedestrian is WAITING.");
            }
        }

        if (other.CompareTag("child"))
        {
            ZebraCrossingMover mover = other.GetComponent<ZebraCrossingMover>();
            other.enabled = false;
            if (mover.isCrossing)
            {
                stats.SaveChildCrossState(false);
            }
            else
            {
                stats.SaveChildCrossState(true);
            }
        }

        if (other.CompareTag("checkEnter"))
        {
            other.enabled = false;
            stats.checkPoint(true);
        }



        // train scene
        if (other.CompareTag("trainTrigger"))
        {
            other.enabled = false;
            float currentTime = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
            float actualSpeed = 0f;

            if (drivetrain != null)
            {
                actualSpeed = drivetrain.speed;
            }

            stats.SaveTrainPassData(timeStringT, actualSpeed);

            trainMove mover = other.GetComponentInChildren<trainMove>();

            float chance = Random.value;

            if (chance <= 0.8f) // 80% chance
            {
                mover.isActive = true;
                Debug.Log("80% chance to train active.");
            }
            else // 20% chance
            {
                mover.isActive = false;
                Debug.Log("20% chance to train not active..");
            }
        }

        if (other.CompareTag("train"))
        {
            trainMove mover = other.GetComponent<trainMove>();
            other.enabled = false;
            if (mover.isActive)
            {
                stats.SafeFromTrainState(false);
            }
            else
            {
                stats.SafeFromTrainState(true);
            }
        }

        if (other.CompareTag("endGame"))
        {
            other.enabled = false;

            float currentTime = Time.timeSinceLevelLoad;
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            string timeStringT = string.Format("{0:00}:{1:00}", minutes, seconds);

            SCC_Drivetrain drivetrain = GetComponentInParent<SCC_Drivetrain>();
            float actualSpeed = 0f;

            if (drivetrain != null)
            {
                actualSpeed = drivetrain.speed;
            }

            stats.checkPointEnd(true, timeStringT, actualSpeed);

            Debug.Log("Game Finished! Session Time: " + timeStringT + " | Restarting in 5 seconds...");
            StartCoroutine(RestartTimer());
        }


        
    }

    IEnumerator RestartTimer()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}