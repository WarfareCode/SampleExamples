using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
using RectangularSensorStreamPluginProxy;

namespace RectangularSensorPlugin
{
    public partial class ProjectionProperties : UserControl, AGI.Ui.Plugins.IAgUiPluginEmbeddedControl
    {
        private AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AGI.STKObjects.AgStkObjectRoot m_root;
        private RectangularSensorPlugin m_uiPlugin;

        private SensorAttributes sensorAttributes;

        public ProjectionProperties()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            // No icon needed for this plugin.
            return null;
        }

        public void OnClosing()
        {
            // No OnClosing action needed for this plguin.
        }

        public void OnSaveModified()
        {
            // No OnSaveModified action need for this plugin.
        }

        public void SetSite(AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as RectangularSensorPlugin;
            m_root = m_uiPlugin.STKRoot;

            sensorAttributes = (SensorAttributes)m_uiPlugin.SensorHashtable[m_uiPlugin.SelectedSensor];

            PopulateProjectionAttributes();
        }

        #endregion

        #region File Box Events
        private void fileBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Video Formats|*.3g2;*.3gp;*.4xm;*.IFF;*.MTV;*.RoQ;*.aac;*.ac3;*.adts;*.aiff;*.alaw;*.amr;*.apc;*.ape;*.asf;*.asf_stream;*.au;*.avi;*.avm2;*.avs;*.bethsoftvid;*.c93;*.crc;*.daud;*.dsicin;*.dts;*.dv;*.dvd;*.dxa;*.ea;*.ea_cdata;*.ffm;*.film_cpk;*.flac;*.flic;*.flv;*.framecrc;*.gif;*.gxf;*.h261;*.h263;*.h264;*.idcin;*.image2;*.image2pipe;*.ingenient;*.ipmovie;*.ipod;*.libnut;*.lmlm4;*.m4v;*.matroska;*.mjpeg;*.mm;*.mmf;*.mov;*.mov;*.mp4;*.m4a;*.3gp;*.3g2;*.mj2;*.mp2;*.mp3;*.mp4;*.mpc;*.mpc8;*.mpeg;*.mpeg1video;*.mpeg2video;*.mpegts;*.mpegtsraw;*.mpegvideo;*.mpjpeg;*.msnwctcp;*.mulaw;*.mxf;*.nsv;*.null;*.nut;*.nuv;*.ogg;*.psp;*.psxstr;*.pva;*.rawvideo;*.redir;*.rl2;*.rm;*.rpl;*.rtp;*.rtsp;*.s16be;*.s16le;*.s8;*.sdp;*.shn;*.siff;*.smk;*.sol;*.svcd;*.swf;*.thp;*.tiertexseq;*.tta;*.txd;*.u16be;*.u16le;*.u8;*.vc1;*.vc1test;*.vcd;*.vfwcap;*.vmd;*.vob;*.voc;*.wav;*.wc3movie;*.wsaud;*.wsvqa;*.wv;*.yuv4mpegpipe|" +
                "Raster Formats|*.bmp;*.ecw;*img;*.jp2;*.ntf;*.nitf;*.png;*.sid;*.tif;*.tiff;*.jpg;*.jpeg;*.ppm;*.pgm;*.clds;*.tga|" +
                "All Files|*.*";

