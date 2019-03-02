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
    class TextBatchCodeSnippet : CodeSnippet
    {
        public TextBatchCodeSnippet()
            : base(@"Primitives\TextBatch\TextBatchCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfStrings",
            /* Description */ "Draw a set of strings",
            /* Category    */ "Graphics | Primitives | Text Batch Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTextBatchPrimitive"
           )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            Array text = new object[]
            {
                /*$string1$The first string$*/"Philadelphia",
                /*$string2$The second string$*/"Washington, D.C.",
                /*$string3$The third string$*/"New Orleans",
                /*$string4$The fourth string$*/"San Jose"
            };

            Array positions = new object[]
            {
                /*$lat1$The latitude of the first string$*/39.88, /*$lon1$The longitude of the first string$*/-75.25, /*$alt1$The altitude of the first string$*/0,    // Philadelphia
                /*$lat2$The latitude of the second string$*/38.85, /*$lon2$The longitude of the second string$*/-77.04, /*$alt2$The altitude of the second string$*/0, // Washington, D.C.   
                /*$lat3$The latitude of the third string$*/29.98, /*$lon3$The longitude of the third string$*/-90.25, /*$alt3$The altitude of the third string$*/0, // New Orleans
                /*$lat4$The latitude of the fourth string$*/37.37, /*$lon4$The longitude of the fourth string$*/-121.92, /*$alt4$The altitude of the fourth string$*/0    // San Jose
            };

            IAgStkGraphicsGraphicsFont font = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline(/*$fontName$Name of the font to use$*/"MS Sans Serif", /*$fontSize$Size of the font$*/12, /*$fontStyle$The style of the font$*/AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold, /*$showOutline$Whether or not to should an outline around the text$*/true);
            IAgStkGraphicsTextBatchPrimitive textBatch = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font);
            ((IAgStkGraphicsPrimitive)textBatch).Color = /*$textColor$The System.Drawing.Color of the text$*/Color.White;
            textBatch.OutlineColor = /*$outlineColor$The System.Drawing.Color of the outline$*/Color.Red;
            textBatch.SetCartographic(/*$planetName$The planet on which the text will be placed$*/"Earth", ref positions, ref text);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)textBatch);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)textBatch;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Primitive != null)
            {
                ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere);
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
