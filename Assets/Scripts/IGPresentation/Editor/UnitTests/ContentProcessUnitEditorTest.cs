using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System;

namespace IGPresentation
{
    namespace test
    {
        public class DummyContentProcess : ContentProcess
        {
            public bool onStartCalled { get; private set; }
            public bool onEndCalled { get; private set; }

            public void Awake()
            {
                onStartCalled = false;
                onEndCalled = false;
            }

            public override void reset()
            {
            }

            protected override void onEnd()
            {
                onEndCalled = true;
            }

            protected override void onFixedUpdate()
            {
            }

            protected override void onStart()
            {
                onStartCalled = true;
            }

            protected override void onUpdate()
            {
            }
        }

        public class ContentProcessUnityTest
        {
            private static GameObject testedObject;
            private static ContentProcess testedContentProcess;


            public void beforeTest()
            {
                testedObject = new GameObject("test");
                testedContentProcess = testedObject.AddComponent<DummyContentProcess>();
            }

            public void afterTest()
            {
                
            }

            [Test]
            public void ConstructionTest()
            {
                beforeTest();
                Assert.AreEqual(testedContentProcess.state, ContentProcessState.INACTIVE);
                afterTest();
            }

            [Test]
            public void isStartedTest()
            {
                beforeTest();
                testedContentProcess.startProcess();
                Assert.AreEqual(testedContentProcess.state, ContentProcessState.STARTED);
                afterTest();
            }

            [Test]
            public void isForcedEndedStateTest()
            {
                beforeTest();
                testedContentProcess.processParameters.canBeForcedToEnd = true;
                testedContentProcess.endProcess();
                Assert.AreEqual(testedContentProcess.state, ContentProcessState.ENDED);
                afterTest();
            }

            [Test]
            public void isForcedEndedNotAllowedTest()
            {
                beforeTest();
                testedContentProcess.processParameters.canBeForcedToEnd = false;
                testedContentProcess.endProcess();
                //We shouldn't have ended.
                Assert.AreNotEqual(testedContentProcess.state, ContentProcessState.ENDED);
                afterTest();
            }

            [Test]
            public void isTwiceEndedWarningCheck()
            {
                beforeTest();
                testedContentProcess.processParameters.canBeForcedToEnd = true;
                testedContentProcess.endProcess();
                testedContentProcess.endProcess();
                LogAssert.Expect(LogType.Warning, testedContentProcess.gameObject.name + " : Cannot end contentProcess since it has already been ended!");
            }

            [Test]
            public void isTwiceStartedWarningCheck()
            {
                beforeTest();
                testedContentProcess.startProcess();
                testedContentProcess.startProcess();
                LogAssert.Expect(LogType.Warning, testedContentProcess.gameObject.name + " : Cannot start contentProcess since it has already been started!");

            }

        }
    }
}