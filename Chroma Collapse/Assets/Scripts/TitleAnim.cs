using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
    public Animator anim;
    public GameObject visuals;
    public float scroll_speed = 1f;
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
        if (tick > 250) {
            if (transform.position.y >= -479) transform.position -= new UnityEngine.Vector3 (0, scroll_speed, 0);
            else {
                visuals.SetActive(true);
                anim.speed = 0f;
                anim.Play("title", 0, 0f);
            }
        }
    }
}
