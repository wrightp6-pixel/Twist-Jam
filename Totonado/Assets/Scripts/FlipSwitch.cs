using System;
using System.Collections;
using UnityEngine;

public class FlipSwitch : MonoBehaviour
{
    //private bool isSwitching;
    [SerializeField] private float switchRate;
    [SerializeField] private GameObject rotatePoint;
    private Vector3 axis;
    private float targetAngle;
    [SerializeField] private float switchTime;
    //private bool isOnRight;
    [SerializeField] private GameObject platform;
    [SerializeField] private PlayerMove player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //isSwitching = false;
        axis = new Vector3(0, 1, 0);
        //isOnRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJump()
    {
        Debug.Log("Jump pressed");
        // Activate platform switch if the platform is currently active (as in not being switched already)
        if (platform.activeSelf)
        {
            Debug.Log("Switch");
            StartCoroutine(waitSwitch());
        }
    }

    IEnumerator waitSwitch()
    {
        // Remove platform object during transition period
        yield return new WaitForSeconds(0.05f);
        platform.gameObject.SetActive(false);
        yield return new WaitForSeconds(switchTime);
        platform.gameObject.SetActive(true);
        // Swap platform to other side of flip switch
        platform.transform.RotateAround(rotatePoint.transform.position, axis, switchRate);
    }


}
