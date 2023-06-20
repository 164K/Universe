using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public struct MyType
    {
        public MyType(Transform t)
        {
            this.t = t;
            rb = t.GetComponent<Rigidbody2D>();
            pos = t.position;
        }
        public Transform t;
        public Rigidbody2D rb;
        public Vector3 pos;
    }
public class Movement : MonoBehaviour
{
    public float G = 1.0f;
    public float MinDistanceInGravity = 1e-5f;
    private List<MyType> children = new();
    // Start is called before the first frame update
    void Start()
    {
        UpdateChildren();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateChildren();
        UpdateGravity();

    }
    void UpdateGravity()
    {
        UpdateChildren();

        foreach (var now_t in children)
        {
            var totalForce = new Vector3();
            foreach (var another_t in children)
            {
                if (now_t.t != another_t.t)
                {
                    var r = another_t.pos - now_t.pos;
                    var r_scale = r.magnitude;
                    if (r_scale < MinDistanceInGravity) r_scale = MinDistanceInGravity;
                    totalForce += G * now_t.rb.mass * another_t.rb.mass / (r_scale * r_scale) * r.normalized;
                }
            }
            now_t.rb.AddForce(totalForce);
        }
    }
    void UpdateChildren()
    {
        children = new();
        GameObject.FindGameObjectsWithTag("Planet").ToList().ForEach((GameObject g) => children.Add(new MyType(g.transform)));
    }
    public void ResumeRigid()
    {
        UpdateChildren();
        children.ForEach((MyType mt) =>
        {
            mt.t.GetComponent<Pauser>().Resume();
        });
    }
    public void PauseRigid()
    {
        UpdateChildren();
        children.ForEach((MyType mt) =>
        {
            mt.t.GetComponent<Pauser>().Pause();
        });
    }
}
