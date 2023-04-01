using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrashCan : XRSocketInteractor {
    private Transform objectAttachTransform;
    [Header("Trash Can")]
    [SerializeField] private Transform ui;
    [SerializeField] private Transform anchor;
    [SerializeField] private Transform trashAttachTransform;
    [SerializeField] private float eliminateTrashDuration;
    [SerializeField] private Transform trash;
    [SerializeField] private int trashMemory;
    [SerializeField] private List<string> disposableLayer;
    [SerializeField] private List<GameObject> trashItems = new List<GameObject>();

    private List<GameObject> temp = new List<GameObject>();

    private new void Start() {
        ui.gameObject.SetActive(false);
        trash.gameObject.SetActive(false);
    }
    private void Update() {

        ui.position = anchor.position;
        ui.rotation = anchor.rotation;

        showInteractableHoverMeshes = temp.Count == 1;

        if (trashItems.Count > trashMemory) {
            Destroy(trashItems[0]);
            trashItems.RemoveAt(0);
        }
    }
    private void Eliminate(GameObject t) {
        if (temp.Contains(t)) {
            trashItems.Add(t);
            t.transform.SetParent(trash);
            temp.Remove(t);
        }
    }
    IEnumerator EliminateTrash(GameObject t) {
        yield return new WaitForSeconds(eliminateTrashDuration);
        Eliminate(t);
        StopCoroutine(EliminateTrash(null));
    }

    public void Recycle(HoverExitEventArgs args) {
        GameObject t = args.interactableObject.transform.gameObject;
        temp.Remove(t);
    }
    public override bool CanHover(IXRHoverInteractable interactable) {
        GameObject t = interactable.transform.gameObject;
        if (disposableLayer.Contains(LayerMask.LayerToName(t.layer))) {
        
            objectAttachTransform = interactable.GetAttachTransform(this);
            
            trashAttachTransform.position = objectAttachTransform.position;
            trashAttachTransform.rotation = objectAttachTransform.rotation;

            if (!trashItems.Contains(t) && !temp.Contains(t)) {
                temp.Add(t);
                StartCoroutine(EliminateTrash(t));
            }
        }
        return base.CanHover(interactable);
    }
    public override bool CanSelect(IXRSelectInteractable interactable) {
        return false;
    }

    public void Move() { ui.gameObject.SetActive(true); }

    public void Drop() { ui.gameObject.SetActive(false); }
}
