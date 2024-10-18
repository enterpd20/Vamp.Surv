using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float move_Speed = 0f;
    private Rigidbody2D rigid2D;
    [SerializeField] private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * move_Speed * Time.deltaTime;        
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
