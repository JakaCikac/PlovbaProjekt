using UnityEngine;
using UnityEngine.UI;

public class RowingDetectionScript : MonoBehaviour
{

    private GestureListener gestureListener;

    public bool controlWithGestures = true;
    public bool controlWithKeys = true;

    // Bunch of variable switches
    private bool rowForward = false;
    private bool rowLeft = false;
    private bool rowRight = false;

    // Sequence of gestures
    private bool leftSequenceLock = false;
    private bool rightSequenceLock = false;
    private bool startSequenceLock = false;

    // Objects and text items
    public Text debugText;
    public Text debugText2;
    public Text debugText3;
    public Text debugText4;
    public GameObject modelcic;
    Vector3 modelcicStartPos;

    void Awake()
    {
        GameObject text = GameObject.Find("/Canvas/DebugText");
        GameObject text2 = GameObject.Find("/Canvas/DebugText2");
        GameObject text3 = GameObject.Find("/Canvas/DebugText3");
        GameObject text4 = GameObject.Find("/Canvas/DebugText4");
        debugText = text.GetComponent<Text>();
        debugText2 = text2.GetComponent<Text>();
        debugText3 = text3.GetComponent<Text>();
        debugText4 = text4.GetComponent<Text>();

        modelcic = GameObject.Find("Modelcic_Character");

        // hide / display mouse cursor
        Cursor.visible = true;
    }

    void Start()
    {
        // get the gestures listener
        gestureListener = Camera.main.GetComponent<GestureListener>();

        debugText.text = "";
        debugText2.text = "";
        debugText3.text = "";
        debugText4.text = "";

        modelcicStartPos = transform.position;

    }

    void Update()
    {
        // dont run Update() if there is no user
        KinectManager kinectManager = KinectManager.Instance;
        if (!kinectManager || !kinectManager.IsInitialized() || !kinectManager.IsUserDetected())
            return;

        // Gesture control and sequence start
        if (controlWithGestures && gestureListener && !startSequenceLock)
        {
            debugText.text = "Listening for gestures!";
            debugText2.text = "Waiting for sequqnce lock!";
            debugText3.text = "!";
            debugText4.text = "!";

            // try to create a combination that is rowing forward..
            // If push is detected then go into listening mode for another gesture..
            // RaiseLeftHand -> Push detected. Waiting or pull..Pull detected -> action recognized -> row forward

            if (gestureListener.IsWave())
            {

                // Reset everything...
                Debug.Log("Wave detected, resetting everything!");
                debugText.text = "RESET!";

                Reset();

            }

            // Beginning of either row forward or turn left gesture detection.
            if (gestureListener.IsRaiseLeftHand() || gestureListener.IsRaiseRightHand())
            {
                startSequenceLock = true;
                debugText.text = "Started hand sequence.";
                Debug.Log("Started hand sequence");

            }

            // Beginning of the row left gesture detection.
            if (gestureListener.IsLeftPush() && startSequenceLock)
            {
                leftSequenceLock = true;
            }

            // Beginning of the row right gesture detection.
            if (gestureListener.IsRightPush() && startSequenceLock)
            {
                rightSequenceLock = true;
            }


        }

        if (startSequenceLock && gestureListener)
        {
            Debug.Log("In Hand Sequence lock.");

            if (gestureListener.IsLeftPush())
            {

                leftSequenceLock = true;
                debugText2.text = "Sequence locked, detected LEFT push, now waiting for LEFT pull.";
                Debug.Log("Sequence locked, detected LEFT push, now waiting for LEFT pull.");

            }

            if (gestureListener.IsRightPush())
            {

                rightSequenceLock = true;
                debugText2.text = "Sequence locked, detected RIGHT push, now waiting for RIGHT pull.";
                Debug.Log("Sequence locked, detected RIGHT push, now waiting for RIGHT pull.");

            }
        }

        // LEFT sequence lock, wait for another gesture in the sequence
        if (leftSequenceLock && gestureListener)
        {

            Debug.Log("In LEFT Sequence lock.");
            // Check if the next gesture fits the sequence of forward row (push, down / pull)
            if (gestureListener.IsLeftPull())
            {
                // we have a rowing sequence, log to console and display info
                debugText3.text = "Rowing LEFT detected.";
                Debug.Log("Rowing LEFT detected.");

                // Row left until new command detected...
                RowLeft();

                leftSequenceLock = false;
            }

        }

        // RIGHT sequence lock, wait for another gesture in the sequence
        if (rightSequenceLock && gestureListener)
        {

            Debug.Log("In RIGHT Sequence lock.");
            // Check if the next gesture fits the sequence of forward row (push, down / pull)
            if (gestureListener.IsRightPull())
            {
                // we have a rowing sequence, log to console and display info
                debugText3.text = "Rowing RIGHT detected.";
                Debug.Log("Rowing RIGHT detected.");

                // Row right until new command detected...
                RowRight();

                rightSequenceLock = false;

            }

        }
    }

    private void RowForward()
    {
        for (int i = 0; i < 20; i++)
            transform.Translate(Vector3.forward * Time.deltaTime, modelcic.transform);
    }

    private void RowLeft()
    {
        for (int i = 0; i < 20; i++)
            transform.Translate(Vector3.left * Time.deltaTime, modelcic.transform);
    }

    private void RowRight()
    {
        for (int i = 0; i < 20; i++)
            transform.Translate(Vector3.right * Time.deltaTime, modelcic.transform);
    }

    private void Reset()
    {

        debugText.text = "";
        debugText2.text = "";
        debugText3.text = "";
        debugText4.text = "";

        rowForward = false;
        rowLeft = false;
        rowRight = false;

        startSequenceLock = false;
        leftSequenceLock = false;
        rightSequenceLock = false;

        // Reset character position
        transform.Translate(modelcicStartPos, modelcic.transform);

    }

}