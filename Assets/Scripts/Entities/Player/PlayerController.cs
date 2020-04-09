using StateMachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerStates
{
    idle,
    move,
    stun
}

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity
{

    [Header("Player Properties")]
    [SerializeField]
    private float _chargeMultiplier = 2;
    [SerializeField]
    private Spell defaultAttack;
    [SerializeField]
    private Skill skill;

    private bool isShooting;
    private float activationValue;

    public int PlayerID { get; private set; }
    public int SpellLevel { get; private set; }
    public int SkillLevel { get; private set; }
    public bool IsStrafing { get; private set; }
    public bool IsCharging { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public StateMachine<PlayerController> StateMachine { get; private set; }
    public CharacterController PlayerMovementController { get; private set; }
    public Dictionary<PlayerStates, State<PlayerController>> States { get; private set; }

    public float ChargeMultiplier => _chargeMultiplier;

    private void Awake()
    {
        StateMachine = new StateMachine<PlayerController>(this);
        PlayerMovementController = GetComponent<CharacterController>();

        MovementMultiplier = 1;

        States = new Dictionary<PlayerStates, State<PlayerController>>()
        {
            { PlayerStates.idle, new IdleState(this) },
            { PlayerStates.move, new MoveState(this) },
            { PlayerStates.stun, new StunState(this) }
        };

        PlayerID = GameManager.Instance.PlayerID;

        defaultAttack.Initialisation(this);
    }

    private void OnEnable()
    {
        activationValue = 0.15f;
        StateMachine.ChangeState(States[PlayerStates.idle]);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
        defaultAttack.UpdateCooldown(Time.deltaTime);
    }

    public void Shooting()
    {
        if (isShooting)
            defaultAttack.DoAttack();
    }

    public void OnMovement(InputValue inputValue)
    {
        if (StateMachine.CurrentState != States[PlayerStates.stun])
            StateMachine.ChangeState(States[PlayerStates.move]);

        MovementInput = inputValue.Get<Vector2>();
    }

    private void OnShoot(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
        isShooting = value > activationValue;
    }

    private void OnSpecial(InputValue inputValue)
    {
        float value = inputValue.Get<float>();
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
}