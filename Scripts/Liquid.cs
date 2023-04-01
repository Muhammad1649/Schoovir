using System.Collections.Generic;
using UnityEngine;

public class Liquid : MonoBehaviour {

    [SerializeField] private float fill;
    [SerializeField] private float empty;
    [SerializeField] private float maxScale;
    private float minScale = 0.01f;

    private IContainable container;

    private float xPosition;
    private float yPosition;

    private float xScale;
    private float yScale;

    void Start() {
        container = GetComponentInParent<IContainable>();

        xPosition = transform.localPosition.x;
        yPosition = transform.localPosition.y;

        xScale = transform.localScale.x;
        yScale = transform.localScale.y;
    }
    void Update() {
        transform.localScale = new Vector3(xScale, yScale,  minScale + ((container.GetInfo()._percentage * ((maxScale - minScale) / 1) ) / 100) );
        transform.localPosition = new Vector3(xPosition, yPosition,  empty + ((container.GetInfo()._percentage * ((fill - empty) / 2) ) / 100) );
    }
    void FixedUpdate() {
        transform.localScale = new Vector3(xScale, yScale,  minScale + ((container.GetInfo()._percentage * ((maxScale - minScale) / 1) ) / 100) );
        transform.localPosition = new Vector3(xPosition, yPosition,  empty + ((container.GetInfo()._percentage * ((fill - empty) / 2) ) / 100) );
    }
    public void Add(List<Chemical> c) {
        if(c.Count > 0) {
            container.AddChemicals(c);
            container.FillContainer(1f);
        }
    }
}
