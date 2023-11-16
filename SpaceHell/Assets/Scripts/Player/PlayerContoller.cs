using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera mainCamera;

    public GameObject bulletPrefab;
    public Animator playerAnimator;

    public float speedDefault = 60f;

    float currSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = speedDefault;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize(); // Ensure diagonal movement isn't faster

        // Move the player in world space
        transform.Translate(movement * currSpeed * Time.deltaTime, Space.World);

        //  if move to the right
        if (horizontalInput > 0)
        {
            playerAnimator.SetBool("Right", true);
            playerAnimator.SetBool("Left", false);
        }

        //  if move to the left
        if (horizontalInput < 0)
        {
            playerAnimator.SetBool("Left", true);
            playerAnimator.SetBool("Right", false);
        }

        // if not moving
        if (horizontalInput == 0)
        {
            playerAnimator.SetBool("Left", false);
            playerAnimator.SetBool("Right", false);
        }

        // When shift is pressed slow down the time
        if (Input.GetKey(KeyCode.LeftShift))
        {
            slowDownTime();
        }
        else
        {
            currSpeed = speedDefault;
        }

        // when space is pressed shoot

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }

    }

    void slowDownTime()
    {
        currSpeed = speedDefault / 2;
    }

    void shoot()
    {
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        // add 10 into the z position so the bullet doesn't collide with the player

        GameObject bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 0f, 8f), rotation);
        bullet.GetComponent<Rigidbody>().velocity = rotation * Vector3.up * 50f;

    }


}
