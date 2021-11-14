using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DawnOfMan;
using HarmonyLib;
using UnityEngine;
using System.Reflection;

namespace CameraHigh {

    internal class CameraModeInteractiveProxy {

        private static CameraModeInteractiveProxy _currentInstance;

        internal static CameraModeInteractiveProxy get(CameraModeInteractive cameraModeInteractive) {
            if (_currentInstance == null || !object.ReferenceEquals(_currentInstance.cameraModeInteractive , cameraModeInteractive )) {
                _currentInstance = new CameraModeInteractiveProxy(cameraModeInteractive);
            }
            return _currentInstance;
        }

        private static MethodInfo mi_updateFocus = AccessTools.Method(typeof(CameraModeInteractive), "updateFocus");
        private static MethodInfo mi_refreshVerticalRotation = AccessTools.Method(typeof(CameraModeInteractive), "refreshVerticalRotation");

        private CameraModeInteractive cameraModeInteractive;
        private Traverse t_cameraModeInteractive;

        public float mZoomAxis;
        public float mRotationAcceleration;
        public float mOrbitAcceleration;
        public float mPitchAcceleration;

        public Traverse<GameCamera> mCamera;
        public Traverse<Vector3> mAcceleration;
        public Traverse<Vector3> mFocusTarget;

        public FastInvokeHandler updateFocus;
        public FastInvokeHandler refreshVerticalRotation;

        private CameraModeInteractiveProxy(CameraModeInteractive cameraModeInteractive) {
            this.cameraModeInteractive = cameraModeInteractive;
            this.t_cameraModeInteractive = Traverse.Create(this.cameraModeInteractive);
            this.mCamera = t_cameraModeInteractive.Field<GameCamera>("mCamera");
            this.mAcceleration = t_cameraModeInteractive.Field<Vector3>("mAcceleration");
            this.mFocusTarget = t_cameraModeInteractive.Field<Vector3>("mFocusTarget");
            this.updateFocus = MethodInvoker.GetHandler(mi_updateFocus);
            this.refreshVerticalRotation = MethodInvoker.GetHandler(mi_refreshVerticalRotation);
        }

    }

}
