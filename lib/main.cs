using System;
using UnityEngine;
using System.Collections.Generic;
using SimpleJSON;

namespace FacialTrackerVamPlugin
{
    public class MyPlugin : MVRScript
    {

        private static readonly string IP = "127.0.0.1";
        private static readonly Int32 PORT = 27000;
        private static readonly float DEFAULT_MORPH_VALUE = 0;
        private static readonly Boolean IGNORE_MISSING_MORPHS = true;

        private static UDPSocket serverSocket;
        private static Boolean isEnabled = false;
        private static Boolean initFinished = false;

        private static Atom person;
        private static MorphMappers morphMappers;
        private static JSONNode latestParsedJson;

        private object processingLock = new object();
        private bool isProcessing = false;

        public override void Init()
        {
            try
            {

                // Are we attached to a Person atom?
                if (containingAtom.type != "Person") throw new System.Exception("Plugin must be attached to a Person atom");

                // Set up MorphMappers class, which will take care of building morph libraries for us
                morphMappers = new MorphMappers(containingAtom, DEFAULT_MORPH_VALUE, IGNORE_MISSING_MORPHS);

                // Set up UDP server socket so we can listen for messages
                startServer();

                initFinished = true;

                //SuperController.LogMessage($"{nameof(MyPlugin)} initialized");

            }
            catch (Exception e)
            {
                SuperController.LogError($"{nameof(MyPlugin)}.{nameof(Init)}: {e}");
            }
        }

        public void OnEnable()
        {
            try
            {
                if (initFinished)
                {
                    startServer();
                }
                
                //SuperController.LogMessage($"{nameof(MyPlugin)} enabled");
            }
            catch (Exception e)
            {
                SuperController.LogError($"{nameof(MyPlugin)}.{nameof(OnEnable)}: {e}");
            }
        }

        public void OnDisable()
        {
            try
            {
                serverSocket.Disconnect();
                //SuperController.LogMessage($"{nameof(MyPlugin)} disabled");
            }
            catch (Exception e)
            {
                SuperController.LogError($"{nameof(MyPlugin)}.{nameof(OnDisable)}: {e}");
            }
        }

        public void OnDestroy()
        {
            try
            {
                //SuperController.LogMessage($"{nameof(MyPlugin)} destroyed");
            }
            catch (Exception e)
            {
                SuperController.LogError($"{nameof(MyPlugin)}.{nameof(OnDestroy)}: {e}");
            }
        }

        public void MsgReceiveCallback(string msg)
        {
            lock (processingLock) {
                if (isProcessing) return; // already processing
                // else, now we're the ones processing
                isProcessing = true;
            }

            // Try to parse JSON
            try
            {
                latestParsedJson = SimpleJSON.JSON.Parse(msg);
            }
            catch (Exception e)
            {
                SuperController.LogError($"Unable to parse JSON message. Error: {e}");
                return;
            }

            // If successful, map all SRanipal morph values to DAZ morphs
            morphMappers._runAll(latestParsedJson);

            JSONStorable js = containingAtom.GetStorableByID("geometry");
            DAZCharacterSelector dcs = js as DAZCharacterSelector;
            GenerateDAZMorphsControlUI morphUI = dcs.morphsControlUI;
            
            lock (processingLock) {
                isProcessing = false; // done processing
            }
        }

        public void startServer()
        {
            serverSocket = new UDPSocket();
            serverSocket.ReceiveCallbackAction = MsgReceiveCallback;
            serverSocket.Server(IP, PORT);
        }

    }
}