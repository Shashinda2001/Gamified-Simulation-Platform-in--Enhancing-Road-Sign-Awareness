using UnityEngine;
using System.Collections;

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
        SetLightStatus(false, false, false);
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
}