using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRayShoot : MonoBehaviour
{
    public GameObject hitMarker;
    public float hitmarkerActiveTime = 0.2f;
    private int hitmarkerActiveAmount = 0;

    private void Start()
    {
        hitMarker.SetActive(false);
    }

    void hitScan(int damageAmt)
    {
        GetComponent<PlayerFlinch>().flinch(PlayerFlinch.FlinchAmount.Small);
        Camera camera = Camera.main;
        Ray camRay = camera.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f));
        RaycastHit hitInfo;

        if (Physics.Raycast(camRay, out hitInfo))
        {
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                hitInfo.collider.gameObject.SendMessage("TakeDamage", damageAmt);
                StartCoroutine(deactivateHitMarker());
                hitMarker.SetActive(true);
                hitmarkerActiveAmount++;
            }
        }
    }
    IEnumerator deactivateHitMarker()
    {
        yield return new WaitForSeconds(hitmarkerActiveTime);
        hitmarkerActiveAmount--;
        if (hitmarkerActiveAmount <= 0)
            hitMarker.SetActive(false);
        yield return null;
    }
}
