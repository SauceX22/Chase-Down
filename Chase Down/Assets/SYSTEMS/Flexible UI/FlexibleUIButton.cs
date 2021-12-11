using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class FlexibleUIButton : FlexiblaUI
{
    Image image;
    Button button;

    protected override void OnSkinUI()
    {
        base.OnSkinUI();

        image = GetComponent<Image>();
        button = GetComponent<Button>();

        button.transition = Selectable.Transition.ColorTint;
        button.targetGraphic = image;

        button.colors = skinData.buttonColorState;

        image.sprite = skinData.buttonSprite;
        image.type = Image.Type.Sliced;
    }
}
