using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    #region Serialized
    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;
    #endregion

    #region private
    private string _positiveColorString;
    private string _negativeColorString;
    private string _coloredStringEnd;
    private StringBuilder _stringBuilder = new StringBuilder();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _positiveColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_positiveColor) + ">";
        _negativeColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_negativeColor) + ">";
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
        else
        {
            _stringBuilder.Append(_negativeColorString);
            _stringBuilder.Append("-");
        }
        _stringBuilder.Append(value);
        _stringBuilder.Append(_coloredStringEnd);
        return _stringBuilder.ToString();

    }
}
