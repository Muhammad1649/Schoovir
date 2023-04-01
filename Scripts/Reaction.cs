using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Reaction {
    [Header("Reaction")]
    [TextArea]
    public string name;
    public List<Chemical> product;
    public Color color = Color.white;
    [Header("Reaction Conditions")]
    public float reactionRate;
    public float activationEnergy;
    [Header("Reaction Effect")]
    public bool reactionEffect;
    public GameObject effect;
    public enum EffectType{Top, Bottom, Liquid_Surface};
    public EffectType effectType;
    public float effectDuration;
}
