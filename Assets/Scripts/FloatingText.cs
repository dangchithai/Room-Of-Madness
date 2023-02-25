using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText
{
    public bool Active;
    public GameObject FloatingTextPrefab;
    public TextMeshProUGUI Content;
    public float Duration = 3f;
    public float LastShown;

    public void Show()
    {
        Active = true;
        LastShown = Time.time;
        FloatingTextPrefab.SetActive(Active);
    }

    public void Hide()
    {
        Active = false;
        FloatingTextPrefab.SetActive(Active);
    }

    public void UpdateFloatingText()
    {
        if (!Active)
        {
            return;
        }

        if (Time.time - LastShown > Duration)
        {
            this.Hide();
        }
    }
}
