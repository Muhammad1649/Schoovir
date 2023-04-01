using UnityEngine;

public class Effect : MonoBehaviour
{
    public float duration = 1f;
    private float timer;
    private void Start() {
        timer = duration;
    }
    private void Update() {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        if(timer <= 0) {
            Destroy(this.gameObject);
        }
        timer -= Time.deltaTime;
    }
}
