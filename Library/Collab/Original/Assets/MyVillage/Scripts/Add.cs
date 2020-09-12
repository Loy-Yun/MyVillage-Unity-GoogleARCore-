using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class Add : MonoBehaviour
{
    [SerializeField] private Text coin = null;
    [SerializeField] private GameObject DB = null;
    private DBManager a = null;
    [SerializeField] private GameObject m_AddWindow = null;
    [SerializeField] private Button m_AddButton = null;
    [SerializeField] private Button m_BackButton = null;
    [SerializeField] private Button[] m_TabButtonList = new Button[7];
    [SerializeField] private GameObject[] m_TabList = new GameObject[7];

    // Start is called before the first frame update
    void Start()
    {
        m_AddButton.onClick.AddListener(_OnAddButtonClicked);
        m_BackButton.onClick.AddListener(_OnBackButtonClicked);

        for (int i=0; i<m_TabButtonList.Length; i++)
        {
            int buttonIndex = i; 
            m_TabButtonList[buttonIndex].onClick.AddListener(() => _OnTabButtonClicked(buttonIndex));
        }
    }

    void Update() {
        a = DB.GetComponent<DBManager>();
        coin.text = a.PlayerData().ToString();
    }

    public void OnDestroy() {
        m_AddButton.onClick.RemoveListener(_OnAddButtonClicked);
        m_BackButton.onClick.RemoveListener(_OnBackButtonClicked);
    }

    public bool CanPlaceAsset() {
        return !m_AddWindow.activeSelf;
    }

    private void _OnAddButtonClicked() {
        m_AddWindow.SetActive(true);
    }

    private void _OnBackButtonClicked() {
        m_AddWindow.SetActive(false);
    }

    private void _OnTabButtonClicked(int idx) {

        for (int i=0; i<m_TabButtonList.Length; i++)
        {
            m_TabList[i].gameObject.SetActive(false);
        }
        m_TabList[idx].gameObject.SetActive(true);

    }
}
