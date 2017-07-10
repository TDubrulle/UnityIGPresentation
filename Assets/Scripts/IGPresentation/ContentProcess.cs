using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGPresentation
{
    public enum ContentProcessState
    {
        INACTIVE,
        STARTED,
        ENDED
    }

    [System.Serializable]
    public class ContentProcessParameters
    {
        public bool canBeForcedToEnd = true;
    }

    /// <summary>
    /// Content processes are everything that is called when a content is reached. It can range from fading in or out a canvas item to moving or rotating an object, for instance.
    /// It is meant to be lightweight, so that update and fixedUpdate is not called when the content is inactive.
    /// Content processes can be reset to their original state with the reset method.
    /// </summary>
    public abstract class ContentProcess : MonoBehaviour
    {
        public ContentProcessParameters processParameters = new ContentProcessParameters();

        private ContentProcessState currentState = ContentProcessState.INACTIVE;
        /// <summary> The current active state of the process. </summary>
        public ContentProcessState state { get { return currentState; } }

        /// <summary>
        /// Starts the process.
        /// </summary>
        public void startProcess()
        {
            if(currentState != ContentProcessState.STARTED)
            {
                currentState = ContentProcessState.STARTED;
                onStart();
            } else
            {
                Debug.LogWarning(this.gameObject.name + " : Cannot start contentProcess since it has already been started!", this.gameObject);
            }

        }

        /// <summary>
        /// Update the process as an Unity update.
        /// </summary>
        public void updateProcess()
        {
            onUpdate();
        }
        
        /// <summary>
        /// Update the process as an Unity fixedUpdate.
        /// </summary>
        public void fixedUpdateProcess()
        {
            onFixedUpdate();
        }

        /// <summary>
        /// End the process manually, if it can be forced to end.
        /// </summary>
        public void endProcess()
        {
            if(processParameters.canBeForcedToEnd)
            {
                end();
            }
        }

        /// <summary>
        /// End the process from inside the content process
        /// </summary>
        protected void end()
        {
            if(currentState != ContentProcessState.ENDED)
            {
                currentState = ContentProcessState.ENDED;
                onEnd();
            } else
            {
                Debug.LogWarning(this.gameObject.name + " : Cannot end contentProcess since it has already been ended!", this.gameObject);
            }
        }


        /// <summary> Called when the process has been started, and the bound content starts active.</summary>
        protected abstract void onStart();
        /// <summary> Called when the process is updating, and the bound content is active.</summary>
        protected abstract void onUpdate();
        /// <summary> Called when the process is updating in the physics engine, and the content is active. It should be used when dealing with physics.</summary>
        protected abstract void onFixedUpdate();
        /// <summary> Called when the process is ending. The data treated by the contentProcess should be set to its end state.</summary>
        protected abstract void onEnd();
        /// <summary> Called when the process needs to be reset to its original state.</summary>
        public abstract void reset();
    }

}