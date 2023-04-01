using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PromptTrigger {
    public string name;
    public Button trigger;
    public CONSTANT.PromptType promptType;
    [TextArea]
    public string promptMessage;

    public PromptTrigger(CONSTANT.PromptType promptType, string name = "New Prompt", string promptMessage = "This is an empty prompt", Button trigger = null) {
        this.name = name;
        this.trigger = trigger;
        this.promptType = promptType;
        this.promptMessage = promptMessage;
    }
}
