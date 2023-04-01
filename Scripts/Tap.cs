using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tap : MonoBehaviour, IContainable {
    [SerializeField] private bool tapSwitch;
    [SerializeField] private Transform ui;
    [SerializeField] private TMP_Text OnOffText;
    [SerializeField] private Image OnOffIndicator;
    [SerializeField] private Transform pourOrigin;
    [SerializeField] private GameObject streamPrefab;
    
    private LiquidStream currentStream;

    [Header("Chemical Properties")]
    [SerializeField] private Chemical chemical;
    [SerializeField] private float temperature;
    void Awake() {
        tapSwitch = false;
    }
    void Update() {
        if(tapSwitch){OnOffText.text = "ON"; OnOffIndicator.color = Color.green;}
        else if(!tapSwitch){OnOffText.text = "OFF"; OnOffIndicator.color = Color.red;}

        ui.position = transform.position;
    }
    public void Switch() {
        tapSwitch = !tapSwitch;
        if(tapSwitch){StartPour();}else if(!tapSwitch){EndPour();}
    }
    void StartPour() {
        currentStream = CreateStream();
        currentStream.Begin();
    }
    void EndPour() {
        currentStream.End();
        currentStream = null;
    }
    LiquidStream CreateStream() {
        GameObject streamObject = Instantiate(streamPrefab, pourOrigin.position, Quaternion.identity, transform);
        return streamObject.GetComponent<LiquidStream>();
    }

    public (string _name, string _formula, List<Chemical> _chemicals, Color _liquidColor, float _liquidFill, float _percentage, float _volume, float _weight, bool _isEmpty, float _temperature, Transform _containerTop, Transform _containerBottom, Transform _liquidSurface) GetInfo() {
        return (chemical.name, chemical.formula, new List<Chemical>(){chemical}, chemical.liquidColor, 0f, 0f, 0f, 0f, false, temperature, null, null, null);
    }

    // Interface Implementation
    public virtual void SetTemperature(float temp) {}
    public virtual void SetPreasure(float preasure) {}
    public void AddChemicals(List<Chemical> chemicalToAdd) {}
    public void AddChemical(Chemical chemicalToAdd) {}
    public void FillContainer(float fillRate) {}
    public void EmptyContainer(float emptyRate) {}
}
