using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class InfoMenu : MonoBehaviour
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
        _viewController.SetInfoScreensActive();
        _paintingInfoScreenController.SetAboutInfoActive(false);
        _paintingInfoScreenController.SetStartInfoPanelActive(true);
        _paintingInfoScreenController.SetPaintingInfoActive(false);
    }

    public void OpenInfoDetailMenu()
    {
        if (_menuDetail.activeSelf)
        {
            _menuDetail.SetActive(false);
            Debug.Log("Info Detail Menu not visible");
        }
        else
        {
            _menuDetail.SetActive(true);
            Debug.Log("Info Detail Menu visible");
        }
    }

    public void GetGeneralInformation()
    {
        _viewController.SetIntroductionScreensActive(true);
        IntroScreensView introductionScreen = _viewController.GetIntroductionScreen().GetComponent<IntroScreensView>();
        // IntroScreensView introductionScreen = GameObject.Find("IntroductionScreens").GetComponent<IntroScreensView>();
        //introductionScreen.SetIntroductionScreens(true);
        introductionScreen.SetIntroductionPanel1Active(true);
    }
}
