using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax_Scroll : MonoBehaviour
{
    public float parallaxSpeed = 0.1f;  // Speed at which the texture scrolls
    private Material material;
    private Vector2 offset;

    void Start()
    {
        // Get the material attached to the plane
        material = GetComponent<Renderer>().material;
        offset = material.mainTextureOffset;
    }

    void Update()
    {
        // Calculate the new offset based on the camera's position
        // Vector3 cameraPosition = Camera.main.transform.position;
        offset.x += parallaxSpeed * Time.deltaTime;
        offset.y += parallaxSpeed * Time.deltaTime;

        // Apply the offset to the material
        material.mainTextureOffset = offset;
    }

}
