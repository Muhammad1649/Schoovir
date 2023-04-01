using System.Collections;
using UnityEngine;

public class PourDetector : MonoBehaviour
{
    public int pourThreshold = 45;
    public Transform origin;
    public GameObject streamPrefab;
    private bool isPouring = false;
    private LiquidStream currentStream;
    void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if (isPouring != pourCheck)
        {
            isPouring = pourCheck;
            if(isPouring)
            {
                StartPour();
            }else {
                EndPour();
            }
        }
    }
    public void StartPour()
    {
        currentStream = CreateStream();
        currentStream.Begin();
    }
    public void EndPour()
    {
        currentStream.End();
        currentStream = null;
    }
    float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }
    LiquidStream CreateStream()
    {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<LiquidStream>();
    }
}