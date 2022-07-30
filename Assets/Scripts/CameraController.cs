using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private float speedmul, touchspeedmul;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveCam();
    }

    void moveCam()
    {
        if(Input.GetButton("Fire2"))
            transform.Rotate((new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * speedmul*1000);

        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Touching: " + touch.deltaPosition);
            transform.Rotate((new Vector3(-touch.deltaPosition.y, touch.deltaPosition.x, 0)) * Time.deltaTime * touchspeedmul);
        }
    }
}
