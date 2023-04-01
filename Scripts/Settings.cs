using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class Settings : MonoBehaviour
{
    // Locomotion Variables
    public XRBaseController leftController;
    public XRBaseController rightController;

    private PlayerMovement playerMovement;
    private CharacterController characterController;
    private DeviceBasedSnapTurnProvider snapTurnProvider;

    // UI Interaction Variables
    public XRController uIInteractor;
    void Start() {
        PlayerPrefs.SetString("Hand", "Right");

        playerMovement = GetComponent<PlayerMovement>();
        characterController = GetComponent<CharacterController>();
        snapTurnProvider = GetComponent<DeviceBasedSnapTurnProvider>();
        UpdateSettings();
    }
    void UpdateSettings() {
        SetActiveController();
    }
    void SetActiveController() {
        if (PlayerPrefs.GetString("Hand") == "Right") {
            uIInteractor.controllerNode = XRNode.RightHand;
            playerMovement.inputSource = XRNode.RightHand;
            snapTurnProvider.controllers = new List<XRBaseController>{leftController};
        } else if (PlayerPrefs.GetString("Hand") == "Left") {
            uIInteractor.controllerNode = XRNode.LeftHand;
            playerMovement.inputSource = XRNode.LeftHand;
            snapTurnProvider.controllers = new List<XRBaseController>{rightController};
        }
    }
    public void ShowUIInteractor(bool show) {
        uIInteractor.gameObject.SetActive(show);
    }
}
