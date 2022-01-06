using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonData
{
    public string text;
    public System.Action action;

    public OptionButtonData(string text, System.Action action)
    {
        this.text = text;
        this.action = action;
    }
}

public class ChoiceUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject panel;

    private void ClearPanel()
    {
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);
    }

    public void ShowOptions(OptionButtonData[] options)
    {
        ClearPanel();

        foreach (var option in options)
        {
            ShowOption(option);
        }

        panel.SetActive(true);
    }

    private void ShowOption(OptionButtonData option)
    {
        GameObject instance = Instantiate(buttonPrefab, panel.transform, true);
        instance.GetComponentInChildren<TextMeshProUGUI>().text = option.text;
        instance.GetComponent<Button>().onClick.AddListener(() =>
        {
            panel.SetActive(false);
            option.action.Invoke();
        });
    }
}
