using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using UnityEngine;
using System.Collections;

namespace FacialTrackerVamPlugin
{
    public class MorphMappers
    {

        public DAZMorphLibrary dazMorphLibrary;
        public SRanipalMorphLibrary sranipalMorphLibrary;

        // eventually these might be user-editable via UI controls
        private static float factorDivisorTongueStep2 = 3;
        private static float factorDivisorTongueUp = 1;
        private static float factorMultiplierMouthOpen = 1.9f;
        private static float factorMultiplierMouthFrown = 1.5f;
        private static float factorDivisorMouthPouty = 2;
		private static float factorGlobal = 1f;

        private MVRScript script;

        private FreeControllerV3 _eyeTarget;
        private MotionAnimationControl _head;


        public static void JawRight()
        {
            _setMorphValue(DAZMorphLibrary.JawRight, SRanipalMorphLibrary.Jaw_Right * factorGlobal * 2);
        }

        public static void JawLeft()
        {
            _setMorphValue(DAZMorphLibrary.JawLeft, SRanipalMorphLibrary.Jaw_Left * factorGlobal * 2);
        }

        public static void JawForward()
        {
            _setMorphValue(DAZMorphLibrary.JawForward, SRanipalMorphLibrary.Jaw_Forward * factorGlobal * 100);
        }

        public static void MouthOpen()
        {
            _setMorphValue(DAZMorphLibrary.MouthOpenWide, SRanipalMorphLibrary.Jaw_Open * factorMultiplierMouthOpen * factorGlobal);
        }

        public static void LipUpperUpRight()
        {
            _setMorphValue(DAZMorphLibrary.LipUpperUp_R, SRanipalMorphLibrary.Mouth_Upper_UpRight * factorGlobal * 1);
        }

        public static void LipUpperUpLeft()
        {
            _setMorphValue(DAZMorphLibrary.LipUpperUp_L, SRanipalMorphLibrary.Mouth_Upper_UpLeft * factorGlobal * 1);
        }

        public static void LipLowerDownRight()
        {
            _setMorphValue(DAZMorphLibrary.LipLowerDown_R, SRanipalMorphLibrary.Mouth_Lower_DownRight * factorGlobal * 0.7f);
        }

        public static void LipLowerDownLeft()
        {
            _setMorphValue(DAZMorphLibrary.LipLowerDown_L, SRanipalMorphLibrary.Mouth_Lower_DownLeft * factorGlobal * 0.7f);
        }

        public static void MouthPout()
        {
            _setMorphValue(DAZMorphLibrary.LipsPucker, SRanipalMorphLibrary.Mouth_Pout * factorGlobal);
            _setMorphValue(DAZMorphLibrary.MouthPouty, SRanipalMorphLibrary.Mouth_Pout / factorDivisorMouthPouty * factorGlobal);
        }

        public static void MouthSmileRight()
        {
            _setMorphValue(DAZMorphLibrary.MouthSmile_R, SRanipalMorphLibrary.Mouth_Smile_Right * factorGlobal * 1.2f);
        }

        public static void MouthSmileLeft()
        {
            _setMorphValue(DAZMorphLibrary.MouthSmile_L, SRanipalMorphLibrary.Mouth_Smile_Left * factorGlobal * 1.2f);
        }

        public static void MouthFrownRight()
        {
            _setMorphValue(DAZMorphLibrary.MouthFrown_R, SRanipalMorphLibrary.Mouth_Sad_Right * factorMultiplierMouthFrown * factorGlobal);
        }

        public static void MouthFrownLeft()
        {
            _setMorphValue(DAZMorphLibrary.MouthFrown_L, SRanipalMorphLibrary.Mouth_Sad_Left * factorMultiplierMouthFrown * factorGlobal);
        }

