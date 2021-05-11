using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Movement config")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 100f;

    [Header("Reference")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float graviScale;

    private float _gravity;
    private bool _jumping;
    private bool _gameOver;

    #region Awake OnDestroy Start Update
    private void Awake()
    {
        Exit.Finish += GameOver;
        Savior.RestartLvl += GameOver;
    }

    private void OnDestroy()
    {
        Exit.Finish -= GameOver;
        Savior.RestartLvl -= GameOver;
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_gameOver) return;

        Move();
        Rotate();
    }
    #endregion

    #region Move Rotate
    private void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        animator.SetFloat("HorizontalMove", inputH);
        animator.SetFloat("VerticalMove", inputV);
        animator.SetBool("Moving", inputH != 0 || inputV != 0);

        Vector3 moveDirection = transform.forward * inputV + transform.right * inputH;
        if (moveDirection.magnitude > 1) moveDirection.Normalize();

        if (characterController.isGrounded)
        {
            _jumping = Input.GetButton("Jump");
            _gravity = _jumping ? jumpHeight : 0;
        }
        else
        {
            if (characterController.velocity.y == 0 && _jumping) _gravity = -0.01f;
            else _gravity += graviScale * Physics.gravity.y * Time.deltaTime;
        }
        moveDirection.y = _gravity;

        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);
        
    }

    private void Rotate()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseHorizontal * rotateSpeed * Time.deltaTime);
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    #region GameOver
    public void GameOver(bool win)
    {
        PauseGame(true);
        if (!win) Dead();
    }

    public void Dead()
    {
        print("i'm dead");
    }

    private void PauseGame(bool isPause)
    {
        _gameOver = isPause;
        Cursor.visible = isPause;
        Cursor.lockState = isPause ? CursorLockMode.None : CursorLockMode.Locked;
    }
    #endregion
}
