using System;
using System.Collections;
using System.Windows.Forms;
using AGI.Grid;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKVgt;

namespace Agi.Ui.Plugins.CSharp.VgtGridPlugin
{
    public partial class VgtGridManager :  UserControl,
                                           AGI.Ui.Plugins.IAgUiPluginEmbeddedControl
    {
        private AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AgStkObjectRoot m_root;
        private IAgStkGraphicsSceneManager sceneManager;
        private VgtGridPlugin m_uiPlugin;

        private Hashtable gridSettings = new Hashtable();

        public VgtGridManager()
        {
            InitializeComponent();
        }

        #region IAgUiPluginEmbeddedControl Members

        public stdole.IPictureDisp GetIcon()
        {
            // Not required.
            return null;
        }

        public void OnClosing()
        {
            // Remove the plugin's handling of STK events
            m_root.OnStkObjectAdded -= new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            m_root.OnStkObjectDeleted -= new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);
            m_root.OnStkObjectRenamed -= new IAgStkObjectRootEvents_OnStkObjectRenamedEventHandler(m_root_OnStkObjectRenamed);
        }

        public void OnSaveModified()
        {
            // Not required.
        }

        public void SetSite(AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as VgtGridPlugin;
            m_root = m_uiPlugin.STKRoot;
            sceneManager = ((IAgScenario)m_root.CurrentScenario).SceneManager;

            // Gather the list of scenario objects for use with the plugin
            PopulateObjects(m_root.CurrentScenario.Children);
            if (objectList.SelectedItems.Count == 1)
                PopulateGridSettings(objectList.SelectedItem.ToString());

            // Add handlers for relevant STK events
            m_root.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            m_root.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);
            m_root.OnStkObjectRenamed += new IAgStkObjectRootEvents_OnStkObjectRenamedEventHandler(m_root_OnStkObjectRenamed);
        }

        #endregion

        #region STK Object Event Handlers
        void m_root_OnStkObjectRenamed(object Sender, string OldPath, string NewPath)
        {
            // Form the short path of the new and old object so we can check if it was in our list
            string[] oldPathParts = OldPath.Split('/');
            int oldPartCount = oldPathParts.Length;
            string oldObjectPath;

            if (oldPathParts[oldPartCount - 4] == "Scenario")
                oldObjectPath = oldPathParts[oldPartCount - 2] + "/" + oldPathParts[oldPartCount - 1];
            else
                oldObjectPath = oldPathParts[oldPartCount - 4] + "/" + oldPathParts[oldPartCount - 3] + "/" + oldPathParts[oldPartCount - 2] + "/" + oldPathParts[oldPartCount - 1];

            IAgStkObject newObject = m_root.GetObjectFromPath(NewPath);

            if (objectList.Items.Contains(oldObjectPath))
            {
                objectList.Items.Remove(oldObjectPath);

                if (gridSettings.ContainsKey(oldObjectPath))
                {
                    GridSettings oldGridSettings = gridSettings[oldObjectPath] as GridSettings;

                    // Add a new entry for the object's new name and copy over its settings
                    gridSettings.Add(GetObjectsShortPath(newObject), new GridSettings());
                    gridSettings[GetObjectsShortPath(newObject)] = oldGridSettings.Copy();

                    // Remove the old grid settings from the hash table.
                    gridSettings.Remove(oldObjectPath);
                }

                AddObject(newObject);
            }

            // If the renamed object has children, we need to check them too.
            if (newObject.HasChildren)
            {
                // Split the NewPath too so we can access just the new name
                string[] newPathParts = NewPath.Split('/');
                int newPartCount = newPathParts.Length;

                // Get locators for the object that was changed.  Will look like "Aircraft/Jet", where "Jet" is the new or old name.
                string newObjectLocator = newPathParts[newPartCount - 2] + "/" + newPathParts[newPartCount - 1];
                string oldObjectLocator = oldPathParts[oldPartCount - 2] + "/" + oldPathParts[oldPartCount - 1];

                foreach (IAgStkObject child in newObject.Children)
                {
                    string oldChildPath = oldObjectLocator + "/" + child.ClassName + "/" + child.InstanceName;
                    string newChildPath = newObjectLocator + "/" + child.ClassName + "/" + child.InstanceName;

                    // Check if this child of the renamed object is in our list
                    if (objectList.Items.Contains(oldChildPath))
                    {
                        objectList.Items.Remove(oldChildPath);

                        if (gridSettings.ContainsKey(oldChildPath))
                        {
                            GridSettings oldGridSettings = gridSettings[oldChildPath] as GridSettings;

                            // Add a new entry for the object's new name and copy over its settings
                            gridSettings.Add(newChildPath, new GridSettings());
                            gridSettings[newChildPath] = oldGridSettings.Copy();

                            // Remove the old grid settings from the hash table.
                            gridSettings.Remove(oldChildPath);
                        }

                        // Add the child back with its new parent's name
                        objectList.Items.Add(newChildPath);
                        objectList.SelectedItem = newChildPath;
                    }
                }
            }
        }

        void m_root_OnStkObjectDeleted(object Sender)
        {
            // If the entie scenario is closing we don't need to update anything
            if (m_root.CurrentScenario != null)
            {
                // Form the short path of the object so we can check if it was in out list
                string[] pathParts = Sender.ToString().Split('/');
                int partCount = pathParts.Length;
                string deletedObjectPath;

                if (pathParts[partCount - 4] == "Scenario") // Child of the scenario
                    deletedObjectPath = pathParts[partCount - 2] + "/" + pathParts[partCount - 1];
                else // Child of another STK object
                    deletedObjectPath = pathParts[partCount - 4] + "/" + pathParts[partCount - 3] + "/" + pathParts[partCount - 2] + "/" + pathParts[partCount - 1];

                RemoveGrids(deletedObjectPath);

                if (objectList.Items.Contains(deletedObjectPath))
                    objectList.Items.Remove(deletedObjectPath);

                if (gridSettings.ContainsKey(deletedObjectPath))
                    gridSettings.Remove(deletedObjectPath);
            }
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            IAgStkObject obj = m_root.GetObjectFromPath(Sender.ToString());
            if (obj.IsVgtSupported())
                AddObject(m_root.GetObjectFromPath(Sender.ToString()));
        }
        #endregion

        /// <summary>
        /// Recursively traverses the current STK scenario's objects and adds any that support
        /// VGT to the plugin's list box so that a grid can be configured for it.
        /// </summary>
        private void PopulateObjects(IAgStkObjectCollection collection)
        {
            foreach (IAgStkObject obj in collection)
            {
                // If the object has children, call PopulateObjects() on the children.
                if (obj.Children.Count > 0)
                    PopulateObjects(obj.Children);

                if (obj.IsVgtSupported())
                    AddObject(obj);
            }
        }

        /// <summary>
        /// Adds an STK Object to the GUI (list box) and creates an entry in the hash table
        /// to store grid settings for the object.
        /// </summary>
        private void AddObject(IAgStkObject obj)
        {
            string objPath = GetObjectsShortPath(obj);

            // Add the object to the list
            if (!objectList.Items.Contains(objPath))
                objectList.Items.Add(objPath);

            // Give the object a place to store its grid settings
            if (!gridSettings.ContainsKey(objPath))
                gridSettings.Add(objPath, new GridSettings());

            objectList.SelectedItem = objPath;
        }

        /// <summary>
        /// Removes the GridPrimitive that corresponds to the provided object from the scene.
        /// </summary>
        private void RemoveGrids(string objectPath)
        {
            GridSettings objSettings = gridSettings[objectPath] as GridSettings;

            if (objSettings.GridPrimitive != null)
            {
                sceneManager.Primitives.Remove(objSettings.GridPrimitive);
                objSettings.GridPrimitive = null;
            }

            sceneManager.Render();
        }

        /// <summary>
        /// Creates a GridPrimitive for an object using its settings from the hash table.
        /// </summary>
        private IAgStkGraphicsPrimitive MakeGridFromSettings(string objectPath)
        {
            IAgStkObject selectedObj = m_root.GetObjectFromPath(objectPath);
            GridSettings objectSettings = gridSettings[objectPath] as GridSettings;
            GridPrimitive gridPrimitive = new GridPrimitive(selectedObj);

            if (customSystemRadioButton.Checked)
            {
                // Set the custom system
                gridPrimitive.Origin = selectedObj.Vgt.Points[originComboBox.SelectedItem];
                gridPrimitive.Axes = selectedObj.Vgt.Axes[axesComboBox.SelectedItem];
            }

            //
            // Configure the settings for each plane
            //

            // XY Plane
            gridPrimitive.XYPlane.Display = objectSettings.XYVisible;
            gridPrimitive.XYPlane.Color = objectSettings.XYColor;
            gridPrimitive.XYPlane.Size = objectSettings.XYSize;
            gridPrimitive.XYPlane.Spacing = objectSettings.XYSpacing;
            gridPrimitive.XYPlane.LineWidth = objectSettings.XYLineWidth;
            gridPrimitive.XYPlane.Translucency = objectSettings.XYTranslucency / 100.0f;

            // XZ Plane
            gridPrimitive.XZPlane.Display = objectSettings.XZVisible;
            gridPrimitive.XZPlane.Color = objectSettings.XZColor;
            gridPrimitive.XZPlane.Size = objectSettings.XZSize;
            gridPrimitive.XZPlane.Spacing = objectSettings.XZSpacing;
            gridPrimitive.XZPlane.LineWidth = objectSettings.XZLineWidth;
            gridPrimitive.XZPlane.Translucency = objectSettings.XZTranslucency / 100.0f;

            // YZ Plane
            gridPrimitive.YZPlane.Display = objectSettings.YZVisible;
            gridPrimitive.YZPlane.Color = objectSettings.YZColor;
            gridPrimitive.YZPlane.Size = objectSettings.YZSize;
            gridPrimitive.YZPlane.Spacing = objectSettings.YZSpacing;
            gridPrimitive.YZPlane.LineWidth = objectSettings.YZLineWidth;
            gridPrimitive.YZPlane.Translucency = objectSettings.YZTranslucency / 100.0f;

            // Create the grid primitive from these settings
            objectSettings.GridPrimitive = gridPrimitive.GetPrimitive(((IAgScenario)m_root.CurrentScenario).SceneManager);

            return objectSettings.GridPrimitive;
        }

        /// <summary>
        /// Saves the GUI grid settings for the provided object to its hash table entry.
        /// </summary>
        private bool SaveGridSettings(string objectPath)
        {
            if (ValidateGridSettings() == true)
            {
                GridSettings objectSettings = gridSettings[objectPath] as GridSettings;

                // XY Plane
                objectSettings.XYVisible = xyVisibleCheckBox.Checked;
                objectSettings.XYColor = xyColorBox.SelectedColor;
                objectSettings.XYSize = double.Parse(xySizeBox.Text);
                objectSettings.XYSpacing = double.Parse(xySpacingBox.Text);
                objectSettings.XYLineWidth = float.Parse(xyLineWidthBox.Text);
                objectSettings.XYTranslucency = float.Parse(xyTranslucencyBox.Text);

                // XZ Plane
                objectSettings.XZVisible = xzVisibleCheckBox.Checked;
                objectSettings.XZColor = xzColorBox.SelectedColor;
                objectSettings.XZSize = double.Parse(xzSizeBox.Text);
                objectSettings.XZSpacing = double.Parse(xzSpacingBox.Text);
                objectSettings.XZLineWidth = float.Parse(xzLineWidthBox.Text);
                objectSettings.XZTranslucency = float.Parse(xzTranslucencyBox.Text);

                // YZ Plane
                objectSettings.YZVisible = yzVisibleCheckBox.Checked;
                objectSettings.YZColor = yzColorBox.SelectedColor;
                objectSettings.YZSize = double.Parse(yzSizeBox.Text);
                objectSettings.YZSpacing = double.Parse(yzSpacingBox.Text);
                objectSettings.YZLineWidth = float.Parse(yzLineWidthBox.Text);
                objectSettings.YZTranslucency = float.Parse(yzTranslucencyBox.Text);

                // Reference Frame
                objectSettings.UseBodySystem = bodySystemRadioButton.Checked;
                objectSettings.UseCustomSystem = customSystemRadioButton.Checked;
                if (customSystemRadioButton.Checked)
                {
                    objectSettings.CustomOriginName = originComboBox.SelectedItem.ToString();
                    objectSettings.CustomAxesName = axesComboBox.SelectedItem.ToString();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Loads the grid settings for the provided object from the hash table and
        /// uses them to populate the fields in the GUI
        /// </summary>
        private void PopulateGridSettings(string objectPath)
        {
            IAgStkObject obj = m_root.GetObjectFromPath(objectPath);
            GridSettings objectSettings = gridSettings[objectPath] as GridSettings;

            // XY Plane
            xyVisibleCheckBox.Checked = objectSettings.XYVisible;
            xyColorBox.SelectedColor = objectSettings.XYColor;
            xyColorBox.Refresh();
            xySizeBox.Text = objectSettings.XYSize.ToString();
            xySpacingBox.Text = objectSettings.XYSpacing.ToString();
            xyLineWidthBox.Text = objectSettings.XYLineWidth.ToString();
            xyTranslucencyBox.Text = objectSettings.XYTranslucency.ToString();

            // XZ Pl;ane
            xzVisibleCheckBox.Checked = objectSettings.XZVisible;
            xzColorBox.SelectedColor = objectSettings.XZColor;
            xzColorBox.Refresh();
            xzSizeBox.Text = objectSettings.XZSize.ToString();
            xzSpacingBox.Text = objectSettings.XZSpacing.ToString();
            xzLineWidthBox.Text = objectSettings.XZLineWidth.ToString();
            xzTranslucencyBox.Text = objectSettings.XZTranslucency.ToString();

            // YZ Plane
            yzVisibleCheckBox.Checked = objectSettings.YZVisible;
            yzColorBox.SelectedColor = objectSettings.YZColor;
            yzColorBox.Refresh();
            yzSizeBox.Text = objectSettings.YZSize.ToString();
            yzSpacingBox.Text = objectSettings.YZSpacing.ToString();
            yzLineWidthBox.Text = objectSettings.YZLineWidth.ToString();
            yzTranslucencyBox.Text = objectSettings.YZTranslucency.ToString();

            // Reference Frame
            originComboBox.Items.Clear();
            originComboBox.SelectedItem = null;
            originComboBox.Text = string.Empty;
            foreach (IAgCrdn point in obj.Vgt.Points)
            {
                originComboBox.Items.Add(point.Name);
            }

            axesComboBox.Items.Clear();
            axesComboBox.SelectedItem = null;
            axesComboBox.Text = string.Empty;
            foreach (IAgCrdn axis in obj.Vgt.Axes)
            {
                axesComboBox.Items.Add(axis.Name);
            }

            if (objectSettings.UseCustomSystem)
            {
                customSystemRadioButton.Checked = true;
                originComboBox.SelectedItem = objectSettings.CustomOriginName;
                axesComboBox.SelectedItem = objectSettings.CustomAxesName;
            }
            else if (objectSettings.UseBodySystem)
            {
                bodySystemRadioButton.Checked = true;
            }
        }

        /// <summary>
        /// Determines whether the user's input for all of the grid settings is valid.
        /// If it is not, a message box is displayed to the user to explain the misconfiguration.
        /// This should be called before any grid settings are saved or applied to prevent errors.
        /// </summary>
        private bool ValidateGridSettings()
        {
            double tempDouble;
            float tempFloat;
            IAgStkGraphicsPolylinePrimitive tempPrimitive = sceneManager.Initializers.PolylinePrimitive.Initialize();

            #region Reference Frame
            if (customSystemRadioButton.Checked)
            {
                if (originComboBox.SelectedItem == null)
                {
                    MessageBox.Show("An origin is required for a custom reference frame.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
                if (axesComboBox.SelectedItem == null)
                {
                    MessageBox.Show("A set of axes is required for a custom reference frame.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return false;
                }
            }
            #endregion

            #region Size
            if (!double.TryParse(xySizeBox.Text, out tempDouble))
            {
                MessageBox.Show("The XY Size entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempDouble <= 0.0)
            {
                MessageBox.Show("The XY Size must be a positive value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!double.TryParse(xzSizeBox.Text, out tempDouble))
            {
                MessageBox.Show("The XZ Size entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempDouble <= 0.0)
            {
                MessageBox.Show("The XZ Size must be a positive value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!double.TryParse(yzSizeBox.Text, out tempDouble))
            {
                MessageBox.Show("The YZ Size entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempDouble <= 0.0)
            {
                MessageBox.Show("The YZ Size must be a positive value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            #endregion

            #region Spacing
            if (!double.TryParse(xySpacingBox.Text, out tempDouble))
            {
                MessageBox.Show("The XY Spacing entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempDouble <= 0.0 || tempDouble > double.Parse(xySizeBox.Text))
            {
                MessageBox.Show("The XY Spacing must be positive and less than or equal to the XY Size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!double.TryParse(xzSpacingBox.Text, out tempDouble))
            {
                MessageBox.Show("The XZ Spacing entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempDouble <= 0.0 || tempDouble > double.Parse(xzSizeBox.Text))
            {
                MessageBox.Show("The XZ Spacing must be positive and less than or equal to the XZ Size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!double.TryParse(yzSpacingBox.Text, out tempDouble))
            {
                MessageBox.Show("The YZ Spacing entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempDouble <= 0.0 || tempDouble > double.Parse(yzSizeBox.Text))
            {
                MessageBox.Show("The YZ Spacing must be positive and less than or equal to the YZ Size.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            #endregion

            #region Line Width
            if (!float.TryParse(xyLineWidthBox.Text, out tempFloat))
            {
                MessageBox.Show("The XY Line Width entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempFloat < tempPrimitive.MinimumWidthSupported || tempFloat > tempPrimitive.MaximumWidthSupported)
            {
                MessageBox.Show("The XY Line Width must be between " + tempPrimitive.MinimumWidthSupported.ToString() + " and " + tempPrimitive.MaximumWidthSupported + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!float.TryParse(xzLineWidthBox.Text, out tempFloat))
            {
                MessageBox.Show("The XZ Line Width entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempFloat < tempPrimitive.MinimumWidthSupported || tempFloat > tempPrimitive.MaximumWidthSupported)
            {
                MessageBox.Show("The XZ Line Width must be between " + tempPrimitive.MinimumWidthSupported.ToString() + " and " + tempPrimitive.MaximumWidthSupported + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!float.TryParse(yzLineWidthBox.Text, out tempFloat))
            {
                MessageBox.Show("The YZ Line Width entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempFloat < tempPrimitive.MinimumWidthSupported || tempFloat > tempPrimitive.MaximumWidthSupported)
            {
                MessageBox.Show("The YZ Line Width must be between " + tempPrimitive.MinimumWidthSupported.ToString() + " and " + tempPrimitive.MaximumWidthSupported + ".", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            #endregion

            #region Translucency
            if (!float.TryParse(xyTranslucencyBox.Text, out tempFloat))
            {
                MessageBox.Show("The XY Translucency entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempFloat < 0.0f || tempFloat > 100.0f)
            {
                MessageBox.Show("The XY Translucency must be between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!float.TryParse(xzTranslucencyBox.Text, out tempFloat))
            {
                MessageBox.Show("The XZ Translucency entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempFloat < 0.0f || tempFloat > 100.0f)
            {
                MessageBox.Show("The XZ Translucency must be between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            if (!float.TryParse(yzTranslucencyBox.Text, out tempFloat))
            {
                MessageBox.Show("The YZ Translucency entered is not valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (tempFloat < 0.0f || tempFloat > 100.0f)
            {
                MessageBox.Show("The YZ Translucency must be between 0 and 100.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            #endregion

            return true; // Since we made it through the checks.
        }

        /// <summary>
        /// Save the currently selected object's grid settings and add the GridPrimtive
        /// to the scene.
        /// </summary>
        private void applyGridButton_Click(object sender, EventArgs e)
        {
            if (SaveGridSettings(objectList.SelectedItem.ToString()))
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)m_root.CurrentScenario).SceneManager;

                RemoveGrids(objectList.SelectedItem.ToString());
                manager.Primitives.Add(MakeGridFromSettings(objectList.SelectedItem.ToString()));
                manager.Render();
            }
        }

        /// <summary>
        /// Try to remove the curretly selected object's GridPrimitive from the scene.
        /// </summary>
        private void removeGridButton_Click(object sender, EventArgs e)
        {
            RemoveGrids(objectList.SelectedItem.ToString());
        }

        /// <summary>
        /// Enable or disable the GUI grid settings controls depending on whether or
        /// not an object is selected in the list box.
        /// </summary>
        private void objectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectList.SelectedItems.Count == 0)
            {
                bodySystemRadioButton.Enabled = false;
                customSystemRadioButton.Enabled = false;
                originComboBox.Enabled = false;
                axesComboBox.Enabled = false;

                xyVisibleCheckBox.Enabled = false;
                xyColorBox.Enabled = false;
                xySizeBox.Enabled = false;
                xySpacingBox.Enabled = false;
                xyLineWidthBox.Enabled = false;
                xyTranslucencyBox.Enabled = false;

                xzVisibleCheckBox.Enabled = false;
                xzColorBox.Enabled = false;
                xzSizeBox.Enabled = false;
                xzSpacingBox.Enabled = false;
                xzLineWidthBox.Enabled = false;
                xzTranslucencyBox.Enabled = false;

                yzVisibleCheckBox.Enabled = false;
                yzColorBox.Enabled = false;
                yzSizeBox.Enabled = false;
                yzSpacingBox.Enabled = false;
                yzLineWidthBox.Enabled = false;
                yzTranslucencyBox.Enabled = false;

                applyGridButton.Enabled = false;
                removeGridButton.Enabled = false;
            }
            else
            {
                PopulateGridSettings(objectList.SelectedItem.ToString());

                bodySystemRadioButton.Enabled = true;
                customSystemRadioButton.Enabled = true;
                if (customSystemRadioButton.Checked)
                {
                    originComboBox.Enabled = true;
                    axesComboBox.Enabled = true;
                }

                xyVisibleCheckBox.Enabled = true;
                if (xyVisibleCheckBox.Checked)
                {
                    xyColorBox.Enabled = true;
                    xySizeBox.Enabled = true;
                    xySpacingBox.Enabled = true;
                    xyLineWidthBox.Enabled = true;
                    xyTranslucencyBox.Enabled = true;
                }

                xzVisibleCheckBox.Enabled = true;
                if (xzVisibleCheckBox.Checked)
                {
                    xzColorBox.Enabled = true;
                    xzSizeBox.Enabled = true;
                    xzSpacingBox.Enabled = true;
                    xzLineWidthBox.Enabled = true;
                    xzTranslucencyBox.Enabled = true;
                }

                yzVisibleCheckBox.Enabled = true;
                if (yzVisibleCheckBox.Checked)
                {
                    yzColorBox.Enabled = true;
                    yzSizeBox.Enabled = true;
                    yzSpacingBox.Enabled = true;
                    yzLineWidthBox.Enabled = true;
                    yzTranslucencyBox.Enabled = true;
                }

                applyGridButton.Enabled = true;
                removeGridButton.Enabled = true;
            }
        }

        /// <summary>
        /// Make sure the list of points is up-to-date
        /// </summary>
        private void originComboBox_DropDown(object sender, EventArgs e)
        {
            IAgStkObject selectedObject = m_root.GetObjectFromPath(objectList.SelectedItem.ToString());
            foreach (IAgCrdn point in selectedObject.Vgt.Points)
            {
                if (!originComboBox.Items.Contains(point.Name))
                    originComboBox.Items.Add(point.Name);

                if (!selectedObject.Vgt.Points.Contains(point.Name))
                    originComboBox.Items.Remove(point.Name);
            }
        }

        /// <summary>
        /// Make sure the list of axes is up-to-date
        /// </summary>
        private void axesComboBox_DropDown(object sender, EventArgs e)
        {
            IAgStkObject selectedObject = m_root.GetObjectFromPath(objectList.SelectedItem.ToString());
            foreach (IAgCrdn axes in selectedObject.Vgt.Axes)
            {
                if (!axesComboBox.Items.Contains(axes.Name))
                    axesComboBox.Items.Add(axes.Name);

                if (!selectedObject.Vgt.Axes.Contains(axes.Name))
                    axesComboBox.Items.Remove(axes.Name);
            }
        }

        /// <summary>
        /// Enable or disable parts of the grid settings GUI depending on whether or not
        /// they can be configured based on the user's chosen settings.
        /// </summary>
        #region CheckChanged Events
        private void xyVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (xyVisibleCheckBox.Checked)
            {
                xyColorBox.Enabled = true;
                xySizeBox.Enabled = true;
                xySpacingBox.Enabled = true;
                xyLineWidthBox.Enabled = true;
                xyTranslucencyBox.Enabled = true;
            }
            else
            {
                xyColorBox.Enabled = false;
                xySizeBox.Enabled = false;
                xySpacingBox.Enabled = false;
                xyLineWidthBox.Enabled = false;
                xyTranslucencyBox.Enabled = false;
            }
        }

        private void xzVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (xzVisibleCheckBox.Checked)
            {
                xzColorBox.Enabled = true;
                xzSizeBox.Enabled = true;
                xzSpacingBox.Enabled = true;
                xzLineWidthBox.Enabled = true;
                xzTranslucencyBox.Enabled = true;
            }
            else
            {
                xzColorBox.Enabled = false;
                xzSizeBox.Enabled = false;
                xzSpacingBox.Enabled = false;
                xzLineWidthBox.Enabled = false;
                xzTranslucencyBox.Enabled = false;
            }
        }

        private void yzVisibleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (yzVisibleCheckBox.Checked)
            {
                yzColorBox.Enabled = true;
                yzSizeBox.Enabled = true;
                yzSpacingBox.Enabled = true;
                yzLineWidthBox.Enabled = true;
                yzTranslucencyBox.Enabled = true;
            }
            else
            {
                yzColorBox.Enabled = false;
                yzSizeBox.Enabled = false;
                yzSpacingBox.Enabled = false;
                yzLineWidthBox.Enabled = false;
                yzTranslucencyBox.Enabled = false;
            }
        }

        private void bodySystemRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (bodySystemRadioButton.Checked)
            {
                customSystemRadioButton.Checked = false;
                originComboBox.Enabled = false;
                axesComboBox.Enabled = false;
            }
            else
            {
                customSystemRadioButton.Checked = true;
                originComboBox.Enabled = true;
                axesComboBox.Enabled = true;
            }
        }

        private void customSystemRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (customSystemRadioButton.Checked)
            {
                bodySystemRadioButton.Checked = false;
                originComboBox.Enabled = true;
                axesComboBox.Enabled = true;
            }
            else
            {
                bodySystemRadioButton.Checked = true;
                originComboBox.Enabled = false;
                axesComboBox.Enabled = false;
            }
        }
        #endregion

        /// <summary>
        /// Returns the path to an STK object consisting of its class name and instance name.
        /// It will be preceded by its parent's class and instance name if applicable.
        /// The "short path" is used to reference a specific STK object throughout the plugin.
        /// </summary>
        private static string GetObjectsShortPath(IAgStkObject obj)
        {
            if (obj.Parent.ClassType == AgESTKObjectType.eScenario)
                return obj.ClassName + "/" + obj.InstanceName;
            else
                return obj.Parent.ClassName + "/" + obj.Parent.InstanceName + "/"
                    + obj.ClassName + "/" + obj.InstanceName;
        }
    }
}
