using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public Transform ObstacleManager;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject restartBackground;
    [SerializeField] private GameObject onGame;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject title;

    public static bool isLose;
    public static bool noGame;

    private void Start()
    {
        isLose = false;
        noGame = true;
        restartBackground.SetActive(false);
        onGame.SetActive(true);
    }

    private void Update()
    {
        if(PlaneController.timeLeft < 0)
        {
            isLose = true;
        }
        
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            noGame = false;
            hand.SetActive(false);
            title.SetActive(false);
        }
        if (noGame)
        {
            plane.GetComponent<PlaneController>().enabled = false;
            plane.GetComponent<AudioSource>().enabled = false;
            plane.GetComponent<Rigidbody>().useGravity = false;
            onGame.SetActive(false);
        }
        else if(!noGame)
        {
            plane.GetComponent<Rigidbody>().useGravity = true;
            plane.GetComponent<PlaneController>().enabled = true;
            onGame.SetActive(true);
        }
        if (isLose)
        {
            Lose();
        }
        
        if(plane != null)
        {
            if (isLose && noGame)
            {
                Lose();
            }
        }
        else
        {
            if (isLose && noGame)
            {
                plane.SetActive(true);
            }
        }
    }

    private void Lose()
    {
        plane.SetActive(false);
        Instantiate(explosion, plane.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        restartBackground.SetActive(true);
        onGame.SetActive(false);
        isLose = false;
        noGame = true;
        PlaneController.timeLeft = 10f;
        PlaneController.acceleration = 1f;
    }
}
