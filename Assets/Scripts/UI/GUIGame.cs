using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using TMPro;
using UnityEngine;

public class GUIGame : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private PlayerCollideSystem _playerCollideSystem;

    private Event<EventScoreUpdated> OnScoreUpdated;

    private void Start()
    {
        UpdateTextScore(0);

        Init(World.Default);
    }

    private void Init(World world)
    {
        OnScoreUpdated = world.GetEvent<EventScoreUpdated>();
        OnScoreUpdated.Subscribe(OnScoreUpdate);
    }

    private void OnScoreUpdate(FastList<EventScoreUpdated> postTriggered)
    {
        foreach (EventScoreUpdated e in postTriggered)
        {
            UpdateTextScore(e.Score);
        }
    }

    private void UpdateTextScore(int score)
    {
        _text.SetText("Score: {0}", score);
    }
}
