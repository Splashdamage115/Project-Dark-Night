using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public GameObject highlightBox;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subText;
    public GameObject bgTile;
    public Inventory playerInventory;
    private List<GameObject> inventoryTiles;
    private int lastInsertNum = 0;


    [Header("Tile Spacing Variables")]
    private float bgTileSizeX = 30.0f;
    private float bgTileSizeY = 30.0f;
    public float gap = 10.0f;
    public float left = 100.0f;
    public float top = 100.0f;
    public int maxWidth = 3;

    void Start()
    {
        inventoryTiles = new List<GameObject>();
        bgTileSizeX = bgTile.GetComponent<RectTransform>().sizeDelta.x;
        bgTileSizeY = bgTile.GetComponent<RectTransform>().sizeDelta.y;

        playerInventory = this.GetComponentInParent<Inventory>();

        for (int i = 0; i < playerInventory.maxSize; i++)
        {
            var t = Instantiate(bgTile, new Vector3(left + ((i) % maxWidth) * (bgTileSizeX + gap), -((i / maxWidth) * (bgTileSizeY + gap) + top)), Quaternion.identity);
            t.transform.SetParent(transform, false);
            inventoryTiles.Add(t);
            t.SetActive(false);
            t.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    public void setTexts(int itemNum)
    {
        highlightBox.transform.position = inventoryTiles[itemNum].transform.position;
        titleText.text = playerInventory.Items[itemNum].DisplayName;
        subText.text = playerInventory.Items[itemNum].description;
    }

    public void addItem()
    {
        var t = inventoryTiles[lastInsertNum];
        t.SetActive(true);
        t.GetComponentInChildren<TextMeshProUGUI>().text = playerInventory.Items[lastInsertNum].DisplayName;
        t.GetComponent<ClickedInventoryButton>().selfNum = lastInsertNum;
        lastInsertNum++;
    }
}
