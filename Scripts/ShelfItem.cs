using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShelfItem : XRBaseInteractor {

    [Header("Object")]
    [SerializeField] private GameObject shelfObject;
    [SerializeField] private Transform uiAnchor;
    [SerializeField] private Transform uiParent;
    [SerializeField] private Transform parent;
    [SerializeField] private float respawnTime = 10.0f;
    [Header("Properties")]
    [SerializeField] private new string name;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [Header("UI")]
    [SerializeField] private GameObject ui;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image image;


    private bool spawn;
    private float counter;

    XRBaseInteractable i;

    private new void Start() {
        counter = 0;
        spawn = false;
        i = Instantiate(shelfObject, parent).GetComponent<XRBaseInteractable>();
        this.StartManualInteraction((IXRSelectInteractable)i);

        Transform nameTag = Instantiate(ui, uiParent).transform;
        nameTag.GetComponentInChildren<TMP_Text>().text = name;

        nameTag.position = uiAnchor.position;
        nameTag.rotation = uiAnchor.rotation;

        base.Start();
    }

    private void Update() {
        if (i.isHovered) { ShowDescription(); }

        if (spawn) {
            counter += Time.deltaTime;
            if (counter >= respawnTime) {
                i = Instantiate(shelfObject, parent).GetComponent<XRBaseInteractable>();
                StartManualInteraction((IXRSelectInteractable)i);
                counter = 0;
                spawn = false;
            }
        }
    }

    public override void StartManualInteraction(IXRSelectInteractable interactable) {
        base.StartManualInteraction(interactable);
    }

    void ShowDescription() { descriptionText.text = description; image.sprite = icon; title.text = name; }

    public void Respawn() { spawn = true; }
}
