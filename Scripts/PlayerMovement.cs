using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
public class PlayerMovement : MonoBehaviour {
    public XRNode inputSource;
    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController controller;

    public float additionalHeight = 0.2f;
    public float speed = 1;

    private float yPosition;
    public static PlayerMovement instance;
    private void Awake() {
        if (instance == null) { instance = this; } else { Destroy(gameObject); return; }
        transform.position = CONSTANT.playerPosition;
    }
    void Start() {
        controller = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }
    void Update() {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        CONSTANT.playerPosition = transform.position;
    }
    void FixedUpdate() {
        CapsuleFollowHeadset();
        Quaternion headYaw = Quaternion.Euler(0f, rig.Camera.transform.eulerAngles.y, 0f);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0f, inputAxis.y);
        controller.Move(direction * Time.fixedDeltaTime * speed);
    }
    void CapsuleFollowHeadset() {
        controller.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        controller.center = new Vector3(capsuleCenter.x, controller.height/2 + controller.skinWidth, capsuleCenter.z);
    }
}
