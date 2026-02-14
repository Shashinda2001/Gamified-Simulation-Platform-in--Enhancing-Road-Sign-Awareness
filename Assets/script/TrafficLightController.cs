using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{

     

    public GameObject redSphere;
    public GameObject yellowSphere;
    public GameObject greenSphere;

    // This method will be called by the Player script
    // Private variables to hold the Light components
    private Light rLight;
    private Light yLight;
    private Light gLight;

    void Awake()
    {
        // Automatically grab the Light component from each sphere at the start
        if (redSphere) rLight = redSphere.GetComponent<Light>();
        if (yellowSphere) yLight = yellowSphere.GetComponent<Light>();
        if (greenSphere) gLight = greenSphere.GetComponent<Light>();

        // Ensure all lights start OFF
        SetLightStatus(false, false, true);
    }

    
    public void StartSequence()
    {
        StartCoroutine(LightRoutine());
    }

    IEnumerator LightRoutine()
    {
        // 1. Yellow ON (1 sec)
        SetLightStatus(false, true, false);
        yield return new WaitForSeconds(1f);

        // 2. Red ON (3 sec)
        SetLightStatus(true, false, false);
        yield return new WaitForSeconds(3f);

        // 3. Green ON
        SetLightStatus(false, false, true);
    }

    void SetLightStatus(bool r, bool y, bool g)
    {
        if (rLight) rLight.enabled = r;
        if (yLight) yLight.enabled = y;
        if (gLight) gLight.enabled = g;
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        // 1. Disable collider immediately so it only runs once
    //        GetComponent<BoxCollider>().enabled = false;

    //        // 2. Check the state and log the result
    //        if (gLight.enabled)
    //        {
    //            Debug.Log("Player Enter safe: Success");
    //            // stats.RecordSuccess(); // Optional: send to PlayerStat
    //        }
    //        else
    //        {
    //            Debug.Log("Player Enter unsafe: Violation");
    //            // stats.RecordViolation(); // Optional: send to PlayerStat
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        // LOG 1: Did any object touch the trigger?
        Debug.Log("Trigger touched by: " + other.name);

        if (other.CompareTag("PlayerCar"))
        {
            // LOG 2: Did the tag match work?
            Debug.Log("Tag check passed for Player!");

            GetComponent<BoxCollider>().enabled = false;
            //get player stats component  object
            // We use GetComponentInParent because the collider might be a child of the main Player object
            PlayerStat stats = other.GetComponentInParent<PlayerStat>();

            if (gLight != null && gLight.enabled)
            {
                Debug.Log("Safe: Green Light was ON");
                stats.SaveTrafficState(true);
            }
            else
            {
                Debug.Log("Unsafe: Green Light was OFF");
                stats.SaveTrafficState(false);
            }
        }
        else
        {
            // LOG 3: Why was the object ignored?
            Debug.Log("Object ignored. Tag was: " + other.tag);
        }
    }
}