            if (open.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = open.FileName;

                // See if the selected file is a compatible video file
                try
                {
                    IAgStkGraphicsVideoStream videoStream = ((IAgScenario)m_root.CurrentScenario).SceneManager.Initializers.VideoStream.InitializeWithStringUri(fileTextBox.Text);
                    videoEndtimeBox.Text = new TimeSpan(0, 0, 0, 0, (int)(videoStream.EndTime * 1000.0)).ToString();

                    if (manualFrameRateCheckBox.Checked == false)
                        frameRateBox.Text = NumberTo6DecimalString(videoStream.FrameRate);

                    // If we make it this far without an ArgumentException, the video is compatible.
                    videoPlaybackSettingsPanel.Visible = true;
                    sensorAttributes.UriIsCompatibleVideo = true;
                }
                catch (ArgumentException)
                {
                    // The selected file is not a compatible video file.
                    videoPlaybackSettingsPanel.Visible = false;
                    sensorAttributes.UriIsCompatibleVideo = false;
                }
            }
        }

        private void clearFileBox_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove the file from this projection?", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fileTextBox.Text = String.Empty;
                sensorAttributes.UriIsCompatibleVideo = false;
                videoPlaybackSettingsPanel.Visible = false;
            }
        }

        private void clearFileBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip removeFileToolTip = new ToolTip();
            removeFileToolTip.Show("Remove file from projection.", removeFileBox, 5000);
        }
        #endregion

        #region Translucency Controls
        private void borderTranslucencyTrackBar_Scroll(object sender, EventArgs e)
        {
            borderTranslucencyTextBox.Text = NumberTo6DecimalString(borderTranslucencyTrackBar.Value);
        }

        private void shadowTranslucencyTrackBar_Scroll(object sender, EventArgs e)
        {
            shadowTranslucencyTextBox.Text = NumberTo6DecimalString(shadowTranslucencyTrackBar.Value);
        }

        private void farplaneTranslucencyTrackBar_Scroll(object sender, EventArgs e)
        {
            farPlaneTranslucencyTextBox.Text = NumberTo6DecimalString(farPlaneTranslucencyTrackBar.Value);
        }

        private void frustumTranslucencyTrackBar_Scroll(object sender, EventArgs e)
        {
            frustumTranslucencyTextBox.Text = NumberTo6DecimalString(frustumTranslucencyTrackBar.Value);
        }

        private void borderTranslucencyTextBox_Validated(object sender, EventArgs e)
        {
            float value;
            if (float.TryParse(borderTranslucencyTextBox.Text, out value))
            {
                borderTranslucencyTrackBar.Value = (int)value;
            }
            else
            {
                MessageBox.Show("The value entered for Border Translucency is not a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void shadowTranslucencyTextBox_Validated(object sender, EventArgs e)
        {
            float value;
            if (float.TryParse(shadowTranslucencyTextBox.Text, out value))
            {
                shadowTranslucencyTrackBar.Value = (int)value;
            }
            else
            {
                MessageBox.Show("The value entered for Shadow Translucency is not a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void farplaneTranslucencyTextBox_Validated(object sender, EventArgs e)
        {
            float value;
            if (float.TryParse(farPlaneTranslucencyTextBox.Text, out value))
            {
                farPlaneTranslucencyTrackBar.Value = (int)value;
            }
            else
            {
                MessageBox.Show("The value entered for Far Plane Translucency is not a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void frustumTranslucencyTextBox_Validated(object sender, EventArgs e)
        {
            float value;
            if (float.TryParse(frustumTranslucencyTextBox.Text, out value))
            {
                frustumTranslucencyTrackBar.Value = (int)value;
            }
            else
            {
                MessageBox.Show("The value entered for Frustum Translucency is not a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        #endregion

        #region CheckChanged Events
        private void tintCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tintCheckBox.Checked)
                tintColor.Enabled = true;
            else
                tintColor.Enabled = false;
        }

        private void transparentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (transparentCheckBox.Checked)
                transparentColor.Enabled = true;
            else
                transparentColor.Enabled = false;
        }

        private void manualFrameRateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (manualFrameRateCheckBox.Checked == true)
                frameRateBox.Enabled = true;
            else
                frameRateBox.Enabled = false;
        }

        private void realTimeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (realTimeRadioButton.Checked == true)
            {
                timeIntervalRadioButton.Checked = false;

                realTimePanel.Visible = true;
                manualFrameRateCheckBox.Enabled = true;
                frameRateBox.Enabled = manualFrameRateCheckBox.Checked;
                loopCheckBox.Enabled = true;
            }
            else
            {
                realTimePanel.Visible = false;
                manualFrameRateCheckBox.Enabled = false;
                frameRateBox.Enabled = false;
                loopCheckBox.Enabled = false;
            }
        }

        private void timeIntervalRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (timeIntervalRadioButton.Checked == true)
            {
                realTimeRadioButton.Checked = false;

                timeIntervalPanel.Visible = true;
                timeIntervalStartTime.Enabled = true;
                timeIntervalEndTime.Enabled = true;
            }
            else
            {
                timeIntervalPanel.Visible = false;
                timeIntervalStartTime.Enabled = false;
                timeIntervalEndTime.Enabled = false;
            }
        }

        private void shadowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (shadowCheckBox.Checked)
            {
                shadowColor.Enabled = true;
                shadowTranslucencyTextBox.Enabled = true;
                shadowTranslucencyTrackBar.Enabled = true;
            }
            else
            {
                shadowColor.Enabled = false;
                shadowTranslucencyTextBox.Enabled = false;
                shadowTranslucencyTrackBar.Enabled = false;
            }
        }

        private void farplaneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (farPlaneCheckBox.Checked)
            {
                farPlaneColor.Enabled = true;
                farPlaneTranslucencyTextBox.Enabled = true;
                farPlaneTranslucencyTrackBar.Enabled = true;
            }
            else
            {
                farPlaneColor.Enabled = false;
                farPlaneTranslucencyTextBox.Enabled = false;
                farPlaneTranslucencyTrackBar.Enabled = false;
            }
        }

        private void frustumCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (frustumCheckBox.Checked)
            {
                frustumColor.Enabled = true;
                frustumTranslucencyTextBox.Enabled = true;
                frustumTranslucencyTrackBar.Enabled = true;
            }
            else
            {
                frustumColor.Enabled = false;
                frustumTranslucencyTextBox.Enabled = false;
                frustumTranslucencyTrackBar.Enabled = false;
            }
        }
        #endregion

        #region Bottom Button (OK/Cancel/Apply) Events
        private void applyButton_Click(object sender, EventArgs e)
        {
            if (ValidateProjectionProperties() == true)
                ApplyChanges();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (ValidateProjectionProperties() == true)
            {
                ApplyChanges();
                m_pEmbeddedControlSite.Window.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            m_pEmbeddedControlSite.Window.Close();
        }
        #endregion

        private void borderWidth_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Draw the background of the item.
            e.DrawBackground();

            // Create a filled square sized to represents the width
            Rectangle rectangle = new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + e.Bounds.Height / 2 - (e.Index / 2),
                    e.Bounds.Width - 8, e.Index);
            e.Graphics.FillRectangle(new SolidBrush(System.Drawing.Color.Black), rectangle);

            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void PopulateProjectionAttributes()
        {
            fileTextBox.Text = sensorAttributes.Uri;
            videoPlaybackSettingsPanel.Visible = sensorAttributes.UriIsCompatibleVideo;

            tintCheckBox.Checked = sensorAttributes.UseTintColor;
            tintColor.SelectedColor = sensorAttributes.TintColor;
            transparentCheckBox.Checked = sensorAttributes.UseTransparentColor;
            transparentColor.SelectedColor = sensorAttributes.TransparentColor;

            videoStartTimeBox.Text = sensorAttributes.VideoStartTime;
            videoEndtimeBox.Text = sensorAttributes.VideoEndTime;

            realTimeRadioButton.Checked = sensorAttributes.UseRealTime;
            manualFrameRateCheckBox.Checked = sensorAttributes.UseManualFrameRate;
            frameRateBox.Text = NumberTo6DecimalString(sensorAttributes.FrameRate);
            loopCheckBox.Checked = sensorAttributes.Loop;

            timeIntervalRadioButton.Checked = sensorAttributes.UseTimeInterval;
            if (sensorAttributes.TimeIntervalStart == String.Empty && sensorAttributes.TimeIntervalEnd == String.Empty)
            {
                // Set them to the currect scenario's start and end time
                timeIntervalStartTime.Text = ((AGI.STKObjects.IAgScenario)m_root.CurrentScenario).StartTime.ToString();
                timeIntervalEndTime.Text = ((AGI.STKObjects.IAgScenario)m_root.CurrentScenario).StopTime.ToString();
            }
            else
            {
                timeIntervalStartTime.Text = sensorAttributes.TimeIntervalStart;
                timeIntervalEndTime.Text = sensorAttributes.TimeIntervalEnd;
            }

            borderColor.SelectedColor = sensorAttributes.BorderColor;
            borderWidth.Text = sensorAttributes.BorderWidth.ToString();
            borderTranslucencyTextBox.Text = NumberTo6DecimalString(sensorAttributes.BorderTranslucency);
            borderTranslucencyTrackBar.Value = (int)sensorAttributes.BorderTranslucency;

            shadowCheckBox.Checked = sensorAttributes.ShowShadow;
            shadowColor.SelectedColor = sensorAttributes.ShadowColor;
            shadowTranslucencyTextBox.Text = NumberTo6DecimalString(sensorAttributes.ShadowTranslucency);
            shadowTranslucencyTrackBar.Value = (int)sensorAttributes.ShadowTranslucency;

            farPlaneCheckBox.Checked = sensorAttributes.ShowFarPlane;
            farPlaneColor.SelectedColor = sensorAttributes.FarPlaneColor;
            farPlaneTranslucencyTextBox.Text = NumberTo6DecimalString(sensorAttributes.FarPlaneTranslucency);
            farPlaneTranslucencyTrackBar.Value = (int)sensorAttributes.FarPlaneTranslucency;

            frustumCheckBox.Checked = sensorAttributes.ShowFrustum;
            frustumColor.SelectedColor = sensorAttributes.FrustumColor;
            frustumTranslucencyTextBox.Text = NumberTo6DecimalString(sensorAttributes.FrustumTranslucency);
            frustumTranslucencyTrackBar.Value = (int)sensorAttributes.FrustumTranslucency;
        }

        private void ApplyChanges()
        {
            sensorAttributes.IsConfigured = true;

            sensorAttributes.Uri = fileTextBox.Text;

            sensorAttributes.UseTintColor = tintCheckBox.Checked;
            sensorAttributes.TintColor = tintColor.SelectedColor;
            sensorAttributes.UseTransparentColor = transparentCheckBox.Checked;
            sensorAttributes.TransparentColor = transparentColor.SelectedColor;

            sensorAttributes.VideoStartTime = videoStartTimeBox.Text;
            sensorAttributes.VideoEndTime = videoEndtimeBox.Text;

            sensorAttributes.UseRealTime = realTimeRadioButton.Checked;
            sensorAttributes.UseManualFrameRate = manualFrameRateCheckBox.Checked;
            sensorAttributes.FrameRate = double.Parse(frameRateBox.Text);
            sensorAttributes.Loop = loopCheckBox.Checked;

            sensorAttributes.UseTimeInterval = timeIntervalRadioButton.Checked;
            sensorAttributes.TimeIntervalStart = timeIntervalStartTime.Text;
            sensorAttributes.TimeIntervalEnd = timeIntervalEndTime.Text;

            sensorAttributes.BorderColor = borderColor.SelectedColor;
            sensorAttributes.BorderWidth = Int32.Parse(borderWidth.Text);
            sensorAttributes.BorderTranslucency = float.Parse(borderTranslucencyTextBox.Text);

            sensorAttributes.ShowShadow = shadowCheckBox.Checked;
            sensorAttributes.ShadowColor = shadowColor.SelectedColor;
            sensorAttributes.ShadowTranslucency = float.Parse(shadowTranslucencyTextBox.Text);

            sensorAttributes.ShowFarPlane = farPlaneCheckBox.Checked;
            sensorAttributes.FarPlaneColor = farPlaneColor.SelectedColor;
            sensorAttributes.FarPlaneTranslucency = float.Parse(farPlaneTranslucencyTextBox.Text);

            sensorAttributes.ShowFrustum = frustumCheckBox.Checked;
            sensorAttributes.FrustumColor = frustumColor.SelectedColor;
            sensorAttributes.FrustumTranslucency = float.Parse(frustumTranslucencyTextBox.Text);

            if (sensorAttributes.IsProjectionAdded == true)
            {
                // Let the sensor control refresh the projection
                m_uiPlugin.RaiseSensorAttributeChanged(sensorAttributes);
            }
        }

        private bool ValidateProjectionProperties()
        {
            if (fileTextBox.Text == String.Empty)
            {
                MessageBox.Show("Please select a file for the projection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            // Validate the video start and end time if we need to
            if (fileTextBox.Text != String.Empty && sensorAttributes.UriIsCompatibleVideo == true)
            {
                TimeSpan temp;
                if (TimeSpan.TryParse(videoStartTimeBox.Text, out temp) == false)
                {
                    MessageBox.Show("The video start time entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                if (TimeSpan.TryParse(videoEndtimeBox.Text, out temp) == false)
                {
                    MessageBox.Show("The video end time entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

            }

            // Validate the Interval Start and Interval End times if we need to
            if (timeIntervalRadioButton.Checked == true)
            {
                DateTime temp;
                if (DateTime.TryParse(timeIntervalStartTime.Text, out temp) == false)
                {
                    MessageBox.Show("The interval start time entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }

                if (DateTime.TryParse(timeIntervalEndTime.Text, out temp) == false)
                {
                    MessageBox.Show("The interval end time entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Converts a number to a string with 6 decimal places.
        /// </summary>
        private static string NumberTo6DecimalString(double num)
        {
            string returnString = num.ToString();

            if (returnString.Contains('.') == false)
            {
                returnString += ".";
            }

            int decimalDigitCount = returnString.Substring(returnString.IndexOf('.') + 1).Length;

            for (int i = 0; i + decimalDigitCount < 6; i++)
            {
                returnString += "0";
            }

            return returnString;
        }
        private static string NumberTo6DecimalString(float num)
        {
            return NumberTo6DecimalString((double)num);
        }
        private static string NumberTo6DecimalString(int num)
        {
            return NumberTo6DecimalString((double)num);
        }
    }
}
