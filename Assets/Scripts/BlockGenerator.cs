using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{

    public GameObject blockPrefab;
    void Start()
    {

    }

    void Update()
    {

    }

    public List<BlockController> GenerateBlock()
    {
        List<BlockController> newBlock = new List<BlockController>();
        BlockController theBlock = Instantiate(blockPrefab, new Vector3(0.25f, 5, 0), Quaternion.identity).GetComponent<BlockController>();
        theBlock.x = 5;
        theBlock.y = 19;
        newBlock.Add(theBlock);
        return newBlock;
    }
}
