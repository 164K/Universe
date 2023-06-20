using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 oriPosition;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdatePosDirect(oriPosition, direction);
    }
    public void UpdatePosDirect(Vector2 pos, Vector2 direct)
    {
        RectTransform rtf = GetComponent<RectTransform>();
        rtf.position = new Vector3(pos.x, pos.y, rtf.position.z);
        rtf.eulerAngles = new Vector3(rtf.rotation.x, rtf.rotation.y, math.degrees(math.atan2(direct.y, direct.x)));
    }
}