using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject plum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Camera follows player (plum)
        transform.position = new Vector3(plum.transform.position.x, plum.transform.position.y, 25);
    }
}
