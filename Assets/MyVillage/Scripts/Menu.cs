//-----------------------------------------------------------------------
// <copyright file="DepthMenu.cs" company="Google LLC">
//
// Copyright 2020 Google LLC. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleARCore.Examples.Common {
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Depth setting menu, including the menu window and depth card window.
    /// </summary>
    public class Menu : MonoBehaviour {
        /// <summary>
        /// The Menu Window shows the depth configurations.
        /// </summary>
        [SerializeField] private GameObject m_MenuWindow = null;


        /// <summary>
        /// The button to open the menu window.
        /// </summary>
        [SerializeField] private Button m_MenuButton = null;

        /// <summary>
        /// The button to apply the config and close the menu window.
        /// </summary>
        [SerializeField] private Button m_YesButton = null;

        /// <summary>
        /// The button to cancel the changs and close the menu window.
        /// </summary>
        [SerializeField] private Button m_NoButton = null;

        /// <summary>
        /// The menu text.
        /// </summary>
        [SerializeField] private Text m_MenuText = null;





        /// <summary>
        /// Unity's Start() method.
        /// </summary>
        public void Start() {
            m_MenuButton.onClick.AddListener(_OnMenuButtonClicked);
            m_YesButton.onClick.AddListener(_OnYesButtonClicked);
            m_NoButton.onClick.AddListener(_OnNoButtonClicked);

            m_MenuWindow.SetActive(false);
        }

        /// <summary>
        /// Unity's OnDestroy() method.
        /// </summary>
        public void OnDestroy() {
            m_MenuButton.onClick.RemoveListener(_OnMenuButtonClicked);
            m_YesButton.onClick.RemoveListener(_OnYesButtonClicked);
            m_NoButton.onClick.RemoveListener(_OnNoButtonClicked);
        }



        /// <summary>
        /// Check whether the user could place asset.
        /// </summary>
        /// <returns>Whether the user could place asset.</returns>
        public bool CanPlaceAsset() {
            return !m_MenuWindow.activeSelf;
        }


        private void _OnMenuButtonClicked() {
            m_MenuText.text = "";
            m_MenuWindow.SetActive(true);
        }

        private void _OnYesButtonClicked() {
            m_MenuWindow.SetActive(false);
        }

        private void _OnNoButtonClicked() {
            m_MenuWindow.SetActive(false);
        }
    }
}
