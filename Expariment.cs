using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Expariment {
    public string name;
    [TextArea]
    public string description;
    public float estimatedTime;
    public GameObject[] presets;

    public List<Reaction> reactionList;
    [TextArea]
    public string[] exparementSteps;
    [TextArea]
    public string[] exparementStepsInstructions;
}
