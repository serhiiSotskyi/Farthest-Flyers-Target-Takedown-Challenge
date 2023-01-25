using UnityEngine;
using TMPro;

public class PlaneController : MonoBehaviour
{
    public VariableJoystick varJoystick;
    Rigidbody rb;

    private float horizontalInput;
    private float verticalInput;

    public float forwardSpeed = 15f;
    public float horizontalSpeed = 4f;
    public float verticalSpeed = 4f;

    public float smoothness = 5f;
    public float rotationSmoothness = 5f;

    public float maxHorizontalRotation = 0.05f;
    public float maxVerticalRotation = 0.06f;

    private float forwardSpeedMultiplier = 100f;
    private float speedMultiplier = 1000f;

    public static float timeLeft = 10;
    public static float acceleration = 1f;
    private float score;
    private float highscore;
    

    [SerializeField] Transform propella;
    public static AudioSource engineSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        engineSound.enabled = false;
        
    }
    private void Start()
    {
        acceleration = 1f;
    }
    private float radius = 2f;
    private void Update()
    {
        JoyStickInput();
        PropellaRotation(propella);
        DetectTarget();
        score = transform.position.z / 10;
        
        if (score >= PlayerPrefs.GetFloat("highScore", highscore))
        {
            highscore = score;
            PlayerPrefs.SetFloat("highScore", highscore);
        }
        
        Acceleration();
    }

    private void FixedUpdate()
    {
        if (!GameController.isLose && !GameController.noGame && !CanvasButton.isPaused)
        {
            HandlePlaneMovement();
            HandlePlaneRotation();
            engineSound.enabled = true;
        }
        else if(GameController.isLose || GameController.noGame || CanvasButton.isPaused)
        {
            engineSound.enabled = false;
        }
        timeLeft -= Time.deltaTime * acceleration;
        UpdateTime();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameController.isLose = true;
        }
        
    }

    private void JoyStickInput()
    {
        horizontalInput = varJoystick.Horizontal;
        verticalInput = varJoystick.Vertical;
    }
    private void HandlePlaneMovement()
    {
        rb.velocity = new Vector3(
            rb.velocity.x,
            rb.velocity.y,
            forwardSpeed * forwardSpeedMultiplier * Time.deltaTime);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, forwardSpeed * speedMultiplier * Time.deltaTime);

        float xVelocity = horizontalInput * speedMultiplier * horizontalSpeed * Time.deltaTime;
        float yVelocity = -verticalInput * speedMultiplier * verticalSpeed * Time.deltaTime;

        rb.velocity = Vector3.Lerp(
            rb.velocity,
            new Vector3(xVelocity, yVelocity, rb.velocity.z),
            Time.deltaTime * smoothness);
    }

    private void HandlePlaneRotation()
    {
        float horizontalRotation = -horizontalInput * maxHorizontalRotation;
        float verticalRotation = verticalInput * maxVerticalRotation;

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            new Quaternion(verticalRotation, transform.rotation.y, horizontalRotation, transform.rotation.w),
            Time.deltaTime * rotationSmoothness);
    }

    private void PropellaRotation(Transform propella)
    {
        propella.Rotate(Vector3.right * forwardSpeed);
    }
    
    private void DetectTarget()
    {
        Collider[] Colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider target in Colliders)
        {
            if(target.gameObject.tag == "Target")
            {
                timeLeft += 3;
                Destroy(target);
            }
        }
    }
    public TextMeshProUGUI time;
    public TextMeshProUGUI scores;
    private void UpdateTime()
    {
        time.text = "TIME LEFT: " + timeLeft.ToString("F0");
        scores.text = "<color=#FF8000>HIGH SCORE: </color>" + "<color=#FF8000>" + PlayerPrefs.GetFloat("highScore", highscore).ToString("F0") + "</color>" + "\n";
        scores.text += "SCORE: " + score.ToString("F0");
    }
    private float checkScore = 500f;
    private void Acceleration()
    {
        if(checkScore < (score - 500f))
        {
            acceleration += .2f;
            checkScore = score;
        }
    }
}
