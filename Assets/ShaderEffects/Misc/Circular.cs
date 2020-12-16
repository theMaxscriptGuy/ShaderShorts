using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circular : MonoBehaviour
{
    public float speed;
    public float radius;
    public Vector2 offset;
    private float m_angle = 0;
    // Update is called once per frame
    void Update()
    {
        m_angle += Time.deltaTime * speed;
        var x = offset.x + (radius * Mathf.Sin(m_angle * Mathf.Deg2Rad));
        var z = offset.y + (radius * Mathf.Cos(m_angle * Mathf.Deg2Rad));
        Vector3 pos = new Vector3(x, 0.5f, z);
        transform.position = pos;
    }
}
