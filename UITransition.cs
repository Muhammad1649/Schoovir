using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UITransition {
    public string name;
    public bool addToBackStack = true;
    public bool clearBackStack = false;
    public Button trigger;
    public GameObject currentState;
    public GameObject triggerdState;
}
