using UnityEngine;

public class EnemyRunController : MonoBehaviour
{
    [SerializeField] EnemyRunData Data;
    private Rigidbody2D rb;
    [HideInInspector] public bool IsFacingRight;
    private GameObject player;
    private Vector2 direction;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.Find("Player");
        IsFacingRight = true;
    }

    void Update()
    {
        if (player == null)
        {
            Debug.Log("Player Not found");
            return;
        } 

        direction = (player.transform.position - transform.position).normalized;

        //Debug.Log(direction.ToString());

        if (direction.x != 0)
        {
            CheckDirectionToFace(direction.x > 0);
        }
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        float targetSpeedX = direction.x * Data.maxSpeed;
        float targetSpeedY = direction.y * Data.maxSpeed;

        float accelRateX;
        float accelRateY;

        accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? Data.accelAmount : Data.accelAmount;
        accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? Data.accelAmount : Data.accelAmount;
    
        if (Data.doConserveMomentum && Mathf.Abs(rb.linearVelocityX) > Mathf.Abs(targetSpeedX) && Mathf.Sign(rb.linearVelocityX) == Mathf.Sign(targetSpeedX) && Mathf.Abs(targetSpeedX) > 0.01f)
        {
            accelRateX = 0;
        }

        if (Data.doConserveMomentum && Mathf.Abs(rb.linearVelocityY) > Mathf.Abs(targetSpeedY) && Mathf.Sign(rb.linearVelocityY) == Mathf.Sign(targetSpeedY) && Mathf.Abs(targetSpeedY) > 0.01f)
        {
            accelRateY = 0;
        }

        float speedDiffX = targetSpeedX - rb.linearVelocityX;
        float speedDiffY = targetSpeedY - rb.linearVelocityY;

        

        float movementX = speedDiffX * accelRateX;
        float movementY = speedDiffY * accelRateY;

        rb.AddForce(movementX * Vector2.right, ForceMode2D.Force);
        rb.AddForce(movementY * Vector2.up, ForceMode2D.Force);
    }

    private void Turn()
    {
        // sprite поворачивается, но оружие продолжает стрелять в одном направлении
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }

    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
        {
            Turn();
        }
    }
}
