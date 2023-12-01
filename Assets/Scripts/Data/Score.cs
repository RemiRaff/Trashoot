using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Holds the score value of the player.
/// </summary>
[CreateAssetMenu(fileName = "Score", menuName = "Data/Score")]
public class Score : ScriptableObject, IValue<int>, IAdd<int>, IReset, IObservable<int>
{
    [ShowNonSerializedField] private int _value;

    /// <summary>
    /// The score value.
    /// </summary>
    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    /// <summary>
    /// Invoked when the score value changes.
    /// </summary>
    public event System.Action<int> OnValueChanged;

    /// <summary>
    /// Adds points to the score.
    /// </summary>
    /// <param name="value">the amount of point to add to the score</param>
    public void Add(int value) => Value += value;

    /// <summary>
    /// Resets the score value to its default (0).
    /// </summary>
    public void Reset() => Value = 0;
}
