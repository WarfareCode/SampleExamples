#region UsingDirectives
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.TextBatch
{
    class TextBatchUnicodeCodeSnippet : CodeSnippet
    {
        public TextBatchUnicodeCodeSnippet()
            : base(@"Primitives\TextBatch\TextBatchUnicodeCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfUnicodeStrings",
            /* Description */ "Draw a set of strings in various languages",
            /* Category    */ "Graphics | Primitives | Text Batch Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTextBatchPrimitive"
           )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
            #region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            //
            // Create the strings
            //
            char[] german = { '\u0057', '\u0069', '\u006C', '\u006B', '\u006F', '\u006D', '\u006D', '\u0065', '\u006E' };
            char[] french = { '\u0042', '\u0069', '\u0065', '\u006E', '\u0076', '\u0065', '\u006E', '\u0075', '\u0065', '\u0020' };
            char[] portuguese = { '\u0042', '\u0065', '\u006D', '\u002D', '\u0076', '\u0069', '\u006E', '\u0064', '\u006F' };
            char[] english = { '\u0057', '\u0065', '\u006C', '\u0063', '\u006F', '\u006D', '\u0065' };
            char[] russian = { '\u0414', '\u043E', '\u0431', '\u0440', '\u043E', '\u0020', '\u043F', '\u043E', 
                               '\u0436', '\u0430', '\u043B', '\u043E', '\u0432', '\u0430', '\u0442', '\u044C' };
            char[] arabic = {  '\u0623', '\u0647', '\u0644', '\u0627', '\u064B', '\u0020', '\u0648', '\u0020', 
                               '\u0633', '\u0647', '\u0644', '\u0627', '\u064B' };

            Array text = new object[]
            {
                new string(german),
                new string(french),
                new string(portuguese),
                new string(english),
                new string(russian),
                new string(arabic)
            };

            //
            // Create the positions
            //
            Array positions = new object[]
            {
                /*$lat1$The latitude of the first string$*/51.00, /*$lon1$The longitude of the first string$*/09.00, /*$alt1$The altitude of the first string$*/0.0,
                /*$lat2$The latitude of the second string$*/46.00, /*$lon2$The longitude of the second string$*/02.00, /*$alt2$The altitude of the second string$*/0.0,
                /*$lat3$The latitude of the third string$*/39.50, /*$lon3$The longitude of the third string$*/-08.00, /*$alt3$The altitude of the third string$*/0.0,
                /*$lat4$The latitude of the fourth string$*/38.00, /*$lon4$The longitude of the fourth string$*/-97.00, /*$alt4$The altitude of the fourth string$*/0.0,
                /*$lat5$The latitude of the fifth string$*/60.00, /*$lon5$The longitude of the fifth string$*/100.00, /*$alt5$The altitude of the fifth string$*/0.0,
                /*$lat6$The latitude of the sixth string$*/25.00, /*$lon6$The longitude of the sixth string$*/45.00, /*$alt6$The altitude of the sixth string$*/0.0
            };

            IAgStkGraphicsGraphicsFont font = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline(/*$fontName$Name of the font to use$*/"Unicode", /*$fontSize$Size of the font$*/12, /*$fontStyle$The style of the font$*/AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold, /*$showOutline$Whether or not to should an outline around the text$*/true);
            IAgStkGraphicsTextBatchPrimitive textBatch = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font);
            textBatch.SetCartographic(/*$planetName$The planet on which the text will be placed$*/"Earth", ref positions, ref text);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)textBatch);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)textBatch;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Primitive != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                Array center = m_Primitive.BoundingSphere.Center;
                IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(
                    ref center, m_Primitive.BoundingSphere.Radius * 2);

                ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            if (m_Primitive != null)
            {
                manager.Primitives.Remove(m_Primitive);
                scene.Render();

                m_Primitive = null;
            }
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
