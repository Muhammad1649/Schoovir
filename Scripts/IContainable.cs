using System;
using System.Collections.Generic;
using UnityEngine;

public interface IContainable {
    abstract void SetTemperature(float temp);
    abstract void SetPreasure(float preasure);

    void AddChemicals(List<Chemical> chemicalToAdd);
    void AddChemical(Chemical chemicalToAdd);

    void FillContainer(float fillRate);
    void EmptyContainer(float emptyRate);

    (string _name, string _formula, List<Chemical> _chemicals, Color _liquidColor, float _liquidFill, float _percentage, float _volume, float _weight, bool _isEmpty, float _temperature, Transform _containerTop, Transform _containerBottom, Transform _liquidSurface) GetInfo();
}
