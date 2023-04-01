using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    [SerializeField] GameObject[] sceneContents;
    [SerializeField] private float loadingDelay = 1f;
    private string loadingScene;

    public static GameManager instance;
    private void Awake() {
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
    }
    void Start() {
        Debug.Log(Microphone.devices.Length);
        Debug.Log(Application.internetReachability.ToString());
    }
        // -- SAVE SETTINGS --
    public void SetHand(string hand) {
        PlayerPrefs.SetString("Hand", hand);
    }
    public void SetLocomotion(string type) {
        PlayerPrefs.SetString("Movement", type);
    }
        // -- SAVE SETTINGS --
        
        // -- CHANGE SCENE --
    public void ChangeScene(string name) {
        loadingScene = name;
        Invoke("LoadNewScene", loadingDelay);
    }

    public void ReloadScene() {
        loadingScene = SceneManager.GetActiveScene().name;
        VanishContents();
        Invoke("LoadNewScene", loadingDelay);
    }
    
    private void VanishContents() {
        Prompt.instance.ShowPrompt(new PromptTrigger(CONSTANT.PromptType.Loading));
        foreach (GameObject content in sceneContents) {
            content.SetActive(false);
        }
    }
    private void LoadNewScene() {
        StartCoroutine(LoadAsyncronusly());
    }
    private IEnumerator LoadAsyncronusly() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(loadingScene);

        while (operation.isDone) {
            yield return null;
        }
    }
        // -- CHANGE SCENE --

    public void Quit() {
        Application.Quit();
    }
}
