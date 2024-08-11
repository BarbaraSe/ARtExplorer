using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField]
    private GameObject _imageTargetHarbor;
    [SerializeField]
    public GameObject _imageTargetDinner;
    [SerializeField]
    public GameObject infoButton;
    [SerializeField]
    private GameObject _introductionScreens;
    [SerializeField]
    private GameObject _infoScreens;

    private GameObject[] _paintings;

    void Start()
    {
        SetInfoScreensActive(false);
        SetIntroductionScreensActive(true);

        _paintings = new GameObject[2] { _imageTargetHarbor, _imageTargetDinner };
        foreach (var item in _paintings)
        {
            item.SetActive(false);
        }
    }

    public void SetInfoScreensActive(bool active)
    {
        _infoScreens.SetActive(active);
    }

    public GameObject GetInfoScreensCanvas()
    {
        return _infoScreens;
    }

    public void SetIntroductionScreensActive(bool active)
    {
        _introductionScreens.SetActive(active);
    }

    public GameObject GetIntroductionScreen()
    {
        return _introductionScreens;
    }

    public void StartImageRecognition()
    {
        foreach (var painting in _paintings)
        {
            painting.SetActive(true);
            AddInfoButtonToPainting(painting);
        }
        SetIntroductionScreensActive(false);
    }

    private void AddInfoButtonToPainting(GameObject painting)
    {
        GameObject childCopyBtn = Instantiate(infoButton);
        GameObject infoButtonMenu = childCopyBtn.transform.Find("InfoMenuDetail").gameObject;
        childCopyBtn.transform.SetParent(painting.transform);
        childCopyBtn.SetActive(true);
        infoButtonMenu.SetActive(false);
        childCopyBtn.AddComponent<InfoDetailMenu>();
        childCopyBtn.transform.name = "InfoBtn";
        childCopyBtn.transform.localPosition = new Vector3(0.4f, 0, 0);
    }
}
