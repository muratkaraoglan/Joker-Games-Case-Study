using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;

public class GameManager : Singelton<GameManager>
{
    #region Serialized
    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;
    [SerializeField] private Color _defaultColor = Color.black;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    #endregion

    #region private
    private string _positiveColorString;
    private string _negativeColorString;
    private string _defaultColorString;
    private string _coloredStringEnd;
    private StringBuilder _stringBuilder = new StringBuilder();
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;
    private PlayerBase _player;
    private bool _isCameraShaking;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        _positiveColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_positiveColor) + ">";
        _negativeColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_negativeColor) + ">";
        _defaultColorString = "<color=#" + ColorUtility.ToHtmlStringRGBA(_defaultColor) + ">";
        _coloredStringEnd = "</color>";
        _multiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        StartCoroutine(ChangeFOV(TileSpawner.Instance.TileConfig.TileCountOnEdge * 5 + 5, .5f));
    }

    private void Start()
    {
        DiceManager.Instance.OnDiceCollide += OnDiceCollide;
    }

    IEnumerator ChangeFOV(float endFOV, float duration)
    {
        float startFOV = _virtualCamera.m_Lens.FieldOfView;
        float elapsed = 0;
        while (elapsed < duration)
        {
            float currentFOV = Mathf.Lerp(startFOV, endFOV, elapsed / duration);
            _virtualCamera.m_Lens.FieldOfView = currentFOV;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }


    private async void OnDiceCollide()
    {
        if (_isCameraShaking) return;
        _isCameraShaking = true;
        _multiChannelPerlin.m_AmplitudeGain = 1;
        _multiChannelPerlin.m_FrequencyGain = 1;
        await Task.Delay(500);
        _multiChannelPerlin.m_AmplitudeGain = 0;
        _multiChannelPerlin.m_FrequencyGain = 0;
        _isCameraShaking = false;
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
