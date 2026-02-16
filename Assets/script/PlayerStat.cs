using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Statistics")]
    public int coinCount = 0; // This stores your total coins
    public int currentMark = 0; // This can be used to track the player's current score or mark

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
    public bool enteredArea = false;

    [Header("safe from train")]
    public bool isplayerSafeFromTrain = true;

    public bool isSpawnNeeded = false;

    // New: This list holds every recordable moment
    private SaveDataWrapper sessionLog = new SaveDataWrapper();

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


        if (cross)
        {
            RecordEvent("Checkpoint Reached", true);

            // 1. Define your specific path
            string customPath = @"D:\research\test1\dataC";
            string fileName = "ResearchData.json";
            string fullPath = System.IO.Path.Combine(customPath, fileName);

            try
            {
                // 2. Ensure the folder exists before saving
                if (!System.IO.Directory.Exists(customPath))
                {
                    System.IO.Directory.CreateDirectory(customPath);
                }

                // 3. Convert the LIST wrapper to JSON
                string json = JsonUtility.ToJson(sessionLog, true);

                // 4. Write the file
                System.IO.File.WriteAllText(fullPath, json);

                Debug.Log("Successfully saved to: " + fullPath);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save to custom path: " + e.Message);
            }
        }
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
        Debug.Log(" Check! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    
    }

    public void SaveTrafficState(bool state)
    {
        trafficSafety = state;
        if (!state) { isSpawnNeeded = true; } else { currentMark = currentMark + 5; }

        Debug.Log("traffic safe : " + trafficSafety);

        currentMark = currentMark + 10;


        TafficLight("TRAFFIC", savedTime, speedAtTrigger, state, currentMark);
    }

    public void SaveChildCrossRoadData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("child cross! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
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

            currentMark = currentMark + 10;
            RecordEvent("round the road ", true);
        }
        // WRONG: Anything else (Skipping like 1->4 or Reversing like 4->3)
        else if (currentPoint != lastRoundPoint)
        {
            Debug.LogWarning("WRONG PATTERN! Expected " + (lastRoundPoint + 1) + " but hit " + currentPoint);
            isWrongDirection=true;
            SpawnStateSet(true);
             
            RecordEvent("round the road ", false);
            // Option: Reset lastRoundPoint to 0 so they have to start a new sequence
            lastRoundPoint = 0;
        }
        
        
        
    }



    // A helper method to "Log" an event without saving to disk yet
    private void RecordEvent(string label, bool safetyStatus)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = savedTime;
        newEvent.EntrySpeed = speedAtTrigger;
        newEvent.IsSafe = safetyStatus;
        //newEvent.coins = coinCount;
        newEvent.ScoreAwarded = currentMark;

        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }

    //===========================================help to save data in json file===========================================
    
    // This is a more specific logging method for traffic light events
    private void TafficLight(string label,string EntryT, float speed, bool safetyStatus,int currentMark)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = EntryT;
        newEvent.EntrySpeed = speed;
        newEvent.IsSafe = safetyStatus;
        newEvent.ScoreAwarded = currentMark;

        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }


    //===========================================help to save data in json file===========================================


}


[System.Serializable]
public class SimulationEvent
{
    public string EventType;        // e.g., "TrafficCheck", "TrainCrossing"

    [Header("Entry State")]
    public string EntryTime;        // Time when the player entered the trigger area
    public float EntrySpeed;        // Speed when the player entered the trigger area

    [Header("Event Outcome")]
    public string OutcomeTime;      // The exact time the success/fail happened
    public float OutcomeSpeed;      // The speed at the moment of success/fail

    [Header("Results")]
    public bool IsSafe;             // Did the player pass the safety check?
    public int TotalCoins;          // Total coins collected so far
    public int CurrentMark;         // The specific mark/point index in the path
    public int ScoreAwarded;        // Points gained or lost from this specific event
}

[System.Serializable]
public class SaveDataWrapper
{
    public System.Collections.Generic.List<SimulationEvent> allEvents = new System.Collections.Generic.List<SimulationEvent>();
}



