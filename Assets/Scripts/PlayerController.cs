using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float conrtolSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 2.5f;

    [Header("Screen-Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;

    [Header("Conrtol-throw Based")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            TranslateTheStuff();
            processRotation();
        }
    }

    void processRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void TranslateTheStuff()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float yOffset = yThrow * conrtolSpeed * Time.deltaTime;
        float xOffset = xThrow * conrtolSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void OnPlayerDeath() //Called by SendMessage()
    {
        isControlEnabled = false;
    }

}
