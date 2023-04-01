using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class Logger : MonoBehaviour {
    [SerializeField] private ScrollRect scroll;

    [Header("Logs")]
    [SerializeField] private TMP_Text logText;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private TMP_Text errorText;
    
    [Header("Controls")]
    [SerializeField] private Button clearButtton;
    [SerializeField] private Button pauseButtton;
    [SerializeField] private Button logButtton;
    [SerializeField] private Button warningButtton;
    [SerializeField] private Button errorButtton;

    private bool pause;
    private bool filterLog;
    private bool filterWarning;
    private bool filterError;

    private void Awake() {
        pause = false;
        filterLog = true;
        filterWarning = true;
        filterError = true;

        pauseButtton.onClick.AddListener(Pause);
        clearButtton.onClick.AddListener(Clear);
        logButtton.onClick.AddListener(FilterLog);
        warningButtton.onClick.AddListener(FilterWarning);
        errorButtton.onClick.AddListener(FilterError);
    }
    void Start () {

    }

    // Controls

    private void Pause() {
        pause = !pause;

        TMP_Text t = pauseButtton.GetComponentInChildren<TMP_Text>();
        if (pause) {
            t.text = "Resume";
        } else {
            t.text = "Pause";
        }
    }

    private void Clear() {
        logText.text = "";
        warningText.text = "";
        errorText.text = "";
    }

    private void FilterLog() {
        filterLog = !filterLog;
        logText.gameObject.SetActive(filterLog);
    }

    private void FilterWarning() {
        filterWarning = !filterWarning;
        warningText.gameObject.SetActive(filterWarning);
    }

    private void FilterError() {
        filterError = !filterError;
        errorText.gameObject.SetActive(filterError);
    }

    // Controls

    private void Update() {
        
    }

    void OnEnable() {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable() {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type) {

        if(pause) { return; }

        if (type == LogType.Log) {
            logText.text += 
            "TYPE : " + type.ToString().ToUpper() + "\n" + 
            "MESSAGE : " + message + "\n" + 
            "STACKTRACE : " + stackTrace +
            "___________________________" + "\n";
            return;
        } else if (type == LogType.Warning) {
            warningText.text += 
            "TYPE : " + type.ToString().ToUpper() + "\n" + 
            "MESSAGE : " + message + "\n" + 
            "STACKTRACE : " + stackTrace +
            "___________________________" + "\n";
            return;
        } else if (type == LogType.Error) {
            errorText.text += 
            "TYPE : " + type.ToString().ToUpper() + "\n" + 
            "MESSAGE : " + message + "\n" + 
            "STACKTRACE : " + stackTrace +
            "___________________________" + "\n";
            return;
        }

        scroll.normalizedPosition = new Vector2(0, 0);
    }
}