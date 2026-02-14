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


}



