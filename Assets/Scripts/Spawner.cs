using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public delegate void moveHori();
    public static event moveHori movehori;
    public delegate void moveVert();
    public static event moveVert movevert;

    [SerializeField]
    private GameObject tile, parent;
    public int gridsize = 4;

    private float[] posi = { -1.5f, -0.5f, 0.5f, 1.5f };
    public static List<int> empty = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= gridsize; i++)
            for (int j = 1; j <= gridsize; j++)
                for (int k = 1; k <= gridsize; k++)
                    empty.Add((i*100) + (j*10) + k);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            moveHorizontal();
        } else if (Input.GetButtonDown("Vertical"))
        {
            moveVertical();
        }
    }

    void moveVertical()
    {
        movevert?.Invoke();
        spawnTile();
    }

    void moveHorizontal()
    {
        movehori?.Invoke();
        spawnTile();
    }

    void spawnTile()
    {
        //Debug.Log(empty.Count);
        if (empty.Count == 0)
        {
            Debug.Log("Game Over");
            return;
        }
        int rand = empty[Random.Range(0, empty.Count)];
        GameObject newtile = Instantiate(tile, parent.transform, false);
        newtile.transform.position = new Vector3(posi[(rand%10)-1], posi[((rand/10) % 10) - 1], posi[(rand / 100) - 1]);
        newtile.GetComponent<Tile>().gridX = rand % 10;
        newtile.GetComponent<Tile>().gridY = (rand / 10) % 10;
        newtile.GetComponent<Tile>().gridZ = rand / 100;
        empty.Remove(rand);
    }

}
