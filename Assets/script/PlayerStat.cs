using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerStat : MonoBehaviour
{
    [Header("Player Statistics")]
    public int coinCount = 0; // This stores your total coins
    public int currentMark = 0; // This can be used to track the player's current score or mark

    [Header("Traffic History")]
    public System.Collections.Generic.List<string> timeHistory = new System.Collections.Generic.List<string>();
    public System.Collections.Generic.List<float> speedHistory = new System.Collections.Generic.List<float>();

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

    [Header("events")]
    public bool checkEnterNoEntry1 = false;
    public bool checkEnterNoEntry2 = false;
    public bool checkEnterNoEntry3 = false;

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
    
        public void checkClossPoint(bool cross, string time,string typeOfEven)
    {
        enteredArea = cross;
        savedTime = time;
        if (cross) isSpawnNeeded = true;

        if(typeOfEven== "checkEnterNoEntry1")
        {
            checkEnterNoEntry1=true;
            checkEnterNoEntryNegative("Prohibited_Entry_Check", false);
        }
        else if(typeOfEven == "checkEnterNoEntry2")
        {
            checkEnterNoEntry2=true;
            checkEnterNoEntryNegative("Prohibited_Entry_Check", false);
        }
        else if(typeOfEven == "checkEnterNoEntry3")
        {
            checkEnterNoEntry3 = true;
            checkEnterNoEntryNegative("Construction_Zone_Entry", false);
        }

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
            if (!checkEnterNoEntry1)
            {
                currentMark = currentMark + 10;
                checkEnterNoEntryPossitive("Prohibited_Entry_Check", true);
            }
            if (!checkEnterNoEntry2)
            {
                currentMark = currentMark + 10;
                checkEnterNoEntryPossitive("Prohibited_Entry_Check", true);
            }
            if (!checkEnterNoEntry3)
            {
                currentMark = currentMark + 10;
                checkEnterNoEntryPossitive("Construction_Zone_Entry", true);
            }

            currentMark = currentMark + 10;
            RecordEvent("Session_Complete_with_Stop_Sign", true, 10);

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

  

    public void SaveTrainPassData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("Train Pass! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    }

    public void SafeFromTrainState(bool state)
    {
        isplayerSafeFromTrain = state;
        if (!state)
        { isSpawnNeeded = true;
            trainRec("Danger_Railway_Crossing", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, false, 0);
        }
        else {
            currentMark = currentMark + 10;
            trainRec("Danger_Railway_Crossing", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, true, 10);

        }
        Debug.Log("safe from train : " + isplayerSafeFromTrain);
    }
    public void SaveTrafficData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;

        // 2. Add to the history (the "Array")
        timeHistory.Add(time);
        speedHistory.Add(speed);

        Debug.Log(" Check! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    
    }

    public void roadDangerArea(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;

        // 2. Add to the history (the "Array")
        timeHistory.Add(time);
        speedHistory.Add(speed);

        if((speed- speedHistory[speedHistory.Count - 2]) < 0.0) { 
            currentMark = currentMark + 10;
            //store data in json
            roadTraps("Sharp_Downward_Slope", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, true, 10);

        }
        else
        {
            roadTraps("Sharp_Downward_Slope", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, false, 0);
            isSpawnNeeded = true;
        }

        Debug.Log(" Check! Sharp_Downward_Slope: " + time + " | Speed: " + speed.ToString("F1") + " KMH");

    }

    public void roadBumpArea(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;

        // 2. Add to the history (the "Array")
        timeHistory.Add(time);
        speedHistory.Add(speed);

        if ((speed - speedHistory[speedHistory.Count - 2]) < 0.0)
        {
             currentMark = currentMark + 10;
            //store data in json file
            roadTraps("Speed_Bump_Compliance", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, true, 10);

        }
        else
        {
            roadTraps("Speed_Bump_Compliance", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, false, 0);

            isSpawnNeeded = true;
        }

        Debug.Log(" Check! Speed_Bump_Compliance: " + time + " | Speed: " + speed.ToString("F1") + " KMH");

    }

    public void SaveTrafficState(bool state)
    {
        trafficSafety = state;


        Debug.Log("traffic safe : " + trafficSafety);

        if (!state)
        {
            isSpawnNeeded = true;
            TafficLight("Traffic_Compliance", savedTime, speedAtTrigger, state, 0);
        }
        else
        {
            currentMark = currentMark + 10;
            TafficLight("Traffic_Compliance", savedTime, speedAtTrigger, state, 10);
        }
            
    }

    public void SaveChildCrossRoadData(string time, float speed)
    {
        savedTime = time;
        speedAtTrigger = speed;
        Debug.Log("child cross! Time: " + time + " | Speed: " + speed.ToString("F1") + " KMH");
    }

    public void SaveChildCrossState(bool state)
    {
        chhildCrossSafe = state;

        if (state) { 
            currentMark = currentMark + 10;
            childCross("Pedestrian_Crossing_Child", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, state, 10);

        }
        else {
            childCross("Pedestrian_Crossing_Child", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, state, 0);
            if (!state) isSpawnNeeded = true;

        }


    }

    

         public void SavepeopleCrossState(bool state)
    {
        chhildCrossSafe = state;

        if (state)
        {
            currentMark = currentMark + 10;
            childCross("Pedestrian_Crossing_Adult", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, state, 10);

        }
        else
        {
            childCross("Pedestrian_Crossing_Adult", timeHistory[timeHistory.Count - 2], speedHistory[speedHistory.Count - 2], savedTime, speedAtTrigger, state, 0);
            if (!state) isSpawnNeeded = true;

        }


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
            RecordEvent("Roundabout_Entry ", true,10);
        }
        // WRONG: Anything else (Skipping like 1->4 or Reversing like 4->3)
        else if (currentPoint != lastRoundPoint)
        {
            Debug.LogWarning("WRONG PATTERN! Expected " + (lastRoundPoint + 1) + " but hit " + currentPoint);
            isWrongDirection=true;
            SpawnStateSet(true);
             
            RecordEvent("Roundabout_Entry ", false,0);
            // Option: Reset lastRoundPoint to 0 so they have to start a new sequence
            lastRoundPoint = 0;
        }
        
        
        
    }



    // A helper method to "Log" an event without saving to disk yet
    private void RecordEvent(string label, bool safetyStatus,int mark)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = savedTime;
        newEvent.EntrySpeed = speedAtTrigger;
        newEvent.IsSafe = safetyStatus;
        //newEvent.coins = coinCount;
        newEvent.ScoreAwarded = mark;
        newEvent.CurrentMark = currentMark;

        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }

    //===========================================help to save data in json file===========================================
    
    // This is a more specific logging method for traffic light events
    private void TafficLight(string label,string EntryT, float speed, bool safetyStatus,int mark)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = EntryT;
        newEvent.EntrySpeed = speed;
        newEvent.IsSafe = safetyStatus;
        newEvent.ScoreAwarded = mark;
        newEvent.CurrentMark = currentMark;
        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }

    // This is a more specific logging method for child crossing events
    private void childCross(string label, string EntryT, float speed, string EntryO, float speedO, bool safetyStatus, int mark)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = EntryT;
        newEvent.EntrySpeed = speed;

        newEvent.OutcomeTime= EntryO;
        newEvent.OutcomeSpeed = speedO;
        
        newEvent.IsSafe = safetyStatus;
        newEvent.ScoreAwarded = mark;
        newEvent.CurrentMark = currentMark;
        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }

    // This is a more specific logging method for no entry events
    private void checkEnterNoEntryPossitive(string label, bool safetyStatus)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
       
        newEvent.IsSafe = safetyStatus;
        newEvent.CurrentMark = currentMark;
        newEvent.ScoreAwarded = 10;
        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }
    // This is a more specific logging method for no entry events
    private void checkEnterNoEntryNegative(string label, bool safetyStatus)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.OutcomeTime= savedTime;
        newEvent.IsSafe = safetyStatus;
        newEvent.CurrentMark = currentMark;
        newEvent.ScoreAwarded = 0;
        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }

    // This is a more specific logging method for road traps events
    private void roadTraps(string label, string EntryT, float speed, string EntryO, float speedO, bool safetyStatus, int mark)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = EntryT;
        newEvent.EntrySpeed = speed;

        newEvent.OutcomeTime = EntryO;
        newEvent.OutcomeSpeed = speedO;

        newEvent.IsSafe = safetyStatus;
        newEvent.ScoreAwarded = mark;
        newEvent.CurrentMark = currentMark;
        sessionLog.allEvents.Add(newEvent);
        Debug.Log($"Logged: {label}");
    }

    // This is a more specific logging method for train events
    private void trainRec(string label, string EntryT, float speed, string EntryO, float speedO, bool safetyStatus, int mark)
    {
        SimulationEvent newEvent = new SimulationEvent();
        newEvent.EventType = label;
        newEvent.EntryTime = EntryT;
        newEvent.EntrySpeed = speed;

        newEvent.OutcomeTime = EntryO;
        newEvent.OutcomeSpeed = speedO;

        newEvent.IsSafe = safetyStatus;
        newEvent.ScoreAwarded = mark;
        newEvent.CurrentMark = currentMark;
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



