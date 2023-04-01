using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Prompt : MonoBehaviour {
    [SerializeField] private Transform face;
    [SerializeField] private Transform faceTracker;
    [SerializeField] private Transform faceTrackerContainer;

    [Header("CHOICE PROMPT")]
    [SerializeField] private GameObject choicePrompt;
    [SerializeField] private TMP_Text choiceText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button rejectButton;

    [Header("LOADING PROMPT")]
    [SerializeField] private GameObject loadingPrompt;

    [Header("MESSAGE PROMPT")]
    [SerializeField] private GameObject messagePrompt;
    [SerializeField] private GameObject warningPrompt;
    [SerializeField] private GameObject errorPrompt;

    private List<GameObject> logStash;

    public static Prompt instance;
    private void Awake() {
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
    }

    private void Start() {
        logStash = new List<GameObject>();
        ResetAll();
    }

    private void FixedUpdate() {
        faceTracker.transform.position = face.transform.position;
        faceTracker.transform.rotation = face.transform.rotation;

        // faceTracker.transform.rotation = Quaternion.Euler(0, face.transform.rotation.z, 0);
    }

    public void ShowPrompt(PromptTrigger p) {
        if (p.promptType == CONSTANT.PromptType.Choice) {
            choiceText.text = p.promptMessage;

            acceptButton.onClick.AddListener(delegate{InvokeEventCaller(p.trigger);});
            rejectButton.onClick.AddListener(delegate{Reset(CONSTANT.PromptType.Choice);});

            choicePrompt.SetActive(true);
        } else if (p.promptType == CONSTANT.PromptType.Loading) {
            loadingPrompt.SetActive(true);
        } 
        /* else if (p.promptType == CONSTANT.PromptType.Message) {
            GameObject g =  Instantiate(messagePrompt, faceTrackerContainer);
            Button b = g.GetComponentInChildren<Button>();

            TMP_Text t = g.GetComponentInChildren<TMP_Text>();
            t.text = p.promptMessage;

            b.onClick.AddListener(delegate{Destroy(g);});
            logStash.Add(g);
            Invoke("Destroy", CONSTANT.promptMessageTimeout);

        } else if (p.promptType == CONSTANT.PromptType.Warning) {
            GameObject g =  Instantiate(warningPrompt, faceTrackerContainer);
            Button b = g.GetComponentInChildren<Button>();

            TMP_Text t = g.GetComponentInChildren<TMP_Text>();
            t.text = p.promptMessage;

            b.onClick.AddListener(delegate{Destroy(g);});
            logStash.Add(g);
            Invoke("Destroy", CONSTANT.promptWarningTimeout);

        } else if (p.promptType == CONSTANT.PromptType.Error) {
            GameObject g =  Instantiate(errorPrompt, faceTrackerContainer);
            Button b = g.GetComponentInChildren<Button>();

            TMP_Text t = g.GetComponentInChildren<TMP_Text>();
            t.text = p.promptMessage;

            b.onClick.AddListener(delegate{Destroy(g);});
            logStash.Add(g);
            Invoke("Destroy", CONSTANT.promptErrorTimeout);
        } */

        Debug.Log(logStash.Count);
    }
    private void InvokeEventCaller(Button caller) {
        for (int i = 0; i < caller.onClick.GetPersistentEventCount(); i++) {
            if (caller.onClick.GetPersistentTarget(i) != this) {
                caller.onClick.SetPersistentListenerState(i, UnityEventCallState.RuntimeOnly);
            }
        }

        caller.onClick.Invoke();
        Reset(CONSTANT.PromptType.Choice);

        for (int i = 0; i < caller.onClick.GetPersistentEventCount(); i++) {
            if (caller.onClick.GetPersistentTarget(i) != this) {
                caller.onClick.SetPersistentListenerState(i, UnityEventCallState.Off);
            }
        }
    }
    public void Reset(CONSTANT.PromptType t) {
        switch (t) {
            case CONSTANT.PromptType.Choice: {
                choicePrompt.SetActive(false);
                acceptButton.onClick.RemoveAllListeners();
                rejectButton.onClick.RemoveAllListeners();
            }
                break;
            case CONSTANT.PromptType.Loading: {
                loadingPrompt.SetActive(false);
            }
                break;
        }
    }
    public void Destroy() {
        Destroy(logStash[logStash.Count - 1]);
        logStash.RemoveAt(logStash.Count - 1);
        Debug.Log("Destroy Invoked");
        Debug.Log(logStash.Count);
    }
    public void Destroy(GameObject g) {
        Destroy(g);
        Debug.Log("Button Destroy Invoked");
    }
    public void ResetAll() {
        Reset(CONSTANT.PromptType.Choice);
        Reset(CONSTANT.PromptType.Loading);
    }
}
