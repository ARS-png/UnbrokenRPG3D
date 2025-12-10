using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class DeepgramTTSRequest
{
    public string text;
}

[RequireComponent(typeof(AudioSource))]
public class DeepgramTTS : MonoBehaviour
{
    [Header("Deepgram Configuration")]
    [SerializeField] private string apiKey = "YOUR_DEEPGRAM_API_KEY";
    [SerializeField] private string model = "aura-asteria-en"; // Модель выбирается в инспекторе

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;

    [Header("Debug")]
    [SerializeField] private bool enableLogs = true;

    private bool isPlaying = false;

    private void OnEnable()
    {
        if (GameEventsManager.instance != null && GameEventsManager.instance.tTSEvents != null)
            GameEventsManager.instance.tTSEvents.onSpeak += Speak;
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance != null && GameEventsManager.instance.tTSEvents != null)
            GameEventsManager.instance.tTSEvents.onSpeak -= Speak;
    }

    void Start()
    {
        //Log("=== DEEPGRAM TTS STARTED ===");
        SetupAudioSource();
        //Log($"Using model: {model}");
    }

    private void SetupAudioSource()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 0f;
            Log("AudioSource created automatically");
        }
    }

    public void Speak(string text)
    {
        if (!isPlaying)
        {
            StartCoroutine(GenerateSpeech(text));
        }
        else
        {
            Log("Audio is already playing, wait for it to finish.");
        }
    }

    private IEnumerator GenerateSpeech(string text)
    {
        isPlaying = true;
        Log($"Starting TTS for: '{text}'");

        // Используем модель из SerializeField
        string url = $"https://api.deepgram.com/v1/speak?model={model}";
        Log($"API URL: {url}");

        DeepgramTTSRequest requestData = new DeepgramTTSRequest
        {
            text = text
        };

        string jsonData = JsonUtility.ToJson(requestData);
        Log($"JSON Data: {jsonData}");

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerAudioClip(url, AudioType.MPEG);

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Token {apiKey}");

            Log("Sending request to Deepgram API...");
            yield return request.SendWebRequest();

            Log($"Request completed. Status: {request.result}, HTTP Code: {request.responseCode}");

            if (request.result == UnityWebRequest.Result.Success)
            {
                Log("TTS request successful!");

                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                if (clip != null)
                {
                    Log($"Audio clip loaded: {clip.length} seconds");

                    audioSource.clip = clip;
                    audioSource.Play();
                    Log("Audio playback STARTED");

                    yield return new WaitForSeconds(clip.length);
                    Log("Audio playback FINISHED");
                }
                else
                {
                    Debug.LogError("Failed to create AudioClip from response");
                }
            }
            else
            {
                Debug.LogError($"TTS Error: {request.error}");
                Debug.LogError($"HTTP Status: {request.responseCode}");
            }
        }

        isPlaying = false;
        Log("TTS process finished");
    }

    // Метод для смены модели через события
    public void SetModel(string newModel)
    {
        model = newModel;
        Log($"Model changed to: {model}");
    }

    // Быстрые методы для смены голосов через события
    public void SetMaleVoice() => SetModel("aura-orion-en");
    public void SetFemaleVoice() => SetModel("aura-asteria-en");
    public void SetZeusVoice() => SetModel("aura-zeus-en");
    public void SetLunaVoice() => SetModel("aura-luna-en");

    private void Log(string message)
    {
        if (enableLogs)
        {
            Debug.Log($"[DeepgramTTS] {message}");
        }
    }
}