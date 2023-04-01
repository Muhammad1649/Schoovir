using UnityEngine;

public class Table : MonoBehaviour {
    [SerializeField] private Transform heightAjuster;
    [SerializeField] private Transform tableHeight;

    [SerializeField] private new SkinnedMeshRenderer renderer;
    [SerializeField] private Color tableTopColor;
    void Start() {
        heightAjuster.gameObject.SetActive(false);
    }
    private void Update() {
        renderer.materials[renderer.materials.Length - 1].color = tableTopColor;

        tableHeight.transform.position = heightAjuster.transform.position;
    }

    public void ShowAjuster(bool show) {
        heightAjuster.gameObject.SetActive(show);
    }
}
