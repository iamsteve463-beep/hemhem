using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HemAI hemAI;
    public QuizSystem quiz;

    void Start()
    {
        if (hemAI != null)
            hemAI.OnCatch += OnCaught;
    }
    void OnDestroy()
    {
        if (hemAI != null)
            hemAI.OnCatch -= OnCaught;
    }
    void OnCaught()
    {
        if (quiz != null) quiz.ShowRandom();
    }
}
