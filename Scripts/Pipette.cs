using System.Collections.Generic;
using UnityEngine;

public class Pipette : MonoBehaviour {
    private List<Chemical> chemicals;
    private IContainable collectingContainer;
    private IContainable pipetteContainer;
    private Container pipette;

    private bool trigger;
    private void Start() {
        pipetteContainer = GetComponentInParent<IContainable>();
        pipette = GetComponentInParent<Container>();
    }
    private void OnTriggerEnter(Collider collider) {
        if ( collider.transform.gameObject.layer == LayerMask.NameToLayer("Liquid") && trigger) {

            collectingContainer = collider.transform.GetComponentInParent<IContainable>();

            if (collectingContainer == null) { return; }

            if (!collectingContainer.GetInfo()._isEmpty) {
                chemicals = collectingContainer.GetInfo()._chemicals;
                pipetteContainer.AddChemicals(chemicals);
            }
        }
    }

    private void OnTriggerStay(Collider collider) {
        if (collider.transform.gameObject.layer == LayerMask.NameToLayer("Liquid") && trigger) {

            if (collectingContainer == null) { return; }

            if (!collectingContainer.GetInfo()._isEmpty) {

                pipetteContainer.FillContainer(1f);
                collectingContainer.EmptyContainer(1f);
            }
        }
    }
    private void OnTriggerExit(Collider collider) {
        if ( collider.transform.gameObject.layer == LayerMask.NameToLayer("Liquid") && trigger) {

            collectingContainer = null;
        }
    }

    public void PipetteLiquid() {
        trigger = true;
        pipette.EndPour(transform.name);
    }

    public void PourLiquid() {
        trigger = false;
        pipette.StartPour(transform.name);
    }
}