using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour, IContainable {

    [SerializeField] private Chemical chemical;

    public (string _name, string _formula, List<Chemical> _chemicals, Color _liquidColor, float _liquidFill, float _percentage, float _volume, float _weight, bool _isEmpty, float _temperature, Transform _containerTop, Transform _containerBottom, Transform _liquidSurface) GetInfo() {
        return (chemical.name, chemical.formula, new List<Chemical>() {chemical}, chemical.liquidColor, 1f, 100f, 600f, 60f, false, 35f, transform, transform, transform);
    }

    public virtual void SetTemperature(float temp) {}
    public virtual void SetPreasure(float preasure) {}

    public void AddChemicals(List<Chemical> chemicalToAdd) {}
    public void AddChemical(Chemical chemicalToAdd) {}

    public void FillContainer(float fillRate) {}
    public void EmptyContainer(float emptyRate) {}
}
