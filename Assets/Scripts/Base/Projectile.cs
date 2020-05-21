using System.Collections;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile Attributes")]
    [SerializeField]
    private string _name = string.Empty;
    [SerializeField]
    private string _description = string.Empty;
    [SerializeField]
    private StatusEffect _statusEffect = null;
    [SerializeField, Range(0, 1)]
    private float _statusChance = 0;
    [SerializeField]
    private float _projectileSpeed = 0;
    [SerializeField]
    private float timeout = 0;
    [SerializeField]
    protected bool persists;

    protected float damage;

    public string Name => _name;
    public string Description => _description;
    protected StatusEffect StatusEffect => _statusEffect;
    protected float StatusChance => _statusChance;
    private void OnEnable()
    {
        StartCoroutine(TimedDisable(timeout));
    }

    private void Update()
    {
        transform.position += transform.forward * _projectileSpeed * Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }

    protected abstract void CheckCollision(Collider other);

    protected void CollisionLogic(Collider other)
    {
        Entity target = other.GetComponent<Entity>();
        if (target != null)
        {
            target.TakeDamage(damage);
            float statusRoll = Random.Range(0, 1);
            if (StatusEffect != null)
            {
                if (statusRoll <= StatusChance)
                {
                    target.SetStatus(StatusEffect);
                }
            }
            if (!persists)
                gameObject.SetActive(false);
        }
    }

    private IEnumerator TimedDisable(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}