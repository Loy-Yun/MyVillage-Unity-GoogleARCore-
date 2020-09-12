namespace GoogleARCore.Examples.Common {
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.UI;

    public class Alert : MonoBehaviour {
        /// <summary>
        /// The Menu Window shows the depth configurations.
        /// </summary>
        [SerializeField] private Button m_YesButton = null;

        [SerializeField] private Button m_NoButton = null;

        [SerializeField] private Text m_AlertText = null;

        public delegate void AnswerCallBack();
        private event AnswerCallBack yCallBack;
        private event AnswerCallBack nCallBack;

        public void Start() {
            m_AlertText.text = "여기서 PLAY하시겠습니까?";
        }

        public void SetYesCallback(AnswerCallBack listener) {
            yCallBack += listener;
        }
        public void SetNoCallback(AnswerCallBack listener) {
            nCallBack += listener;
        }
        public void OnYes() {
            yCallBack?.Invoke();
        }
        public void OnNo() {
            nCallBack?.Invoke();
        }

    }   
}
