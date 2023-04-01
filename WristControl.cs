using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristControl : MonoBehaviour {
    [SerializeField] private Transform wrist;
    [SerializeField] private Transform rightWrist;
    [SerializeField] private Transform leftWrist;

    private void Update() {
        transform.position = wrist.position;
        transform.rotation = wrist.rotation;
    }
}
