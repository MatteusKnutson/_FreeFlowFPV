using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_AlphaTestMenu : MonoBehaviour
{
    public Button btn_form;
    public Button btn_close;

    void OnFormClicked(ClickEvent evt)
    {
        Application.OpenURL("https://forms.gle/5PHrf1ednA81PN4u8");
    }

    void OnCloseClicked(ClickEvent evt)
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;

        var root = GetComponent<UIDocument>().rootVisualElement;

        btn_form = root.Q<Button>("btn_openForm") as Button;
        btn_close = root.Q<Button>("btn_close") as Button;

        btn_form.RegisterCallback<ClickEvent>(OnFormClicked);
        btn_close.RegisterCallback<ClickEvent>(OnCloseClicked);
    }
}
