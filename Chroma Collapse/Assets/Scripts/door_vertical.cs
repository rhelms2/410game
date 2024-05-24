using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_vertical : MonoBehaviour
{
    public GameObject physicalDoor;
    public door_trigger trigger1;
    public door_trigger trigger2;
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
        if (trigger1.get_state()) {
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
            trigger1.set_state(false);
        }
        else if (trigger2.get_state()) {
            if (currTrigger == 0){
                currTrigger = 2;
                gameObject.GetComponent<AudioSource>().Play(0);
                StartCoroutine(moveDoor(1));
            }
            else if (currTrigger == 1){
                currTrigger = 0;
                gameObject.GetComponent<AudioSource>().Play(0);
                StartCoroutine(moveDoor(-1));
            }
            trigger2.set_state(false);
        }
    }
}
