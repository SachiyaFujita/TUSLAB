using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockManager : MonoBehaviour
{

    [SerializeField]
1:      private GameObject mPrefabBlock;

    // ブロックの辺のサイズ
    private readonly float BLOCK_SIZE = 1.0f;
    private readonly float BLOCK_SIZE_HALF = 0.5f;

    // ブロックをListで管理
    private List<NumberChanger> mBlockList = new List<NumberChanger>();

    void Start()
    {
        CreateField(GameController.LEVEL_EASY);
    }

    /// <summary>
    /// フィールドを生成する
    /// </summary>
    public void CreateField(int gameLevel)
    {
        // 前のブロックが存在する場合は全て破棄
        foreach (NumberChanger model in mBlockList)
        {
            Destroy(model.gameObject);
        }
        mBlockList.Clear();

        // ゲームレベルによってサイズと爆弾の数を決定
        int xLength;
        int yLength;
        int bombCount;
        switch (gameLevel)
        {
            case GameController.LEVEL_EASY:
                xLength = 9;
                yLength = 9;
                bombCount = 10;
                break;
            case GameController.LEVEL_NORMAL:
                xLength = 16;
                yLength = 16;
                bombCount = 40;
                break;
            default:
                xLength = 30;
                yLength = 16;
                bombCount = 99;
                break;
        }

        // ブロックを並べる
        InstantiateBlocks(xLength, yLength);
    }

    /// <summary>
    /// ブロックを並べる
    /// 同時に生成されたBlockModelをListに格納する
    /// </summary>
    /// <param name="xLength"></param>
    /// <param name="yLength"></param>
    private void InstantiateBlocks(int xLength, int yLength)
    {
        for (int y = 0; y < yLength; y++)
        {
            for (int x = 0; x < xLength; x++)
            {
                GameObject blockGo = Instantiate(mPrefabBlock, new Vector3(x * BLOCK_SIZE + BLOCK_SIZE_HALF, BLOCK_SIZE_HALF, y * BLOCK_SIZE + BLOCK_SIZE_HALF), Quaternion.identity, transform);
                NumberChanger blockModel = blockGo.GetComponent<NumberChanger>();
                blockModel.SetPosition(x, y);
                mBlockList.Add(blockModel);
            }
        }
    }

}