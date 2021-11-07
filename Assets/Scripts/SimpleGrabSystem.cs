using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Simple example of Grabbing system.
/// </summary>
public class SimpleGrabSystem : MonoBehaviour
{
    public int trys;
    private int count;
    public float speed;
    public bool firsttime = true;
    public ParticleSystem rocks;
    public GameObject RndmScene;
    List<GameObject> TakeIt = new List<GameObject>();

    // Reference to the character camera.
    [SerializeField]
    private Camera characterCamera;

    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;

    // Reference to the currently held item.
    private PickableItem pickedItem;


    /// <summary>
    /// Method called very frame.
    /// </summary>
    private void Update()
    {

        
        // Execute logic only on button pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Check if player picked some item already
            if (pickedItem)
            {
                // If yes, drop picked item
                DropItem(pickedItem);
            }
            else
            {
                // If no, try to pick item in front of the player
                // Create ray from center of the screen
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                // Shot ray to find object to pick
                if (Physics.Raycast(ray, out hit, 4f))
                {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();

                    if (pickable && firsttime==true)
                    {
                        pickable.transform.position += Vector3.up * 0.07f;
                        count++;
                        FindObjectOfType<AudioManager>().Play("swordpickup1");
                    }
                    else {
                        // Pick it
                        PickItem(pickable);
                    }
                        if (count == trys)
                    {
                        // Pick it
                        FindObjectOfType<AudioManager>().Play("swordinarms");
                        PickItem(pickable);
                        firsttime = false;
                    }


                }


            }
        }
    }

    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void PickItem(PickableItem item)
    {
        // Assign reference
        pickedItem = item;

        // Disable rigidbody and reset velocities
        item.Rb.useGravity = true;
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        // Set Slot as a parent
        item.transform.SetParent(slot);

        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;

        pickedItem.tag = "TakeWithYou";
        Invoke("BreakFloor", 2);
        Invoke("LoadNew", 3);

    }

    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    private void DropItem(PickableItem item)
    {
        // Remove reference
        pickedItem = null;

        // Remove parent
        item.transform.SetParent(null);

        // Enable rigidbody
        item.Rb.isKinematic = false;

        // Add force to throw item a little bit
        item.Rb.AddForce(item.transform.forward * 3, ForceMode.VelocityChange);
    }


    public void BreakFloor()
    {
        FindObjectOfType<AudioManager>().Play("breakground");
        rocks.Play();
        foreach (GameObject things in GameObject.FindGameObjectsWithTag("TakeWithYou"))
        {
            TakeIt.Add(things);
            UnityEngine.Debug.Log(TakeIt);
        }
        foreach (GameObject floor in GameObject.FindGameObjectsWithTag("Floor"))
        {
            Destroy(floor);

        }
    }
    public void LoadNew() => RndmScene.GetComponent<RandomSceneLoading>().LoadRandomScene();
}