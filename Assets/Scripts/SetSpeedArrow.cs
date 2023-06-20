using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class SetSpeedArrow : MonoBehaviour
{
    public GameObject arrow;
    // Start is called before the first frame update
    private GameObject newArrow;
    void Start()
    {
        newArrow = Instantiate(arrow, transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateArrowPosDirect(newArrow, transform.position,GetComponent<Rigidbody2D>().velocity);
    }
    public void UpdateArrowPosDirect(GameObject arrow, Vector2 pos, Vector2 direct)
    {
        RectTransform rtf = arrow.GetComponent<RectTransform>();
        // 调整位置
        rtf.position = new Vector3(pos.x, pos.y, rtf.position.z);
        // 调整角度
        rtf.eulerAngles = new Vector3(rtf.rotation.x, rtf.rotation.y, Vector2.SignedAngle(Vector2.right, direct));
        // 调整大小
        rtf.localScale = Vector3.one * math.length(direct);
    }
}
