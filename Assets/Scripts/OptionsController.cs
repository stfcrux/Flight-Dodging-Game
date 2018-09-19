using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public Slider difficultySlider;

    public void DifficultySliderChanged()
    {
        GlobalOptions.difficulty = difficultySlider.value;
    }
}
