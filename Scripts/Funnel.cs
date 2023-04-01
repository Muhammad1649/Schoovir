using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Funnel : MonoBehaviour, IContainable {
    private IContainable container;

    public void SetContainer(SelectEnterEventArgs args) {
        container = args.interactorObject.transform.gameObject.GetComponentInParent<IContainable>();
    }
    public void ClearContainer() {
        container = null;
    }
    public void AddChemicals(List<Chemical> chemicalToAdd) {
        if (container != null) {
            container.AddChemicals(chemicalToAdd);
            container.FillContainer(1f);
        }
    }
    public (string _name, string _formula, List<Chemical> _chemicals, Color _liquidColor, float _liquidFill, float _percentage, float _volume, float _weight, bool _isEmpty, float _temperature, Transform _containerTop, Transform _containerBottom, Transform _liquidSurface) GetInfo() {
        return (this.name, string.Empty, new List<Chemical>(), Color.white, 0.00204f*16.38297872340426f, 0f, 0f, 0f, false, 0f, null, null, null);
    }

    // Interface Implementation
    public void AddChemical(Chemical chemicalToAdd) {}
    public void FillContainer(float fillRate) {}
    public void EmptyContainer(float emptyRate) {}
    public virtual void SetTemperature(float temp) {}
    public virtual void SetPreasure(float preasure) {}
}
