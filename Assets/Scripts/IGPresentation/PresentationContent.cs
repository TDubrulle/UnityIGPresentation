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
        /// <summary> The list of transitions that need to be played when this content is activated.</summary>
        public List<ContentProcess> transitionProcesses = new List<ContentProcess>();

        private HashSet<ContentProcess> activeProcesses = new HashSet<ContentProcess>();

        protected void Awake()
        {
            if(transitionProcesses.Count == 0)
            {
                Debug.LogWarning(this.gameObject.name + " : has no contentProcess to use.");
            }
        }

        /// <summary>
        /// Update the transitions as a standard update.
        /// </summary>
        public void updateTransitions()
        {
            foreach(ContentProcess cp in activeProcesses)
            {
                cp.updateProcess();
                checkActiveProcessEnded(cp);
            }
        }

        /// <summary>
        /// Update the transitions as a fixed update.
        /// </summary>
        public void fixedUpdateTransitions()
        {
            foreach (ContentProcess cp in activeProcesses)
            {
                cp.fixedUpdateProcess();
                checkActiveProcessEnded(cp);
            }
        }

        /// <summary>
        /// Returns whether the presentationContent is transiting or not.
        /// </summary>
        /// <returns>true if the presentationContent is currently transiting, false otherwise.</returns>
        public bool isTransiting()
        {
            return activeProcesses.Count > 0;
        }

        /// <summary>
        /// start all content processes bound to this content.
        /// </summary>
        public void startTransitions()
        {
            if(!isTransiting())
            {
                for(int i = 0; i < transitionProcesses.Count; ++i)
                {
                    activeProcesses.Add(transitionProcesses[i]);
                }
                foreach(ContentProcess cp in activeProcesses)
                {
                    cp.startProcess();
                }
            }
        }

        /// <summary>
        /// Set all transition to their ending state.
        /// </summary>
        public void forceEndTransitions()
        {
            if(isTransiting())
            {
                //We only need to end prematurely active processes.
                foreach (ContentProcess cp in activeProcesses)
                {
                    cp.endProcess();
                    checkActiveProcessEnded(cp);
                }
            } else
            {
                for(int i = 0; i < transitionProcesses.Count; ++i)
                {
                    transitionProcesses[i].endProcess();
                }
            }

        }

        /// <summary>
        /// Reset all transitions to their original state, and discard any active transition.
        /// </summary>
        public void resetTransitions()
        {
            for(int i = 0; i < transitionProcesses.Count; ++i)
            {
                transitionProcesses[i].reset();
            }
            activeProcesses.Clear();
        }
        
        private void checkActiveProcessEnded(ContentProcess cp)
        {
            if(cp.state == ContentProcessState.ENDED)
            {
                activeProcesses.Remove(cp);
            }
        }
    }

}