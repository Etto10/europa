using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [SerializeField] private int width, height;
    public bool checkPlate, checkSoil;

    public InventoryItemData itemData;

    public InventoryItemController itemController;

    [HideInInspector]
    public bool gridMode;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform container;

    [SerializeField] private int posFix = 19;


    private void Awake()
    {
        Instance = this;
        gridMode = false;
        itemData = null;
        itemController = null;
    }

    private void Start()
    {
        checkPlate = false;
        checkSoil = false;
        StartCoroutine(GenerateGrid());
        temp = new Vector3(width, height, posFix);
    }

    IEnumerator GenerateGrid()
    {
        yield return new WaitForSeconds(1f);
        for (int x = -posFix; x < width - posFix; x++)
        {
            for (int y = -posFix; y < height - posFix; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x * 5, y * 5), Quaternion.identity);
                spawnedTile.transform.SetParent(container, true);
                spawnedTile.transform.name = x.ToString() + " " + y.ToString();

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
        HideGrid();
    }

    public void ShowGrid()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.gameObject.SetActive(true);
        }
        gridMode = true;
        Chest.Instance.CloseChest();
    }

    public void HideGrid()
    {
        container.gameObject.SetActive(false);
        gridMode = false;
    }

    //We use this update method to update the width, height and posFix while the game is running
    Vector3 temp;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HideGrid();
        }

        if(temp != new Vector3(width, height, posFix))
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }

            StartCoroutine(GenerateGrid());
        }
    }

    
}

