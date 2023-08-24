using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float rotation = 360;

    private void Update()
    {
        transform.Rotate(0, 0, rotation * speed * Time.deltaTime);
    }
}
