//using Unity.VisualScripting;
//using UnityEngine;

//public class Checkpoint : MonoBehaviour
//{
//    // The position the car will move to if it fails the next hazard
//    [Header("Player position")]
//    public GameObject respawnPoint;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("PlayerCar"))  
//        {
//            GameObject player = GameObject.FindGameObjectWithTag("PlayerCar");

//            if (player != null)
//            {
//                PlayerTriggerHandler script = player.GetComponent<PlayerTriggerHandler>();
//                script.SetCurrentCheckpoint(respawnPoint.transform);
//                script.RespawnPlayer();
//            }
//            GetComponent<BoxCollider>().enabled = false;
//            //PlayerTriggerHandler handler = other.GetComponent<PlayerTriggerHandler>();
//            //if (handler != null)
//            //{
//            //    handler.SetCurrentCheckpoint(respawnPoint.transform);
//            //    Debug.Log("Checkpoint Updated: " + gameObject.name);
//            //    // handler.RespawnPlayer();
//            //    GetComponent<BoxCollider>().enabled = false;
//            //}


//        }
//        else
//        {
//            Debug.Log("colide by: " + other.name);
//        }
//    }
//}

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Player position")]
    public GameObject respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerCar"))
        {
            // 1. Find the object with tag "spw"
            //GameObject spawnObject = GameObject.FindWithTag("spw");
            //Debug.Log(" hapu naaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa ");
            if (respawnPoint != null)
            {
                // 2. Access the player script
                PlayerTriggerHandler script = other.GetComponentInParent<PlayerTriggerHandler>();

                if (script != null)
                {
                    // 3. Assign the transform of the found "spw" object
                    script.SetCurrentCheckpoint(respawnPoint.transform);

                    Debug.Log("Checkpoint Saved using tag 'spw': " + respawnPoint.name);
                    script.RespawnPlayer();
                    GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    Debug.LogError("PlayerTriggerHandler not found on parent of: " + other.name);
                }
            }
            else
            {
                Debug.LogError("CRITICAL: No object found with tag 'spw' in the scene!");
            }
        }
    }
}