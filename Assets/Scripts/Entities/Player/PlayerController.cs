using StateMachine;
using StateMachine.PlayerStates;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity
{

    #region Serialized Fields

    [Header("Player Properties")]
    [SerializeField]
    private float _chargeMultiplier = 2;
    [SerializeField]
    private Transform _spellPoint = null;
    [SerializeField]
    private Spell _defaultAttack = null;
    [SerializeField]
    private Skill skill = null;

    [Header("Input")]
    [SerializeField]
    private InputActionAsset _playerControls;
    [SerializeField]
    private InputActionAsset _buildUIControls;

    #endregion

    #region Fields

    private bool isShooting;
    private float activationValue;
    private BuildUI buildUI;

    #endregion

    #region Auto Properties

    public int PlayerID { get; private set; }
    public int SpellLevel { get; private set; }
    public int SkillLevel { get; private set; }
    public int Money { get; private set; }
    public bool IsStrafing { get; private set; }
    public bool IsCharging { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public CharacterController PlayerMovementController { get; private set; }
    public StateMachine<PlayerController> StateMachine { get; private set; }
    public PlayerInput Input { get; private set; }
    public TurretBuildZone CurrentBuildZone { get; private set; }

    #endregion

    #region Full Properties

    public float ChargeMultiplier => _chargeMultiplier;
    public Transform SpellPoint => _spellPoint;
    public Spell DefaultAttack => _defaultAttack;
    public InputActionAsset PlayerControls => _playerControls;
    public InputActionAsset BuildUIControls => _buildUIControls;

    #endregion

    #region Unity Callbacks

    protected override void Awake()
    {
        base.Awake();
        StatusEffects = new List<StatusEffect>();
        StateMachine = new StateMachine<PlayerController>(this);
        PlayerMovementController = GetComponent<CharacterController>();
        buildUI = GetComponent<BuildUI>();
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        SpellLevel = 1;
        activationValue = 0.15f;
        PlayerID = GameManager.Instance.EntityManager.PlayerID;
        DefaultAttack.Initialisation(this);
        skill.Initialisation(this);
        StateMachine.ChangeState(IdleState.Instance);
        CurrentBuildZone = null;
        Input.actions = PlayerControls;
        ModifiyMoney(150);
    }

    private void Update()
    {
        PlayerMovementController.Move(Physics.gravity * Time.deltaTime);
        StateMachine.Update();
        DefaultAttack.UpdateCooldown(Time.deltaTime);
        skill.UpdateCooldown(Time.deltaTime);
        UpdateStatus();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TurretZone"))
        {
            CurrentBuildZone = other.GetComponent<TurretBuildZone>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TurretZone"))
        {
            CurrentBuildZone = null;
        }
    }

    #endregion

    #region Public Methods

    public void Shooting()
    {
        if (isShooting)
            DefaultAttack.DoAttack();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (CurrentHitPoints <= 0)
            GameManager.Instance.EntityManager.OnPlayerDie(this);
    }

    public void ModifiyMoney(int value)
    {
        Money += value;
        GameManager.Instance.UIManager.UpdateGoldText();
    }

    #endregion

    #region Private Methods

    private void OnMovement(InputValue inputValue)
    {
        if (StateMachine.CurrentState != StunState.Instance)
            StateMachine.ChangeState(MoveState.Instance);

        MovementInput = inputValue.Get<Vector2>();
    }

    private  void OnShoot(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        isShooting = value > activationValue;
    }

    private void OnSpecial(InputValue inputValue)
    {
        if (StateMachine.CurrentState != StunState.Instance)
            skill.DoSkill();
    }

    private void OnStrafe(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        IsStrafing = value >= activationValue;
    }

    private void OnCharge(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        IsCharging = value >= activationValue;
    }

    private void OnInteract(InputValue inputValue)
    {
        if(CurrentBuildZone != null)
        {
            if (CurrentBuildZone.builtTurret == -1)
            {
                if (!buildUI.IsOpen)
                {
                    Input.SwitchCurrentActionMap("BuildUI");
                    MovementInput = Vector2.zero;
                    buildUI.OpenBuildUI(this);
                }
            }
        }
    }

    #endregion

    #region Protected Methods

    #endregion
}