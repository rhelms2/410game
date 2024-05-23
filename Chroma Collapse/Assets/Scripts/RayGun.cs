using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public GameObject gun;

    RaycastHit hit;
    float range = 1000.0f;

    float maxRecoil_x = -20f;
    private float recoilSpeed = 2f;
    // in seconds
    public float recoil = 0.2f;

    void Update()
    {
        recoiling();
        if (Input.GetMouseButton(0))
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                shootRay();
                m_shootRateTimeStamp = Time.time + shootRate;
                recoil += 0.2f;
            }
        }


        //Mathf.Sin(1);
    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(m_shotPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            GameObject.Destroy(laser, 2f);


        }

    }

    //public void StartRecoil(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
    //{
    //    // in seconds
    //    recoil = recoilParam;
    //    maxRecoil_x = maxRecoil_xParam;
    //    recoilSpeed = recoilSpeedParam;
    //}

    void recoiling()
    {
        if (recoil > 0f)
        {
            Quaternion maxRecoil = Quaternion.Euler(maxRecoil_x, 0f, 0f);
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeed);
            recoil -= Time.deltaTime;
        }
        else
        {
            recoil = 0f;
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeed / 2);
        }
    }


}
