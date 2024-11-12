using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVideoManager : MonoBehaviour
{
    public VideoPlayer introVideo;
    public GameObject canvas; // התייחסות ל-Canvas של התפריט

    void Start()
    {
        introVideo.loopPointReached += OnVideoEnd; // חיבור לאירוע סיום הסרטון
        canvas.SetActive(true); // וידוא שהקנבס מוצג בהתחלה
    }

    public void PlayIntroVideo()
    {
        introVideo.Play();
        canvas.SetActive(false); // הסתרת הקנבס כשהסרטון מתחיל
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(1); // מעבר לשלב הראשון
    }

    void Update()
    {
        // בדיקה אם נלחץ כפתור כלשהו כדי לדלג על הסרטון
        if (introVideo.isPlaying && Input.anyKeyDown)
        {
            SkipVideo();
        }
    }

    private void SkipVideo()
    {
        introVideo.Stop();
        SceneManager.LoadScene(1); // מעבר לשלב הראשון
    }
}
