using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject tile;

    public int xoffsetAmount;
    public int yoffsetAmount;
    public int x;
    public int y;

    int xcurrentOffset;
    int ycurrentOffset;

    Vector3 spawnLocation;


    [ContextMenu("GenerateTiles")]
    void GenerateTiles()
    {
        xcurrentOffset = 0;
        ycurrentOffset = 0;
        for (int i = 0; i < y; i++)
        {
            for(int p = 0; p < x; p++)
            {
                spawnLocation = new Vector3(xcurrentOffset, ycurrentOffset, 0);
                GameObject spawnedtile = Instantiate(tile, spawnLocation, transform.rotation);
                spawnedtile.transform.parent = this.transform;
                xcurrentOffset += xoffsetAmount;
            }
                xcurrentOffset = 0;
                ycurrentOffset += yoffsetAmount;

        }
    }
}
