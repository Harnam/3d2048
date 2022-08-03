using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject touchMove;

    // Start is called before the first frame update
    void Start()
    {
        if(Input.touchSupported)
            showTouchMove();
    }

    void showTouchMove()
    {
        touchMove.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
