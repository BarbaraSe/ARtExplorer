using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class InfoDetailMenu : MonoBehaviour
{
    private GameObject _menuDetail;
    private string _parentName;
    private GameObject _parent;
    private UIMenuController _uIMenuController;
    private ViewController _viewController;
    private PaintingInfoScreensController _paintingInfoScreenController;

    void Update()
    {
        // gameObject is infoBtn
        _parent = gameObject.transform.parent.gameObject;
        _parentName = _parent.name;
        _menuDetail = gameObject.transform.Find("InfoMenuDetail").gameObject;
        _uIMenuController = FindObjectOfType<UIMenuController>();
        _viewController = FindObjectOfType<ViewController>();

        AddButtonListeners();

        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInfoDetailMenu();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetGeneralInformation();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenPaintingInfos();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            _uIMenuController.ObjectVisibilityButton();
        }
    }

    public string GetParentName()
    {
        return _parentName;
    }

    public void AddButtonListeners()
    {
        PressableButton[] buttons = _menuDetail.transform.GetComponentsInChildren<PressableButton>();
        foreach (var item in buttons)
        {
            if (item.name == "GeneralInfo")
            {
                item.ButtonPressed.AddListener(GetGeneralInformation);
            }
            else if (item.name == "PaintingInfo")
            {
                item.ButtonPressed.AddListener(OpenPaintingInfos);
            }
            else if (item.name == "Hide3DBtn")
            {
                item.ButtonPressed.AddListener(_uIMenuController.ObjectVisibilityButton);
            }
        }

        if (gameObject.GetComponent<PressableButton>() != null)
        {
            gameObject.GetComponent<PressableButton>().ButtonPressed.AddListener(OpenInfoDetailMenu);
        }
        else
        {
            Debug.LogError("Info button component not found!");
        }
    }

    public void OpenPaintingInfos()
    {
        _viewController.SetInfoScreensActive(true);
        _paintingInfoScreenController = _viewController.GetInfoScreensCanvas().GetComponent<PaintingInfoScreensController>();
        _paintingInfoScreenController.SetStartScreenInfoActive(true);
        _paintingInfoScreenController.SetAboutInfoActive(false);
        _paintingInfoScreenController.SetPaintingInfoActive(false);
    }

    public void OpenInfoDetailMenu()
    {
        if (_menuDetail.activeSelf)
        {
            _menuDetail.SetActive(false);
        }
        else
        {
            _menuDetail.SetActive(true);
        }
    }

    public void GetGeneralInformation()
    {
        // _viewController.SetIntroductionScreensActive(true);
        //IntroScreensView introductionScreen = FindObjectOfType<IntroScreensView>();
        _viewController.SetIntroductionScreensActive(true);
        IntroScreensView introductionScreen = GameObject.Find("IntroductionScreens").GetComponent<IntroScreensView>();
        // IntroScreensView introductionScreen = _viewController.GetIntroductionScreen().GetComponent<IntroScreensView>();
        introductionScreen.SetIntroductionPanel1Active(true);
    }
}
