using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField]
    private Color color;

    void Start()
    {
        ParticleSystem.MinMaxGradient gradient = new ParticleSystem.MinMaxGradient();
        gradient.mode = ParticleSystemGradientMode.Color;
        gradient.color = color;
        ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
        main.startColor = gradient;
    }
}