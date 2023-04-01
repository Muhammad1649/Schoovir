using UnityEngine;

public class LoadingAnimation : MonoBehaviour {
    [SerializeField] private RectTransform t;
    Vector3 rotation;
    private void Awake() {
        gameObject.SetActive(false);
    }
    private void Start() {
        rotation = new Vector3(0, 0, 500.0f);
    }

    private void Update() {
        t.Rotate(rotation * Time.deltaTime, Space.World);
    }
}
