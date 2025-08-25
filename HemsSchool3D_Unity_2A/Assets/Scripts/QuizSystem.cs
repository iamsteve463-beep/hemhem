using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuizSystem : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string prompt;
        public string[] options = new string[4];
        public int correctIndex;
    }

    public CanvasGroup quizGroup;
    public Text questionText;
    public Button[] optionButtons;
    public float jailSeconds = 5f;
    public Transform jailPoint;
    public Transform player;

    public AudioSource ttsSpeaker;

    private List<Question> bank = new List<Question>();
    private Question current;

    void Start()
    {
        bank.Add(new Question{prompt="Quadratic formula?", options=new[]{ "(-b±√(b²-4ac))/(2a)", "a²+b²=c²", "F=ma", "E=mc²"}, correctIndex=0});
        bank.Add(new Question{prompt="Pythagoras theorem?", options=new[]{ "a²+b²=c²", "PV=nRT", "sinθ=opp/hyp", "v=u+at"}, correctIndex=0});
        bank.Add(new Question{prompt="Newton's 2nd law?", options=new[]{ "P=VI", "F=ma", "x=ut+½at²", "V=IR"}, correctIndex=1});
        bank.Add(new Question{prompt="Ohm's law?", options=new[]{ "V=IR", "P=F/A", "KE=½mv²", "a²+b²=c²"}, correctIndex=0});
        Hide();
    }

    public void ShowRandom()
    {
        current = bank[Random.Range(0, bank.Count)];
        questionText.text = "Have you learned the formula?\\n" + current.prompt;
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            optionButtons[i].GetComponentInChildren<Text>().text = current.options[i];
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => Pick(idx));
        }
        quizGroup.alpha = 1f; quizGroup.blocksRaycasts = true; quizGroup.interactable = true;
        Time.timeScale = 0f;
    }

    void Hide()
    {
        quizGroup.alpha = 0f; quizGroup.blocksRaycasts = false; quizGroup.interactable = false;
        Time.timeScale = 1f;
    }

    void Pick(int index)
    {
        if (index == current.correctIndex)
        {
            Speak("Good! You got it right.");
            Hide();
        }
        else
        {
            string[] lines = {
                "You don't have the time to read the formula right.",
                "Give me your ID card, I will call your parents and keep you here till 7 PM."
            };
            Speak(lines[Random.Range(0, lines.Length)]);
            Hide();
            if (player && jailPoint) player.position = jailPoint.position;
            StartCoroutine(JailTimer());
        }
    }

    System.Collections.IEnumerator JailTimer()
    {
        float end = Time.realtimeSinceStartup + jailSeconds;
        Time.timeScale = 0f;
        while (Time.realtimeSinceStartup < end)
            yield return null;
        Time.timeScale = 1f;
    }

    public void Speak(string text)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalEval($"window.speechSynthesis.speak(new SpeechSynthesisUtterance('{EscapeJs(text)}'))");
#else
        if (ttsSpeaker) ttsSpeaker.Play();
#endif
    }

    string EscapeJs(string s)
    {
        return s.Replace("\\\\", "\\\\\\\\").Replace("'", "\\\\'");
    }
}