        public static void CheekPuffSuck()
        {
            float vRight = 0;
            float vLeft = 0;

            if (SRanipalMorphLibrary.Cheek_Suck > 0)
            {
                vRight = SRanipalMorphLibrary.Cheek_Suck;
                vLeft = SRanipalMorphLibrary.Cheek_Suck;
            }
            else
            {
                vRight = 0 - SRanipalMorphLibrary.Cheek_Puff_Right;
                vLeft = 0 - SRanipalMorphLibrary.Cheek_Puff_Left;
            }

            _setMorphValue(DAZMorphLibrary.CheekSuckRight, vRight * factorGlobal);
            _setMorphValue(DAZMorphLibrary.CheekSuckLeft, vLeft * factorGlobal);
        }

        public static void Tongue()
        {
            float vInOut = 1 - SRanipalMorphLibrary.Tongue_LongStep1;

            float vLength = SRanipalMorphLibrary.Tongue_LongStep2 / factorDivisorTongueStep2;

            float vRaiseLower = 0;
            if (SRanipalMorphLibrary.Tongue_Up > 0)
            {
                vRaiseLower = SRanipalMorphLibrary.Tongue_Up;
            }
            else if (SRanipalMorphLibrary.Tongue_Down > 0)
            {
                vRaiseLower = 0 - (SRanipalMorphLibrary.Tongue_Up / factorDivisorTongueUp);
            }

            float vRoll = SRanipalMorphLibrary.Tongue_Roll;

            float vSideSide = 0;
            if (SRanipalMorphLibrary.Tongue_Right > 0)
            {
                vSideSide = SRanipalMorphLibrary.Tongue_Right;
            }
            else if (SRanipalMorphLibrary.Tongue_Left > 0)
            {
                vSideSide = 0 - SRanipalMorphLibrary.Tongue_Left;
            }

            _setMorphValue(DAZMorphLibrary.TongueInOut, vInOut * factorGlobal);
            _setMorphValue(DAZMorphLibrary.TongueLength, vLength * factorGlobal);
            _setMorphValue(DAZMorphLibrary.TongueRaiseLower, 0.3f+vRaiseLower * factorGlobal);
            _setMorphValue(DAZMorphLibrary.TongueRoll2, vRoll * factorGlobal * 2.5f);
            _setMorphValue(DAZMorphLibrary.TongueSideSide, vSideSide * factorGlobal);

        }

		//added
		public static void JawDown()
        {
            _setMorphValue(DAZMorphLibrary.JawChew, SRanipalMorphLibrary.Mouth_Ape_Shape * factorGlobal *1.4f);
        }

		public static void LipTopDown()
        {
            _setMorphValue(DAZMorphLibrary.LipTopDown, SRanipalMorphLibrary.Mouth_Lower_Inside * factorGlobal *1.4f);
        }
		
		public static void LipBottomIn()
        {
            _setMorphValue(DAZMorphLibrary.LipBottomIn, SRanipalMorphLibrary.Mouth_Upper_Inside * factorGlobal *1.4f);
        }

        // TODO as we're looking at a static position we should update this function every time the `containingAtom` moves
        public void Eye()
        {
            float ySeePlane = 0.5f;
            Vector3 seeVector = new Vector3(SRanipalMorphLibrary.Eye_X_Right, SRanipalMorphLibrary.Eye_Y_Right, ySeePlane);
            /*if (Mathf.Abs(SRanipalMorphLibrary.Eye_X_Right) < 0.05f && Mathf.Abs(SRanipalMorphLibrary.Eye_Y_Right) < 0.05f &&
                    (Mathf.Abs(SRanipalMorphLibrary.Eye_X_Left) > 0.05f || Mathf.Abs(SRanipalMorphLibrary.Eye_Y_Left) > 0.05f)) {
                // wink with the right eye
                seeVector = new Vector3(-SRanipalMorphLibrary.Eye_X_Left, SRanipalMorphLibrary.Eye_Y_Left, ySeePlane); // the left eye needs to be mirrored
            }*/
            
            _eyeTarget.control.position = this._head.transform.position + this._head.transform.rotation * seeVector;
        }
        
