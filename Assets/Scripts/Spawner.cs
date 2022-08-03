using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public delegate void updatePos();
    public static event updatePos updatepos;

    [SerializeField]
    private GameObject tile, parent, frame;
    public int gridsize = 4;

    public static float[] posi = { -1.5f, -0.5f, 0.5f, 1.5f };
    public static Dictionary<int, bool> empty = new Dictionary<int, bool>();
    
    public Dictionary<int, GameObject> tiles = new Dictionary<int, GameObject>();

    private bool needToSpawn, isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = true;
        for (int i = 1; i <= gridsize; i++)
            for (int j = 1; j <= gridsize; j++)
                for (int k = 1; k <= gridsize; k++)
                    empty.Add((i*100) + (j*10) + k, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (Input.GetButtonDown("Horizontal"))
                moveHorizontal(0);
            else if (Input.GetButtonDown("Vertical"))
                moveVertical(0);
            else if (Input.GetKeyDown(KeyCode.Q))
                moveZAxis(1);
            else if (Input.GetKeyDown(KeyCode.E))
                moveZAxis(-1);
        }
    }

    private void LateUpdate()
    {
        if (needToSpawn) spawnTile();
    }

    void moveZAxis(int side)
    {
        switch (frame.GetComponent<Frame>().state)
        {
            case 0:
                move(false, false, true, side);
                break;
            case 1:
                move(true, false, false, -side);
                break;
            case 2:
                move(true, false, false, side);
                break;
            case 3:
                move(false, true, false, -side);
                break;
            case 4:
                move(false, true, false, side);
                break;
            default:
                move(false, false, true, -side);
                break;
        }
        updatepos?.Invoke();
        needToSpawn = true;
    }

    void moveVertical(int side)
    {
        if(side == 0) side = (Input.GetAxisRaw("Vertical") < 0) ? -1 : 1;
        switch (frame.GetComponent<Frame>().state)
        {
            case 0:
                move(false, true, false, side);
                break;
            case 1:
                move(false, true, false, side);
                break;
            case 2:
                move(false, true, false, side);
                break;
            case 3:
                move(false, false, true, side);
                break;
            case 4:
                move(false, false, true, -side);
                break;
            default:
                move(false, true, false, side);
                break;
        }
        updatepos?.Invoke();
        needToSpawn = true;
    }

    void moveHorizontal(int side)
    {
        if(side == 0) side = (Input.GetAxisRaw("Horizontal") < 0) ? -1 : 1;
        switch (frame.GetComponent<Frame>().state)
        {
            case 0:
                move(true, false, false, side);
                break;
            case 1:
                move(false, false, true, side);
                break;
            case 2:
                move(false, false, true, -side);
                break;
            case 3:
                move(true, false, false, side);
                break;
            case 4:
                move(true, false, false, side);
                break;
            default:
                move(true, false, false, -side);
                break;
        }
        updatepos?.Invoke();
        needToSpawn = true;
    }

    void spawnTile()
    {   
        List<int> emp = new List<int>();
        foreach (KeyValuePair<int, bool> i in empty)
            if (i.Value) emp.Add(i.Key);
        if (emp.Count == 0)
        {
            Debug.Log("Game Over");
            needToSpawn = false;
            return;
        }
        int rand = emp[Random.Range(0, emp.Count)];

        GameObject newtile = Instantiate(tile, parent.transform, false);
        tiles.Add(rand, newtile);
        newtile.transform.position = new Vector3(posi[(rand%10)-1], posi[((rand/10) % 10) - 1], posi[(rand / 100) - 1]);
        newtile.GetComponent<Tile>().gridX = rand % 10;
        newtile.GetComponent<Tile>().gridY = (rand / 10) % 10;
        newtile.GetComponent<Tile>().gridZ = rand / 100;
        empty[rand] = false;
        needToSpawn = false;
    }

    void move(bool x, bool y, bool z, int side)
    {
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                for (int k = ((side == 1)?4:1); ((side == 1)?(k >= 2):(k <= 3)); k-=side)
                {
                    int key = 0;
                    if (x) key = (i * 100) + (j * 10) + k;
                    else if (y) key = (i * 100) + (k * 10) + j;
                    else if (z) key = (k * 100) + (j * 10) + i;
                    if (!empty[key])
                    {
                        for (int ik = k - side; ((side == 1) ? (ik >= 1) : (ik <= 4)); ik-=side)
                        {
                            int nextkey = 0;
                            if (x) nextkey = (i * 100) + (j * 10) + ik;
                            else if (y) nextkey = (i * 100) + (ik * 10) + j;
                            else if (z) nextkey = (ik * 100) + (j * 10) + i;
                            if (!empty[nextkey])
                            {
                                Tile thistile = tiles[key].GetComponent<Tile>();
                                Tile nexttile = tiles[nextkey].GetComponent<Tile>();
                                if (thistile.val == nexttile.val)
                                {
                                    thistile.val *= 2;
                                    empty[nextkey] = true;
                                    tiles.Remove(nextkey);
                                    Destroy(nexttile.gameObject);
                                }
                                break;
                            }
                        }
                    }
                }
                for (int k = ((side == 1) ? 3 : 2); ((side == 1) ? (k >= 1) : (k <= 4)); k -= side)
                {
                    int key = 0;
                    if (x) key = (i * 100) + (j * 10) + k;
                    else if (y) key = (i * 100) + (k * 10) + j;
                    else if (z) key = (k * 100) + (j * 10) + i;
                    if (!empty[key])
                    {
                        Tile thistile = tiles[key].GetComponent<Tile>();
                        tiles.Remove(key);
                        int nextkey = 0;
                        if (x) nextkey = (i * 100) + (j * 10) + k + side;
                        else if (y) nextkey = (i * 100) + ((k + side) * 10) + j;
                        else if (z) nextkey = ((k + side) * 100) + (j * 10) + i;
                        while (((x)?thistile.gridX:((y)?thistile.gridY:thistile.gridZ)) != ((side == 1)?4:1) && empty[nextkey])
                        {
                            if (x) thistile.gridX += side;
                            else if (y) thistile.gridY += side;
                            else if (z) thistile.gridZ += side;
                            key = nextkey;
                            if (x) nextkey = ((key / 100) * 100) + (((key / 10) % 10) * 10) + (key % 10) + side;
                            else if (y) nextkey = ((key / 100) * 100) + ((((key / 10) % 10) + side) * 10) + (key % 10);
                            else if (z) nextkey = (((key / 100) + side) * 100) + (((key / 10) % 10) * 10) + (key % 10);
                        }
                        if (x) empty[(i * 100) + (j * 10) + k] = true;
                        else if (y) empty[(i * 100) + (k * 10) + j] = true;
                        else if (z) empty[(k * 100) + (j * 10) + i] = true;
                        empty[key] = false;
                        tiles.Add(key, thistile.gameObject);
                    }
                }
            }
        }
    }

}