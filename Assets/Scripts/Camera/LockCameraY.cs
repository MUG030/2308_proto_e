using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject Hero;
    [SerializeField] float cameraY;

    private void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, cameraY);
    }
}
