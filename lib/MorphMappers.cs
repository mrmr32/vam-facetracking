using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;

namespace FacialTrackerVamPlugin
{
    public class MorphMappers
    {

        public DAZMorphLibrary dazMorphLibrary;
        public SRanipalMorphLibrary sranipalMorphLibrary;

        // eventually these might be user-editable via UI controls
        private static float factorDivisorTongueStep2 = 3;
        private static float factorDivisorTongueUp = 2;
        private static float factorMultiplierMouthOpen = 1.2f;
        private static float factorMultiplierMouthFrown = 1.5f;
        private static float factorDivisorMouthPouty = 2;

        public static void JawRight()
        {
            _setMorphValue(DAZMorphLibrary.JawRight, SRanipalMorphLibrary.Jaw_Right);
        }

        public static void JawLeft()
        {
            _setMorphValue(DAZMorphLibrary.JawLeft, SRanipalMorphLibrary.Jaw_Left);
        }

        public static void JawForward()
        {
            _setMorphValue(DAZMorphLibrary.JawForward, SRanipalMorphLibrary.Jaw_Forward);
        }

        public static void MouthOpen()
        {
            _setMorphValue(DAZMorphLibrary.MouthOpenWide, SRanipalMorphLibrary.Jaw_Open * factorMultiplierMouthOpen);
        }

        public static void LipUpperUpRight()
        {
            _setMorphValue(DAZMorphLibrary.LipUpperUp_R, SRanipalMorphLibrary.Mouth_Upper_UpRight);
        }

        public static void LipUpperUpLeft()
        {
            _setMorphValue(DAZMorphLibrary.LipUpperUp_L, SRanipalMorphLibrary.Mouth_Upper_UpLeft);
        }

        public static void LipLowerDownRight()
        {
            _setMorphValue(DAZMorphLibrary.LipLowerDown_R, SRanipalMorphLibrary.Mouth_Lower_DownRight);
        }

        public static void LipLowerDownLeft()
        {
            _setMorphValue(DAZMorphLibrary.LipLowerDown_L, SRanipalMorphLibrary.Mouth_Lower_DownLeft);
        }

        public static void MouthPout()
        {
            _setMorphValue(DAZMorphLibrary.LipsPucker, SRanipalMorphLibrary.Mouth_Pout);
            _setMorphValue(DAZMorphLibrary.MouthPouty, SRanipalMorphLibrary.Mouth_Pout / factorDivisorMouthPouty);
        }

        public static void MouthSmileRight()
        {
            _setMorphValue(DAZMorphLibrary.MouthSmile_R, SRanipalMorphLibrary.Mouth_Smile_Right);
        }

        public static void MouthSmileLeft()
        {
            _setMorphValue(DAZMorphLibrary.MouthSmile_L, SRanipalMorphLibrary.Mouth_Smile_Left);
        }

        public static void MouthFrownRight()
        {
            _setMorphValue(DAZMorphLibrary.MouthFrown_R, SRanipalMorphLibrary.Mouth_Sad_Right * factorMultiplierMouthFrown);
        }

        public static void MouthFrownLeft()
        {
            _setMorphValue(DAZMorphLibrary.MouthFrown_L, SRanipalMorphLibrary.Mouth_Sad_Left * factorMultiplierMouthFrown);
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

            _setMorphValue(DAZMorphLibrary.CheekSuckRight, vRight);
            _setMorphValue(DAZMorphLibrary.CheekSuckLeft, vLeft);
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

            _setMorphValue(DAZMorphLibrary.TongueInOut, vInOut);
            _setMorphValue(DAZMorphLibrary.TongueLength, vLength);
            _setMorphValue(DAZMorphLibrary.TongueRaiseLower, vRaiseLower);
            _setMorphValue(DAZMorphLibrary.TongueRoll2, vRoll);
            _setMorphValue(DAZMorphLibrary.TongueSideSide, vSideSide);

        }

        // Class constructor
        public MorphMappers(Atom containingAtom, float defaultMorphValue, Boolean ignoreMissingMorphs)
        {
            dazMorphLibrary = new DAZMorphLibrary(containingAtom, defaultMorphValue, ignoreMissingMorphs);
            sranipalMorphLibrary = new SRanipalMorphLibrary();
        }

        // Function to run all mappers at once
        public void _runAll(JSONNode newSranipalMorphValues = null)
        {
            // If new SRanipal morph values were passed, update morph library
            if (newSranipalMorphValues != null) sranipalMorphLibrary._updateFromJsonNode(newSranipalMorphValues);

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

            // ... any new mapper functions should be called here

        }

        // Call this to update a morph value instead of calling SetValue directly. 
        // If a morph wasn't found, we won't try to update it. Required because Male person atoms may lack Female morphs. 
        private static void _setMorphValue(DAZMorph morph, float value)
        {
            if (morph != null) morph.SetValue(value);
        }

    }
}