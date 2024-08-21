using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboarding : MonoBehaviour
{
    // From this video: https://www.youtube.com/watch?v=hxZ7lysQa3o&t=144s
    Vector3 cameraDir;

    // Update is called once per frame
    void Update()
    {
        cameraDir = Camera.main.transform.forward;
        cameraDir.y = 0;

        transform.rotation = Quaternion.LookRotation(cameraDir);
    }
}
