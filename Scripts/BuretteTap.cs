using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuretteTap : MonoBehaviour {
    [SerializeField] private Outline outline;
    [SerializeField] private GameObject funnelSnap;
    [SerializeField] private BuretteContainer container;
    [SerializeField] private string colliderTag;
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider collider) {
        if (collider.transform.CompareTag(colliderTag)) {
            container.StartPour(transform.name);
            outline.DisableOutline();
        }
    }
    private void OnTriggerExit(Collider collider) {
        if (collider.transform.CompareTag(colliderTag)) {
            container.EndPour(transform.name);
            outline.EnableOutline();
        }
    }

    public void Selected(SelectEnterEventArgs args) {
        if ( args.interactorObject.transform.CompareTag(colliderTag) ) {
            rb.detectCollisions = false;
            funnelSnap.SetActive(false);
        }
    }

    public void Deselected(SelectExitEventArgs args) {
        if ( args.interactorObject.transform.CompareTag(colliderTag) ) {
            rb.detectCollisions = true;
            funnelSnap.SetActive(true);
        }
    }
}
