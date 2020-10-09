using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float maxY;

    private void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        // Keeps the camera with a min/max boundaries
        Vector3 newPostion = Vector3.Lerp(transform.position, target.transform.position + offset, moveSpeed * Time.deltaTime);
        if (newPostion.x > maxX)
            newPostion.x = maxX;
        else if (newPostion.x < minX)
            newPostion.x = minX;

        if (newPostion.y > maxY)
            newPostion.y = maxY;
        else if (newPostion.y < minY)
            newPostion.y = minY;
        
        transform.position = newPostion;
    }
}
