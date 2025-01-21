using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public float destroyWait = 0.0f;
    private Material[] materials;
    public float clipIncrease = 1.0f;
    public float clipAmt = 0.0f;
    public GameObject materialHolder;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            materials = materialHolder.GetComponent<MeshRenderer>().materials;

            if (materials.Length <= 0)
            {
                materials = materialHolder.GetComponent<SkinnedMeshRenderer>().materials;
            }
        }
        catch
        {
 
        }
        StartCoroutine(deleteObject());

    }
    IEnumerator deleteObject()
    {
        yield return new WaitForSeconds(destroyWait);
        while (clipAmt < 1.0f)
        {
            clipAmt += clipIncrease * Time.deltaTime;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i]?.SetFloat("_AlphaClip", clipAmt);

            }
            yield return new WaitForNextFrameUnit();
        }
        Destroy(gameObject);
        yield return null;
    }
}
