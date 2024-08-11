using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class UIMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] objects3D;
    private GameObject selectedObject;
    private Dictionary<string, TransformData> initialTransforms;
    private Dictionary<string, TransformData> recentTransforms;
    private bool initialTransformsUpdated = false;
    private bool isTargetTracked = false;

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
        initialTransforms = new Dictionary<string, TransformData>();
        recentTransforms = new Dictionary<string, TransformData>();

        for (int i = 0; i < objects3D.Length; i++)
        {
            GameObject obj = objects3D[i];
            string identifier = obj.name;
            initialTransforms[identifier] = new TransformData(obj);
            var objectManipulator = obj.GetComponent<ObjectManipulator>();
            objectManipulator.OnManipulationStarted.AddListener(OnManipulationStarted);
            objectManipulator.OnManipulationEnded.AddListener(OnManipulationEnded);
        }
    }

    void Update()
    {
        if (isTargetTracked && !initialTransformsUpdated)
        {
            initialTransformsUpdated = true;
            UpdateInitialTransforms();
        }
    }

    public void TargetFound()
    {
        isTargetTracked = true;
        StartCoroutine(WaitForObjectsTracking(3.0f));
    }

    public void TargetLost()
    {
        Debug.Log("Target Lost");
    }

    private void OnManipulationStarted(ManipulationEventData eventData)
    {
        selectedObject = eventData.ManipulationSource;

        string identifier = selectedObject.name;
        if (initialTransforms.ContainsKey(identifier))
        {
            recentTransforms[identifier] = new TransformData(selectedObject);
        }
        Debug.Log($"Object {selectedObject.name} grabbed.");
    }

    private void OnManipulationEnded(ManipulationEventData eventData)
    {
        Debug.Log($"Object {eventData.ManipulationSource.name} released.");
    }

    private IEnumerator WaitForObjectsTracking(float delay)
    {
        yield return new WaitForSeconds(delay);
        UpdateInitialTransforms();
    }

    private void UpdateInitialTransforms()
    {
        for (int i = 0; i < objects3D.Length; i++)
        {
            GameObject obj = objects3D[i];
            string identifier = obj.name;
            initialTransforms[identifier] = new TransformData(obj);
            Debug.Log($"Updated initial transform for {identifier} after tracking.");
        }
    }

    public void UndoLastAction()
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

    public void UndoEverythingButton()
    {
        foreach (var keyValue in initialTransforms)
        {
            UndoTransformButton(keyValue.Key);
        }
    }

    public void ObjectVisibilityButton()
    {
        foreach (var keyValue in initialTransforms)
        {
            if (keyValue.Key != "InfoBtn" && keyValue.Key != "InfoMenuDetail")
            {
                keyValue.Value.GameObject.SetActive(!keyValue.Value.GameObject.activeSelf);
                Debug.Log(keyValue.Value.GameObject.activeSelf ? "Enable Object Visibility" : "Disable Object Visibility");
            }
        }
    }
}