using System.Collections;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public float shootRate = 0.5f;
    private float m_shootRateTimeStamp;

    public GameObject m_shotPrefab;
    public GameObject gun;
    public AudioSource shootSound;
    public Camera playerCamera;
    [SerializeField] GameObject crosshairs;

    public GameObject playerObject;

    RaycastHit hit;
    float range = 1000.0f;

    public float maxRecoil_x = -5f;
    //public float recoilSpeed = 1f;
    public float recoverySpeed = 20f;  // Increased recovery speed for faster return

    private float currentRecoil_x = 0f;
    private float targetRecoil_x = 0f;
    private bool recoiling = false;
    private bool recovering = false;
    private float initialRecoil_x;

    void Awake()
    {
        initialRecoil_x = gun.transform.rotation.eulerAngles.x;
        crosshairs.SetActive(true);
    }

    void Update()
    {
        HandleRecoil();
        if (GameManager.controls.Gameplay.Shoot.IsPressed())
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
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(160, 120));
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = Instantiate(m_shotPrefab, playerObject.transform.position, playerObject.transform.rotation);
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

            // Get the current rotation in Euler angles
            Vector3 currentEulerAngles = gun.transform.localRotation.eulerAngles;

            // Set the x component based on the initial x angle and current recoil
            currentEulerAngles.x = initialRecoil_x + currentRecoil_x;
            gun.transform.localRotation = Quaternion.Euler(currentEulerAngles);

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

            // Get the current rotation in Euler angles
            Vector3 currentEulerAngles = gun.transform.localRotation.eulerAngles;

            // Set the x component based on the initial x angle and current recoil
            currentEulerAngles.x = initialRecoil_x + currentRecoil_x;
            gun.transform.localRotation = Quaternion.Euler(currentEulerAngles);

            if (Mathf.Approximately(currentRecoil_x, 0f))
            {
                recovering = false;
                currentRecoil_x = 0f;  // Ensure it snaps back to zero
            }
        }
    }
}
