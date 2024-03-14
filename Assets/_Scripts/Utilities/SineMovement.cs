using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    private new Transform transform;
    [SerializeField][Range(0.01f, 10f)] private float range = 0.05f;
    [SerializeField][Range(0.01f, 10f)] private float speed = 1f;
    private float y;
    private void Awake()
    {
        transform = GetComponent<Transform>();
        y = transform.position.y;
    }
    private void Update()
    {
        float _y = Mathf.Sin(Time.time * speed) * range + y;
        Vector3 _position = transform.position;
        _position = new Vector3(_position.x, _y, _position.z);
        transform.position = _position;
    }
}
