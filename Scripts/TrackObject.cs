using UnityEngine;

public class TrackObject : MonoBehaviour
{
    [SerializeField] private Transform objectToTrack;
    [Header("Parameters")]
    [SerializeField] private bool trackLocation;
    [SerializeField] private bool trackRotation;
    [SerializeField] private bool trackScale;
    [SerializeField] private bool setAsChild;
    [Header(" ")]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    [SerializeField] private Vector3 scaleOffset;

    private void Update() {
        if (setAsChild) {
            transform.position = positionOffset;
            transform.rotation = Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
            if (scaleOffset != Vector3.zero) {transform.localScale = scaleOffset;}
            transform.SetParent(objectToTrack);
        } else {
            if (trackLocation) {transform.position = objectToTrack.position + positionOffset;}
            if (trackRotation) {transform.rotation = Quaternion.Euler(objectToTrack.rotation.x + rotationOffset.x, objectToTrack.rotation.y + rotationOffset.y, objectToTrack.rotation.z + rotationOffset.z);}
            if (scaleOffset != Vector3.zero) {
                if (trackScale) {transform.localScale = objectToTrack.localScale + scaleOffset;}
            }
        }
    }
}
