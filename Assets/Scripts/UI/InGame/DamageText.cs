using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
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
        statusText = GetComponent<TextMeshProUGUI>();
        startLoc = transform.position;
    }

    private void OnDisable()
    {
        transform.position = startLoc;
        elapsedTime = 0;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        Timer();
    }

    public void SetText(float damage)
    {
        statusText.text = damage.ToString();
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
