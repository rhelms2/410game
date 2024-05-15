using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_vertical : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;
    public GameObject physicalDoor;
    public GameObject player;
    int currTrigger = 0; //0 - closed, 1 - open with trigger1, 2 - opened with trigger2
    public int moveDist = 10;
    public float mspeed = 0.20f;
    float originY;
    // Start is called before the first frame update
    void Start()
    {
        originY = physicalDoor.transform.position.y; // dont know if we need this
    }

    IEnumerator moveDoor(int dir){
        float accu = 0;
        float bound = dir * moveDist;
        while (accu < bound){
            accu += mspeed; //linear for now
            physicalDoor.transform.position += new Vector3(0, mspeed, 0);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if (Vector3.Distance(player.transform.position, trigger1.transform.position) < 2f){
            if (currTrigger == 0) {
                currTrigger = 1;
                gameObject.GetComponent<AudioSource>().Play(0);
                StartCoroutine(moveDoor(1));
            }
            else if (currTrigger == 2) {
                currTrigger = 0;
                gameObject.GetComponent<AudioSource>().Play(0);
                StartCoroutine(moveDoor(-1));
            }
       } 
       else if (Vector3.Distance(player.transform.position, trigger2.transform.position) < 2f){
            if (currTrigger == 0){
                currTrigger = 2;
                StartCoroutine(moveDoor(1));
            }
            else if (currTrigger == 1){
                currTrigger = 0;
                StartCoroutine(moveDoor(-1));
            }
       }
    }
}
