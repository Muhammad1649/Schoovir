using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Theme : MonoBehaviour
{
    // Get Theme Colors
    [Header("Theme Colors")]
    public Color primaryLightColor;
    public Color primaryDarkColor;
    public Color secondaryLightColor;
    public Color secondaryDarkColor;
    // Get The UI Group
    [Header("UI Group")]
    public GameObject UIGroup;
    // Get The UI Text ID
    [Header("UI Text ID")]
    public string primaryLight;
    public string primaryDark;
    public string secondaryLight;
    public string secondaryDark;
    // Private Variables
    private string curentTheme;
    private List<Image> UIImages;
    private List<TMP_Text> UIText;
    private void Start() {
        // Import Text & Images
        Image[] images = GetComponentsInChildren<Image>();
        TMP_Text[] text = GetComponentsInChildren<TMP_Text>();

        // Add Text & Images to List
        foreach(Image i in images) {
            UIImages.Add(i);
        }
        foreach(TMP_Text t in text) {
            UIText.Add(t);
        }
    }
    public void ChangeTheme(string theme) {
        if(theme != curentTheme) {
            // Cange To Light Theme
            if(theme == "Light") {
                // Change Dark Images To Light
                foreach(Image i in UIImages) {
                    if(i.name.Contains(primaryLight)) {
                        i.color = primaryLightColor;
                    } else if(i.name.Contains(primaryDark)) {
                        i.color = primaryDarkColor;
                    } else if(i.name.Contains(secondaryLight)) {
                        i.color = secondaryLightColor;
                    } else if(i.name.Contains(secondaryDark)) {
                        i.color = secondaryDarkColor;
                    }
                }
                // Change Dark Text To Light
                foreach(TMP_Text t in UIText) {
                    if(t.name.Contains(primaryLight)) {
                        t.color = primaryLightColor;
                    } else if(t.name.Contains(primaryDark)) {
                        t.color = primaryDarkColor;
                    } else if(t.name.Contains(secondaryLight)) {
                        t.color = secondaryLightColor;
                    } else if(t.name.Contains(secondaryDark)) {
                        t.color = secondaryDarkColor;
                    }
                }
            // Change To Dark Theme
            }else if(theme == "Dark") {
                // Change Light Images To Dark
                foreach(Image i in UIImages) {
                    if(i.name.Contains(primaryLight)) {
                        i.color = secondaryDarkColor;
                    } else if(i.name.Contains(primaryDark)) {
                        i.color = primaryLightColor;
                    } else if(i.name.Contains(secondaryLight)) {
                        i.color = secondaryLightColor;
                    } else if(i.name.Contains(secondaryDark)) {
                        i.color = secondaryDarkColor;
                    }
                }
                // Change Light Images To Dark
                foreach(TMP_Text t in UIText) {
                    if(t.name.Contains(primaryLight)) {
                        t.color = secondaryDarkColor;
                    } else if(t.name.Contains(primaryDark)) {
                        t.color = primaryLightColor;
                    } else if(t.name.Contains(secondaryLight)) {
                        t.color = secondaryLightColor;
                    } else if(t.name.Contains(secondaryDark)) {
                        t.color = secondaryDarkColor;
                    }
                }
            }
        }
    }
}
