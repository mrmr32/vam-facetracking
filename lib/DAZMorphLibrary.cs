using System;
using System.Collections.Generic;

namespace FacialTrackerVamPlugin
{
    public class DAZMorphLibrary
    {

        // Morphs in scope
        public static DAZMorph JawRight;
        public static DAZMorph JawLeft;
        public static DAZMorph JawForward;
        public static DAZMorph MouthOpenWide;
        public static DAZMorph LipUpperUp_R;
        public static DAZMorph LipUpperUp_L;
        public static DAZMorph LipLowerDown_R;
        public static DAZMorph LipLowerDown_L;
        public static DAZMorph MouthSmile_R;
        public static DAZMorph MouthSmile_L;
        public static DAZMorph MouthFrown_R;
        public static DAZMorph MouthFrown_L;
        public static DAZMorph MouthPouty;
        public static DAZMorph LipsPucker;
        public static DAZMorph CheekSuckLeft;
        public static DAZMorph CheekSuckRight;
        public static DAZMorph TongueInOut;
        public static DAZMorph TongueLength;
        public static DAZMorph TongueRaiseLower;
        public static DAZMorph TongueRoll2;
        public static DAZMorph TongueSideSide;

        // Other properties
        private static GenerateDAZMorphsControlUI morphUI;
        private static float defaultMorphValue;
        private static Boolean ignoreMissingMorphs;

        // Get morphs from attached Person atom
        public DAZMorphLibrary(Atom containingAtom, float defaultMorphValueParam, Boolean ignoreMissingMorphsParam)
        {
            if (containingAtom.type != "Person")
            {
                throw new System.Exception("Plugin must be attached to a Person atom");
            }

            JSONStorable js = containingAtom.GetStorableByID("geometry");
            DAZCharacterSelector dcs = js as DAZCharacterSelector;
            morphUI = dcs.morphsControlUI;

            defaultMorphValue = defaultMorphValueParam;
            ignoreMissingMorphs = ignoreMissingMorphsParam;

            // Get morphs by their UID and set default values
            try
            {
                JawRight = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/JawRight.vmi",
                    "JawRight"
                );
                JawLeft = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/JawLeft.vmi",
                    "JawLeft"
                );
                JawForward = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/JawForward.vmi",
                    "JawForward"
                );

                MouthOpenWide = _initMorph("Mouth Open Wide");

                LipUpperUp_R = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/LipUpperUp_R.vmi",
                    "LipUpperUp_R"
                );
                LipUpperUp_L = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/LipUpperUp_L.vmi",
                    "LipUpperUp_L"
                );
                LipLowerDown_R = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/LipLowerDown_R.vmi",
                    "LipLowerDown_R"
                );
                LipLowerDown_L = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/LipLowerDown_L.vmi",
                    "LipLowerDown_L"
                );

                MouthSmile_R = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/MouthSmile_R.vmi",
                    "MouthSmile_R"
                );
                MouthSmile_L = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/MouthSmile_L.vmi",
                    "MouthSmile_L"
                );
                MouthFrown_R = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/MouthFrown_R.vmi",
                    "MouthFrown_R"
                );
                MouthFrown_L = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/MouthFrown_L.vmi",
                    "MouthFrown_L"
                );

                LipsPucker = _initMorph(
                    "Jackaroo.JarModularExpressions.2:/Custom/Atom/Person/Morphs/female/Jackaroo/JaRExpressions1.2/LipsPucker.vmi",
                    "LipsPucker"
                );

                MouthPouty = _initMorph("MouthPouty", "MouthPouty");

                CheekSuckLeft = _initMorph(
                    "WeebU.My_morphs.3:/Custom/Atom/Person/Morphs/female/[Free] SquarePeg3D/Sex Physics/BJ Cheeks - Suck Left.vmi",
                    "BJ Cheeks - Suck Left"
                );
                CheekSuckRight = _initMorph(
                    "WeebU.My_morphs.3:/Custom/Atom/Person/Morphs/female/[Free] SquarePeg3D/Sex Physics/BJ Cheeks - Suck Right.vmi",
                    "BJ Cheeks - Suck Right"
                );

                TongueInOut = _initMorph("Tongue In-Out");
                TongueLength = _initMorph("Tongue Length");
                TongueRaiseLower = _initMorph("Tongue Raise-Lower");
                TongueRoll2 = _initMorph("Tongue Roll 2");
                TongueSideSide = _initMorph("Tongue Side-Side");
            }
            catch (Exception e)
            {
                SuperController.LogError($"Error creating DAZMorph library: {e}");
                throw e;
            }

        }

        private static DAZMorph _initMorph(string morphUid, string morphName = null)
        {

            List<string> idList = new List<string> { morphUid};
            if (morphName != null) idList.Add(morphName);

            DAZMorph morph;

            try
            {
                morph = morphUI.GetMorphByUid(morphUid);
                morph.SetValue(defaultMorphValue);
                return morph;
            }
            catch { }

            try
            {
                morph = morphUI.GetMorphByDisplayName(morphName);
                morph.SetValue(defaultMorphValue);
                return morph;
            }
            catch { }

            string missingMorphMsg = $"Unable to find morph with UID/name {String.Join(",", idList.ToArray())}. Either a required .var dependency is missing or these morphs are not supported on this Person type.";
            if (ignoreMissingMorphs)
            {
                SuperController.LogError(missingMorphMsg);
                SuperController.LogError($"**IGNORE_MISSING_MORPHS is True - will ignore this morph**\n");
            }
            else
            {
                throw new Exception(missingMorphMsg);
            }

            return null;
        }
                    

    }
}

