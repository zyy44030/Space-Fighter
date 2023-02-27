using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;
    public Weapon[] weapons;
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public GameObject lastTriggerGo = null;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;


    [SerializeField]
    private float _shieldLevel = 1;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Another Hero.S exists!");
        }
        ClearWeapons();
        weapons[0].activateWeapon();
        weapons[0].activated = true;
    }
    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        if (go == lastTriggerGo)
        {
            return;
        }
        lastTriggerGo = go;
        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered:" + go.name);
        }
    }
    public float shieldLevel
    {
        get
        {
            return _shieldLevel;
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                Destroy(gameObject);
                Main.S.DelayedRestart(Main.S.gameRestartDelay);
            }
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp up = go.GetComponent<PowerUp>();
        Weapon w = GetEmptyWeaponSlot();
        if(w != null){
            w.activateWeapon();
            w.activated = true;
        }else{
            Main.S.scoreBoard += 3;
        }
        up.AbsorbedBy(gameObject);
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (!weapons[i].activated)
            {
                return weapons[i];
            }
        }
        return null;
    }

    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.deActivateWeapon();
            w.activated = false;
        }
    }
}

