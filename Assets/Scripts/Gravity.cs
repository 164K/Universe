using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float G = 1.0f;
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
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        List<MyType> children = new();
        List<GameObject> childrenGameobject = GameObject.FindGameObjectsWithTag("Planet").ToList();
        childrenGameobject.ForEach((GameObject g) => children.Add(new MyType(g.transform)));

        foreach (var now_t in children)
        {
            var totalForce = new Vector3();
            foreach (var another_t in children)
            {
                if (now_t.t != another_t.t)
                {
                    var r = another_t.pos - now_t.pos;
                    var r_scale = r.magnitude;
                    totalForce += G * now_t.rb.mass * another_t.rb.mass / (r_scale * r_scale) * r.normalized;
                }
            }
            now_t.rb.AddForce(totalForce);
        }
    }

}
