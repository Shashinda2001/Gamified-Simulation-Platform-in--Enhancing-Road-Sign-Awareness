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

    [Header("safe cross child")]
    public bool chhildCrossSafe = true;

    [Header("areas")]
    private bool enteredArea = false;

    [Header("safe from train")]
    public bool isplayerSafeFromTrain = true;

    public bool isSpawnNeeded = false;

    public void SpawnStateSet(bool state)
    {
        isSpawnNeeded = state;
        Debug.Log("spawn needed : " + isSpawnNeeded);
    }


    // Other scripts will call this function
    public void AddCoin(int amount)
    {
        coinCount += amount;
        Debug.Log("Coins Collected: " + coinCount);
    }

    public void checkPoint(bool cross)
    {
        enteredArea=cross;
        if (cross) isSpawnNeeded = true;
        Debug.Log("player enter the area: " + cross);
    }

    public void checkPointEnd(bool cross, string time, float speed)
    {
        enteredArea = cross;
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("player enter the area: " + cross);
    }

    public void SaveChildCrossState(bool state)
    {
        chhildCrossSafe = state;
        if (!state) isSpawnNeeded = true;
        Debug.Log("child safe : " + chhildCrossSafe);
    }

    public void SaveTrainPassData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("Train Pass! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    }

    public void SafeFromTrainState(bool state)
    {
        isplayerSafeFromTrain = state;
        if (!state) isSpawnNeeded = true;
        Debug.Log("safe from train : " + isplayerSafeFromTrain);
    }
    public void SaveTrafficData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("Traffic Check! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    }

    public void SaveChildCrossRoadData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("child cross! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    }

    public void SaveTrafficState(bool state)
    {
        trafficSafety = state;
        if(!state)isSpawnNeeded=true;
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
            SpawnStateSet(true);

            // Option: Reset lastRoundPoint to 0 so they have to start a new sequence
            lastRoundPoint = 0;
        }
    }


}



