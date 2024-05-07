using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
}
