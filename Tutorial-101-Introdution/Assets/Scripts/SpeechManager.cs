using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager : MonoBehaviour
{
    KeywordRecognizer _recognizer;
    Dictionary<string, System.Action> _keywords;

    // Use this for initialization
    void Start()
    {
        _keywords = new Dictionary<string, System.Action>
        {
            {"Drop", () =>
            {
                if(GazeGestureManager.Instance.FocusedObject != null)
                    GazeGestureManager.Instance.FocusedObject.SendMessage("OnSelect");
            }},
            {"Reset", () =>
            {
                BroadcastMessage("OnReset");
            }}
        };

        _recognizer = new KeywordRecognizer(_keywords.Keys.ToArray());
        _recognizer.OnPhraseRecognized += _recognizer_OnPhraseRecognized;
        _recognizer.Start();
    }

    private void _recognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (_keywords.ContainsKey(args.text)) _keywords[args.text]();
    }
}

//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Windows.Speech;

//public class SpeechManager : MonoBehaviour
//{
//    KeywordRecognizer keywordRecognizer = null;
//    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

//    // Use this for initialization
//    void Start()
//    {
//        keywords.Add("Reset world", () =>
//        {
//            // Call the OnReset method on every descendant object.
//            this.BroadcastMessage("OnReset");
//        });

//        keywords.Add("Drop Sphere", () =>
//        {
//            var focusObject = GazeGestureManager.Instance.FocusedObject;
//            if (focusObject != null)
//            {
//                // Call the OnDrop method on just the focused object.
//                focusObject.SendMessage("OnSelect");
//            }
//        });

//        // Tell the KeywordRecognizer about our keywords.
//        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

//        // Register a callback for the KeywordRecognizer and start recognizing!
//        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
//        keywordRecognizer.Start();
//    }

//    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
//    {
//        System.Action keywordAction;
//        if (keywords.TryGetValue(args.text, out keywordAction))
//        {
//            keywordAction.Invoke();
//        }
//    }
//}