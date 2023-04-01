using UnityEngine;
using TMPro;

public class PeriodicTableInfoChart : MonoBehaviour
{
    [SerializeField] private GameObject tableInfo;
    [SerializeField] private GameObject elementInfo;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text atomicStructure;
    [SerializeField] private TMP_Text protons;
    [SerializeField] private TMP_Text neutrons;
    [SerializeField] private TMP_Text electrons;
    [SerializeField] private TMP_Text atomicStructure_2;
    [SerializeField] private TMP_Text atomicRadius;
    [SerializeField] private TMP_Text physicalProperties;
    [SerializeField] private TMP_Text meltingPoint;
    [SerializeField] private TMP_Text boilingPoint;
    [SerializeField] private TMP_Text periodicProperty;
    [SerializeField] private TMP_Text group;
    [SerializeField] private TMP_Text period;
    [SerializeField] private TMP_Text extra;

    private void Start() { ResetInfo(); }
    public void SetInfo(Color elementColor, string elementName, string symbol, string position, int protons, int neutrons, int electrons, int valency, float atomicMass, float atomicRadius, string physicalState, string metalicProperty, float meltingPoint, float boilingPoint, string reactivity, int group, int period, string extra) {
        tableInfo.SetActive(false);
        elementInfo.SetActive(true);
        
        title.color = elementColor;
        title.text = elementName;
        this.protons.text = protons.ToString();
        this.neutrons.text = neutrons.ToString();
        this.electrons.text = electrons.ToString();
        this.atomicRadius.text = atomicRadius.ToString();
        this.meltingPoint.text = meltingPoint.ToString() + "°C";
        this.boilingPoint.text = boilingPoint.ToString() + "°C";
        this.group.text = group.ToString();
        this.period.text = period.ToString();

        atomicStructure.text = "This is the " + protons + position + " element in the periodic table, having the symbol '" + symbol + "', and the following atomic structure.";
        atomicStructure_2.text = "This element has a valency of " + valency + " and a " + atomicMass + " atomic mass, with the following atomic radius.";
        physicalProperties.text = "It is a " + physicalState + " " + metalicProperty + " at room temperature, having the melting and boiling point below.";
        periodicProperty.text = "It is " + reactivity + " belonging to the group and period below.";
        this.extra.text = extra;
    }

    public void ResetInfo() {
        elementInfo.SetActive(false);
        tableInfo.SetActive(true);
    }
}
