using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController singleton;

    InputAction fire;
    InputAction bomb;
    InputAction movementInput;
    InputAction focus;

    public float moveSpeed = 7f;
    public float moveSpeedNormal = 7f;
    public float moveSpeedFocused = 3.5f;

    public GameObject emitter;
    public GameObject emitter2;
    public GameObject emitter3;
    public GameObject spell;
    //public bool firingDisabled = false;
    //public bool isPostBoss = false;
    //bool spellCooldown = false;
    //int framesSinceEnemyCardStart = 0;

    Vector3 movementVector;

    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        } else singleton = this;
    }

    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    void EnableEmitters()
    {
        emitter.SetActive(true);
        emitter2.SetActive(true);
        emitter3.SetActive(true);
    }

    void DisableEmitters()
    {
        emitter.SetActive(false);
        emitter2.SetActive(false);
        emitter3.SetActive(false);
    }

    //Input action handling----------------------------------------
    private void OnEnable()
    {
        SetupAllInputActions();
        EnableAllInputActions();
    }

    private void OnDisable()
    {
        DisableAllInputActions();
    }

    void SetupAllInputActions()
    {
        fire = InputManager.inputActions.Default.Fire;
        bomb = InputManager.inputActions.Default.Bomb;
        movementInput = InputManager.inputActions.Default.Movement;
        focus = InputManager.inputActions.Default.Focus;
    }

    public void EnableAllInputActions()
    {
        EnableDestructiveInputActions();
        EnableMovementActions();
        
    }

    public void DisableAllInputActions()
    {
        DisableDestructiveInputActions();
        DisableMovementActions();
    }

    public void EnableDestructiveInputActions()
    {
        EnableFireAction();
        EnableBombAction();
    }

    public void DisableDestructiveInputActions()
    {
        DisableFireAction();
        DisableBombAction();
        DisableEmitters();
    }

    public void EnableMovementActions()
    {
        focus.performed += OnFocusPressed;
        focus.canceled += OnFocusReleased;
        focus.Enable();
        movementInput.Enable();
    }

    public void DisableMovementActions()
    {
        focus.performed -= OnFocusPressed;
        focus.canceled -= OnFocusReleased;
        focus.Disable();
        movementInput.Disable();
    }

    void EnableFireAction()
    {
        fire.performed += OnFirePressed;
        fire.canceled += OnFireReleased;
        fire.Enable();
    }

    void DisableFireAction()
    {
        fire.performed -= OnFirePressed;
        fire.canceled -= OnFireReleased;
        fire.Disable();
    }

    void EnableBombAction()
    {
        bomb.performed += OnBombPressed;
        bomb.Enable();
    }

    void DisableBombAction()
    {
        bomb.performed -= OnBombPressed;
        bomb.Disable();
    }

    void OnFirePressed(InputAction.CallbackContext callbackContext)
    {
        EnableEmitters();
    }

    void OnFireReleased(InputAction.CallbackContext callbackContext) {
        DisableEmitters();
    }

    void OnBombPressed(InputAction.CallbackContext callbackContext)
    {
        //
        //if(!spellCooldown && Parameters.singleton.bombs >= 0)
        if(Parameters.singleton.bombs >= 0)
        {
            Instantiate(spell, transform);
            Parameters.singleton.bombs--;
            Parameters.singleton.updateBombDisplay();
            StartCoroutine(BombInvulnerability());
        }
    }

    void OnFocusPressed(InputAction.CallbackContext callbackContext)
    {
        moveSpeed = moveSpeedFocused;
    }

    void OnFocusReleased(InputAction.CallbackContext callbackContext)
    {
        moveSpeed = moveSpeedNormal;
    }

    void Update() 
    {
        //read the value from the movement input action
        movementVector = movementInput.ReadValue<Vector2>();

        //constrain player position to the visible play area
        if ((transform.position.x <= -11.5f && movementVector.x < 0) || (transform.position.x > 11.5f && movementVector.x > 0))
        {
            movementVector.x = 0;
        }
        if ((transform.position.y <= -9.05f && movementVector.y < 0) || (transform.position.y > 9.05f && movementVector.y > 0))
        {
            movementVector.y = 0;
        }

        movementVector.Normalize();
        
    }

    void FixedUpdate() //movement
    {
        transform.position += movementVector * moveSpeed * Time.fixedDeltaTime;

        /*if (!fire.enabled && !isPostBoss)
        {
            if(framesSinceEnemyCardStart >= 100)
            {
                EnableDestructiveInputActions();
                framesSinceEnemyCardStart = 0;
            }
            framesSinceEnemyCardStart++;
        }*/
    }
    //Input action handling END----------------------------------

    IEnumerator BombInvulnerability()
    {
        GetComponent<PlayerCollision>().isColliding = true;
        DisableBombAction();
        yield return new WaitForSeconds(2f);
        GetComponent<PlayerCollision>().isColliding = false;
        EnableBombAction();
    }
}
