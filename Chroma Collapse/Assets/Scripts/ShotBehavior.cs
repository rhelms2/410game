using UnityEngine;
using System.Collections;

//https://www.youtube.com/watch?v=Yl9MhhoBkFU

public class ShotBehavior : ColorSwitcherRotation
{

    public Vector3 m_target;
    public GameObject collisionExplosion;
    public float speed;
    public GameObject self;

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * 300f;// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;


        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                explode();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }
        // This should change emission color of bullet
        self.GetComponent<Light>().color = color_array[color];
        collisionExplosion.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", color_array[color]);
        collisionExplosion.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color_array[color]);
        collisionExplosion.GetComponent<ParticleSystem>().startColor = color_array[color];
        //self.GetComponent<Light>().color = Color.blue;

    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }

    void explode()
    {
        if (collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(
                collisionExplosion, transform.position, transform.rotation);
            
            Destroy(gameObject);
            Destroy(explosion, 1f);
        }


    }

}