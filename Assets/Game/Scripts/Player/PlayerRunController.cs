using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerRunController : MonoBehaviour
{
    private Rigidbody2D rb;

    [HideInInspector] public bool IsFacingRight;
    private Vector2 moveValue;
    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Start()
    {
        IsFacingRight = true;
    }

    private void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();   

        if(moveValue.x != 0)
        {
            CheckDirectionToFace(moveValue.x > 0);
        }
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        float targetSpeedX = moveValue.x * Managers.Player.PlayerStats.speed * Managers.Player.PlayerStats.speedAmplifier;
        float targetSpeedY = moveValue.y * Managers.Player.PlayerStats.speed * Managers.Player.PlayerStats.speedAmplifier;

        float accelRateX;
        float accelRateY;

        accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? Managers.Player.PlayerStats.accelAmount : Managers.Player.PlayerStats.accelAmount;
        accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? Managers.Player.PlayerStats.accelAmount : Managers.Player.PlayerStats.accelAmount;

        if (Managers.Player.PlayerStats.doConserveMomentum && Mathf.Abs(rb.linearVelocityX) > Mathf.Abs(targetSpeedX) && Mathf.Sign(rb.linearVelocityX) == Mathf.Sign(targetSpeedX) && Mathf.Abs(targetSpeedX) > 0.01f)
        {
            accelRateX = 0;
        }

        if (Managers.Player.PlayerStats.doConserveMomentum && Mathf.Abs(rb.linearVelocityY) > Mathf.Abs(targetSpeedY) && Mathf.Sign(rb.linearVelocityY) == Mathf.Sign(targetSpeedY) && Mathf.Abs(targetSpeedY) > 0.01f)
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
