using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Set In Inpsector")]
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;
    [Header("Set Dynamically")]
    public Vector3 p0;
    public Vector3 p1;
    public float birthTime;
    private void Start()
    {
        p0 = Vector3.zero;
        p0.x = -boundsCheck.camWidth + boundsCheck.radius;
        p0.y = Random.Range(-boundsCheck.camHeight, boundsCheck.camHeight);

        p1 = Vector3.zero;
        p1.x = boundsCheck.camWidth - boundsCheck.radius;
        p1.y = Random.Range(-boundsCheck.camHeight, boundsCheck.camHeight);

        if (Random.value > 0.5f)
        {
            p0.x *= -1;
            p1.x *= -1;
        }
        birthTime = Time.time;
    }
    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;
        if (u > 1)
        {
            Destroy(gameObject);
            return;
        }
        u += sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        
        pos = (1 - u) * p0 + u * p1;
        Vector3 rot = new Vector3(0, (u - 0.5f) * -90, 0);
        transform.rotation = Quaternion.Euler(rot);
    }
}
