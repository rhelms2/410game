using UnityEngine;
using System.Collections;
// using UnityEditor.UI;
using Unity.VisualScripting;
// using UnityEditor.Search;

//https://www.youtube.com/watch?v=Yl9MhhoBkFU

public class ShotBehavior : Player_Inventory
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
        setColor(self, collisionExplosion);
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

    void setColor(GameObject bullet, GameObject explosion) {

        bool set_true_color = false;

        if (color == (int) color_enum.white) {
            if (inventory_activation[5] && inventory_activation[4] && inventory_activation[3]) {
                set_true_color = true;
            }
        }
        else if (color == (int) color_enum.green) {
            if (inventory_activation[5] && inventory_activation[4]) {
                set_true_color = true;
            }
        }
        else if (color == (int) color_enum.purple) {
            if (inventory_activation[5] && inventory_activation[3]) {
                set_true_color = true;
            }
        }
        else if (color == (int) color_enum.orange) {
            if (inventory_activation[4] && inventory_activation[3]) {
                set_true_color = true;
            }
        }
        else if (color == (int) color_enum.red && inventory_activation[3]) {
            set_true_color = true;
        }
        else if (color == (int) color_enum.yellow && inventory_activation[4]) {
            set_true_color = true;
        }
        else if (color == (int) color_enum.blue && inventory_activation[5]) {
            set_true_color = true;
        }

        if (set_true_color) {
            self.GetComponent<Renderer>().material.color = color_array[color];
            self.GetComponent<Light>().color = color_array[color];  // Actual bullet
            collisionExplosion.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", color_array[color]);
            collisionExplosion.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color_array[color]);
            collisionExplosion.GetComponent<ParticleSystem>().startColor = color_array[color];
        }
        else {
            self.GetComponent<Renderer>().material.color = color_array[0];
            self.GetComponent<Light>().color = color_array[0];  // Actual bullet
            collisionExplosion.GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", color_array[0]);
            collisionExplosion.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", color_array[0]);
            collisionExplosion.GetComponent<ParticleSystem>().startColor = color_array[0];
        }
    }

}