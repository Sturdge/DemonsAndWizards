using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float duration;
    [SerializeField]
    private float speed;

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
        ResetPosition();
    }

    public void Activate(float damage)
    {
        elapsedTime = 0;
        gameObject.SetActive(false);
        statusText.text = damage.ToString();
        gameObject.SetActive(true);
    }

    private void ResetPosition()
    {
        transform.localPosition = startLoc;
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
