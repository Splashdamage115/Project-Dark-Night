using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public GameObject bgTile;
    public Inventory playerInventory;

    [Header("Tile Spacing Variables")]
    private float bgTileSize = 30.0f;
    public float gap = 10.0f;
    public float left = 100.0f;
    public float top = 100.0f;
    public int maxWidth = 3;

    void Start()
    {
        bgTileSize = bgTile.GetComponent<RectTransform>().sizeDelta.x;

        playerInventory = this.GetComponentInParent<Inventory>();

        for (int i = 0;i < playerInventory.maxSize; i++)
        {
            var t = Instantiate(bgTile, new Vector3(left + ((i) % maxWidth) * (bgTileSize + gap), -((i / maxWidth) * (bgTileSize + gap) + top)), Quaternion.identity);
            t.transform.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
