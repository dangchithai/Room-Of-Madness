using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject TextContainerUI;
    public GameObject TextPrefabUI;

    private List<FloatingText> floatingTexts = new List<FloatingText>(); // pooling purpose
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        foreach (var item in floatingTexts)
        {
            item.UpdateFloatingText();
        }
    }

    private FloatingText GetFloatingText()
    {
        FloatingText floatingTxt = floatingTexts.Find(x => !x.Active);

        if (floatingTxt == null)
        {
            floatingTxt = new FloatingText();
            floatingTxt.FloatingTextPrefab = Instantiate(TextPrefabUI);
            floatingTxt.FloatingTextPrefab.transform.SetParent(TextContainerUI.transform);
            floatingTxt.Content = floatingTxt.FloatingTextPrefab.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(floatingTxt);
        }

        return floatingTxt;
    }

    public void Show(string msg, Vector3 position, float duration)
    {
        FloatingText floatingTxt = this.GetFloatingText();
        floatingTxt.Content.text = msg;
        floatingTxt.FloatingTextPrefab.transform.position = mainCam.WorldToScreenPoint(position);
        floatingTxt.Duration = duration;

        floatingTxt.Show();
    }
}
