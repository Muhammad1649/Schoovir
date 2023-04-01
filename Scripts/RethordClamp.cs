using UnityEngine;

public class RethordClamp : MonoBehaviour {
    private Transform t;
    private Vector3 position;
    private bool isEnabled;
    void Start() {
        t = this.transform;
        // position = t.TransformPoint(new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z));
    }

    void FixedUpdate() {
        if (isEnabled) {
            // t.localPosition = new Vector3(position.x, Mathf.Clamp(position.y, 0.77f, 3.601f), position.z);
            t.localPosition = new Vector3(t.position.x, Mathf.Clamp(t.position.y, 0.77f, 3.601f), t.position.z);
        }
    }
    void Update() {
        if (isEnabled) {
            t.localPosition = new Vector3(t.position.x, Mathf.Clamp(t.position.y, 0.77f, 3.601f), t.position.z);
        }
    }

    public void Enable(bool enable) {
        isEnabled = enable;
    }
}
