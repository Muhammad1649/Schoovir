using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PeriodicElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    [Header("Info Chart")]
    [SerializeField] private PeriodicTableInfoChart infoChart;

    [Header("Element Info")]
    [SerializeField] private string elementName;
    private string symbol; // Derived
    private string position; // Derived
    [SerializeField] private int protons;
    private int neutrons; // Derived
    private int electrons; // Derived
    private int valency; // Derived
    [SerializeField] private float atomicMass;
    [SerializeField] private float atomicRadius;
    private int group; // Derived
    private int period; // Derived
    [SerializeField] private string metalicProperty;
    [SerializeField] private string physicalState;
    [SerializeField] private float meltingPoint;
    [SerializeField] private float boilingPoint;
    [SerializeField] private string reactivity;
    [TextArea]
    [SerializeField] private string extra;

    [Header("Variables")]

    [SerializeField] private Image background;
    [SerializeField] private Shadow outline;

    [SerializeField] private TMP_Text symbolText;
    [SerializeField] private TMP_Text atomicNumberText;
    [SerializeField] private TMP_Text massNumberText;

    [SerializeField] private Color textColor;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color outlineColor;

    private float hoverWidth = 0.2f;
    private float hoverHeight = 0.2f;
    private float hoverLocation = -1f;
    private float restLocation = 0;

    private RectTransform t;
    private void Start() {
        t = GetComponent<RectTransform>();

        symbol = gameObject.name;
        electrons = protons;
        neutrons = Mathf.RoundToInt(atomicMass - protons);
        if (protons == 1) { group = 1; } else if (protons == 2) { group = 8; } else { group = (int)((protons - 2 ) % 8); }
        period = (int)((protons - group) / 8);
        if (group > 4) { valency = 8 - group; } if (group <= 4) { valency = group; }

        background.color = backgroundColor;
        outline.effectColor = outlineColor;
        
        symbolText.color = textColor;
        atomicNumberText.color = textColor;
        massNumberText.color = textColor;

        symbolText.text = symbol;
        atomicNumberText.text = protons.ToString();
        massNumberText.text = atomicMass.ToString();

        position = "th";
        switch(protons) {
            case 1: position = "st";
                break;
            case 2: position = "nd";
                break;
            case 3: position = "rd";
                break;
        }
    }

    private void DisplayInfo() {
        infoChart.SetInfo(backgroundColor, elementName, symbol, position, protons, neutrons, electrons, valency, atomicMass, atomicRadius, physicalState, metalicProperty, meltingPoint, boilingPoint, reactivity, group, period, extra);
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        t.localScale = new Vector3(t.localScale.x + hoverWidth, t.localScale.y + hoverHeight, t.localScale.z);
        t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, hoverLocation);
        transform.SetAsLastSibling();
        outline.effectColor = Color.white;

        AudioManager.instance.PlaySfx("Pop");
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        t.localScale = new Vector3(t.localScale.x - hoverWidth, t.localScale.y - hoverHeight, t.localScale.z);
        t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, restLocation);
        transform.SetAsFirstSibling();
        outline.effectColor = outlineColor;
    }

    public void OnPointerClick(PointerEventData pointerEventData) { DisplayInfo(); }

}
