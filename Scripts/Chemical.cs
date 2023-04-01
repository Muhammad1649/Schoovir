using UnityEngine;
[System.Serializable]
public class Chemical {
    public string name;
    public string formula;
    public Color solidColor = Color.white;
    public Color liquidColor = Color.white;
    public Color gaseousColour = Color.white;
    [Range(0, 1)]
    public float transparency;
    public enum State{Solid, Liquid, Gas, Aqeous};
    public State state;
    public float boilingPoint;
    public float meltingPoint;
    [Range(0, 14)]
    public float pH;
    public enum Type{Chemical, Indicator, Catalyst};
    public Type type;
    public GameObject substance;
    public float yield;
    public float concentration;
    public float percentage;
}
