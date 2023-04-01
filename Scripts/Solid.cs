using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour, IContainable {
    [SerializeField] private Chemical chemical;

    private void OnTriggerEnter(Collider other) {
        ContainerManager c = other.GetComponentInChildren<ContainerManager>();
        c.AddChemical(chemical);
    }

    public void SetTemperature(float temp) {}
    public void SetPreasure(float preasure) {}

    public void AddChemicals(List<Chemical> chemicalToAdd) {}
    public void AddChemical(Chemical chemicalToAdd) {}

    public void FillContainer(float fillRate) {}
    public void EmptyContainer(float emptyRate) {}

    public (string _name, string _formula, List<Chemical> _chemicals, Color _liquidColor, float _liquidFill, float _percentage, float _volume, float _weight, bool _isEmpty, float _temperature, Transform _containerTop, Transform _containerBottom, Transform _liquidSurface) GetInfo() {
        return (chemical.name, chemical.formula, new List<Chemical>(){chemical}, chemical.solidColor, 0f, 0f, 0f, 0f, false, 15f, null, null, null);
    }
}
