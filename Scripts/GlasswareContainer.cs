using UnityEngine;

public class GlasswareContainer : Container {
    [SerializeField] private int pourThreshold = 45;
    private bool isPouring = false;
    void Update() {
        // StartPour();
        bool pourCheck = CalculatePourAngle() < pourThreshold;
        if (isPouring != pourCheck) {
            isPouring = pourCheck;
            if(isPouring) {
                StartPour(transform.name);
            } else {
                EndPour(transform.name);
            }
        }
    }
}