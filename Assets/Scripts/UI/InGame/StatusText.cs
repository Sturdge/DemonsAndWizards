using TMPro;
using UnityEngine;

public class StatusText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float duration = 0;
    [SerializeField]
    private float speed = 0;

    private float elapsedTime;
    private Vector3 startLoc;
    private TextMeshProUGUI statusText;

    private void Awake()
    {
        startLoc = transform.localPosition;
        statusText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Timer();
    }

    private void OnDisable()
    {
        transform.localPosition = startLoc;
    }

    public void Activate(StatusEffect newStatus)
    {
        statusText.text = $"{newStatus.Name}!";
        statusText.color = newStatus.TextColour;
        gameObject.SetActive(true);
    }

    private void Timer()
    {
        if (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;
            CheckTimerEnd();
        }
    }

    private void CheckTimerEnd()
    {
        if (elapsedTime > duration)
        {
            gameObject.SetActive(false);
        }
    }
}