using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercontinentalDemo : MonoBehaviour {
    private GameManager gameManager;
    [SerializeField] private List<Reaction> reactions;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        // gameManager.reactions.AddRange(this.reactions);
    }
}
