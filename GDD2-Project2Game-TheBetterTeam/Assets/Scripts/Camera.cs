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

    private void Start()
    {
        if (target == null)
            target = GameObject.FindWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.transform.position + offset, moveSpeed * Time.deltaTime);
    }
}
