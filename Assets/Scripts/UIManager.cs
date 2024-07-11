using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtCollectibleCount;
    [SerializeField] private TMP_Text _txtDamageTimer;

    // Start is called before the first frame update
    void Start()
    {
        _txtCollectibleCount.text = "0";
        _txtDamageTimer.text = "0:00";
    }

    public void UpdateCollectibleCount(int collectibleCount)
    {
        _txtCollectibleCount.text = collectibleCount.ToString();
    }

    public void UpdateDamageTimer(TimeSpan timeSinceLastDamage)
    {
        _txtDamageTimer.text = FormatTimeSinceLastDamage(timeSinceLastDamage);
    }

    private string FormatTimeSinceLastDamage(TimeSpan timeSinceLastDamage)
    {
        return String.Format("{0:0}:{1:00}", timeSinceLastDamage.Minutes, timeSinceLastDamage.Seconds);
    }
}
