using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ContainerManager : MonoBehaviour, IContainable {
    // Scipt Reference
    private GameManager gameManager;
    private new MeshRenderer renderer;
    private Container container;

    [Header ("Chemicals")]
    [TextArea]
    [SerializeField] private string chemicalName;
    [TextArea]
    [SerializeField] private string formula;
    [SerializeField] private List<GameObject> physicalSubstances;
    [Header ("Chemical Properties")]
    [SerializeField] private List<Chemical> chemicals;
    [SerializeField] private List<Reaction> reactions;
    [SerializeField] private Color liquidColor = Color.white;
    [SerializeField] private Color solidColor = Color.white;
    [SerializeField] private Color gasiousColor = Color.white;
    [Range(0, 1)]
    [SerializeField] private float liquidTransparency = 0.5f;
    [SerializeField] private float precipitation = 0f;
    [SerializeField] private float coagulation = 0f;

    [Header ("Physical Properties")]
    [SerializeField] private float full = 0.5f;
    [SerializeField] private float empty = -0.5f;
    [SerializeField] private float liquidFill = 0;
    private bool isEmpty;
    private bool isFull;
    [SerializeField] private float containerVolume;
    [SerializeField] private float containerWeight;
    [SerializeField]private float temperature;
    [SerializeField]private float preasure;

    private bool changeTemp;
    private float tempChangeSpeed;
    private float newTemp;

    // Color Change
    private bool changeColor;
    private float colorChangeSpeed;
    private float colorChangeAnimation;
    private Color prevColor;
    private Color newColor;

    [Header ("Container")]
    [SerializeField] private Transform containerTop;
    [SerializeField] private Transform containerBottom;
    [SerializeField] private Transform liquidSurface;
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        renderer = GetComponent<MeshRenderer>();
        container = GetComponent<Container>();
        
        liquidFill = empty;
    }
    private void Update() {
        // Set Material Parameters
        renderer.materials[1].SetColor("_LiquidColor", liquidColor);
        renderer.materials[1].SetFloat("_LiquidFill", liquidFill);
        renderer.materials[1].SetFloat("_LiquidTransparency", liquidTransparency);
        // Liquid Empty & Full Check
        if(liquidFill <= empty) {
            isEmpty = true;
            liquidFill = empty;
            container.EndPour(transform.name);
            EmptyContainer();
        } else if(liquidFill >= full) {
            isFull = true;
            liquidFill = full;
        } else {
            isFull = false;
            isEmpty = false;
        }

        // Change Liquid Color
        if(changeColor) {
            if(colorChangeAnimation < 1) {
                colorChangeAnimation += Time.deltaTime/colorChangeSpeed;
                liquidColor = Color.Lerp(prevColor, newColor, colorChangeAnimation);
            }else if(colorChangeAnimation >= 1) {
                colorChangeAnimation = 0;
                changeColor = false;
            }
        }

        

        // // Change Temperature
        // if(changeTemp) {
        //     tempChangeSpeed += (liquidFill/full)*100;
        //     temperature = Mathf.Lerp(temperature, newTemp, tempChangeSpeed*Time.deltaTime);
        //     if(temperature == newTemp) {
        //         changeTemp = false;
        //     }
        // }

        // // Change Chemical State
        // //-- Solidify --
        // var x = Array.FindAll(chemicals.ToArray(), c => c.boilingPoint >= temperature);
        // if(x != null) {
        //     Solidify(x.ToList(), 1);
        //     x = null;
        // }
        // //-- Melt --
        // var y = Array.FindAll(chemicals.ToArray(), c => c.meltingPoint <= temperature);
        // if(y != null) {
        //     Melt(y.ToList(), 1);
        //     y = null;
        // }
        // //-- Evaporate --
        // var z = Array.FindAll(chemicals.ToArray(), c => c.boilingPoint <= temperature);
        // if(z != null) {
        //     Evaporate(z.ToList(), 1);
        //     z = null;
        // }
    }
    private float CalculatePercentage() {
        float t = full - empty;
        float p = ((liquidFill - empty) / t) * 100;
        float check = ((p * t) / 100) + empty;

        return Mathf.Clamp(p, 0, 100);
        /* if (Mathf.Approximately(liquidFill, check)) { return Mathf.Clamp(p, 0, 100); } else { return -1f; } */
    }

    private float GetLiquidFill(float percent) {
        float t = full - empty;
        float p = ((percent * t) / 100f) - empty;

        return p;
    }
    private float CalculateVolume() {
        float currentVolume = containerVolume * (CalculatePercentage() / 100);
        return currentVolume;
    }
    private float CalculateWeight() {
        return 500f;
    }
    private (string name, string formula) Mixture() {
        chemicalName = null;
        formula = null;
        foreach (Chemical c in chemicals) {
            chemicalName += c.name + " ";
            formula += c.formula + " ";
        }
        return (chemicalName, formula);
    }

    // Container Functions
    public void FillContainer(float fillRate) {
        float fillPercentage = (fillRate / containerVolume) * 100f;
        liquidFill += GetLiquidFill(fillPercentage) * Time.deltaTime;

        string trigger = transform.name.Replace(" ", "_").ToUpper() + "_RECEIVING";
        SessionManager.instance.TriggerValue(trigger, (int)GetInfo()._percentage);
    }
    public void EmptyContainer(float emptyRate) {
        float fillPercentage = (emptyRate / containerVolume) * 100f;
        liquidFill -= GetLiquidFill(fillPercentage) * Time.deltaTime;
    }
    private void EmptyContainer(){
        chemicals.Clear();
        reactions.Clear();
    }
    public void ChangeLiquidColor(Color colorToChange, float speed = 10f) {
        colorChangeAnimation = 0;
        newColor = colorToChange;
        prevColor = liquidColor;
        colorChangeSpeed = speed;
        changeColor = true;
    }
    public void ChangeTempreture(float t) {
        newTemp = t;
        changeTemp = true;
    }
    private void Evaporate(List<Chemical> z, float rate) {
        foreach(Chemical c in z) {
            if(c.concentration > 0) {
                float r = rate/20;
                c.concentration -= r;
                EmptyContainer(r);
            } else {
                RemoveChemical(c);
            }
        }
    }
    private void Melt(List<Chemical> z, float rate) {
        
    }
    private void Solidify(List<Chemical> z, float rate) {

    }
    private float GetChemicalConcentration(Chemical c = null) {
        float total = 0f;
        foreach (Chemical chem in chemicals) {
            total += chem.concentration;
        }
        foreach (Chemical chem in chemicals) {
            chem.percentage = (chem.concentration / total) * 100;
        }

        if (c != null) {
            return c.percentage;
        }

        return 0f;
    }
    public void AddChemicals(List<Chemical> chemicalToAdd) {
        if (chemicals.Count() == 0) {
            liquidColor = chemicalToAdd[0].liquidColor;
        }
        foreach(Chemical chem in chemicalToAdd) {
            Chemical i = Array.Find(chemicals.ToArray(), c => c.name == chem.name);
            if(i == null) {
                chemicals.Add(new Chemical{
                    name = chem.name,
                    formula = chem.formula,
                    solidColor = chem.solidColor,
                    liquidColor = chem.liquidColor,
                    gaseousColour = chem.gaseousColour,
                    transparency = chem.transparency,
                    state = chem.state,
                    boilingPoint = chem.boilingPoint,
                    meltingPoint = chem.meltingPoint,
                    pH = chem.pH,
                    type = chem.type,
                    yield = chem.yield,
                    concentration = 0,
                    percentage = 0
                });

                if (chem.substance != null) {
                    AddSubstance(chem.substance);
                }

                ReactChemicals();
            } else {
                int c = chemicals.IndexOf(i);
                chemicals[c].concentration ++;

                if (chem.substance != null) {
                    AddSubstance(chem.substance);
                }
            }
        }

        GetChemicalConcentration();
    }
    public void AddChemical(Chemical chemicalToAdd) {
        Chemical i = Array.Find(chemicals.ToArray(), c => c.name == chemicalToAdd.name);
        if(i == null) {
            chemicals.Add(chemicalToAdd);
            ReactChemicals();
        } else {
            int c = chemicals.IndexOf(i);
            chemicals[c].concentration ++;
        }
    }
    public void AddSubstance(GameObject s) {
        if (!physicalSubstances.Contains(s)) {
            physicalSubstances.Add(s);
        }
    }
    public void RemoveChemical(Chemical chem) {
        Chemical i = Array.Find(chemicals.ToArray(), c => c.name == chem.name);
        chemicals.Remove(i);
    }
    public void RemoveReaction(Reaction react) {
        Reaction i = Array.Find(reactions.ToArray(), r => r.name == react.name);
        reactions.Remove(i);
    }
    private void ReactChemicals() {
        List<Reaction> reactionList = SessionManager.instance.reactions;
        foreach(Reaction reaction in reactionList) {
            string x = reaction.name;
            x = string.Concat(x.Where(c => !char.IsWhiteSpace(c)));
            Mixture();
            for(int i = 0; i < chemicals.Count; i++) {
                for(int j = 0; j < chemicals.Count; j++) {
                    x = x.Replace(chemicals[i].formula, null);
                    x = x.Replace(chemicals[j].formula, null);
                    var r = Array.Find(reactions.ToArray(), react => react.name == reaction.name);
                    if(x == "" && r == null) {
                        reactions.Add(reaction);
                        GameObject reactorInstance = Instantiate(new GameObject("reactorInstance"), liquidSurface);
                        reactorInstance.SetActive(false);
                        reactorInstance.gameObject.AddComponent(typeof(Reactor)).GetComponent<Reactor>().Constructor(
                            this, 
                            reaction, 
                            new List<Chemical>(){chemicals[j], chemicals[i]});
                        reactorInstance.transform.SetParent(this.transform); 
                        reactorInstance.SetActive(true);
                    } else {
                        Debug.LogWarning("No Reaction");
                    }
                }
            }
        }
    }
    private float GetPH() {
        float ph = 0f;
        float total = 0f;
        foreach (Chemical chem in chemicals) {
            ph += chem.pH;
            total += 14;
        }

        float currentPh = (ph / total) * 14f;
        
        return currentPh;
    }
    private void GetCatalystAction() {
        // ...
    }
    public (string _name, string _formula, List<Chemical> _chemicals, Color _liquidColor, float _liquidFill, float _percentage, float _volume, float _weight, bool _isEmpty, float _temperature, Transform _containerTop, Transform _containerBottom, Transform _liquidSurface) GetInfo() {
        return (Mixture().name, Mixture().formula, chemicals, liquidColor, liquidFill, CalculatePercentage(), CalculateVolume(), CalculateWeight(), isEmpty, temperature, containerTop, containerBottom, liquidSurface);
    }

    // Interface Implementation
    public virtual void SetTemperature(float temp) {}
    public virtual void SetPreasure(float preasure) {}
}