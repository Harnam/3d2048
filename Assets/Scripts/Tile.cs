using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //[HideInInspector]
    public int val, gridX, gridY, gridZ;

    //private Dictionary<int, Color> colorVal = new Dictionary<int, Color>()
    //{
    //    { 2, (new Color32(238,228,218,255)) },
    //    { 4, (new Color32(237,224,200,255)) },
    //    { 8, (new Color32(242,177,121,255)) },
    //    { 16, (new Color32(245,149,99,255)) },
    //    { 32, (new Color32(246,124,95,255)) },
    //    { 64, (new Color32(246,94,59,255)) },
    //    { 128, (new Color32(237,207,114,255)) },
    //    { 256, (new Color32(237,204,97,255)) },
    //    { 512, (new Color32(237,200,80,255)) },
    //    { 1024, (new Color32(237,197,63,255)) },
    //    { 2048, (new Color32(237,194,46,255)) },
    //    { 4096, (new Color32(62,57,51,255)) }
    //};
    private Dictionary<int, Color> colorVal = new Dictionary<int, Color>() // very bright different colors for testing to differentiate
    {
        { 2, Color.yellow },
        { 4, Color.red },
        { 8, Color.green },
        { 16, Color.blue },
        { 32, Color.cyan },
        { 64, Color.magenta },
        { 128, Color.white },
        { 256, (new Color32(237,204,97,255)) },
        { 512, (new Color32(237,200,80,255)) },
        { 1024, (new Color32(237,197,63,255)) },
        { 2048, (new Color32(237,194,46,255)) },
        { 4096, (new Color32(62,57,51,255)) }
    };


    private void OnEnable()
    {
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
        
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_Color", colorVal[val]);
    }
}
