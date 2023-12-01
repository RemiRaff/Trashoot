using TNRD;
using UnityEngine;

/// <summary>
/// Adds a score value to the player score when executed.
/// </summary>
public class TrashScoreEffect : MonoBehaviour, IExecute
{
    [Header("Data")]
    [SerializeField] private SerializableInterface<IAdd<int>> _scoreAddSerialized;

    [SerializeField] private Trash _trash;

    // Properties for the interfaces
    private IAdd<int> _scoreAdd => _scoreAddSerialized.Value;

    /// <summary>
    /// Executes the score effect, adding the corresponding value to the player score.
    /// </summary>
    public void Execute()
    {
        // Add score
        _scoreAdd.Add(CalculateScore());
    }

    private int CalculateScore()
    {
        int scoreValue = 0;
        if (_trash._size < 0.7f)
        {
            scoreValue = 100; // small trash
        }
        else if (_trash._size < 1.4f)
        {
            scoreValue = 50; // medium trash
        }
        else
        {
            scoreValue = 25; // large trash, easiest
        }
        return scoreValue;
    }
}
