using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zorb_Wallhit : MonoBehaviour
{
    public Zorb_All sn;

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == "wall") sn.wallHitTrue();
    }
}
