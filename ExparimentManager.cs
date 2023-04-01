using System.IO;
using System;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;

public class ExparimentManager : MonoBehaviour {
    [Header("Exparement Start Events")]
    public UnityEvent onExparimenttSarted;

    public Expariment[] exparements;

    private void Start() {
        // Store();  // Suppose to load
    }

    public void SelectExpariment(string name) {
        Expariment e = Array.Find(exparements, exparement => exparement.name == name);
        SessionManager.instance.SetExparement(e.reactionList, e.exparementSteps, e.exparementStepsInstructions, e.estimatedTime, e.presets);
        SessionManager.instance.StartExparement();
        onExparimenttSarted.Invoke();
    }

    public void Load() {
        if( File.Exists("Assets/Data/expariments.json")) {
            var exparimentsJson = File.ReadAllText("Assets/Data/expariments.json");
            exparements = JsonConvert.DeserializeObject<Expariment[]>(exparimentsJson);

            Debug.Log("Expariments Loaded Successfully.");
        }
    }

    public void Store() {
        var exparimentsJson = JsonConvert.SerializeObject(exparements);
        File.WriteAllText("Assets/Data/expariments.json", exparimentsJson); 

        Debug.Log("Expariments Stored Successfully.");
    }
}
