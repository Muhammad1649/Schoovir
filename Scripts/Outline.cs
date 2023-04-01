using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Outline : MonoBehaviour
{
    [SerializeField] private string interactorTag;
    private new MeshRenderer renderer;
    private bool enableOutline;
    private bool isSelected;

    private void Start() {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
        enableOutline = true;
        isSelected = false;
        Blink(false);
    }

    public void ShowOutline(HoverEnterEventArgs args) {
        if (!isSelected && enableOutline && args.interactorObject.transform.CompareTag(interactorTag)) {
            renderer.enabled = true;
        }
    }

    public void HideOutline(SelectEnterEventArgs args) {
        if (enableOutline && args.interactorObject.transform.CompareTag(interactorTag)) {
            renderer.enabled = false;
            isSelected = true;
        }
    }

    public void HideOutline() {
        renderer.enabled = false;
    }

    public void UnSelect() {
        isSelected = false;
    }

    public void Blink(bool blink = true) {
        if (blink && renderer.materials.Length > 1) {
            foreach (Material mat in renderer.materials) {
                if (blink) { mat.SetInt("_blink", 1); }
                else { mat.SetInt("_blink", 0); }
            }
        }
        if (blink) { renderer.material.SetInt("_blink", 1); }
        else { renderer.material.SetInt("_blink", 0); }
    }

    public void EnableOutline() { enableOutline = true; }

    public void DisableOutline() { enableOutline = false; }
}
