using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Attacks/Skill")]
public class Skill : ScriptableObject
{
    [Header("Skill Attributes")]
    [SerializeField]
    private int _manaCost;
    [SerializeField]
    private int _baseDamage;
    [SerializeField]
    private int cooldown;
    [SerializeField]
    private GameObject objectToSpawn;

    private float cooldownTimer;
    private bool isOnCooldown;
    private PlayerController parent;

    public int ManaCost { get { return _manaCost; } }
    public int BaseDamage { get { return _baseDamage; } }

    public void Initialisation(PlayerController controller)
    {
        isOnCooldown = false;
        cooldownTimer = 0;
        parent = controller;
    }

    public void DoSkill()
    {

    }

    public void UpdateCooldown(float deltatime)
    {
        if (isOnCooldown)
        {
            if (cooldownTimer < cooldown)
            {
                cooldownTimer += deltatime;
                CheckTimerEnd();
            }
        }
    }

    private void CheckTimerEnd()
    {
        if (cooldownTimer >= cooldown)
        {
            cooldownTimer = 0;
            isOnCooldown = false;
        }
    }
}