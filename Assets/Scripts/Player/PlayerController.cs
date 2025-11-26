using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //シングルトン
    public static PlayerController singleton;

    //プレイヤー操作InputAction
    InputAction fire;
    InputAction bomb;
    InputAction movementInput;
    InputAction focus;

    //移動速度------------------
    //現在速度
    float moveSpeed = 7f;
    //普通速度
    [Tooltip("Normal movement speed.")]
    public float moveSpeedNormal = 7f;
    //フォーカス速度
    [Tooltip("Focused movement speed.")]
    public float moveSpeedFocused = 3.5f;
    //-------------------------

    //プレイヤーの弾スポナー参照
    [Tooltip("References to the player's bullet spawners.")]
    public GameObject emitter;
    public GameObject emitter2;
    public GameObject emitter3;

    //スペルカードプレハブ
    [Tooltip("The prefab to spawn when the spellcard button is pressed.")]
    public GameObject spell;

    //移動ベクトル
    Vector3 movementVector;

    //ロードの際
    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
        } else singleton = this;
    }

    //削除の際
    private void OnDestroy()
    {
        if (singleton == this)
        {
            singleton = null;
        }
    }

    //発射開始
    void EnableEmitters()
    {
        emitter.SetActive(true);
        emitter2.SetActive(true);
        emitter3.SetActive(true);
    }

    //発射停止
    void DisableEmitters()
    {
        emitter.SetActive(false);
        emitter2.SetActive(false);
        emitter3.SetActive(false);
    }

    //入力機能管理
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

    //全てのInputActionをセットアップ
    void SetupAllInputActions()
    {
        fire = InputManager.inputActions.Default.Fire;
        bomb = InputManager.inputActions.Default.Bomb;
        movementInput = InputManager.inputActions.Default.Movement;
        focus = InputManager.inputActions.Default.Focus;
    }

    //入力有効
    public void EnableAllInputActions()
    {
        EnableDestructiveInputActions();
        EnableMovementActions();
        
    }

    //入力無効
    public void DisableAllInputActions()
    {
        DisableDestructiveInputActions();
        DisableMovementActions();
    }

    //発射とスペルカード入力有効
    public void EnableDestructiveInputActions()
    {
        EnableFireAction();
        EnableBombAction();
    }

    //発射とスペルカード入力無効
    public void DisableDestructiveInputActions()
    {
        DisableFireAction();
        DisableBombAction();
        DisableEmitters();
    }

    //移動入力有効
    public void EnableMovementActions()
    {
        focus.performed += OnFocusPressed;
        focus.canceled += OnFocusReleased;
        focus.Enable();
        movementInput.Enable();
    }

    //移動入力無効
    public void DisableMovementActions()
    {
        focus.performed -= OnFocusPressed;
        focus.canceled -= OnFocusReleased;
        focus.Disable();
        movementInput.Disable();
    }

    //発射入力有効
    void EnableFireAction()
    {
        fire.performed += OnFirePressed;
        fire.canceled += OnFireReleased;
        fire.Enable();
    }

    //発射入力無効
    void DisableFireAction()
    {
        fire.performed -= OnFirePressed;
        fire.canceled -= OnFireReleased;
        fire.Disable();
    }

    //スペルカード入力有効
    void EnableBombAction()
    {
        bomb.performed += OnBombPressed;
        bomb.Enable();
    }

    //スペルカード入力無効
    void DisableBombAction()
    {
        bomb.performed -= OnBombPressed;
        bomb.Disable();
    }

    //発射ボタンを押したら
    void OnFirePressed(InputAction.CallbackContext callbackContext)
    {
        EnableEmitters();
    }

    //発射ボタンを離したら
    void OnFireReleased(InputAction.CallbackContext callbackContext) {
        DisableEmitters();
    }

    //スペルカードボタンを押したら
    void OnBombPressed(InputAction.CallbackContext callbackContext)
    {
        //スペルカードが残っているかを確認
        if(Parameters.singleton.bombs >= 0)
        {
            //スペルカードを起動
            Instantiate(spell, transform);
            Parameters.singleton.bombs--;
            Parameters.singleton.updateBombDisplay();
            //プレイヤーを一旦無敵にする
            StartCoroutine(BombInvulnerability());
        }
    }

    //フォーカスボタンを押したら
    void OnFocusPressed(InputAction.CallbackContext callbackContext)
    {
        moveSpeed = moveSpeedFocused;
    }

    //フォーカスボタンを離したら
    void OnFocusReleased(InputAction.CallbackContext callbackContext)
    {
        moveSpeed = moveSpeedNormal;
    }

    void Update() 
    {
        //移動入力を読み取る
        //read the value from the movement input action
        movementVector = movementInput.ReadValue<Vector2>();

        //プレイヤーの位置を画面内に制限
        //constrain player position to the visible play area
        if ((transform.position.x <= -11.5f && movementVector.x < 0) || (transform.position.x > 11.5f && movementVector.x > 0))
        {
            movementVector.x = 0;
        }
        if ((transform.position.y <= -9.05f && movementVector.y < 0) || (transform.position.y > 9.05f && movementVector.y > 0))
        {
            movementVector.y = 0;
        }

        //移動を正気化
        movementVector.Normalize();
    }

    void FixedUpdate() //movement
    {
        //移動する
        transform.position += moveSpeed * Time.fixedDeltaTime * movementVector;
    }
    //入力機能管理終わり
    //Input action handling END----------------------------------

    //スペルカードの一旦無敵機能
    IEnumerator BombInvulnerability()
    {
        //無敵にする
        GetComponent<PlayerCollision>().isColliding = true;
        //スペルカードのクールダウンを開始
        DisableBombAction();
        //指定秒数を待つ
        yield return new WaitForSeconds(2f);
        //無敵状態を解除
        GetComponent<PlayerCollision>().isColliding = false;
        //スペルカードのクールダウンを終了
        EnableBombAction();
    }
}
