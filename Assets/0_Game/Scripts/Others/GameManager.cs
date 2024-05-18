using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    #region Serialized
    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;
    [SerializeField] private Color _defaultColor = Color.black;
    #endregion

    #region private
    private string _positiveColorString;
    private string _negativeColorString;
    private string _defaultColorString;
    private string _coloredStringEnd;
    private StringBuilder _stringBuilder = new StringBuilder();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _positiveColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_positiveColor) + ">";
        _negativeColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_negativeColor) + ">";
        _defaultColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_defaultColor) + ">";
        _coloredStringEnd = "</color>";
    }

    public string GetColoredString(int value)
    {
        _stringBuilder.Clear();
        if (value > 0)
        {
            _stringBuilder.Append(_positiveColorString);
            _stringBuilder.Append("+");
        }
        else if (value < 0)
        {
            _stringBuilder.Append(_negativeColorString);
        }
        else
        {
            _stringBuilder.Append(_defaultColorString);
        }
        _stringBuilder.Append(value);
        _stringBuilder.Append(_coloredStringEnd);
        return _stringBuilder.ToString();

    }
}
