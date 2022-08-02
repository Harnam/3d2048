using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //[HideInInspector]
    public int val, gridX, gridY, gridZ;

    public Texture2D[] texs = new Texture2D[11];
    private Dictionary<int, Texture2D> colorVal = new Dictionary<int, Texture2D>() // very bright different colors for testing to differentiate
    {
        { 2, null },
        { 4, null },
        { 8, null },
        { 16, null },
        { 32, null },
        { 64, null },
        { 128, null },
        { 256, null },
        { 512, null },
        { 1024, null },
        { 2048, null },
    };

    private void OnEnable()
    {
        colorVal[2] = texs[0];
        colorVal[4] = texs[1];
        colorVal[8] = texs[2];
        colorVal[16] = texs[3];
        colorVal[32] = texs[4];
        colorVal[64] = texs[5];
        colorVal[128] = texs[6];
        colorVal[256] = texs[7];
        colorVal[512] = texs[8];
        colorVal[1024] = texs[9];
        colorVal[2048] = texs[10];
        val = (Random.Range(0, 2) == 0)?2:4;
        updateColor();
        Spawner.updatepos += updatePos;
    }

    private void OnDisable()
    {
        Spawner.updatepos -= updatePos;
    }

    void updatePos()
    {
        transform.position = new Vector3(Spawner.posi[gridX - 1], Spawner.posi[gridY - 1], Spawner.posi[gridZ - 1]);
        updateColor();
    }

    void updateColor()
    {

        gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_MainTex", colorVal[val]); ;
    }
}