        // @see https://github.com/mrmr32/TriggerIncrementer/blob/main/TriggerIncrementer.cs#L175
        private MotionAnimationControl GetHead() {
            foreach (MotionAnimationControl mac in this.script.containingAtom.motionAnimationControls) { // TODO get head inside linkableRigidbodies?
                if (!mac.name.Equals("headControl")) continue;
                return mac;
            }

            return null; // not found
        }

        public static void Blink()
        {
            _setMorphValue(DAZMorphLibrary.EyeBlink_L, SRanipalMorphLibrary.Eye_Blink_Left * factorGlobal);
            _setMorphValue(DAZMorphLibrary.EyeBlink_R, SRanipalMorphLibrary.Eye_Blink_Right * factorGlobal);
            
            _setMorphValue(DAZMorphLibrary.EyeSquint_L, SRanipalMorphLibrary.Eye_Squint_Left * factorGlobal);
            _setMorphValue(DAZMorphLibrary.EyeSquint_R, SRanipalMorphLibrary.Eye_Squint_Right * factorGlobal);
        }

        public static void Brow()
        {
            _setMorphValue(DAZMorphLibrary.BrowDown_L, SRanipalMorphLibrary.Brow_Down_Left * factorGlobal);
            _setMorphValue(DAZMorphLibrary.BrowDown_R, SRanipalMorphLibrary.Brow_Down_Right * factorGlobal);
            _setMorphValue(DAZMorphLibrary.BrowInnerUp, SRanipalMorphLibrary.Brow_Inner_Up * factorGlobal);
            _setMorphValue(DAZMorphLibrary.BrowOuterUp_L, SRanipalMorphLibrary.Brow_Outer_Up_Left * factorGlobal);
            _setMorphValue(DAZMorphLibrary.BrowOuterUp_R, SRanipalMorphLibrary.Brow_Outer_Up_Right * factorGlobal);
        }

        // Class constructor
        public MorphMappers(Atom containingAtom, MVRScript script, float defaultMorphValue, Boolean ignoreMissingMorphs)
        {
            this.script = script;

            dazMorphLibrary = new DAZMorphLibrary(containingAtom, defaultMorphValue, ignoreMissingMorphs);
            sranipalMorphLibrary = new SRanipalMorphLibrary();

            _eyeTarget = containingAtom.freeControllers.FirstOrDefault(fc => fc.name == "eyeTargetControl");
            if (_eyeTarget == null) throw new NullReferenceException(nameof(_eyeTarget));
            if ((this._head = GetHead()) == null) throw new InvalidOperationException("Head not found in added object");
        }

        // Function to run all mappers at once
        public void _runAll(JSONNode newSranipalMorphValues = null)
        {
            // If new SRanipal morph values were passed, update morph library
            if (newSranipalMorphValues != null) sranipalMorphLibrary._updateFromJsonNode(newSranipalMorphValues.AsObject);

            // Call mapper functions
            JawRight();
            JawLeft();
            MouthOpen();
            LipUpperUpRight();
            LipUpperUpLeft();
            LipLowerDownRight();
            LipLowerDownLeft();
            MouthPout();
            MouthSmileRight();
            MouthSmileLeft();
            MouthFrownRight();
            MouthFrownLeft();
            CheekPuffSuck();
            Tongue();
			//added
			JawDown();
			LipTopDown();
			LipBottomIn();
            Eye();
            Blink();
            Brow();

            // ... any new mapper functions should be called here

        }

        // Call this to update a morph value instead of calling SetValue directly. 
        // If a morph wasn't found, we won't try to update it. Required because Male person atoms may lack Female morphs. 
        private static void _setMorphValue(DAZMorph morph, float value)
        {
            if (morph != null) {
                morph.SetValue(value);
                morph.SyncJSON();
            }
        }

    }
}