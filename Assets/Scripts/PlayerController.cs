using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<BlockController> currentBlock;
    private LevelController theLevel;
    public float cooldownTime = 1.0f;
    public float cooldownCounter;
    void Start()
    {
        theLevel = FindObjectOfType<LevelController>();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            if (Input.GetButtonDown("Left") && GetMinX() - 1 >= 0)
            {
                Move("Left");
            }
            if (Input.GetButtonDown("Right") && GetMaxX() + 1 < theLevel.GRID_WIDTH)
            {
                Move("Right");
            }
            if (Input.GetButtonDown("Down"))
            {
                Move("Down");
            }
        }

        if (cooldownCounter > 0)
            cooldownCounter -= Time.fixedDeltaTime;
        else
        {
            MoveBlockDown();
            cooldownCounter = cooldownTime;
        }
    }

    void Move(string direction)
    {
        for (int i = 0; i < currentBlock.Count; i++)
            theLevel.ClearGird(currentBlock[i].x, currentBlock[i].y);

        switch (direction)
        {
            case "Left":
                for (int i = 0; i < currentBlock.Count; i++)
                    currentBlock[i].x--;
                break;
            case "Right":
                for (int i = 0; i < currentBlock.Count; i++)
                    currentBlock[i].x++;
                break;
            case "Down":
                while (!MoveBlockDown())
                { }
                break;
        }

        if (!direction.Equals("Down"))
            for (int i = 0; i < currentBlock.Count; i++)
            {
                theLevel.UpdateGrid(currentBlock[i].x, currentBlock[i].y, currentBlock[i]);
                currentBlock[i].transform.position = new Vector3(theLevel.START_SCREEN_X + currentBlock[i].x * theLevel.LENGTH, theLevel.START_SCREEN_Y + currentBlock[i].y * theLevel.LENGTH, -2);
            }
    }

    bool MoveBlockDown()
    {
        bool isGround = false;

        for (int i = 0; i < currentBlock.Count; i++)
            theLevel.ClearGird(currentBlock[i].x, currentBlock[i].y);

        for (int i = 0; i < currentBlock.Count; i++)
        {
            currentBlock[i].y--;
            theLevel.UpdateGrid(currentBlock[i].x, currentBlock[i].y, currentBlock[i]);
            currentBlock[i].transform.position = new Vector3(theLevel.START_SCREEN_X + currentBlock[i].x * theLevel.LENGTH, theLevel.START_SCREEN_Y + currentBlock[i].y * theLevel.LENGTH, -2);

            if (currentBlock[i].y <= 0 || !theLevel.IsGirdEmpty(currentBlock[i].x, currentBlock[i].y - 1))
                isGround = true;
        }

        if (isGround)
        {
            OnGround();
            return true;
        }
        return false;
    }

    void OnGround()
    {
        cooldownCounter = cooldownTime;
        currentBlock = theLevel.theGenerate.GenerateBlock();
        theLevel.CheckArrange();
    }

    int GetMinX()
    {
        int minX = int.MaxValue;
        for (int i = 0; i < currentBlock.Count; i++)
            if (currentBlock[i].x < minX)
                minX = currentBlock[i].x;
        return minX;
    }

    int GetMaxX()
    {
        int maxX = 0;
        for (int i = 0; i < currentBlock.Count; i++)
            if (currentBlock[i].x > maxX)
                maxX = currentBlock[i].x;
        Debug.Log(maxX);
        return maxX;
    }
}
