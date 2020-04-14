using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    private Vector3 pos1;
    private Vector3 pos2;
    private float speed = 2.0f;

    private void Start()
    {
        pos1 = new Vector3(-4, transform.position.y, transform.position.z);
        pos2 = new Vector3(4, transform.position.y, transform.position.z);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}
