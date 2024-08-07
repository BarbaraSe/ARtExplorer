using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.UIElements;

public class UIButtonController : MonoBehaviour
{

    //public GameObject uiPanel;

    [SerializeField] private GameObject[] objects3D;

    private GameObject selectedObject;
    private Dictionary<string, TransformData> initialTransforms;
    private Dictionary<string, TransformData> recentTransforms;

    // Contains all necessary data to manipulate the objects using the UI
    private struct TransformData
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }
        public GameObject GameObject { get; }
        public Transform Transform => GameObject.transform;
        public TransformData(GameObject gameObject)
        {
            Position = gameObject.transform.position;
            Rotation = gameObject.transform.rotation;
            Scale = gameObject.transform.localScale;
            GameObject = gameObject;
        }
    }

        void Start()
    {
        // Store initial transforms
        initialTransforms = new Dictionary<string, TransformData>();
        recentTransforms = new Dictionary<string, TransformData>();

        for (int i = 0; i < objects3D.Length; i++)
        {
            GameObject obj = objects3D[i];
            string identifier = objects3D[i].name; // Using object name as identifier for each object
            initialTransforms[identifier] = new TransformData(objects3D[i]);

            // Look for the object manipulator component on the objects and listen to it
            var objectManipulator = obj.GetComponent<ObjectManipulator>();
            objectManipulator.OnManipulationStarted.AddListener(OnManipulationStarted);
            objectManipulator.OnManipulationEnded.AddListener(OnManipulationEnded);
        }
    }

    private void Update()
    {

        // TODO: Swap all Keyboard Inputs with corresponding UI Events

        if (Input.GetKeyDown(KeyCode.D))
        {
            ObjectVisbilityButton();
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Reset All Objects");
            UndoEverythingButton();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UndoLastAction();
        }
        
        /* if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UndoTransformButton(lastSelectedObject.name);
        } */
    }

    private void OnManipulationStarted(ManipulationEventData eventData)
    {
        selectedObject = eventData.ManipulationSource;

        string identifier = selectedObject.name;
        if (initialTransforms.ContainsKey(identifier))
        {
            // Store the most recent transform before manipulation starts
            recentTransforms[identifier] = new TransformData(selectedObject);
        }
        Debug.Log($"Object {selectedObject.name} grabbed.");
    }

    private void OnManipulationEnded(ManipulationEventData eventData)
    {
        Debug.Log($"Object {eventData.ManipulationSource.name} released.");

    }

    public void InfoBtnPressed()
    {

    }

    // removes 3D objects shows only painting
    public void ObjectVisbilityButton()
    {
        foreach (var keyValue in initialTransforms)
        {
            if (keyValue.Value.GameObject.activeSelf)
            {
                keyValue.Value.GameObject.SetActive(false);
                Debug.Log("Disable Objetc Visibility");
            }
            else
            {
                keyValue.Value.GameObject.SetActive(true);
                Debug.Log("Enable Objetc Visibility");
            }
        }
    }

    /*
    public void UndoPositionButton(string identifier)
    {
        if (initialTransforms.ContainsKey(identifier))
        {
            TransformData data = initialTransforms[identifier];
            Transform objTransform = data.Transform;
            objTransform.position = data.Position;
            Debug.Log($"Position for {identifier} reset to initial state.");
        }
        else
        {
            Debug.LogWarning($"No initial transform data found for identifier: {identifier}");
        }
    }

    public void UndoRotationButton(string identifier)
    {
        if (initialTransforms.ContainsKey(identifier))
        {
            TransformData data = initialTransforms[identifier];
            Transform objTransform = data.Transform;
            objTransform.rotation = data.Rotation;
            Debug.Log($"Position for {identifier} reset to initial state.");
        }
        else
        {
            Debug.LogWarning($"No initial transform data found for identifier: {identifier}");
        }
    }


    public void UndoScaleButton(string identifier)
    {
        if (initialTransforms.ContainsKey(identifier))
        {
            TransformData data = initialTransforms[identifier];
            Transform objTransform = data.Transform;
            objTransform.localScale = data.Scale;
            Debug.Log($"Scale for {identifier} reset to initial state.");
        }
        else
        {
            Debug.LogWarning($"No initial transform data found for identifier: {identifier}");
        }
    } */

    private void UndoLastAction()
    {
        if (selectedObject != null)
        {
            string identifier = selectedObject.name;
            if (recentTransforms.ContainsKey(identifier))
            {
                TransformData data = recentTransforms[identifier];
                Transform objTransform = data.GameObject.transform;
                objTransform.position = data.Position;
                objTransform.rotation = data.Rotation;
                objTransform.localScale = data.Scale;
                Debug.Log($"Last action on {identifier} undone.");
            }
            else
            {
                Debug.LogWarning($"No recent transform data found for: {identifier}");
            }
        }
        else
        {
            Debug.LogWarning("No last grabbed object to undo.");
        }
    }

    // reset transform of specific object
    public void UndoTransformButton(string identifier)
    {
        if (initialTransforms.ContainsKey(identifier))
        {
            TransformData data = initialTransforms[identifier];
            Transform objTransform = data.Transform;
            objTransform.position = data.Position;
            objTransform.rotation = data.Rotation;
            objTransform.localScale = data.Scale;
            Debug.Log($"Transform for {identifier} reset to initial state.");
        }
        else
        {
            Debug.LogWarning($"No initial transform data found for identifier: {identifier}");
        }
    }

    //reset transform of all objects
    public void UndoEverythingButton()
    {
        foreach (var keyValue in initialTransforms)
        {
            UndoTransformButton(keyValue.Key);
        }
    }

    public void CancelUI()
    {
        //uiPanel.SetActive(false);
    }
}
