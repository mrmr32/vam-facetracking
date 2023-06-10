using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SimpleJSON;

namespace FacialTrackerVamPlugin
{
    public class SRanipalMorphLibrary
    {
        public static float Jaw_Right = 0;
        public static float Jaw_Left = 0;
        public static float Jaw_Forward = 0;
        public static float Jaw_Open = 0;
        public static float Mouth_Ape_Shape = 0;
        public static float Mouth_Upper_Right = 0;
        public static float Mouth_Upper_Left = 0;
        public static float Mouth_Lower_Right = 0;
        public static float Mouth_Lower_Left = 0;
        public static float Mouth_Upper_Overturn = 0;
        public static float Mouth_Lower_Overturn = 0;
        public static float Mouth_Pout = 0;
        public static float Mouth_Smile_Right = 0;
        public static float Mouth_Smile_Left = 0;
        public static float Mouth_Sad_Right = 0;
        public static float Mouth_Sad_Left = 0;
        public static float Cheek_Puff_Right = 0;
        public static float Cheek_Puff_Left = 0;
        public static float Cheek_Suck = 0;
        public static float Mouth_Upper_UpRight = 0;
        public static float Mouth_Upper_UpLeft = 0;
        public static float Mouth_Lower_DownRight = 0;
        public static float Mouth_Lower_DownLeft = 0;
        public static float Mouth_Upper_Inside = 0;
        public static float Mouth_Lower_Inside = 0;
        public static float Mouth_Lower_Overlay = 0;
        public static float Tongue_LongStep1 = 0;
        public static float Tongue_Up = 0;
        public static float Tongue_Left = 0;
        public static float Tongue_Right = 0;
        public static float Tongue_Down = 0;
        public static float Tongue_Roll = 0;
        public static float Tongue_LongStep2 = 0;
        public static float Tongue_UpRight_Morph = 0;
        public static float Tongue_UpLeft_Morph = 0;
        public static float Tongue_DownRight_Morph = 0;
        public static float Tongue_DownLeft_Morph = 0;

        public SRanipalMorphLibrary()
        {

        }

        public void _updateFromJsonNode(JSONNode sranipalValues)
        {

            try
            {

                float.TryParse(sranipalValues["Jaw_Right"], out Jaw_Right);
                float.TryParse(sranipalValues["Jaw_Left"], out Jaw_Left);
                float.TryParse(sranipalValues["Jaw_Forward"], out Jaw_Forward);
                float.TryParse(sranipalValues["Jaw_Open"], out Jaw_Open);
                float.TryParse(sranipalValues["Mouth_Ape_Shape"], out Mouth_Ape_Shape);
                float.TryParse(sranipalValues["Mouth_Upper_Right"], out Mouth_Upper_Right); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Upper_Left"], out Mouth_Upper_Left); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Lower_Right"], out Mouth_Lower_Right); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Lower_Left"], out Mouth_Lower_Left); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Upper_Overturn"], out Mouth_Upper_Overturn); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Lower_Overturn"], out Mouth_Lower_Overturn); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Pout"], out Mouth_Pout);
                float.TryParse(sranipalValues["Mouth_Smile_Right"], out Mouth_Smile_Right);
                float.TryParse(sranipalValues["Mouth_Smile_Left"], out Mouth_Smile_Left);
                float.TryParse(sranipalValues["Mouth_Sad_Right"], out Mouth_Sad_Right);
                float.TryParse(sranipalValues["Mouth_Sad_Left"], out Mouth_Sad_Left);
                float.TryParse(sranipalValues["Cheek_Puff_Right"], out Cheek_Puff_Right);
                float.TryParse(sranipalValues["Cheek_Puff_Left"], out Cheek_Puff_Left);
                float.TryParse(sranipalValues["Cheek_Suck"], out Cheek_Suck);
                float.TryParse(sranipalValues["Mouth_Upper_UpRight"], out Mouth_Upper_UpRight);
                float.TryParse(sranipalValues["Mouth_Upper_UpLeft"], out Mouth_Upper_UpLeft);
                float.TryParse(sranipalValues["Mouth_Lower_DownRight"], out Mouth_Lower_DownRight);
                float.TryParse(sranipalValues["Mouth_Lower_DownLeft"], out Mouth_Lower_DownLeft);
                float.TryParse(sranipalValues["Mouth_Upper_Inside"], out Mouth_Upper_Inside); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Lower_Inside"], out Mouth_Lower_Inside); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Mouth_Lower_Overlay"], out Mouth_Lower_Overlay); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Tongue_LongStep1"], out Tongue_LongStep1);
                float.TryParse(sranipalValues["Tongue_Up"], out Tongue_Up);
                float.TryParse(sranipalValues["Tongue_Left"], out Tongue_Left);
                float.TryParse(sranipalValues["Tongue_Right"], out Tongue_Right);
                float.TryParse(sranipalValues["Tongue_Down"], out Tongue_Down);
                float.TryParse(sranipalValues["Tongue_Roll"], out Tongue_Roll);
                float.TryParse(sranipalValues["Tongue_LongStep2"], out Tongue_LongStep2);
                float.TryParse(sranipalValues["Tongue_UpRight_Morph"], out Tongue_UpRight_Morph); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Tongue_UpLeft_Morph"], out Tongue_UpLeft_Morph); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Tongue_DownRight_Morph"], out Tongue_DownRight_Morph); // NOT CURRENTLY USED
                float.TryParse(sranipalValues["Tongue_DownLeft_Morph"], out Tongue_DownLeft_Morph); // NOT CURRENTLY USED

            }
            catch (Exception e)
            {
                SuperController.LogError($"Unable to retrieve SRanipal morph values from JSON message");
                throw e;
            }

        }

    }

}