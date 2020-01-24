using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [HideInInspector] public float START_SCREEN_Y = 5.0f;
    [HideInInspector] public float START_SCREEN_X = -2.25f;
    [HideInInspector] public float DOWN_SCREEN_Y = -4.5f;
    [HideInInspector] public float LENGTH = 0.5f;

    [HideInInspector] public int GRID_WIDTH = 10;
    [HideInInspector] public int GRID_HEIGHT = 20;

    public BlockGenerator theGenerate;
    public BlockController[,] allGrid;

    void Start()
    {
        theGenerate = FindObjectOfType<BlockGenerator>();
        allGrid = new BlockController[GRID_WIDTH, GRID_HEIGHT];

    }

    void Update()
    {

    }

    public void UpdateGrid(int x, int y, BlockController theBlock)
    {
        allGrid[x, y] = theBlock;
    }

    public void ClearGird(int x, int y)
    {
        allGrid[x, y] = null;
    }

    public bool IsGirdEmpty(int x, int y)
    {
        if (allGrid[x, y] == null)
            return true;
        return false;
    }

    public void CheckArrange()
    {
        for (int y = 0; y < GRID_HEIGHT; y++)
        {
            int nullCounter = 0;
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                if (allGrid[x, y] == null)
                    nullCounter++;
            }
            if (nullCounter <= 0)
            {
                for (int x = 0; x < GRID_WIDTH; x++)
                    Destroy(allGrid[x, y].gameObject);

                moveDonwAbove(y + 1);
            }
        }
    }

    void moveDonwAbove(int above)
    {
        for (int y = above; y < GRID_HEIGHT; y++)
            for (int x = 0; x < GRID_WIDTH; x++)
            {
                if (allGrid[x, y] != null)
                {
                    allGrid[x, y].y--;
                    allGrid[x, y - 1] = allGrid[x, y];
                    allGrid[x, y - 1].transform.position = new Vector3(START_SCREEN_X + x * LENGTH, START_SCREEN_Y + (y - 1) * LENGTH, -2);
                    allGrid[x, y] = null;
                }
            }
    }
}
