using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePedestal : MonoBehaviour
{
    public GameObject platform;
    public GameObject pedestal;
    public GameObject item;
    private Vector3 deltaButtonPosition = new Vector3(0f, 0.05f, 0f);
    private Vector3 deltaPedestalPosition = new Vector3(0f, 2.5f, 0f);
    private Vector3 targetPedestalPosition;
    private Vector3 targetItemPosition;
    private Vector3 targetPlatformPosition;
    public float platformSpeed = 2f;
    public bool platformMoving = false;
    private bool pedestalMoving = false;
    private bool keyCardMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPlatformPosition = new Vector3(platform.transform.position.x, platform.transform.position.y, platform.transform.position.z - 2.7f);
        targetPedestalPosition = pedestal.transform.position + deltaPedestalPosition;
        targetItemPosition = item.transform.position + deltaPedestalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (platformMoving)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPlatformPosition, platformSpeed * Time.deltaTime);
            if (platform.transform.position == targetPlatformPosition)
            {
                platformMoving = false;
                pedestalMoving = true;
                keyCardMoving = true;
            }
        }

        if (pedestalMoving)
        {
            pedestal.transform.position = Vector3.MoveTowards(pedestal.transform.position, targetPedestalPosition, platformSpeed * Time.deltaTime);
            if (pedestal.transform.position == targetPedestalPosition)
            {
                pedestalMoving = false;
            }
        }

        if (keyCardMoving)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, targetItemPosition, platformSpeed * Time.deltaTime);
            if (item.transform.position == targetItemPosition)
            {
                keyCardMoving = false;
            }
        }
    }
}
