using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour{
    private ContainerManager c;
    private Reaction r;
    private List<Chemical> reactants;
    private bool timer;
    private float t = 0f;
    private bool finishedReaction = false;
    public void Constructor(ContainerManager c, Reaction r, List<Chemical> reactants) {
        this.c = c;
        this.r = r;
        this.reactants = reactants;
        t = r.reactionRate;
    }
    private void Start() {
        c.ChangeLiquidColor(r.color);
        if (r.reactionEffect) {
            Transform t = null;
            switch (r.effectType) {
                case Reaction.EffectType.Top: t = c.GetInfo()._containerTop;
                    break;
                case Reaction.EffectType.Bottom: t = c.GetInfo()._containerBottom;
                    break;
                case Reaction.EffectType.Liquid_Surface: t = c.GetInfo()._liquidSurface;
                    break;
                default: t = this.transform;
                    break;
            }
            GameObject e = Instantiate(r.effect, t);
            e.GetComponent<Effect>().duration = r.effectDuration;
        }
    }
    private void Update() {
        // if(ConditionsForReaction() == false){
        //     DestroyReaction();
        // } else {
        //     if(t > 0) {
        //         t -= Time.deltaTime;
        //     } else {
        //         finishedReaction = true;
        //     }
        // }
        // if(finishedReaction) {
        //     DestroyReaction();
        // }

        // --- Reaction ---
        // foreach(Chemical r in reactants) {
        //     if(r.concentration <= 0) {
        //         c.RemoveChemical(r);
        //     } else {
        //         r.concentration -= 1f;
        //     }
        // }
        // c.AddChemicals(r.product);
        // c.ChangeLiquidColor(r.color);
        Debug.Log("Ongoing Reaction: " + r.name);

        // --- Reaction ---
    }
    private bool ConditionsForReaction() {
        int i = 1;
        if(c.GetInfo()._temperature >= r.activationEnergy) {
            i--;
        }

        if(i > 0) {
            return false;
        } else {
            return true;
        }
    }
    private void DestroyReaction() {
        // c.RemoveReaction(r);
        // Destroy(this);
    }
}
