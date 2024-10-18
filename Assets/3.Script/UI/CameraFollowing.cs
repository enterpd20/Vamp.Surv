using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(0, -0.8f, 0) + GameManager.Instance.player.transform.position;        

        rect.position = 
            Camera.main.WorldToScreenPoint(newPosition);
        Debug.Log(rect.position);
    }
}
