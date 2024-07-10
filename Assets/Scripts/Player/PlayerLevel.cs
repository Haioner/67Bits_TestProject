using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private float maxXP = 100f;
    [SerializeField] private float maxXPMultiplier = 1.225f;
    [SerializeField] private ItemShop levelItemShop;

    private int _level = 1;
    private float _currentXP = 0f;
    private StackingManager _stackingManager;

    //Invoke xp change
    public delegate void OnXPChanged(float xpValue, float maxXPValue);
    public static event OnXPChanged onXPChanged;

    //Invoke level change
    public delegate void OnLevelChanged(int levelValue);
    public static event OnLevelChanged onLevelChanged;

    private void Start()
    {
        _stackingManager = GetComponent<StackingManager>();
    }

    public void AddXP()
    {
        _currentXP += maxXP/3;
        LevelUP();
    }

    private void LevelUP()
    {
        if (_currentXP >= maxXP)
        {
            float remainingXP = _currentXP - maxXP;
            _level++;
            maxXP *= maxXPMultiplier;
            _currentXP = remainingXP;
            _stackingManager.AddMaxStack();
        }

        onXPChanged?.Invoke(_currentXP, maxXP);
        onLevelChanged?.Invoke(_level);
    }
}
