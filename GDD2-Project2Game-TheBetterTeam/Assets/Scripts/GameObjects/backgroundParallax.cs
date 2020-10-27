using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundParallax : MonoBehaviour
{
    public GameObject camera;
    public int offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((camera.transform.position.x * -.1f) + offset, transform.position.y, transform.position.z);
    }
}
