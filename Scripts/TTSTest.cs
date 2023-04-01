using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SchoovrSpeechSynthesis;
using TMPro;
using UnityEngine.Android;

public class TTSTest : MonoBehaviour {
    const string LANG_CODE = "en-US";

    private TextToSpeech tTS;
    private SpeechToText sTT;

    [Header(" ")]
    [SerializeField] private TMP_Text text;

    private void Start() {

        tTS = TextToSpeech.instance;
        sTT = SpeechToText.instance;

        tTS.Setting(LANG_CODE, 1, 1);
        sTT.Setting(LANG_CODE);

        sTT.onResultCallback = DoneRecording;
        sTT.onPartialResultsCallback = DoneRecording;

        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone)) {
            Permission.RequestUserPermission(Permission.Microphone);
        }

    }

    private void Update() {
        if (Microphone.IsRecording(Microphone.devices[0])) {
            sTT.StartRecording();
        }
    }

    private void DoneRecording(string recording) {
        text.text = recording;
    }

    public void Speek() {
        tTS.StopSpeak();
        tTS.StartSpeak(text.text);
    }
}
