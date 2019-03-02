#region UsingDirectives
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using AGI.STKObjects;
using AGI.STKGraphics;
using System;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.TextBatch
{
    class TextBatchColorsCodeSnippet : CodeSnippet
    {
        public TextBatchColorsCodeSnippet()
            : base(@"Primitives\TextBatch\TextBatchColorsCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfColoredStrings",
            /* Description */ "Draw a set of uniquely colored strings",
            /* Category    */ "Graphics | Primitives | Text Batch Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTextBatchPrimitive"
           )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array strings = new object[]
            {
                /*$string1$The first string$*/"San Francisco",
                /*$string2$The second string$*/"Sacramento",
                /*$string3$The third string$*/"Los Angeles",
                /*$string4$The fourth string$*/"San Diego"
            };

            Array positions = new object[]
            {
                /*$lat1$The latitude of the first string$*/37.62, /*$lon1$The longitude of the first string$*/-122.38, /*$alt1$The altitude of the first string$*/0.0,
                /*$lat2$The latitude of the second string$*/38.52, /*$lon2$The longitude of the second string$*/-121.50, /*$alt2$The altitude of the second string$*/0.0,
                /*$lat3$The latitude of the third string$*/33.93, /*$lon3$The longitude of the third string$*/-118.40, /*$alt3$The altitude of the third string$*/0.0,
                /*$lat4$The latitude of the fourth string$*/32.82, /*$lon4$The longitude of the fourth string$*/-117.13, /*$alt4$The altitude of the fourth string$*/0.0
            };

            Array colors = new object[]
            {
                /*$color1$The color of the first string (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.Red.ToArgb(),
                /*$color2$The color of the second string (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.Green.ToArgb(),
                /*$color3$The color of the third string (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.Blue.ToArgb(),
                /*$color4$The color of the fourth string (a System.Drawing.Color converted to Argb and cast to an unsigned integer$*/(uint)Color.White.ToArgb()
            };

            IAgStkGraphicsTextBatchPrimitiveOptionalParameters parameters = manager.Initializers.TextBatchPrimitiveOptionalParameters.Initialize();
            parameters.SetColors(ref colors);

            IAgStkGraphicsGraphicsFont font = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline(/*$fontName$Name of the font to use$*/"MS Sans Serif", /*$fontSize$Size of the font$*/12, /*$fontStyle$The style of the font$*/AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold, /*$showOutline$Whether or not to should an outline around the text$*/false);
            IAgStkGraphicsTextBatchPrimitive textBatch = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font);
            textBatch.SetCartographicWithOptionalParameters(/*$planetName$The planet on which the text will be placed$*/"Earth", ref positions, ref strings, parameters);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)textBatch);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)textBatch;
            OverlayHelper.AddTextBox(
@"A collection of colors is provided to the TextBatchPrimitive, in addition to collections of 
positions and strings, to visualize strings with different colors.", manager);
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
                m_Primitive = null;

                OverlayHelper.RemoveTextBox(manager);
                scene.Render();
            }
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}
