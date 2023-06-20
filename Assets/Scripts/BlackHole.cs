using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    Planet planet;
    CircleCollider2D cc;
    // Start is called before the first frame update
    void Awake()
    {
        planet = GetComponent<Planet>();
        cc = GetComponent<CircleCollider2D>();
        cc.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
