using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AGI.Ui.Plugins;
using AGI.STKObjects;
using System.Threading;

namespace Agi.Ui.Plugins.CSharp.Basic
{
    public partial class CustomUserInterface : UserControl, IAgUiPluginEmbeddedControl
    {
        private IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private BasicCSharpPlugin m_uiPlugin;

        public CustomUserInterface()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnClosing()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void OnSaveModified()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void SetSite(IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as BasicCSharpPlugin;
            m_root = m_uiPlugin.STKRoot;
        }

        #endregion

        private void Button1_Click(object sender, EventArgs e)
        {
            //Example use of StkObjectRoot
            if (m_root.CurrentScenario == null)
            {
                MessageBox.Show("I know that no scenario is open.");
            }
            else
            {
                string strScenName = m_root.CurrentScenario.Path.ToString();
                MessageBox.Show("I know your scenario's Connect path is " + strScenName);
            }
        }

        private void progressButton_Click(object sender, EventArgs e)
        {
            //Example use of Progress Bar
            IAgProgressTrackCancel progress = m_uiPlugin.ProgressBar;
            progress.BeginTracking(AgEProgressTrackingOptions.eProgressTrackingOptionNone, AgEProgressTrackingType.eTrackAsProgressBar);

            for (int i = 0; i <= 100; i++)
            {
                progress.SetProgress(i, "Testing the progress bar...");
                Thread.Sleep(100);
                if (!progress.Continue)
                    break;
            }

            progress.EndTracking();
        }
    }
}
