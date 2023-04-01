using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SessionManager : MonoBehaviour {
    public List<Reaction> reactions;
    [SerializeField] Transform hidePresets;

    [Space]
    [Header("Exparement Learn Events")]
    public UnityEvent <string> onInstructionChange;
    public UnityEvent <int> onStepChange;

    [Space]
    [Header("Exparement Finished Events")]
    public UnityEvent onExparimentFinished;
    public UnityEvent <SessionManager> onExparimentResults;

    [Space]
    [Header("Exparement Events")]
    public UnityEvent onExparimentStarted;
    public UnityEvent onInstructionRepeat;
    public UnityEvent <string> onExparimentPaused;
    public UnityEvent onExparimentResume;
    public UnityEvent onExparimentCancled;

    private bool paused;

    private string[] exparementSteps;
    private string[] exparementStepsInstructions;
    private List<string> stepsTemp;

    private GameObject[] presets;

    private int step;
    private int practiceStep;
    private int stepHolder;

    private Dictionary<string, int> actions = new Dictionary<string, int>() {
    ["HAND_GRABBING"] = 0, ["RIGHT_HAND_GRABBING"] = 0, ["LEFT_HAND_GRABBING"] = 0,
    ["BEAKER_GRABBED"] = 0, ["BEAKER_POURING"] = 0, ["BEAKER_RECEIVING"] = 0,
    ["BURETTE_GRABBED"] = 0, ["BURETTE_TAP_POURING"] = 0, ["BURETTE_RECEIVING"] = 0, ["FILTER_FUNNEL_ATTACHED"] = 0,
    ["CONICAL_FLASK_GRABBED"] = 0, ["CONICAL_FLASK_POURING"] = 0, ["CONICAL_FLASK_RECEIVING"] = 0,
    ["VOLUMETRIC_PIPETTE_GRABBED"] = 0, ["VOLUMETRIC_PIPETTE_POURING"] = 0, ["VOLUMETRIC_PIPETTE_RECEIVING"] = 0,
    ["GRADUATED_CYLINDER_GRABBED"] = 0, ["GRADUATED_CYLINDER_POURING"] = 0, ["GRADUATED_CYLINDER_RECEIVING"] = 0,
    ["RETHORD_STAND_GRABBED"] = 0, ["BURETTE_CLAMP_GRABBED"] = 0, ["BURETTE_CLAMP_ATTACHED"] = 0,
    ["WEIGHT_BALANCE_GRABBED"] = 0, ["WEIGHT_BALANCE_WEIGHING"] = 0,
    ["GAS_CYLINDER_GRABBED"] = 0, ["GAS_CYLINDER_ATTACHED"] = 0,
    ["TRASH_CAN_GRABBED"] = 0, ["TRASH_CAN_DUMPED"] = 0,
    ["CHEMICAL_BOTTLE_GRABBED"] = 0, ["CHEMICAL_BOTTLE_POURING"] = 0,
    ["FILTER_FUNNEL_GRABBED"] = 0, ["FILTER_FUNNEL_RECEIVING"] = 0,
    ["SOLID_GRABBED"] = 0, ["SOLID_POURED"] = 0};
    
    private int totalPercentage, accuracyPercentage, timePercentage;
    
    private string accuracyLog, delayLog, timeLog;

    private float imperfections, totalImperfections;

    private float time, elapsedTime, exparementEstimatedTime, pausedTime;

    private bool isExparimenting;

    string stepTemp;

    private bool precisionCalculation;
    string precisionTrigger;

    public static SessionManager instance;
    private void Awake() {
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
        isExparimenting = false;
        precisionCalculation = false;
        paused = false;
    }

    private void Update() {
        if (isExparimenting && !paused) { time += Time.deltaTime; pausedTime += Time.deltaTime; }
    }
    
    public void SetExparement(List<Reaction> reactionList, string[] steps, string[] instructions, float estimatedTime, GameObject[] presets) {
        reactions = reactionList;
        exparementSteps = steps;
        exparementStepsInstructions = instructions;
        exparementEstimatedTime = estimatedTime;

        this.presets = presets;

        foreach (GameObject p in presets) { p.transform.SetParent(null); }

        isExparimenting = true;
    }

    public void StartExparement() {
        step = 0;
        
        onInstructionChange.Invoke(exparementStepsInstructions[step]);
        onStepChange.Invoke(step + 1);

        stepsTemp = exparementSteps.ToList();
        stepTemp = exparementSteps[step];
        onExparimentStarted.Invoke();
    }

    // -------------------Trigger Methods
    public void TriggerOn(string trigger) {
        actions[trigger] = 1;
        if (precisionCalculation) {return;}
        CompareSteps();
    }

    public void TriggerOff(string trigger) {
        actions[trigger] = 0;
    }

    public void TriggerValue(string trigger, int value) {
        actions[trigger] = value;
        precisionCalculation = true;
        precisionTrigger = trigger;
        CancelInvoke("PrecisionTrigger");
        Invoke("PrecisionTrigger", 2);
    }
    private void PrecisionTrigger() {
        precisionCalculation = false;
        CompareSteps();
    }
    // --------------------Trigger Methods

    public void Repeat() {
        onInstructionRepeat.Invoke();
    }
    public void Pause() {
        if (paused) { return; }
        paused = true;

        string pausedTimeText = "No Time";

        if (pausedTime <= 60) {
        pausedTimeText = Mathf.RoundToInt(pausedTime).ToString();
        } else if (pausedTime <= 3600) {
            pausedTimeText = Mathf.RoundToInt( pausedTime / 60 ) + ":" + Mathf.RoundToInt( pausedTime % 60 ).ToString();
        } else if (pausedTime <= 216000) {
            pausedTimeText = Mathf.RoundToInt( (pausedTime / 60) / 60 ) + ":" + Mathf.RoundToInt( (pausedTime % 60) % 60 ).ToString() + ":" + Mathf.RoundToInt( ( (pausedTime % 60) % 60 ) % 60 ).ToString();
        }

        foreach (GameObject p in presets) {
            p.SetActive(false);
        }

        onExparimentPaused.Invoke(pausedTimeText);
    }
    public void Resume() {

        foreach (GameObject p in presets) {
            p.SetActive(true);
        }

        onExparimentResume.Invoke();
        paused = false;
    }
    public void Finish() {
        // Total Time Taken
        if (elapsedTime <= 60) {
        timeLog = Mathf.RoundToInt(elapsedTime) + " Seconds";
        } else if (elapsedTime <= 3600) {
            timeLog = Mathf.RoundToInt( elapsedTime / 60 ) + ":" + Mathf.RoundToInt( elapsedTime % 60 ) + " Seconds";
        } else if (elapsedTime <= 216000) {
            timeLog = Mathf.RoundToInt( (elapsedTime / 60) / 60 ) + ":" + Mathf.RoundToInt( (elapsedTime % 60) % 60 ) + " Minutes";
        }

        // Accuracy or Precision Percentage
        accuracyPercentage = 0;
        accuracyPercentage = Mathf.RoundToInt( 100 - ((imperfections / totalImperfections) * 100) );

        // Time Taken or Delay Percentage
        timePercentage = 0;
        if (elapsedTime <= exparementEstimatedTime) {
            timePercentage = 100;
        } else {
            timePercentage = Mathf.RoundToInt( (exparementEstimatedTime / elapsedTime) * 100 );
        }

        // Total Score Percentage
        totalPercentage = Mathf.RoundToInt( ((accuracyPercentage + timePercentage) / 200) * 100 );

        isExparimenting = false;
        onExparimentResults.Invoke(this);
        onExparimentFinished.Invoke();
    }
    public void Cancel() {
        CleanUp();
        onExparimentCancled.Invoke();
        GameManager.instance.ReloadScene();
    }
    private void CompareSteps() {
        if (!paused) {
            if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Learn) {
                CompareLearningSteps();
            } else if (CONSTANT.exparimentMode == CONSTANT.ExparimentMode.Test) {
                CompareTestSteps();
            }
        }
    }

    private void CompareLearningSteps() {
        string key = "";
        float value = 0f;
        foreach (string k in actions.Keys) {
            if (actions[k] > 0) {
                if (actions[k] == 1) {
                    stepTemp = stepTemp.Replace(k, null);
                } else {
                    stepTemp = stepTemp.Replace(k, null);
                    key = k;
                    value = actions[k];
                }
            }
        }
        
        float f = 0f;
        try {
            f = float.Parse(stepTemp, CultureInfo.InvariantCulture.NumberFormat);
        } catch {}

        if (stepTemp == "" || f > 0) {

            if (f > 0) {
                imperfections += ( Mathf.Max(f, value) - Mathf.Min(f, value) );
                totalImperfections += 100;
                string keyName = key.Replace("RECEIVING", "");
                keyName = keyName.Replace("_", " ");
                accuracyLog += (step + 1) + ". You filled the " + keyName.ToLower() + " to " + value + "%, " + " insted of " + f + "% \n"; 
            }
            practiceStep = step;
            Next();
        }
    }

    private void CompareTestSteps() {
        string key = "";
        float value = 0f;
        foreach (string k in actions.Keys) {
            if (actions[k] > 0) {
                if (actions[k] == 1) {
                    for (int i = 0; i < stepsTemp.Count; i++) {
                        stepsTemp[i] = stepsTemp[i].Replace(k, null);
                    }
                } else {
                    for (int i = 0; i < stepsTemp.Count; i++) {
                        stepsTemp[i] = stepsTemp[i].Replace(k, null);
                    }
                    key = k;
                    value = actions[k];
                }
            }
        }

        bool nullCheck = false;
        foreach (string s in stepsTemp) {
            if (s == "") {
                nullCheck = true;
                practiceStep = stepsTemp.IndexOf(s);
                break;
            }
        }
        
        float f = 0f;
        try {
            foreach (string s in stepsTemp) {
                f = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
                practiceStep = stepsTemp.IndexOf(s);
                if (f > 0f) { break; }
            }
        } catch {}

        if (nullCheck || f > 0) {
            Debug.Log("=>" + stepsTemp[practiceStep]);
            if (f > 0) {
                imperfections += ( Mathf.Max(f, value) - Mathf.Min(f, value) );
                totalImperfections += 100;
                string keyName = key.Replace("RECEIVING", "");
                keyName = keyName.Replace("_", " ");
                accuracyLog += (step + 1) + ". You filled the " + keyName.ToLower() + " to " + value + "%, " + " insted of " + f + "% \n"; 
            }

            Next();
        }
    }

    private void Next() {
        actions[precisionTrigger] = 0;
        
        //-------------------
        stepHolder = step;
        step = practiceStep;
        //-------------------

        Debug.Log("COMPLETED STEP" + step);

        // Generate Delay Log
        string t = "no time";
        if (time <= 60) {
            t = Mathf.RoundToInt(time) + " Seconds";
        } else if (time <= 3600) {
            t = Mathf.RoundToInt( time / 60 ) + ":" + Mathf.RoundToInt( time % 60 ) + " Seconds";
        } else if (time <= 216000) {
            t = Mathf.RoundToInt( (time / 60) / 60 ) + ":" + Mathf.RoundToInt( (time % 60) % 60 ) + " Minutes";
        }
        delayLog += (step + 1) + ". You took [ " + t + " ] to : " + exparementStepsInstructions[step] + "\n";
        elapsedTime += time; time = 0;

        //-------------------
        step = stepHolder;
        //-------------------

        Debug.Log("STEP COUNT" + step);


        if (step + 1 >= exparementStepsInstructions.Length) {
            Finish();
            return;
        }
        
        step++;
        onInstructionChange.Invoke(exparementStepsInstructions[step]);
        onStepChange.Invoke(step + 1);
        stepTemp = exparementSteps[step];

        exparementSteps.ToList().RemoveAt(practiceStep);
        stepsTemp = exparementSteps.ToList();

        Debug.Log("EXPARIMENT:  " + exparementSteps.Length);
        Debug.Log("STEPS:  " + stepsTemp.Count());

    }

    public void CleanUp() {
        foreach (GameObject p in presets) {
            p.transform.SetParent(hidePresets);
        }
    }

    public (string elapsedTime, int totalPercentage, int accuracyPercentage, int timePercentage, string accuracyLog, string delayLog) GetResult() {
        return (timeLog, totalPercentage, accuracyPercentage, timePercentage, accuracyLog, delayLog);
    }
}
