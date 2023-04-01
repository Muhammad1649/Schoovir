using UnityEngine;

public class BuretteContainer : Container {
    // In case this is me in the future, this class is ment to be empty, so dont touch it. 
    private void Start() {
        AssistantAI.TriggerOn("RIGHT_HAND_GRABBING");
    }
}
