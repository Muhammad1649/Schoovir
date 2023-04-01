using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class WeightBalance : XRSocketInteractor {
    [Header(" ")]
    [SerializeField] private float weightCalculationIteration = 5.0f;
    [SerializeField] private float weightCalculationSpeed = 0.2f;
    [Header(" ")]
    [SerializeField] private Transform scaleAttachTransform;
    [SerializeField] private Transform screenAnchor;
    [SerializeField] private RectTransform ui;
    [SerializeField] private TMP_Text mg;
    [SerializeField] private TMP_Text g;
    [SerializeField] private TMP_Text kg;

    private Transform objectAttachTransform;
    private List<IContainable> items = new List<IContainable>();

    private float displayWeight;
    private float currentWeight;
    private int objectCount;
    private void Update() {
        ui.position = screenAnchor.position;
        ui.rotation = screenAnchor.rotation;

        showInteractableHoverMeshes = items.Count <= 1;
    }
    private void DisplayWeight() {
        
        if (!Mathf.Approximately(currentWeight, displayWeight)) {
            if(currentWeight == 0) {
                displayWeight -= displayWeight / (weightCalculationIteration / 4);

                mg.text = Mathf.Round((displayWeight * 1000)).ToString() + " MG";
                g.text = (Mathf.Round(displayWeight * 10.0f) * 0.1f).ToString() + " G";
                kg.text = (Mathf.Round((displayWeight / 1000) * 1000.0f) * 0.001f).ToString() + " KG";

                DisplayWeight(true);
                return;
            }
            if(displayWeight > currentWeight) {
                displayWeight -= currentWeight / weightCalculationIteration;

                mg.text = Mathf.Round((displayWeight * 1000)).ToString() + " MG";
                g.text = (Mathf.Round(displayWeight * 10.0f) * 0.1f).ToString() + " G";
                kg.text = (Mathf.Round((displayWeight / 1000) * 1000.0f) * 0.001f).ToString() + " KG";

                DisplayWeight(true);
                return;
            }
            if (displayWeight < currentWeight) {
                displayWeight += currentWeight / weightCalculationIteration;

                mg.text = Mathf.Round((displayWeight * 1000)).ToString() + " MG";
                g.text = (Mathf.Round(displayWeight * 10.0f) * 0.1f).ToString() + " G";
                kg.text = (Mathf.Round((displayWeight / 1000) * 1000.0f) * 0.001f).ToString() + " KG";

                DisplayWeight(true);
                return;
            }   
        } else {
            DisplayWeight(false);
        }
    }
    private void DisplayWeight(bool display) {
        if (display) { Invoke("DisplayWeight", weightCalculationSpeed); }  
    }
    private void CalculateWeight() {

        if (items.Count != objectCount) {

            objectCount = items.Count;
            float weight = 0f;
            if (items.Count != 0) {
                foreach (IContainable container in items) {
                    weight += container.GetInfo()._temperature;
                }
            }
            currentWeight = weight;
            DisplayWeight();
        }
        
    }
    public void ResetScale(HoverExitEventArgs args) {
        IContainable c = args.interactableObject.transform.GetComponentInChildren<IContainable>();
        if (c != null) {
            items.Remove(c);
            CalculateWeight();
        }
    }
    public override bool CanHover(IXRHoverInteractable interactable) {
        IContainable c = interactable.transform.GetComponentInChildren<IContainable>();
        if (c != null) {

            if (!items.Contains(c)) { items.Add(c); }
        
            objectAttachTransform = interactable.GetAttachTransform(this);
            
            scaleAttachTransform.position = objectAttachTransform.position;
            scaleAttachTransform.rotation = objectAttachTransform.rotation;

            CalculateWeight();

        }
        return base.CanHover(interactable);
    }
    public override bool CanSelect(IXRSelectInteractable interactable) {
        return false;
    }
}
