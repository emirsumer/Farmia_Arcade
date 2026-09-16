using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _firstMousePos;
    private Vector2 _currentMousePos;
    private Vector2 _deltaMousePos;

    public Action<Vector2> OnPlayerMove;
    public static PlayerInput Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _firstMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            _currentMousePos = Input.mousePosition;
            _deltaMousePos = _currentMousePos - _firstMousePos;
            _deltaMousePos.Normalize();
            OnPlayerMove?.Invoke(_deltaMousePos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnPlayerMove?.Invoke(Vector2.zero);
        }
    }
}
