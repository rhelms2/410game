using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_door : MonoBehaviour
{
    public GameObject physicalDoor;
    public int moveDist = 10;
    public float mspeed = 0.20f;
    [SerializeField] GunTarget gun_target;
    float originY;

    // Start is called before the first frame update
    void Start()
    {
        originY = physicalDoor.transform.position.y; // dont know if we need this
    }

    IEnumerator moveDoor(int dir){
        float accu = 0;
        float bound = moveDist;
        Vector3 movement_amount = new Vector3 (0, mspeed * dir, 0);

        while (accu < bound) {
            accu += mspeed; //linear for now
            physicalDoor.transform.position += movement_amount;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gun_target.active) {
            StartCoroutine(moveDoor(1));
        }
    }
}
