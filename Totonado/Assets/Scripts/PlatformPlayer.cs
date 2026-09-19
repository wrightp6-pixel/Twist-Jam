using UnityEngine;
using UnityEngine.Splines;

public class PlatformPlayer : MonoBehaviour
{
    [SerializeField] private bool hasPlayer;
    public Vector3 prevTransform;
    [SerializeField] PlayerMove player;
    [SerializeField] GameObject platform;
    public SplineAnimate splineAn;

    [SerializeField] SplineContainer splineA;
    [SerializeField] SplineContainer splineB;
    private float splineTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasPlayer = false;
        prevTransform = platform.transform.position;
        splineAn = platform.GetComponent<SplineAnimate>();
        // Total time to go accross complete spline
        splineTime = splineAn.Duration;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasPlayer)
        {

            // Calculate distance moved
            Vector3 vec = (platform.transform.position  - prevTransform);
            Debug.Log("previous" + prevTransform);
            Debug.Log("current" + platform.transform.position);
            Debug.Log("distance" + vec);

            // Move player based on this
            //player.transform.Translate(vec);
            player.charCon.Move(vec);

            //Save current position for next frame's calculation
            prevTransform = platform.transform.position;


        }


    }

    // ================== Collisions ================== 
    public void switchSplines()
    {
        // Switch from froward moving splien to backwards moving spline
        if(splineAn.Container.Equals(splineA))
        {
            Debug.Log("Switch");
            splineAn.Container = splineB;
            splineAn.ElapsedTime = splineTime - (splineAn.ElapsedTime % splineTime);
            splineAn.ObjectForwardAxis = SplineComponent.AlignAxis.ZAxis;

        } else
        {
            splineAn.Container = splineA;
            splineAn.ElapsedTime = splineTime - (splineAn.ElapsedTime % splineTime);
            splineAn.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeZAxis;
        }
        
    }



    public void OnTwist()
    {
        switchSplines();
    }


    // ================== Collisions ================== 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharacter")) 
        {
            hasPlayer = true;
            Debug.Log("hasPlayer: " + hasPlayer);
            prevTransform = platform.transform.position;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCharacter"))
        {
            hasPlayer = false;
            Debug.Log("hasPlayer: " + hasPlayer);
        }
    }
}
