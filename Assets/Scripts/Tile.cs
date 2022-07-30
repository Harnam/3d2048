using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public int val, gridX, gridY, gridZ;

    private void OnEnable()
    {
        val = (Random.Range(0, 2) == 0)?2:4;

        Spawner.movehori += moveHorizontal;
        Spawner.movevert += moveVertical;
    }

    private void OnDisable()
    {
        Spawner.movehori -= moveHorizontal;
        Spawner.movevert -= moveVertical;
    }

    void moveVertical()
    {
        //move up down
    }

    void moveHorizontal()
    {
        //move left right
    }
}
