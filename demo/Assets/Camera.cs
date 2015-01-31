﻿using UnityEngine;
using System.Collections;
using Assets.Map;

public class Camera : MonoBehaviour
{
    public Map Map;
    float _mousePosX;
    float _mousePosY;
    float _scrollSpeed = 0.2f;
    float _zoomSpeed = 1f;
    Vector2 _mouseLeftClick;

    void Update()
    {
        float deltaX = Input.mousePosition.x - _mousePosX;
        float deltaY = Input.mousePosition.y - _mousePosY;
        
        _mousePosX = Input.mousePosition.x;
        _mousePosY = Input.mousePosition.y;

        if (Input.GetMouseButton(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _mouseLeftClick = hit.point;
                Map.Click(_mouseLeftClick);
            }
        }

        if (Input.GetMouseButton(1))
        {
            transform.parent.Translate(new Vector3(deltaX, deltaY, 0) * _scrollSpeed);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.parent.localPosition.z > -20)
        {
            transform.parent.Translate(new Vector3(0, 0, -_zoomSpeed));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.parent.localPosition.z < -5)
        {
            transform.parent.Translate(new Vector3(0, 0, _zoomSpeed));
        }
    }

    void OnDrawGizmos()
    {
        if (_mouseLeftClick != Vector2.zero)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(new Vector3(_mouseLeftClick.x, _mouseLeftClick.y, 0), 1F);
        }
    }
}