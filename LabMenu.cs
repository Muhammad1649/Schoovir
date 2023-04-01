using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LabMenu : MonoBehaviour {
    private ExparimentManager exparimentManager;
    private SessionManager results;
    [SerializeField] private Transform body;
    [SerializeField] private float bodyDistance;

    [SerializeField] private GameObject menu;
    [Header("Wrist Menu")]
    [SerializeField] private GameObject wristMenu;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button pauseButton;

    [Header("Mode Menu")]
    [SerializeField] private GameObject modeMenu;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TMP_Text pauseExparimentName;
    [SerializeField] private TMP_Text pauseElapsedTime;

    [Header("Expariments Menu")]
    [SerializeField] private GameObject exparimentsMenu;
    [SerializeField] private GameObject exparimentPrefab;
    [SerializeField] private Transform scrollView;

    [Header("Test Setup Menu")]
    [SerializeField] private GameObject testSetupMenu;
    [SerializeField] private Toggle measureAccuracyToggle;
    [SerializeField] private Toggle measureDelayToggle;
    [SerializeField] private Toggle measureNeatnessToggle;
    [SerializeField] private Toggle displayTimeToggle;

    [Header("Expariment Initialization Menu")]
    [SerializeField] private GameObject exparimentInfoMenu;
    [SerializeField] private TMP_Text exparimentInfo;

    [Header("Learn Steps Menu")]
    [SerializeField] private GameObject learnStepsMenu;
    [SerializeField] private TMP_Text learnStepsText;
    [SerializeField] private TMP_Text learnInstructionsText;
    [SerializeField] private Button learnFinishButton;

    [Header("Test Menu")]
    [SerializeField] private Button testFinishButton;

    [Header("Results Menu")]
    [SerializeField] private GameObject resultsMenu;

    [Space]
    [SerializeField] private GameObject goalSummaryMenu;
    [SerializeField] private GameObject goalEvaluationMenu;

    [SerializeField] private GameObject accuracySummaryMenu;
    [SerializeField] private GameObject accuracyEvaluationMenu;

    [SerializeField] private GameObject delaySummaryMenu;
    [SerializeField] private GameObject delayEvaluationMenu;

    [SerializeField] private GameObject neatnessSummaryMenu;
    [SerializeField] private GameObject neatnessEvaluationMenu;

    [Header("Results Menu - Summary")]
    [SerializeField] private TMP_Text overallResult;

    [Space]
    [SerializeField] private Slider goal;
    [SerializeField] private TMP_Text goalPercentage;
    [SerializeField] private Slider accuracy;
    [SerializeField] private TMP_Text accuracyPercentage;
    [SerializeField] private Slider delay;
    [SerializeField] private TMP_Text delayPercentage;
    [SerializeField] private Slider neatness;
    [SerializeField] private TMP_Text neatnessPercentage;

    [Header("Results Menu - Evaluation")]
    [SerializeField] private TMP_Text goalEvaluation;
    [SerializeField] private TMP_Text accuracyEvaluation;
    [SerializeField] private TMP_Text timeEvaluation;
    [SerializeField] private TMP_Text neatnessEvaluation;
    [SerializeField] private TMP_Text comment;

    private bool startExpariment;
    string currentExparement;
    private void Start() {

        modeMenu.SetActive(true);
        pauseMenu.SetActive(false);
        exparimentsMenu.SetActive(false);
        testSetupMenu.SetActive(false);
        exparimentInfoMenu.SetActive(false);
        learnStepsMenu.SetActive(false);
        resultsMenu.SetActive(false);
        testFinishButton.gameObject.SetActive(true);

        exparimentManager = FindObjectOfType<ExparimentManager>();
        foreach (Expariment e in exparimentManager.exparements) {
            Button b  = Instantiate(exparimentPrefab, scrollView).GetComponent<Button>();
            TMP_Text[] i  = b.transform.GetComponentsInChildren<TMP_Text>();

            b.GetComponent<RectTransform>().sizeDelta = new Vector2(60, 10);
            
            b.transform.name = e.name;

            i[0].text = e.name;
            i[1].text = e.description;

            b.onClick.AddListener(delegate{SelectExparement(e.name);});
        }

        startExpariment = false;

        homeButton.gameObject.SetActive(!startExpariment);
        pauseButton.gameObject.SetActive(startExpariment);
    }
    private void Update() {
        if (!startExpariment) {
            modeMenu.SetActive( Vector3.Distance(menu.transform.position, body.position) <= bodyDistance );
        }
    }

    /* Mode Selection ----- */
    public void SelectLearnMode() {
        CONSTANT.exparimentMode = CONSTANT.ExparimentMode.Learn;
        startExpariment = true;
    }

    public void SelectTestMode() {
        CONSTANT.exparimentMode = CONSTANT.ExparimentMode.Test;
        startExpariment = true;
    }

    public void SelectClassMode() {
        // Configure Multiplayer
    }
    /* ----- Mode Selection */

    private void SelectExparement(string exparement) {
        currentExparement = exparement;
        exparimentInfo.text = Array.Find(exparimentManager.exparements, e => e.name == currentExparement).description;
        
        if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Learn) {
            UIManager.instance.Navigate(exparimentsMenu, exparimentInfoMenu, true, false);
        } else if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Test) {
            UIManager.instance.Navigate(exparimentsMenu, testSetupMenu, true, false);
        }
    }

    public void SetTestParameters() { 
        CONSTANT.measureAccuracy = measureAccuracyToggle.isOn;
        CONSTANT.measureDelay = measureDelayToggle.isOn;
        CONSTANT.measureNeatness = measureNeatnessToggle.isOn;
        CONSTANT.displayTime = displayTimeToggle.isOn;
    }

    public void ConfirmExparement() {
        exparimentManager.SelectExpariment(currentExparement);

        if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Learn) {
            UIManager.instance.Navigate(exparimentInfoMenu, learnStepsMenu, false, true);
            learnFinishButton.interactable = false;
        }

        if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Test) {
            UIManager.instance.Navigate(exparimentInfoMenu, null, false, true);
            testFinishButton.gameObject.SetActive(true);
        } else {
            testFinishButton.gameObject.SetActive(false);
        }

        homeButton.gameObject.SetActive(!startExpariment);
        pauseButton.gameObject.SetActive(startExpariment);
    }

    public void ChangeInstructions(string instructions) {
        learnInstructionsText.text = instructions;
    }
    public void ChangeStep(int step) {
        learnStepsText.text = "STEP: " + step;
    }


    public void Pause(string time) {
        if (CONSTANT.displayTime) {
            pauseElapsedTime.text = time;
        } else {
            pauseElapsedTime.text = "0";
        }
        pauseExparimentName.text = currentExparement;

        if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Learn) {
            UIManager.instance.Navigate(learnStepsMenu, pauseMenu, false, true);
        } else if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Test) {
            UIManager.instance.Navigate(null, pauseMenu, false, true);
        }
    }
    public void Resume() {
        if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Learn) {
            UIManager.instance.Navigate(pauseMenu, learnStepsMenu, false, true);
        } else if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Test) {
            UIManager.instance.Navigate(pauseMenu, null, false, true);
        }
    }
    public void Finish() {
        if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Learn) {
            learnFinishButton.interactable = true;
        } else if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Test) {
            UIManager.instance.Navigate(pauseMenu, resultsMenu, false, true);
        }
    }

    public void CalculateResults(SessionManager r) {
        results = r;

        goalSummaryMenu.SetActive(true);
        goalEvaluationMenu.SetActive(true);
        accuracySummaryMenu.SetActive(CONSTANT.measureAccuracy);
        accuracyEvaluationMenu.SetActive(CONSTANT.measureAccuracy);
        delaySummaryMenu.SetActive(CONSTANT.measureDelay);
        delayEvaluationMenu.SetActive(CONSTANT.measureDelay);
        neatnessSummaryMenu.SetActive(CONSTANT.measureNeatness);
        neatnessEvaluationMenu.SetActive(CONSTANT.measureNeatness);

        float score  = results.GetResult().totalPercentage;

        overallResult.text = "You finished in: " + results.GetResult().elapsedTime + ", with a total score of: " + score.ToString() + "%";
        
        goal.value = 100;
        goalPercentage.text = 100 + "%";

        accuracy.value = results.GetResult().accuracyPercentage;
        accuracyPercentage.text = results.GetResult().accuracyPercentage + "%";

        delay.value = results.GetResult().timePercentage;
        delayPercentage.text = results.GetResult().timePercentage + "%";

        timeEvaluation.text = results.GetResult().delayLog;
        accuracyEvaluation.text = results.GetResult().accuracyLog;

        string areaOfImprovement = "";
        float lowestValue = Mathf.Min(100, results.GetResult().accuracyPercentage, results.GetResult().timePercentage);

        if (lowestValue == results.GetResult().accuracyPercentage) {
            areaOfImprovement = "on your ACCURACY";
        } else if (lowestValue == results.GetResult().timePercentage) {
            areaOfImprovement = "on your TIME UTILIZATION";
        }

        if (score >= 75) {
            comment.text = "Excelent performance, keep it up.";
        } else if (score >= 50) {
            comment.text = "You did great, just need a little bit more work " + areaOfImprovement + ".";
        } else if (score >= 35) {
            comment.text = "You can do better.";
        } 
    }
    public void SaveResults() {
        // Actually save the results
        SessionManager.instance.Cancel();
    }
}
