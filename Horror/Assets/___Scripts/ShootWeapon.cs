using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public int damageAmount = 1;
    public GameObject muzzleFlash;
    public Transform ShootPoint;
    public void shoot()
    {
        Quaternion t = transform.rotation;
        t.eulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
        Instantiate(muzzleFlash, ShootPoint.position, t);
    }
}
