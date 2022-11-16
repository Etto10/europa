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


    [HideInInspector]
    public bool gridMode;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform container;

    [SerializeField] private int posFix = 19;

    [SerializeField] private GameObject gridModeUI;
    [SerializeField] private GameObject useUI;

    private void Awake()
    {
        Instance = this;
        gridMode = false;
        itemData = null;
    }

    private void Start()
    {
        checkPlate = false;
        checkSoil = false;
        StartCoroutine(GenerateGrid());
        temp = new Vector3(width, height, posFix);
        gridModeUI.SetActive(false);
        useUI.SetActive(true);
    }

    IEnumerator GenerateGrid()
    {
        yield return new WaitForSeconds(.5f);
        for (int x = -posFix; x < width - posFix; x++)
        {
            for (int y = -posFix; y < height - posFix; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x * 5, y * 5), Quaternion.identity);
                Tile tile = spawnedTile.GetComponent<Tile>();
                tile.tileId = x.ToString() + " " + y.ToString();
                spawnedTile.transform.name = tile.tileId;
                spawnedTile.transform.SetParent(container, true);

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        DataManager.Instance.LoadData();
        container.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
    }


    public void ShowGrid()
    {
        for (int i = 0; i < container.childCount; i++)
        {
            container.gameObject.SetActive(true);
        }
        gridMode = true;
        gridModeUI.SetActive(true);
        useUI.SetActive(false);
    }

    public void HideGrid()
    {
        container.gameObject.SetActive(false);
        gridMode = false;
        gridModeUI.SetActive(false);
        useUI.SetActive(true);
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

