using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Set In Inspector")]
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(.25f, 2);

    [Header("Set Dynamically")]
    public GameObject cube;
    public Vector3 rotPerSecond;
    private Rigidbody rigid;
    private BoundsCheck bndCheck;

    private void Awake()
    {
        cube = transform.Find("Cube").gameObject;

        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        Vector3 vel = Random.onUnitSphere;
        vel.z = 0;
        vel.Normalize();
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        rigid.velocity = vel;
        transform.rotation = Quaternion.identity;
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
                                    Random.Range(rotMinMax.x, rotMinMax.y),
                                    Random.Range(rotMinMax.x, rotMinMax.y));
    }
    private void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);
        if(!bndCheck.isOnScreen){
            Destroy(gameObject);
        }
    }


    public void AbsorbedBy(GameObject target){
        Destroy(gameObject);
    }
}
