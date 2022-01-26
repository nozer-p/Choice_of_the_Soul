using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && switchWeapon.Instance.weaponSwitch == 1)
            {
                shakeCamera.Instance.ShakeRecoil(true);
                Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                timeBtwShots = startTimeBtwShots;
                movePlayer.Instance.Recoil();
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        shakeCamera.Instance.ShakeRecoil(false);
    }
}
