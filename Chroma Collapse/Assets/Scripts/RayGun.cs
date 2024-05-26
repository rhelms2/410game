using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate = 0.5f;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public GameObject gun;
    public AudioSource shootSound;

    RaycastHit hit;
    float range = 1000.0f;

    public float maxRecoil_x = -5f;
    //public float recoilSpeed = 1f;
    public float recoverySpeed = 20f;  // Increased recovery speed for faster return

    private float currentRecoil_x = 0f;
    private float targetRecoil_x = 0f;
    private bool recoiling = false;
    private bool recovering = false;
   

    void Update()
    {
        HandleRecoil();
        if (Input.GetMouseButton(0))
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                ShootRay();
                shootSound.Play();
                m_shootRateTimeStamp = Time.time + shootRate;
                ApplyRecoil();
            }
        }
    }

    void ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = Instantiate(m_shotPrefab, transform.position, transform.rotation);
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            Destroy(laser, 2f);
        }
    }

    void ApplyRecoil()
    {
        recoiling = true;
        recovering = false;
        targetRecoil_x = currentRecoil_x + maxRecoil_x;
    }

    void HandleRecoil()
    {
        if (recoiling)
        {
            // Apply recoil
            currentRecoil_x = Mathf.Lerp(currentRecoil_x, targetRecoil_x, 0.5f);
            gun.transform.localRotation = Quaternion.Euler(currentRecoil_x, 0f, 0f);

            if (Mathf.Abs(currentRecoil_x - targetRecoil_x) < 1f)
            {
                recoiling = false;
                recovering = true;
                targetRecoil_x = 0f;  // Reset target recoil for recovery
            }
        }
        else if (recovering)
        {
            // Recover recoil
            currentRecoil_x = Mathf.Lerp(currentRecoil_x, 0f, recoverySpeed * Time.deltaTime);
            gun.transform.localRotation = Quaternion.Euler(currentRecoil_x, 0f, 0f);

            if (Mathf.Approximately(currentRecoil_x, 0f))
            {
                recovering = false;
                currentRecoil_x = 0f;  // Ensure it snaps back to zero
            }
        }
    }
}
