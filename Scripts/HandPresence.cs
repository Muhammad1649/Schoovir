using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    // References
    public XRNode controller;
    private Animator handAnimator;
    private InputDevice device;
    void Start() {
        // Init
        device = InputDevices.GetDeviceAtXRNode(controller);
        handAnimator = GetComponent<Animator>();
    }
    void Update() {
        UpdateHandAnimation();
    }
    void UpdateHandAnimation() {
        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue)) {
            handAnimator.SetFloat("Grip", gripValue);
        } else { handAnimator.SetFloat("Grip", 0); }

        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) {
            handAnimator.SetFloat("Trigger", triggerValue);
        } else { handAnimator.SetFloat("Trigger", 0); }
    }
}
