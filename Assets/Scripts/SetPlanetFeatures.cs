using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SetPlanetFeatures : MonoBehaviour
{

    public float mass = 1.0f;
    public float radius = 1.0f;
    public float density = 1.0f;
    public float2 velocity = new(0,0);
    public bool setPosition = false;
    public float2 position;
    public bool noMove = false;
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        Planet planet = GetComponent<Planet>();

        if (!setPosition) position = planet.Position;
        if (rb.useAutoMass) planet.Initialized(density, radius, position, velocity, planet.PlanetColor);
        else planet.Initialized(mass, radius, position, velocity, planet.PlanetColor);
        if (noMove) planet.IsNoMove = true;
    }
}
