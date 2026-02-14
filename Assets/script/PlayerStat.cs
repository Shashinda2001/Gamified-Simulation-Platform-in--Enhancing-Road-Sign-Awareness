using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Statistics")]
    public int coinCount = 0; // This stores your total coins

   // [Header("Player Time")]
    public string savedTime;

    [Header("Traffic Data")]
    public float speedAtTrigger;
    public bool trafficSafety;

    [Header("Roundabout Validation")]
    public int lastRoundPoint = 0;
    public bool isWrongDirection = false;

    // Other scripts will call this function
    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins Collected: " + coinCount);
    }

    //public void UpdateSavedTime(string newTime)
    //{
    //    savedTime = newTime;
    //    Debug.Log("Time saved in PlayerStat: " + savedTime);
    //}

   
    public void SaveTrafficData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("Traffic Check! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    }

    public void SaveTrafficState(bool state)
    {
        trafficSafety = state;
        Debug.Log("traffic safe : " + trafficSafety);
    }




    public void CheckPathPatern(int currentPoint)
    {
        // If it's the very first point they hit in a while, accept it
        if (lastRoundPoint == 0)
        {
            lastRoundPoint = currentPoint;
            Debug.Log("Path Started at: " + currentPoint);
            return;
        }

        // SUCCESS: The current point is exactly +1 from the last one
        if (currentPoint == lastRoundPoint + 1)
        {
            Debug.Log("Correct Pattern: " + lastRoundPoint + " -> " + currentPoint);
            lastRoundPoint = currentPoint;
        }
        // WRONG: Anything else (Skipping like 1->4 or Reversing like 4->3)
        else if (currentPoint != lastRoundPoint)
        {
            Debug.LogWarning("WRONG PATTERN! Expected " + (lastRoundPoint + 1) + " but hit " + currentPoint);
            isWrongDirection=true;
            // Option: Reset lastRoundPoint to 0 so they have to start a new sequence
            lastRoundPoint = 0;
        }
    }


}



