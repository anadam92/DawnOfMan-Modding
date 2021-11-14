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

    [HarmonyPatch(typeof(CameraModeInteractive), "update", MethodType.Normal)]
    public static class CameraModeInteractive_update_Patch {

        [HarmonyPrefix]
        public static bool Prefix(CameraModeInteractive __instance, float timeStep) {
            CameraModeInteractiveProxy cmip = CameraModeInteractiveProxy.get(__instance);
            GameCamera mCamera = cmip.mCamera.Value;
            Vector3 mAcceleration = cmip.mAcceleration.Value;

            bool flag = false;
            if (Singleton<GuiManager>.Instance.anyModalPanels()) {
                return false;
            }
            if (cmip.mFocusTarget.Value != Constants.Vector3Zero) {
                cmip.updateFocus(__instance , new object[] { timeStep });
                return false ;
            }

            if (cmip.mZoomAxis == 0f) {
                cmip.mZoomAxis = Input.GetAxis("Zoom");
            }

            float ax = mAcceleration.x;
            float ay = mAcceleration.y;
            float az = mAcceleration.z;

            float currentHeight = mCamera.getCurrentHeight();
            float heightFactor = (currentHeight / CameraHigh. DefaultHeight) * 0.5f;
            Transform transform = mCamera.getTransform();

            // UPDATE position transform
            if (Mathf.Abs(ay) > CameraHigh.MinDisplacement) {
                float num2 = Mathf.Clamp(CameraHigh.ZoomStep * timeStep * heightFactor, 0.01f, 100f);
                float num3 = Mathf.Clamp(currentHeight + ay * num2, CameraHigh.MinHeight, CameraHigh.MaxHeight);
                az += (currentHeight - num3) / num2;
                mCamera.setCurrentHeight(num3);
                cmip.refreshVerticalRotation(__instance, new object[] { });
                flag = true;
            }
            if (Mathf.Abs(az) > CameraHigh.MinDisplacement) {
                transform.position = transform.position + MathUtil.flatten(transform.forward) * az * timeStep * CameraHigh.TranslationStep * heightFactor;
                flag = true;
            }
            if (Mathf.Abs(ax) > CameraHigh.MinDisplacement) {
                transform.position = transform.position + MathUtil.flatten(transform.right) * ax * timeStep * CameraHigh.TranslationStep * heightFactor;
                flag = true;
            }
            if (Mathf.Abs(cmip.mRotationAcceleration) > 0.01f) {
                transform.RotateAround(transform.position, Constants.Vector3Up, cmip.mRotationAcceleration * timeStep * CameraHigh.RotationStep);
                flag = true;
            }
            if (Mathf.Abs(cmip.mOrbitAcceleration) > 0.01f) {
                float newOrbitRotationAngleChange = (0f - cmip.mOrbitAcceleration) * timeStep * CameraHigh.RotationStep;
                transform.RotateAround(transform.position + transform.forward * currentHeight * 1.5f, Constants.Vector3Up, newOrbitRotationAngleChange);
                flag = true;
            }
            if (Mathf.Abs(cmip.mPitchAcceleration) > 0.001f) {
                float newPitchRotationAngleChange = (0f - cmip.mPitchAcceleration) * timeStep * CameraHigh.ZoomStep;
                float currentPitch = Mathf.Clamp(mCamera.getCurrentPitch() + newPitchRotationAngleChange, CameraHigh.MinPitch, CameraHigh.MaxPitch);
                mCamera.setCurrentPitch(currentPitch);
                cmip.refreshVerticalRotation(__instance, new object[] { });
                flag = true;
            }

            // BOUND CAMERA DISPLACEMENT HORIZONTALLY (X, Z)
            Vector3 boundsMin = mCamera.getBoundsMin();
            Vector3 boundsMax = mCamera.getBoundsMax();
            float tpx_bounded = Mathf.Clamp(transform.position.x, boundsMin.x, boundsMax.x);
            float tpz_bounded = Mathf.Clamp(transform.position.z, boundsMin.z, boundsMax.z);
            transform.position = new Vector3(tpx_bounded, transform.position.y, tpz_bounded);

            if ((Mathf.Abs(az) > CameraHigh.MinDisplacement || Mathf.Abs(ax) > CameraHigh.MinDisplacement || Mathf.Abs(cmip.mOrbitAcceleration) > 0.01f) && !Input.GetKey(KeyCode.LeftControl)) {
                mCamera.placeOnFloor();
            }
            if (flag) {
                mCamera.updateTerrainOcclusion();
            }


            return false;
        }

    }

}
