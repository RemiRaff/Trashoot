using TNRD;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

/// <summary>
/// Updates the score text in UI when the score changes.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Data")]
    [SerializeField, Label("Score Observable")] private SerializableInterface<IObservable<int>> _scoreObservableSerialized;

    // Properties for the interfaces
    private IObservable<int> _scoreObservable => _scoreObservableSerialized.Value;

    void Start()
    {
        // Initialize the score text
        OnScoreChanged(_scoreObservable.Value);
    }

    void OnEnable()
    {
        // Subscribe to the score observable
        _scoreObservable.OnValueChanged += OnScoreChanged;
    }

    void OnDisable()
    {
        // Unsubscribe from the score observable
        _scoreObservable.OnValueChanged -= OnScoreChanged;
    }

    /// <summary>
    /// Updates the score text when the score changes.
    /// </summary>
    /// <param name="score">the new score value</param>
    private void OnScoreChanged(int score)
    {
        // Update the score text
        _scoreText.text = $"Score : {score}";
    }
}
