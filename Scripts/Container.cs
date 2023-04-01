using UnityEngine;

public class Container : MonoBehaviour {
    [SerializeField] protected Transform origin;
    [SerializeField] protected GameObject streamPrefab;
    protected LiquidStream currentStream;
    public void StartPour(string name) {
        AudioManager.instance.PlaySfx("Liquid Pour");

        string trigger = name.Replace(" ", "_").ToUpper() + "_POURING";
        SessionManager.instance.TriggerOn(trigger);

        currentStream = CreateStream();
        currentStream.Begin();
    }
    public void EndPour(string name) {
        AudioManager.instance.StopSfx("Liquid Pour");

        string trigger = name.Replace(" ", "_").ToUpper() + "_POURING";
        SessionManager.instance.TriggerOff(trigger);

        if(currentStream != null) {
            currentStream.End();
            currentStream = null;
        }
    }
    protected float CalculatePourAngle() {
        return transform.forward.y * Mathf.Rad2Deg;
    }
    protected LiquidStream CreateStream() {
        GameObject streamObject = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<LiquidStream>();
    }
}