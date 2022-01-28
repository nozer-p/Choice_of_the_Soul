using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchWeapon : MonoBehaviour
{ 
    public static switchWeapon Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public int weaponSwitch = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int currentWeapon = weaponSwitch;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (weaponSwitch <= 0)
            {
                weaponSwitch = transform.childCount - 1;
            }
            else
            {
                weaponSwitch--;
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (weaponSwitch >= transform.childCount - 1)
            {
                weaponSwitch = 0;
            }
            else
            {
                weaponSwitch++;
            }
        }

        if (currentWeapon != weaponSwitch)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == weaponSwitch)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
    /* public GameObject[] weapons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].activeInHierarchy)
                {
                    weapons[i].SetActive(false);

                    if (i != 0)
                    {
                        weapons[i - 1].SetActive(true);
                    }
                    else
                    {
                        weapons[weapons.Length - 1].SetActive(true);
                    }
                    break;
                }
            }
        }
    }*/
}
