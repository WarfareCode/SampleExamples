using System;
using System.Collections;
using System.Windows.Forms;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using RectangularSensorStreamPluginProxy;

namespace RectangularSensorPlugin
{
    public partial class RectangularSensorControl : UserControl,
                                                    AGI.Ui.Plugins.IAgUiPluginEmbeddedControl
    {
        private AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite m_pEmbeddedControlSite;
        private AGI.STKObjects.AgStkObjectRoot m_root;
        private RectangularSensorPlugin m_uiPlugin;

        private Hashtable sensorHashtable;

        public RectangularSensorControl()
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
            m_root.OnStkObjectAdded -= new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            m_root.OnStkObjectDeleted -= new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);
            m_root.OnStkObjectChanged -= new IAgStkObjectRootEvents_OnStkObjectChangedEventHandler(m_root_OnStkObjectChanged);
            m_root.OnStkObjectRenamed -= new IAgStkObjectRootEvents_OnStkObjectRenamedEventHandler(m_root_OnStkObjectRenamed);
            m_root.OnScenarioBeforeClose -= new IAgStkObjectRootEvents_OnScenarioBeforeCloseEventHandler(m_root_OnScenarioBeforeClose);

            m_uiPlugin.OnSensorAttributesChanged -= new RectangularSensorPlugin.OnSensorAttributesChangedEventHandler(m_uiPlugin_OnSensorAttributesChanged);
        }

        public void OnSaveModified()
        {
            // No OnSaveModified action is required for this plugin.
        }

        public void SetSite(AGI.Ui.Plugins.IAgUiPluginEmbeddedControlSite Site)
        {
            m_pEmbeddedControlSite = Site;
            m_uiPlugin = m_pEmbeddedControlSite.Plugin as RectangularSensorPlugin;
            m_root = m_uiPlugin.STKRoot;
            sensorHashtable = m_uiPlugin.SensorHashtable;

            // Add all of the rectangular sensors to this list box
            if (m_root.CurrentScenario.Children.Count > 0)
            {
                rectangularSensorList.Items.Clear();
                GetRectangularSensors((IAgStkObjectCollection)m_root.CurrentScenario.Children);
            }

            // Event handlers
            m_root.OnStkObjectAdded += new IAgStkObjectRootEvents_OnStkObjectAddedEventHandler(m_root_OnStkObjectAdded);
            m_root.OnStkObjectDeleted += new IAgStkObjectRootEvents_OnStkObjectDeletedEventHandler(m_root_OnStkObjectDeleted);
            m_root.OnStkObjectChanged += new IAgStkObjectRootEvents_OnStkObjectChangedEventHandler(m_root_OnStkObjectChanged);
            m_root.OnStkObjectRenamed += new IAgStkObjectRootEvents_OnStkObjectRenamedEventHandler(m_root_OnStkObjectRenamed);
            m_root.OnScenarioBeforeClose += new IAgStkObjectRootEvents_OnScenarioBeforeCloseEventHandler(m_root_OnScenarioBeforeClose);

            m_uiPlugin.OnSensorAttributesChanged += new RectangularSensorPlugin.OnSensorAttributesChangedEventHandler(m_uiPlugin_OnSensorAttributesChanged);

        }

        void m_root_OnScenarioBeforeClose()
        {
            // If the scenario is going to be closed, clear out the hash table.
            // This prevents OnStkObjectDeleted from triggering us to remove sensors we have in storage.
            sensorHashtable.Clear();
        }

        void m_uiPlugin_OnSensorAttributesChanged(object sender, EventArgs e)
        {
            SensorAttributes changedAttributes = (SensorAttributes)sender;
            SensorAttributes selectedAttributes = sensorHashtable[activeProjectionList.SelectedItem.ToString()] as SensorAttributes;

            if (Object.ReferenceEquals(changedAttributes, selectedAttributes))
                RefreshSensorProjection(changedAttributes.Path);
        }
        #endregion

        #region m_root Event Handlers
        void m_root_OnStkObjectChanged(IAgStkObjectChangedEventArgs pArgs)
        {
            IAgStkObject obj = m_root.GetObjectFromPath(pArgs.Path);
            string objPath = GetObjectsShortPath(obj);

            if (obj.ClassType == AgESTKObjectType.eSensor)
            {
                if (((IAgSensor)obj).PatternType == AgESnPattern.eSnRectangular)
                {
                    // Either add it or, if it has an active projection, refresh it.
                    if (sensorHashtable.ContainsKey(objPath) == false)
                        AddRectangularSensor(objPath);
                    else if (((SensorAttributes)sensorHashtable[objPath]).IsProjectionAdded == true)
                        RefreshSensorProjection(objPath);
                    else
                        UpdateSensorPattern(objPath);
                }
                else
                {
                    // See if we need to remove it because it was a rectangular sensor, 
                    // but no longer is a rectangular sensor
                    if (sensorHashtable.ContainsKey(objPath) == true)
                        RemoveRectangularSensor(objPath);
                }
            }
        }

        void m_root_OnStkObjectRenamed(object Sender, string OldPath, string NewPath)
        {
            //        Cases:  A rectangular sensor without a projection
            //                A rectangular sensor with an active projection
            //                A parent of a rectangular sensor without a projection
            //                A parent of the rectangular sensor with an active projection

            // Form the short path of the old and "new" object so we can check if it was in our list
            string[] oldPathParts = OldPath.Split('/');
            int oldPartCount = oldPathParts.Length;
            string oldObjectShortPath = oldPathParts[oldPartCount - 4] + "/" + oldPathParts[oldPartCount - 3] + "/" + oldPathParts[oldPartCount - 2] + "/" + oldPathParts[oldPartCount - 1];

            IAgStkObject newObject = m_root.GetObjectFromPath(NewPath);
            string newObjectShortPath = GetObjectsShortPath(newObject);

            // Check if the renamed object is in our list
            if (rectangularSensorList.Items.Contains(oldObjectShortPath) == true)
            {
                    MoveSensorAttributes(oldObjectShortPath, newObjectShortPath);

                    // Add the new sensor name to the rectangularSensorList
                    rectangularSensorList.Items.Add(newObjectShortPath);
                    rectangularSensorList.SelectedItem = newObjectShortPath;
            }
            else if (activeProjectionList.Items.Contains(oldObjectShortPath) == true)
            {
                    MoveSensorAttributes(oldObjectShortPath, newObjectShortPath);

                    // Add the projection back
                    ProjectionManager.AddProjection(m_root, (SensorAttributes)sensorHashtable[newObjectShortPath]);

                    // Add the new sensor name to the activeProjectionList
                    activeProjectionList.Items.Add(newObjectShortPath);
                    activeProjectionList.SelectedItem = newObjectShortPath;
            }

            // If the renamed object has children, we need to check them too.
            if (newObject.HasChildren == true)
            {
                // Split the NewPath too so we can access just the new name
                string[] newPathParts = NewPath.Split('/');
                int newPartCount = newPathParts.Length;

                // Get locators for the object that was changed.  Will look like "Aircraft/Jet", where "Jet" is the new or old name.
                string newObjectLocator = newPathParts[newPartCount - 2] + "/" + newPathParts[newPartCount - 1];
                string oldObjectLocator = oldPathParts[oldPartCount - 2] + "/" + oldPathParts[oldPartCount - 1];

                foreach (IAgStkObject child in newObject.Children)
                {
                    string oldChildPath =  oldObjectLocator + "/" + child.ClassName + "/" + child.InstanceName;
                    string newChildPath = newObjectLocator + "/" + child.ClassName + "/" + child.InstanceName;

                    // Check this child of the renamed object is in our list
                    if (rectangularSensorList.Items.Contains(oldChildPath) == true)
                    {
                        MoveSensorAttributes(oldChildPath, newChildPath);

                        // Add the new sensor name to the rectangularSensorList
                        rectangularSensorList.Items.Add(newChildPath);
                        rectangularSensorList.SelectedItem = newChildPath;
                    }
                    else if (activeProjectionList.Items.Contains(oldChildPath) == true)
                    {
                        MoveSensorAttributes(oldChildPath, newChildPath);

                        // Add the projection back
                        ProjectionManager.AddProjection(m_root, (SensorAttributes)sensorHashtable[newChildPath]);

                        // Add the new sensor name to the activeProjectionList
                        activeProjectionList.Items.Add(newChildPath);
                        activeProjectionList.SelectedItem = newChildPath;
                    }
                }
            }
        }

        void m_root_OnStkObjectDeleted(object Sender)
        {
            // Form the short path of the object so we can check if it was a rectangular sensor
            string[] pathParts = Sender.ToString().Split('/');
            int partCount = pathParts.Length;
            string deletedObjectPath = pathParts[partCount - 4] + "/" + pathParts[partCount - 3] + "/" + pathParts[partCount - 2] + "/" + pathParts[partCount - 1];

            // If it was a rectangular sensor, remove it (and its projection, if it has one) from our records
            if (sensorHashtable.ContainsKey(deletedObjectPath) == true)
                RemoveRectangularSensor(deletedObjectPath);
        }

        void m_root_OnStkObjectAdded(object Sender)
        {
            IAgStkObject addedObject = m_root.GetObjectFromPath(Sender.ToString());

            if (addedObject.ClassType == AgESTKObjectType.eSensor && ((IAgSensor)addedObject).PatternType == AgESnPattern.eSnRectangular)
                AddRectangularSensor(GetObjectsShortPath(addedObject));
        }
        #endregion

        #region Button Controls
        private void addProjectionButton_Click(object sender, EventArgs e)
        {
            // Ensure exactly one sensor is selected
            if (rectangularSensorList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a sensor to apply its projection.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (rectangularSensorList.SelectedItems.Count > 1)
            {
                MessageBox.Show("Only one sensor's projections can be applied at a time.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // Get that sensor's attributes
            SensorAttributes sensorAttributes = sensorHashtable[rectangularSensorList.SelectedItem.ToString()] as SensorAttributes;

            // Ensure the sensor's projection properties have been configured.
            if (sensorAttributes.IsConfigured == false)
            {
                MessageBox.Show("Please configure the sensor's projection properties before adding the projection.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // Add the sensor's projection since we made it through the checks
            AddSensorProjection(sensorAttributes.Path);
            rectangularSensorList.Items.Remove(sensorAttributes.Path);
        }

        private void removeProjectionButton_Click(object sender, EventArgs e)
        {
            // Ensure exactly one sensor is selected
            if (activeProjectionList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an active projection to remove.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (activeProjectionList.SelectedItems.Count > 1)
            {
                MessageBox.Show("Only one projection can be removed at a time.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            // Remove the projection
            RemoveSensorProjection(activeProjectionList.SelectedItem.ToString());
        }

        private void setProjectionPropertiesButton_Click(object sender, EventArgs e)
        {
            if (rectangularSensorList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an available sensor to set its properties.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (rectangularSensorList.SelectedItems.Count > 1)
            {
                MessageBox.Show("Only one sensor's properties can be set at a time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            m_uiPlugin.OpenProjectionProperties(rectangularSensorList.SelectedItem.ToString());
        }

        private void editProjectionPropertiesButton_Click(object sender, EventArgs e)
        {
            if (activeProjectionList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a sensor with an active projection to edit its properties.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (activeProjectionList.SelectedItems.Count > 1)
            {
                MessageBox.Show("Only one sensor's properties can be edited at a time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            m_uiPlugin.OpenProjectionProperties(activeProjectionList.SelectedItem.ToString());
        }
        #endregion
        
        private void GetRectangularSensors(IAgStkObjectCollection collection)
        {
            foreach (IAgStkObject obj in collection)
            {
                // If the object has children, call GetRectangularSensors() on the children.
                if (obj.Children.Count > 0)
                {
                    GetRectangularSensors(obj.Children);
                }

                if (obj.ClassType == AgESTKObjectType.eSensor
                    && ((IAgSensor)obj).PatternType == AgESnPattern.eSnRectangular)
                {
                    // It's a rectangular sensor, so generate its path and try to add it to the list
                    AddRectangularSensor(GetObjectsShortPath(obj));
                }
            }
        }

        private void AddRectangularSensor(string sensorPath)
        {
            // If it's not in the hashtable, add it there first.
            if (sensorHashtable.ContainsKey(sensorPath) == false)
                sensorHashtable.Add(sensorPath, new SensorAttributes(sensorPath));

            // Retrieve the sensor's Sensor Attributes from the hash table
            SensorAttributes sensorAttributes = sensorHashtable[sensorPath] as SensorAttributes;

            // If the sensor has an active projection and is not in the active
            // projection list, add it there
            if (sensorAttributes.IsProjectionAdded == true && activeProjectionList.Items.Contains(sensorPath) == false)
                activeProjectionList.Items.Add(sensorPath);
            else
                rectangularSensorList.Items.Add(sensorPath);

            IAgStkObject obj = m_root.GetObjectFromPath(sensorPath);
            // If not already created, create the axes needed to transform the sensor orientation so it can be used for projection
            if (obj.Vgt.Axes.Contains(SensorAttributes.BodyFlippedAxesName) == false)
            {
                // Projections require an axes that is x horizontal, y vertical, and -z into the projection
                // The default sensor body axes is y horizontal, x vertical, and z into the projection
                // We create a fixed axes based on the sensor's body axes that is rotated 180 degrees about z, then 90 degrees about x
                // to achieve the desired transformation, which will then be used in the sensor stream plug-in
                IAgCrdnAxesFixed fixedAxes = (IAgCrdnAxesFixed)obj.Vgt.Axes.Factory.Create(SensorAttributes.BodyFlippedAxesName, string.Empty, AgECrdnAxesType.eCrdnAxesTypeFixed);
                fixedAxes.ReferenceAxes.SetAxes(obj.Vgt.Axes["Body"]);
                string prevUnit = obj.Root.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit");
                obj.Root.UnitPreferences.SetCurrentUnit("AngleUnit", "deg");
                fixedAxes.FixedOrientation.AssignEulerAngles(AgEEulerOrientationSequence.e321, 90.0, 0.0, 180.0);
                obj.Root.UnitPreferences.SetCurrentUnit("AngleUnit", prevUnit);
            }
        }

        private void RemoveRectangularSensor(string sensorPath)
        {
            SensorAttributes sensorAttributes = sensorHashtable[sensorPath] as SensorAttributes;
            IAgSensor sensor = m_root.GetObjectFromPath(sensorPath) as IAgSensor;

            if (sensorAttributes.IsProjectionAdded == true)
                {
                    // Remove the projection
                    AGI.STKGraphics.IAgStkGraphicsImageCollection globeImagery = ((IAgScenario)m_root.CurrentScenario).SceneManager.Scenes[0].CentralBodies.Earth.Imagery;
                    
                    globeImagery.Remove(sensorAttributes.ProjectionOverlay);
                    ((IAgScenario)m_root.CurrentScenario).SceneManager.Render();

                    // Restore sensor's settings prior to adding the projection
                    sensor.Graphics.InheritFromScenario = sensorAttributes.GraphicsInheritFromScenario;
                    if (sensorAttributes.GraphicsInheritFromScenario == false)
                    {
                        // We can only modify Graphics.Enable if it is not inheriting
                        sensor.Graphics.Enable = sensorAttributes.GraphicsEnable;
                    }
                    sensor.VO.PercentTranslucency = sensorAttributes.VOPercentTranslucency;

                    activeProjectionList.Items.Remove(sensorAttributes.Path);
                    sensorAttributes.IsProjectionAdded = false;
                }
                else
                {
                    // It didn't have a projection, so remove it from the list of rectangular sensors instead.
                    rectangularSensorList.Items.Remove(sensorPath);
                }

                // Remove the sensor from the hashtable
                sensorHashtable.Remove(sensorPath);
        }

        private void UpdateSensorPattern(string sensorPath)
        {
            SensorAttributes sensorAttributes = sensorHashtable[sensorPath] as SensorAttributes;
            IAgSensor sensor = m_root.GetObjectFromPath(sensorPath) as IAgSensor;

            // Record the sensor's pattern
            IAgSnRectangularPattern sensorPattern = sensor.Pattern as IAgSnRectangularPattern;
            sensorAttributes.HorizontalHalfAngle = Trig.DegreesToRadians(((double)sensorPattern.HorizontalHalfAngle));
            sensorAttributes.VerticalHalfAngle = Trig.DegreesToRadians(((double)sensorPattern.VerticalHalfAngle));
        }

        private void AddSensorProjection(string sensorPath)
        {
            SensorAttributes sensorAttributes = sensorHashtable[sensorPath] as SensorAttributes;
            IAgSensor sensor = m_root.GetObjectFromPath(sensorPath) as IAgSensor;

            // Ensure we have a current record of the sensor's pattern.
            UpdateSensorPattern(sensorAttributes.Path);

            // Backup sensor's current 2D and 3D graphics settings
            sensorAttributes.GraphicsInheritFromScenario = sensor.Graphics.InheritFromScenario;
            sensorAttributes.GraphicsEnable = sensor.Graphics.Enable;
            sensorAttributes.VOPercentTranslucency = sensor.VO.PercentTranslucency;

            // Modify the sensor's 2D and 3D graphics settings so that they don't interfere with the projection
            sensor.VO.PercentTranslucency = 100.0;
            sensor.Graphics.InheritFromScenario = false;
            sensor.Graphics.Enable = false;

            try
            {
                ProjectionManager.AddProjection(m_root, sensorAttributes);
                activeProjectionList.Items.Add(sensorAttributes.Path);
                activeProjectionList.SelectedItem = sensorAttributes.Path;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void RemoveSensorProjection(string sensorPath)
        {
            SensorAttributes sensorAttributes = sensorHashtable[sensorPath] as SensorAttributes;
            IAgSensor sensor = m_root.GetObjectFromPath(sensorAttributes.Path) as IAgSensor;
            AGI.STKGraphics.IAgStkGraphicsImageCollection globeImagery = ((IAgScenario)m_root.CurrentScenario).SceneManager.Scenes[0].CentralBodies.Earth.Imagery;

            // Before we try to remove the projection, double check that it is added.
            if (sensorAttributes.IsProjectionAdded == true)
            {
                try
                {
                    globeImagery.Remove(sensorAttributes.ProjectionOverlay);
                    activeProjectionList.Items.Remove(sensorAttributes.Path);
                    sensorAttributes.IsProjectionAdded = false;
                    
                    // Restore sensor's settings prior to adding the projection
                    sensor.Graphics.InheritFromScenario = sensorAttributes.GraphicsInheritFromScenario;
                    if (sensorAttributes.GraphicsInheritFromScenario == false)
                    {
                        // We can only modify Graphics.Enable if it is not inheriting
                        sensor.Graphics.Enable = sensorAttributes.GraphicsEnable;
                    }
                    sensor.VO.PercentTranslucency = sensorAttributes.VOPercentTranslucency;

                    // Add the sensor back to the list of avaliable sensors
                    rectangularSensorList.Items.Add(sensorAttributes.Path);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void RefreshSensorProjection(string sensorPath)
        {
            SensorAttributes sensorAttributes = sensorHashtable[sensorPath] as SensorAttributes;
            IAgSensor sensor = m_root.GetObjectFromPath(sensorPath) as IAgSensor;
            AGI.STKGraphics.IAgStkGraphicsImageCollection globeImagery = ((IAgScenario)m_root.CurrentScenario).SceneManager.Scenes[0].CentralBodies.Earth.Imagery;

            // Before we try to remove the projection, double check that it is added.
            if (sensorAttributes.IsProjectionAdded == true)
            {
                // Remove the projection
                globeImagery.Remove(sensorAttributes.ProjectionOverlay);
                activeProjectionList.Items.Remove(sensorAttributes.Path);
                sensorAttributes.IsProjectionAdded = false;

                // Update our record of the sensor's properties before we add the projection back.
                UpdateSensorPattern(sensorAttributes.Path);

                // Add the projection back
                ProjectionManager.AddProjection(m_root, sensorAttributes);
                activeProjectionList.Items.Add(sensorAttributes.Path);
                activeProjectionList.SelectedItem = sensorAttributes.Path;
            }
        }

        private void MoveSensorAttributes(string oldSensorPath, string newSensorPath)
        {
            if (sensorHashtable.ContainsKey(oldSensorPath) == true)
            {
                SensorAttributes oldSensorAttributes = sensorHashtable[oldSensorPath] as SensorAttributes;

                // Copy the old path's settings from the hashtable and file them under the newSensorPath.
                sensorHashtable.Add(newSensorPath, new SensorAttributes(newSensorPath));

                sensorHashtable[newSensorPath] = oldSensorAttributes.Copy(newSensorPath);

                // Remove the old sensor name from the list and its projection (if it has one)
                RemoveRectangularSensor(oldSensorPath);
            }
            else
            {
                throw new Exception("The sensor with the path\n" + oldSensorPath + 
                    "\ndoes not have attributes saved in the hash table.");
            }
        }

        private static string GetObjectsShortPath(IAgStkObject obj)
        {
            return obj.Parent.ClassName + "/" + obj.Parent.InstanceName + "/"
                + obj.ClassName + "/" + obj.InstanceName;
        }

        public static class Trig
        {
            public static double DegreesToRadians(double degrees)
            {
                return degrees * (Math.PI / 180.0);
            }
        }
    }

}
