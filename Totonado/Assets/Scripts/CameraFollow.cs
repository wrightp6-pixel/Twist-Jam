using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class CameraFollow : MonoBehaviour
{
    // =========================== Instance Properties ==========================
    private MyControls myControls;
    public GameObject rotatePoint;
    public Vector3 rotatePointVector;
    private Vector3 rotationAxis;
    private Vector2 cameraMoveValue;
    [SerializeField] private float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myControls = new MyControls();
        rotationAxis = new Vector3(0, 1, 0);
        myControls.PlayerCameraControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {

        //FollowTheCamera();
        CameraRotate();
        

    }

    // =========================== Methods =========================== 

    public void CameraRotate() 
    {
        // Get player input for rotating the camera
        cameraMoveValue = myControls.PlayerCameraControls.MoveCam.ReadValue<Vector2>();

        
        if (cameraMoveValue.x != 0)
        {
            //rotatePointVector = new Vector3(rotatePoint.transform.posx, rotatePoint.transform.y, rotatePoint.transform.z);

            // Rotate the camera around the target (on the player) based on player input
            transform.RotateAround(rotatePoint.transform.position, rotationAxis, cameraMoveValue.x * Time.deltaTime * speed);

            //this.transform.Rotate(new Vector3(0, cameraMoveValue.x, 0), Space.Sel;
        }
    }

    public void FollowTheCamera()
    {
        // Calculate distance bewteen camera and player target and then move camera by that distance
        //if (rotatePoint.transform.position.x - this.transform.position.x + 2 != 0)
        //{
        //    float DistanceX = rotatePoint.transform.position.x - this.transform.position.x + 2;
        //} else
        //{
        //    DistanceX = 0;
        //}

        this.transform.Translate(new Vector3(rotatePoint.transform.position.x - this.transform.position.x,
            rotatePoint.transform.position.y - this.transform.position.y,
            rotatePoint.transform.position.z - this.transform.position.z), Space.Self);

    }

    
}
