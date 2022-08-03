using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour
{
    [Range(0,5)]
    public int state = 0;

    //0 - front, 1 - right, 2 - left, 3 - top, 4 - bottom, 5 - back
    [SerializeField]
    private Transform[] posis;

    [SerializeField]
    private Transform cam;
        
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        updateTrans();
    }

    // Update is called once per frame
    void Update()
    {
        int minIndex = 0; float minDist = 10000;
        for(int i = 0; i < 6; i++)
        {
            float dist = Vector3.Distance(cam.position, posis[i].position);
            if(dist < minDist)
            {
                minDist = dist;
                minIndex = i;
            }
        }
        state = minIndex;
        updateTrans();
    }

    void updateTrans()
    {
        gameObject.transform.position = posis[state].transform.position;
        gameObject.transform.rotation = posis[state].transform.rotation;
    }
}
