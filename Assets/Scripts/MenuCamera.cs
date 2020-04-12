using System;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    
    private Camera _camera;
    private Vector3 _originPos;
    private float _originFov;

    private void Start()
    {
        _originPos = transform.position;
        _camera = GetComponent<Camera>();
        _originFov = _camera.fieldOfView;
    }

    private void Update()
    {
        var p1Pos = player1.transform.position;
        var p2Pos = player2.transform.position;
        var newFov = Mathf.Lerp(_camera.fieldOfView, _originFov * (Math.Abs(p1Pos.x - p2Pos.x)/15), Time.deltaTime);
        var newPos = new Vector3((p1Pos.x + p2Pos.x)/2f, _originPos.y, _originPos.z);
        if (newFov > _originFov)
            _camera.fieldOfView = newFov;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime);
    }
}
