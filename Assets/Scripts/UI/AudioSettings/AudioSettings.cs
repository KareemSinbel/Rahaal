using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    private List<Slider> sliders;
    private List<TextMeshProUGUI> sliderTexts;

    private void Awake()
    {
        sliders = new List<Slider>(GetComponentsInChildren<Slider>());
        sliderTexts = new List<TextMeshProUGUI>(GetComponentsInChildren<TextMeshProUGUI>());
    }

    private void Start()
    {
        for(int i =0; i < sliders.Count; i++)
        {
            Slider slider = sliders[i];
            TextMeshProUGUI text = sliderTexts[i];


            text.text = slider.value.ToString();

            slider.onValueChanged.AddListener((value) => {
                text.text = value.ToString();
            });
        }
    }

    private void OnDestroy()
    {
        foreach(Slider slider in sliders)
            slider.onValueChanged.RemoveAllListeners();
    }

}
