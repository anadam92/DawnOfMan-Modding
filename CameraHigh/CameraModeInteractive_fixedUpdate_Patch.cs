using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using UnityModManagerNet;
using DawnOfMan;
using MadrugaShared;

namespace CameraHigh {

    [HarmonyPatch(typeof(CameraModeInteractive), "fixedUpdate", MethodType.Normal)]
    public class CameraModeInteractive_fixedUpdate_Patch {

        [HarmonyPrefix]
        public static bool Prefix(CameraModeInteractive __instance, float timeStep) {
            CameraModeInteractiveProxy cmip = CameraModeInteractiveProxy.get(__instance);
            Vector3 mAcceleration = cmip.mAcceleration.Value;

            float num = timeStep * 6f;
            float num2 = timeStep * 10f;

            CameraRotationMode cameraRotationMode = Singleton<Settings>.Instance.getCameraRotationMode();
            float inputAxisCameraMoveLeftRight = GameUtil.getInputAxis(GameAction.CameraMoveLeft, GameAction.CameraMoveRight);
            float inputAxisCameraMoveForwardBack = GameUtil.getInputAxis(GameAction.CameraMoveBack, GameAction.CameraMoveForward);
            float inputAxisCameraRotateLeftRight = GameUtil.getInputAxis(GameAction.CameraRotateLeft, GameAction.CameraRotateRight);
            float inputAxisCameraZoomInOut = GameUtil.getInputAxis(GameAction.CameraZoomOut, GameAction.CameraZoomIn);
            float inputAxisCameraPitchUpDown = GameUtil.getInputAxis(GameAction.CameraPitchDown, GameAction.CameraPitchUp);

            float mAcceleration_x = mAcceleration.x;
            float mAcceleration_y = mAcceleration.y;
            float mAcceleration_z = mAcceleration.z;

            // PAN
            mAcceleration_x += inputAxisCameraMoveLeftRight * num;
            mAcceleration_z += inputAxisCameraMoveForwardBack * num;

            // ZOOM
            mAcceleration_y -= cmip.mZoomAxis * num2;
            mAcceleration_y -= inputAxisCameraZoomInOut * num2;

            // PITCH
            cmip.mPitchAcceleration += inputAxisCameraPitchUpDown * num2;

            // MIDDLE-CLICK
            if (Input.GetMouseButton(2)) {
                float inputMouseX = Input.GetAxis("MouseX");
                if (cameraRotationMode == CameraRotationMode.Orbit) {
                    inputMouseX = 0f - inputMouseX;
                }
                inputAxisCameraRotateLeftRight += inputMouseX;
            }

            // ROTATION BY CameraRotateLeft, CameraRotateRight
            if (cameraRotationMode == CameraRotationMode.Turn) {
                cmip.mRotationAcceleration += inputAxisCameraRotateLeftRight * num2;
            }
            else {
                cmip.mOrbitAcceleration += inputAxisCameraRotateLeftRight * num2;
            }

            // EDGE SCROLLING
            if (!Application.isEditor && !Input.GetMouseButton(2) && Singleton<Settings>.Instance.getEdgeScrolling()) {
                float screenEdgePercent = (float)Screen.height * 0.01f;
                Vector3 mousePosition = Input.mousePosition;
                if (mousePosition.x >= 0f && mousePosition.y >= 0f && mousePosition.y <= (float)Screen.height && mousePosition.x <= (float)Screen.width) {
                    if (mousePosition.x < screenEdgePercent) {
                        mAcceleration_x -= num;
                    }
                    else if (mousePosition.x > (float)Screen.width - screenEdgePercent) {
                        mAcceleration_x += num;
                    }
                    if (mousePosition.y < screenEdgePercent) {
                        mAcceleration_z -= num;
                    }
                    else if (mousePosition.y > (float)Screen.height - screenEdgePercent) {
                        mAcceleration_z += num;
                    }
                }
            }

            float bound = (!Input.GetKey(KeyCode.LeftShift)) ? 1f : 0.25f;
            float bound2 = bound * 1.5f;
            mAcceleration_x = Mathf.Clamp(mAcceleration_x * (1 -  num), 0f - bound, bound);
            mAcceleration_z = Mathf.Clamp(mAcceleration_z * (1 - num), 0f - bound, bound);
            mAcceleration_y = Mathf.Clamp(mAcceleration_y * (1 - num2), 0f - bound, bound);
            cmip.mAcceleration.Value = new Vector3(mAcceleration_x, mAcceleration_y, mAcceleration_z);
            cmip.mRotationAcceleration = Mathf.Clamp(cmip.mRotationAcceleration * (1 - num2), 0f - bound, bound);
            cmip.mOrbitAcceleration = Mathf.Clamp(cmip.mOrbitAcceleration * (1 - num2), 0f - bound2, bound2);
            cmip.mPitchAcceleration = Mathf.Clamp(cmip.mPitchAcceleration * (1 - num2), 0f - bound, bound);
            cmip.mZoomAxis = 0f;

            return false;
        }

    }
}
