using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    private Vector2 _movementInput;
    private float speed;

    [Header("Jump")]
    public float jumpForce;

    [Header("Look")]
    public Transform RotatePivot;
    public float camMinX;
    public float camMaxX;
    public float lookSensitivity;
    private float camXRot;
    private Vector2 _mouseDelta;

    [Header("Popup")]
    public GameObject statsCanvas;

    [HideInInspector]
    public bool isCursorLock = true;

    private bool isAttack = false;
    private float attackTime;
    private float[] recoilPower = { 1, 1, 5 };
    private Animator _animator;

    private Rigidbody _rigidbody;


    private void Awake()
    {
        if(instance == null) instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
        attackTime += Time.deltaTime;
        if(isAttack && isCursorLock)
        {
            if(attackTime >= GameManager.instance.GetAttackSpeed())
            {
                switch (GameManager.instance.CurrentWeapon.weaponType)
                {
                    case WeaponType.Pistol:
                    case WeaponType.Rifle:
                        BulletManager.instance.Shoot();
                        break;
                    case WeaponType.Shotgun:
                        BulletManager.instance.ShotgunShoot();
                        break;
                }
                _animator.SetTrigger("Attack");
                _animator.speed = 1 / GameManager.instance.GetAttackSpeed();
                HandleRecoil();
                attackTime = 0f;
            }
        }
    }

    private void HandleRecoil()
    {
        transform.Translate(Vector3.up * 0.1f);

        camXRot += recoilPower[(int)GameManager.instance.CurrentWeapon.weaponType] * lookSensitivity;
        camXRot = Mathf.Clamp(camXRot, camMinX, camMaxX);
        RotatePivot.localEulerAngles = new Vector3(-camXRot, 0f, 0f);

        //transform.eulerAngles += new Vector3(-recoilPower[(int)GameManager.instance.CurrentWeapon.weaponType], 0f, 0f);
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Move()
    {
        var dir = transform.forward * _movementInput.y + transform.right * _movementInput.x;
        dir *= speed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }



    private void Look()
    {
        if(!isCursorLock) return;

        camXRot += _mouseDelta.y * lookSensitivity;
        camXRot = Mathf.Clamp(camXRot, camMinX, camMaxX);
        RotatePivot.localEulerAngles = new Vector3(-camXRot, 0f, 0f);

        transform.eulerAngles += new Vector3(0f, _mouseDelta.x * lookSensitivity, 0f);
    }

    public void SetCursor(bool isCursor)
    {
        Cursor.lockState = isCursor ? CursorLockMode.Locked : CursorLockMode.None;
        isCursorLock = isCursor;
    }

    public void ChangeAnimator()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    #region Input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            speed = GameManager.instance.playerCurrentStats.speed;
            _movementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            _movementInput = Vector2.zero;
        }
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            isAttack = true;
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isAttack = false;
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        _mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            speed = GameManager.instance.playerCurrentStats.runSpeed;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            speed = GameManager.instance.playerCurrentStats.speed;
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            SetCursor(false);
            statsCanvas.GetComponent<StatsUI>().interact();
        }
    }
    #endregion

}
