using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
    public Animator anim;
    public GameObject visuals;
    int tick = 0;
    // Start is called before the first frame update
    void Start()
    {
        visuals.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        tick++;
        if (tick > 410) {
            if (transform.position.y >= -479) transform.position -= new Vector3(0, 0.5f, 0);
            else {
                visuals.SetActive(true);
                anim.speed = 0f;
                anim.Play("title", 0, 0f);
            }
        }
    }
}
