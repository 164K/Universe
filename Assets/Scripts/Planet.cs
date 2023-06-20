using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public struct PlanetAttributes
{
    public PlanetAttributes(bool isNoMove, Vector2 velocity, Vector2 position, float radius, float mass, Color planetColor, Color planetTrailColor)
    {
        this.isNoMove = isNoMove;
        this.velocity = velocity;
        this.position = position;
        this.radius = radius;
        this.mass = mass;
        this.planetColor = planetColor;
        this.planetTrailColor = planetTrailColor;
    }

    bool isNoMove;
    Vector2 velocity;
    Vector2 position;
    float radius;
    float mass;
    Color planetColor;
    Color planetTrailColor;
}
public class Planet : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D cc;
    SpriteRenderer sr;

    private bool isNoMove = false;
    private Vector2 velocity;
    private Vector2 position;
    private float radius;
    private float mass;
    private Color planetColor;
    private Color planetTrailColor;

    public PlanetAttributes GetAttributes()
    {
        return new PlanetAttributes(isNoMove, velocity, position, radius, mass, planetColor, planetTrailColor);
    }

    private TrailRenderer trailRenderer;

    public float Mass
    {
        set { mass = value; if (rb.useAutoMass) cc.density = value; else rb.mass = value; }
        get { return (rb.useAutoMass) ? cc.density : rb.mass; }
    }
    public float Radius
    {
        set { radius = value; transform.localScale = (value * 2) * Vector3.one; }
        get { return transform.localScale.x; }
    }
    public Vector2 Position
    {
        set { position = value; rb.MovePosition(value); }
        get { return transform.position; }
    }
    public Vector2 Velocity
    {
        set { velocity = value; rb.velocity = value; }
        get { return rb.velocity; }
    }
    public Color PlanetColor
    {
        set { planetColor = value; sr.color = value;  }
        get { return sr.color; }
    }
    public Color TrailColor
    {
        set { trailRenderer.startColor = ColorDarker(value); trailRenderer.startColor = value; }
    }


    public bool IsKinematic
    {
        set { rb.isKinematic = value; }
        get { return rb.isKinematic; }
    }
    public bool IsNoMove
    {
        set { isNoMove = value; }
        get { return isNoMove; }
    }
    Color ColorDarker(Color color, float amout_rate = 0.3f)
    {
        return color * amout_rate;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
    }
    public void Initialized(float massOrDensity, float radius, Vector2 position = default, Vector2 velocity = default,Color color=default)
    {
        Velocity = velocity;
        Radius = radius;
        Position = position;
        Mass = massOrDensity;
        PlanetColor = color;
        TrailColor = color;
    }

    private void FixedUpdate()
    {
        if (IsNoMove)
        {
            IsKinematic = true;
            Velocity = Vector2.zero;
        }
    }
}
