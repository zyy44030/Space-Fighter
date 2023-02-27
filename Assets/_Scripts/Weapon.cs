using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public GameObject collar;
    public float lastShot;
    public GameObject projectilePrefab;
    public bool activated = false;
    private void Start()
    {
        collar = transform.Find("Collar").gameObject;

        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<Hero>() != null)
        {
            rootGO.GetComponent<Hero>().fireDelegate += Fire;
        }
    }

    public void activateWeapon()
    {
        gameObject.SetActive(true);
        return;
    }

    public void deActivateWeapon()
    {
        gameObject.SetActive(false);
        return;
    }

    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        if (Time.time - lastShot < 0.2f) return;
        Projectile P;
        P = MakeProjectile();
        P.rigid.velocity = Vector3.up * 50f;
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(projectilePrefab);
        go.transform.position = collar.transform.position;
        Projectile P = go.GetComponent<Projectile>();
        lastShot = Time.time;
        return P;
    }
}
