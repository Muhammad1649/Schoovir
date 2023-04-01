using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour {
    
    [Header("NAVIGATION MANAGER")]
    [SerializeField] private GameObject designatedHomeMenu;
    [SerializeField] private Button[] designatedHomeButtons;
    [SerializeField] private Button[] designatedBackButtons;

    [SerializeField] private UITransition[] transitions;
    private List<GameObject> backStack;
    private GameObject currentState;

    [Space]

    [Header("PROMPT MANAGER")]
    [SerializeField] private PromptTrigger[] promptTriggers;

    public static UIManager instance;
    private void Awake() {
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
    }

    private void Start() {
        /* NAVIGATION ----- */
        foreach (Button b in designatedBackButtons) {
            b.onClick.AddListener(Back);
        }

        foreach (Button b in designatedHomeButtons) {
            b.onClick.AddListener(Home);
        }

        foreach (UITransition t in transitions) {
            t.trigger.onClick.AddListener(delegate{Navigate(t.currentState, t.triggerdState, t.addToBackStack, t.clearBackStack);});
        }

        backStack = new List<GameObject>();

        currentState = designatedHomeMenu;
        backStack.Add(currentState);
        /* ----- NAVIGATION */

        /* PROMPT ----- */
        foreach (PromptTrigger p in promptTriggers) {
            for (int i = 0; i < p.trigger.onClick.GetPersistentEventCount(); i++) {
                p.trigger.onClick.SetPersistentListenerState(i, UnityEventCallState.Off);
            }

            p.trigger.onClick.AddListener(delegate{Prompt.instance.ShowPrompt(p);});
        }
        /* ----- PROMPT */
    }

    /* NAVIGATION ----- */
    #region Navigation
        public void Navigate(GameObject start, GameObject end, bool addToBackStack, bool clearBackStack) {
            if (addToBackStack && !clearBackStack) {
                AddToBackStack(start);
            } else {
                if (start != null) {
                    start.SetActive(false);
                }
            }

            if (clearBackStack) {
                if (start != null) {
                    start.SetActive(false);
                }
                ClearBackStack();
            }
            
            currentState = end;

            if (currentState != null) {
                currentState.SetActive(true);
            }

            // Prompt.instance.ShowPrompt(new PromptTrigger(
            //     CONSTANT.PromptType.Message,
            //     "New Prompt",
            //     "UI Navigated Succefully"
            // ));

            // Prompt.instance.ShowPrompt(new PromptTrigger(
            //     CONSTANT.PromptType.Warning,
            //     "New Prompt",
            //     "UI Navigated Succefully"
            // ));

            // Prompt.instance.ShowPrompt(new PromptTrigger(
            //     CONSTANT.PromptType.Error,
            //     "New Prompt",
            //     "UI Navigated Succefully"
            // ));
        }
        public void Back() {
            if (currentState != null) {
                currentState.SetActive(false);
            }
            currentState = backStack[backStack.Count - 1];

            RemoveFromBackStack(currentState);

            if (currentState != null) {
                currentState.SetActive(true);
            }
        }
        public void Home() {
            if (currentState != null) {
                currentState.SetActive(false);
            }

            ClearBackStack();
            designatedHomeMenu.SetActive(true);
        }
        public void MakeTransition(string name) {
            UITransition t = Array.Find(transitions, transition => transition.name == name);
            Navigate(t.currentState, t.triggerdState, t.addToBackStack, t.clearBackStack);
        }

        private void AddToBackStack(GameObject menu) {
            if (currentState != null) {
                menu.SetActive(false);
            }
            backStack.Add(menu);
        }
        private void RemoveFromBackStack(GameObject menu) {
            backStack.Remove(menu);
        }
        private void ClearBackStack() {
            backStack.Clear();
            backStack.Add(designatedHomeMenu);
        }
    #endregion Navigation
    /* ----- NAVIGATION */



    #region Prompt
    /* PROMPT ----- */
    private void TriggerPrompt() {}
    /* ----- PROMPT */
    #endregion Prompt
}
