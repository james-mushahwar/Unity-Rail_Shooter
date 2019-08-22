using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms")] [SerializeField] float xRange = 6f;
    [Tooltip("In ms")] [SerializeField] float yRange = 5f;
    [Tooltip ("In ms^-1")][SerializeField] float speed = 10f;


    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -10f;

    [SerializeField] float positionYawFactor = 5;

    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + (yThrow * controlPitchFactor);
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = (xThrow * controlRollFactor);
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        //vertical movement
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * speed * Time.deltaTime;
        float yNewPosition = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        //horizontal movement
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * speed * Time.deltaTime;
        float xNewPosition = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);

        transform.localPosition = new Vector3(xNewPosition, yNewPosition, transform.localPosition.z);
    }
}
