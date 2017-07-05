using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGPresentation
{
    /// <summary>
    /// Presentation is the main class to make presentations. It can switch between contents back and forth.
    /// </summary>
    public class Presentation : MonoBehaviour
    {
        /// <summary>All the contents inside this presentation.</summary>
        [Tooltip("Contents that will be played by the presentation. Contents will be played in order of the presentation.")]
        public List<PresentationContent> contents = new List<PresentationContent>();

        /// <summary>Which contents are currently transitionning. </summary>
        private HashSet<PresentationContent> currentlyActiveContents = new HashSet<PresentationContent>();

        #region Unity override
        private void Awake()
        {
            if(contents.Count == 0) { Debug.LogWarning("Presentation has no slide to work with.", this.gameObject); }
        }

        void Start()
        {

        }

        void Update()
        {
            foreach (PresentationContent pc in currentlyActiveContents)
            {
                pc.updateTransitions();
            }
        }

        void FixedUpdate()
        {
            foreach(PresentationContent pc in currentlyActiveContents)
            {
                pc.fixedUpdateTransitions();
            }
        }
        #endregion
    }

}

