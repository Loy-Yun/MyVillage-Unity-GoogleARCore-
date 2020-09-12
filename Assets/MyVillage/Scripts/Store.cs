using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class Store : MonoBehaviour {
    [SerializeField] private GameObject m_StoreWindow = null;
    [SerializeField] private GameObject m_SellTap = null;
    [SerializeField] private GameObject m_BuyTap = null;

    [SerializeField] private Button m_StoreButton = null;
    [SerializeField] private Button m_BackButton = null;
    [SerializeField] private Button m_SellButton = null;
    [SerializeField] private Button m_BuyButton = null;

    [SerializeField] private Button[] m_SellItemButtons = new Button[2];
    [SerializeField] private GameObject[] m_SellItemTabs = new GameObject[2];
    [SerializeField] private Button[] m_BuyItemButtons = new Button[4];
    [SerializeField] private GameObject[] m_BuyItemTabs = new GameObject[4];

    [SerializeField] private int before_s;
    [SerializeField] private int before_b;

    // Start is called before the first frame update
    void Start() {
        before_s = 0;
        before_b = 0;
        m_StoreButton.onClick.AddListener(_OnStoreButtonClicked);
        m_BackButton.onClick.AddListener(_OnBackButtonClicked);
        m_SellButton.onClick.AddListener(_OnSellButtonClicked);
        m_BuyButton.onClick.AddListener(_OnBuyButtonClicked);
        
        for (int i = 0; i < m_SellItemButtons.Length; i++) 
            m_SellItemButtons[i].onClick.AddListener(() => _OnSellItemButtonClicked(i));

        for (int i = 0; i < m_BuyItemButtons.Length; i++)
            m_BuyItemButtons[i].onClick.AddListener(() => _OnBuyItemButtonClicked(i));
            
    }

    public void OnDestroy() {
        m_StoreButton.onClick.RemoveListener(_OnStoreButtonClicked);
        m_BackButton.onClick.RemoveListener(_OnBackButtonClicked);
    }

    public bool CanPlaceAsset() {
        return !m_StoreWindow.activeSelf;
    }

    private void _OnStoreButtonClicked() {
        m_StoreWindow.SetActive(true);
    }

    private void _OnBackButtonClicked() {
        m_StoreWindow.SetActive(false);
    }

    private void _OnSellButtonClicked() {
        m_BuyTap.SetActive(false);
        m_SellTap.SetActive(true);
    }

    private void _OnBuyButtonClicked() {
        m_SellTap.SetActive(false);
        m_BuyTap.SetActive(true);
    }
    
    private void _OnSellItemButtonClicked(int num) {
        int index = ((num == 0) ? 1 : 0);
        m_SellItemTabs[index].gameObject.SetActive(false);
        m_SellItemTabs[num].gameObject.SetActive(true);
    }

    private void _OnBuyItemButtonClicked(int num) {
        for(int i = 0; i < m_BuyItemTabs.Length; i++) {
            m_BuyItemTabs[i].gameObject.SetActive(false);
        }
        m_BuyItemTabs[num].gameObject.SetActive(true);
    }   
}