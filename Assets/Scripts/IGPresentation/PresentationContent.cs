using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGPresentation
{
    /// <summary>
    /// PresentationContent allows to manage a single set of content processes. It represents, for instance, a slide content that need to be displayed (through a content process).
    /// </summary>
    public class PresentationContent : MonoBehaviour
    {
        public List<ContentProcess> TransitionProcesses = new List<ContentProcess>();

        private HashSet<ContentProcess> activeProcesses = new HashSet<ContentProcess>();

        public void updateTransitions()
        {
            foreach(ContentProcess cp in activeProcesses)
            {
                cp.updateProcess();
            }
        }

        public void fixedUpdateTransitions()
        {
            foreach (ContentProcess cp in activeProcesses)
            {
                cp.fixedUpdateProcess();
            }
        }

        public bool isTransiting()
        {
            return activeProcesses.Count > 0;
        }

        public void startTransitions()
        {
            
        }

        public void forceEndTransitions()
        {
            foreach(ContentProcess cp in activeProcesses)
            {
                cp.endProcess();
            }
        }

        public void resetTransitions()
        {
            foreach(ContentProcess cp in activeProcesses)
            {
                cp.reset();
            }
        }
    }

}

