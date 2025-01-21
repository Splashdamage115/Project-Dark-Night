using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootWeapon : MonoBehaviour
{
    public int damageAmount = 1;
    public GameObject muzzleFlash;
    public Transform ShootPoint;

    Animator anim;

    public GameObject shellCasing;
    public Transform shellPoint;

    private AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void shoot()
    {
        anim.SetBool("Fire", true);
        StartCoroutine(resetShoot());
        Quaternion t = transform.rotation;
        t.eulerAngles += new Vector3(0.0f, 180.0f, 0.0f);
        Instantiate(muzzleFlash, ShootPoint.position, t);
        t.eulerAngles += new Vector3(90.0f, 0.0f, 0.0f);
        Instantiate(shellCasing, shellPoint.position, t);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

    IEnumerator resetShoot()
    {
        yield return new WaitForNextFrameUnit();
        anim.SetBool("Fire", false);
        yield return null;
    }
}
