using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // ===================== Instance Properties ===================== 
    // Reference to input actions from new input system
    private MyControls myControls;

    CharacterController charCon;

    private Vector2 playerMovementFlat;
    private Vector3 playerMovementTotal;
    [SerializeField] private int moveSpeed;



    // Built in method that is run very first in the object's lifecycle, including before start
    private void Awake()
    {
        // Set refernce to input action, character controller
        myControls = new MyControls();
        charCon = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myControls.PlayerMoveControls.Enable();
        playerMovementFlat = new Vector2(0, 0);
        playerMovementTotal = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

    }

    // ===================== Methods =====================

    private void movePlayer()
    {
        // Update movement value based on player inputs
        playerMovementFlat = myControls.PlayerMoveControls.Move.ReadValue<Vector2>();
        playerMovementTotal.x = playerMovementFlat.x;
        playerMovementTotal.z = playerMovementFlat.y;

        // Get vertical movement

        // Move
        charCon.Move(playerMovementTotal * moveSpeed * Time.deltaTime);
    }
}
