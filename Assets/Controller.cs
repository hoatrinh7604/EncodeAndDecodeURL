using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using ZXing;
using ZXing.QrCode;

public class Controller : MonoBehaviour
{
    [SerializeField] TMP_InputField urlInputField;

    [SerializeField] Button encodeBtn;
    [SerializeField] Button decodeBtn;
    [SerializeField] Button copyBtn;
    [SerializeField] Button clearBtn;

    [SerializeField] DataSaveController saveController;
    //Singleton
    public static Controller Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public void OnValueChanged()
    {
        encodeBtn.interactable = urlInputField.text != "";
        decodeBtn.interactable = urlInputField.text != "";
        copyBtn.interactable = urlInputField.text != "";
        clearBtn.interactable = urlInputField.text != "";
    }

    private void Start()
    {
        Clear();

        urlInputField.onValueChanged.AddListener(delegate { OnValueChanged(); });
        encodeBtn.onClick.AddListener(delegate { Encode(); });
        decodeBtn.onClick.AddListener(delegate { Decode(); });
        copyBtn.onClick.AddListener(delegate { Copy(); });
        clearBtn.onClick.AddListener(delegate { Clear(); });
    }

    public void Encode()
    {
        string result = UnityWebRequest.EscapeURL(urlInputField.text);

        urlInputField.text= result;
    }

    public void Decode()
    {
        string result = UnityWebRequest.UnEscapeURL(urlInputField.text);

        urlInputField.text = result;
    }

    public void Copy()
    {
        GUIUtility.systemCopyBuffer = urlInputField.text;
    }

    public void Clear()
    {
        urlInputField.text = string.Empty;
        encodeBtn.interactable = true;
        decodeBtn.interactable = true;
        copyBtn.interactable = false;
        clearBtn.interactable = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
