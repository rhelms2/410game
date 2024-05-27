using UnityEngine;

public class goomSprite : MonoBehaviour
{
    float myY;
    public GameObject follow;
    public Animator anim;
    void Start() {
        myY = transform.position.y;
        anim.Play("goombidle");
    }
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Bullet") {
            Debug.Log("i've been shot");
        }
    }
    void Update(){
        //transform.position = follow.transform.position;
        transform.position = new Vector3(follow.transform.position.x, myY, follow.transform.position.z);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, follow.transform.localEulerAngles.y, transform.localEulerAngles.z);
        //transform.rotation = follow.transform.rotation;
    }
}
