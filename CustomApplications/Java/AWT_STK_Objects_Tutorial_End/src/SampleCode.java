//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkvgt.IAgCrdnEventIntervalSmartInterval;
import agi.stkobjects.*;

public class SampleCode
{
	private final static String		s_FAC_NAME	= "AGI_HQ_EXTON";
	private final static String		s_SAT_NAME	= "AGI_BIRD_1";

	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgScenarioClass			m_AgScenarioClass;

	private String					m_StkHomeDir;
	private String					m_StartTime;
	private String					m_StopTime;

	/*package*/ SampleCode(AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass = root;
	}

	/*package*/ void setUnits()
	throws AgCoreException
	{
		// ===================================================================================
		// Set measurement unit preferences
		// NOTE: It is best to always set the Unit preferences on the AgStkObjectRoot object.
		// ===================================================================================
		// If this throws a throwable it will be passed back through the initializeSTKEngine method
		// and back through the constructor to the main method to stop the application.
		// This is desired, because if we were to continue with the application,
		// then we would surely get more errors when creating the objects, with
		// specific numbers for specific units of measurement
		// ===================================================================================
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("LongitudeUnit", "deg");
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("LatitudeUnit", "deg");
		// TODO: Set the DateFormat unit to Local Gregorian Time
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DateFormat", "LCLG"); // default is UTCG
	}

	/*package*/ void getStkHomeDir()
	{
		try
		{
			IAgExecCmdResult homePathResult = this.m_AgStkObjectRootClass.executeCommand("GetDirectory / STKHome");
			this.m_StkHomeDir = homePathResult.getItem(0);
		}
		catch(AgCoreException sce)
		{
			System.out.println("===========================================");
			System.out.println("Description = " + sce.getDescription());
			System.out.println("HRESULT hr = 0x" + sce.getHResultAsHexString());
			sce.printStackTrace();
			System.out.println("===========================================");
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	/*package*/ void createScenario()
	throws Throwable
	{
		try
		{
			// ======================================================
			// Tell STK Engine that you are about to update object
			// settings and do not redraw graphics until notified
			// by an endUpdate() call at the end of this method.
			// =======================================================
			this.m_AgStkObjectRootClass.beginUpdate();

			// =======================================================
			// Its always best to just close a scenario in case
			// there is one already created
			// =======================================================
    		// TODO: close any current scenario
			this.m_AgStkObjectRootClass.closeScenario();
    		// TODO: open a new scenario called "Scenario"
			this.m_AgStkObjectRootClass.newScenario("Scenario");

			System.out.println("===========================");
			System.out.println(" Created Scenario/ObjModel ");
			System.out.println("===========================");

			// TODO: Get the current scenario object
			IAgScenario scenario = (IAgScenario)this.m_AgStkObjectRootClass.getCurrentScenario();
			// TODO: Get the available markers type as an object array from the Scenario objects VO interface (IAgScVO).
			Object[] markers = (Object[])scenario.getVO().availableMarkerTypes_AsObject();

			int colCount = markers.length;
			System.out.println("Available Marker Types are:");
			for(int colIndex = 0; colIndex < colCount; colIndex++)
			{
				System.out.println("\t["+colIndex+"] = "+markers[colIndex]);
			}

			// ================================================================
			// Get the current scenario we just created above called ObjModel
			// ================================================================
    		this.m_AgScenarioClass = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
    		this.m_AgScenarioClass.setShortDescription("ObjModel scenario created via AGI Java wrapper for Object Model");
    		this.m_AgScenarioClass.setLongDescription("See Short Description");

			// ==========================================
			// Get the Scenario Start/Stop time
			// ==========================================
    		this.m_StartTime = (String)this.m_AgScenarioClass.getStartTime_AsObject();
    		this.m_StopTime = (String)this.m_AgScenarioClass.getStopTime_AsObject();

			// ==========================================
			// Set the Scenario Animation configuration
			// ==========================================
			IAgScAnimation scAnimation = this.m_AgScenarioClass.getAnimation();

			// Animation Cycle
			scAnimation.setEnableAnimCycleTime(true);
            // TODO: change the animation cycle type of the scenario to AgEScEndLoopType.E_LOOP_AT_TIME
			scAnimation.setAnimCycleType(AgEScEndLoopType.E_LOOP_AT_TIME);
			scAnimation.setAnimCycleTime(this.m_StopTime);

			// Time Step
			scAnimation.setAnimStepType(AgEScTimeStepType.E_SC_TIME_STEP);
			scAnimation.setAnimStepValue(10.0); // seconds

			// ==========================================
			// Set 2D Scenario Graphics
			// ==========================================
			IAgScGraphics scen2D = this.m_AgScenarioClass.getGraphics();

			// The following are already set to true by default, but
			// just showing how to do so. Refer to STK's Scenario Properties
			// Page and navigate to "2DGraphics->GlobalAttributes".
			scen2D.setAccessAnimHigh(true);
			scen2D.setAccessLinesVisible(true);
			scen2D.setAccessStatHigh(true);
			scen2D.setCentroidsVisible(true);
			scen2D.setGndMarkersVisible(true);
			scen2D.setGndTracksVisible(true);
			scen2D.setInertialPosLabelsVisible(true);
			scen2D.setInertialPosVisible(true);
			scen2D.setLabelsVisible(true);
			scen2D.setOrbitMarkersVisible(true);
			scen2D.setOrbitsVisible(true);
			scen2D.setSensorsVisible(true);
			scen2D.setSubPlanetLabelsVisible(true);
			scen2D.setSubPlanetPointsVisible(true);

			// The following are NOT set to true by default.
			// We set them true here to show the capabilities.
			// Refer to STK's Scenario Properties page and navigate to
			// "2DGraphics->GlobalAttributes"
			scen2D.setAllowAnimUpdate(true);
			scen2D.setElsetNumVisible(true);
			scen2D.setPlanetOrbitsVisible(true);

			// ==========================================
			// Set 3D Scenario Graphics
			// ==========================================
			IAgScVO scen3D = this.m_AgScenarioClass.getVO();

			// ==========================================
			// Showing how to access the font's on the
			// 3D Globe.
			// ==========================================
			IAgSc3dFont small3dFont = scen3D.getSmallFont();
			small3dFont.setPtSize(14); // default 12
			small3dFont.setBold(true); // default false
			small3dFont.setItalic(false); // default false

			IAgSc3dFont medium3dFont = scen3D.getMediumFont();
			medium3dFont.setPtSize(16); // default 14
			medium3dFont.setBold(false); // default false
			medium3dFont.setItalic(true); // default false

			IAgSc3dFont large3dFont = scen3D.getLargeFont();
			large3dFont.setPtSize(18); // default 20
			large3dFont.setBold(true); // default false
			large3dFont.setItalic(true); // default false

			// ========================================
			// Display the available fonts
			// ========================================
			Object[] fonts = (Object[])large3dFont.getAvailableFonts_AsObject();
           	System.out.println("Available Fonts Include:" );
   			for( int colIndex = 0; colIndex < fonts.length; colIndex++ )
       		{
   				System.out.println("\t["+colIndex+"] = " + fonts[colIndex] );
        	}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/*package*/ void createFacility()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Create Facility Object
			// ===============================================
    		// TODO: Using the m_AgScenarioClass variable, get the children of the scenario and create a new Facility object
    		// with the name contained in the static variable s_FAC_NAME.  Cast the new object to
    		// a variable of type AgFacilityClass called fac
			AgFacilityClass	fac = (AgFacilityClass)this.m_AgScenarioClass.getChildren()._new(AgESTKObjectType.E_FACILITY, s_FAC_NAME);
			fac.setShortDescription("Facility/" + s_FAC_NAME + " created by Java STK Engine wrapper Obj Model sample");
			fac.setLongDescription("See Short Description");

			System.out.println("===========================");
			System.out.println(" Created Facility/" + s_FAC_NAME);
			System.out.println("===========================");

			// ===============================================
			// Set the position
			// ===============================================
    		IAgPosition facpos 		= fac.getPosition();
    		IAgGeodetic facGeo 		= (IAgGeodetic)facpos.convertTo(AgEPositionType.E_GEODETIC);

			facGeo.setLat(new Double(40.040)); // degrees
			facGeo.setLon(new Double(-75.595)); // degrees

            // TODO: Assign back the Geodetic variable's changes to the facilities
            // position variable called facpos using the assign method()
            // If you don't the new Alt, Lat, Lon will not take effect and the facility
            // will remain at its default LLA
			facpos.assign(facGeo); // Reassign it back to the facilities position.

			// ===============================================
			// Set the 2D Graphics
			// ===============================================
			IAgFaGraphics fa2D = fac.getGraphics();

			fa2D.setColor(AgCoreColor.MEDIUMSPRINGGREEN);
			fa2D.setInheritFromScenario(true); // Already set by default
			fa2D.setLabelVisible(true); // Set the label visible, true by default.
			fa2D.setLabelColor(AgCoreColor.LIMEGREEN);
			fa2D.setUseInstNameLabel(false); // Turn off using the instance name as the label
			fa2D.setLabelName("Java Facility/" + s_FAC_NAME); // Change the label from default name to add Java in it
			fa2D.setMarkerColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));
			fa2D.setMarkerStyle("Circle"); // Show a circle marker instead of the Facility.bmp default.

			// ===============================================
			// Get the 3D Graphics
			// ===============================================
			IAgFaVO faVO = fac.getVO();

			// Build the 3D model path
			String separator = System.getProperty("file.separator");
			StringBuffer sb = new StringBuffer();
			sb.append(this.m_StkHomeDir);
			sb.append(separator);
			sb.append("STKData");
			sb.append(separator);
			sb.append("VO");
			sb.append(separator);
			sb.append("Models");
			sb.append(separator);
			sb.append("Land");
			sb.append(separator);
			sb.append("facility.mdl");

			// ==================
			// Set the 3D Model
			// ==================
			IAgPtTargetVOModel mdl = faVO.getModel();
			mdl.setVisible(true);
    		// TODO: set the model to use model type of AgEModelType.E_MODEL_FILE
			mdl.setModelType(AgEModelType.E_MODEL_FILE);
			mdl.setScaleValue(0);
    		// TODO: Create a IAgVOModelFile variable called mdlFile 
			// by casting the return of the mdl.getModelData() method call.
    		IAgVOModelFile mdlFile = (IAgVOModelFile)mdl.getModelData();
			mdlFile.setFilename(sb.toString());
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/*package*/ void createSatellite()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ========================
			// Create Satellite Object
			// ========================
			AgSatelliteClass sat = null;
			sat = (AgSatelliteClass)this.m_AgScenarioClass.getChildren()._new(AgESTKObjectType.E_SATELLITE, s_SAT_NAME);

			System.out.println("=======================");
			System.out.println(" Created Satellite/" + s_SAT_NAME);
			System.out.println("=======================");

			// ===============================================
			// Set 2D ( Map ) graphics
			// ===============================================
    		// TODO:  Get the satellites 2D graphics interface and assign it
			// to a variable of type IAgSaGraphics called sa2D
			IAgSaGraphics sa2D = sat.getGraphics();

			// ===============================================
			// Set 2D ( Map ) Attributes
			// ===============================================
			int attType = sa2D.getAttributesType();
			System.out.println("Attribute Type == " + attType);

			// ============================
			// Set Basic Attributes
			// ============================
			IAgVeGfxAttributesBasic gfxAttBasic  = (IAgVeGfxAttributesBasic)sa2D.getAttributes();
			gfxAttBasic.setIsVisible(true);
			gfxAttBasic.setMarkerStyle("Plus");
			gfxAttBasic.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.MAGENTA));

			// ===============================================
			// Set Propagator type to TwoBody
			// ===============================================
    		// TODO: Set the satellite's propagator type to AgEVePropagatorType.E_PROPAGATOR_TWO_BODY
			sat.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_TWO_BODY);
			IAgVePropagatorTwoBody twobody = (IAgVePropagatorTwoBody)sat.getPropagator();

			IAgCrdnEventIntervalSmartInterval interval = null;
			interval = twobody.getEphemerisInterval();
			interval.setExplicitInterval(this.m_StartTime, this.m_StopTime);
   			// TODO: Set the propagator's time step to 60.0
			twobody.setStep(60.0);

			// Below is the obj model equivalent of the following connect command...
			// SetState */Satellite/Satellite1 Classical TwoBody \"1 Jul 2006 12:00:00.000\" \"2 Jul 2006 12:00:00.000\" 60 J2000 \"1 Jul 2006 12:00:00.000\" 7000.00 0.0 28.5 0.0 0.0 0.0

			// ====================================
			// Set the epoch in the initial state
			// ====================================
			IAgVeInitialState initState = twobody.getInitialState();

			// ====================================
			// Convert to a classical orbit state
			// ====================================
			IAgOrbitState os = initState.getRepresentation();
			os.setEpoch(this.m_StartTime);
			IAgOrbitStateClassical osc = (IAgOrbitStateClassical)os.convertTo(AgEOrbitStateType.E_ORBIT_STATE_CLASSICAL);

			// =========================
			// J2000 coordinate system
			// =========================
   			// TODO: Set the osc's coordinate system type to an the J2000
   			// coordinate system on the AgECoordinateSystem enumeration
			osc.setCoordinateSystemType(AgECoordinateSystem.E_COORDINATE_SYSTEM_J2000);

			// ====================================
			// Set the Classical Orbit Size Shape
			// ====================================
			osc.setSizeShapeType(AgEClassicalSizeShape.E_SIZE_SHAPE_SEMIMAJOR_AXIS);
			IAgClassicalSizeShapeSemimajorAxis csssa = (IAgClassicalSizeShapeSemimajorAxis)osc.getSizeShape();
   			// TODO: Set the satellite's Semi Major Axis to 7000.00 km from the csssa variable
			csssa.setSemiMajorAxis(7000.00);
			csssa.setEccentricity(0.0);

			// ==================================
			// Set the orbit orientation values
			// ==================================
			IAgClassicalOrientation co = osc.getOrientation();
			co.setArgOfPerigee(0.0); // degress by default
   			// TODO: Set the classical orientation's inclination to 45.0 degrees
			co.setInclination(45.0); // degrees by default

			// =====================================
			// Set the orbit location mean anomaly
			// =====================================
			osc.setLocationType(AgEClassicalLocation.E_LOCATION_MEAN_ANOMALY);
			IAgClassicalLocationMeanAnomaly clma = (IAgClassicalLocationMeanAnomaly)osc.getLocation();
			clma.setValue(0.0);

			// =============================================================
			// Set the orbit raan ( right ascension of the ascending node )
			// =============================================================
			co.setAscNodeType(AgEOrientationAscNode.E_ASC_NODE_RAAN);
			IAgOrientationAscNodeRAAN raan = (IAgOrientationAscNodeRAAN)co.getAscNode();
			raan.setValue(180.0);

			// =====================================================
			// Assign the classical state back to the orbit state
			// or the new orbit state values will not take effect.
			// =====================================================
   			// TODO: Assign the Orbital Classical state variable callsed osc to the
   			// Orbital State variable called os.  If you do not your orbital state
   			// will not be changed in the satellite and its orbit will remain with the default
   			// values
			os.assign(osc);

			// ===================================================================
			// Now propagate the twobody classical J2000 coordinate system orbit
			// ===================================================================
			twobody.propagate();

		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/*package*/ void createAccess()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			IAgStkObjectCollection scenObjects = this.m_AgScenarioClass.getChildren();

			IAgStkObject objSat = scenObjects.getItem(s_SAT_NAME);
			IAgStkObject objFac = scenObjects.getItem(s_FAC_NAME);

			String satPath = objSat.getPath();
    		// TODO: Get the IAgStkAccess interface from the objFac by
    		// passing the satPath to the getAccess() method.
			IAgStkAccess satAccess = objFac.getAccess(satPath);

    		// TODO:  Compute the access from the IAgStkAccess interface you
    		// obtained from above
			satAccess.computeAccess();
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/*package*/ void createVectors()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get existing Facility F16_Base Object
			// ===============================================
			IAgFacility fac = (IAgFacility)this.m_AgScenarioClass.getChildren().getItem(s_FAC_NAME);

			// =====================
			// Set 3D Vector info
			// =====================
			IAgFaVO faVO = fac.getVO();
    		// TODO:  Get the IAgVOVector interface from the facilities VO interface.
    		// Call this new variable voVector
			IAgVOVector voVector = faVO.getVector();

			voVector.setAngleSizeScale(1.000);
			voVector.setScaleRelativeToModel(true);
			voVector.setVectorSizeScale(3.000);

			// =================================
			// Set 3D Vectors for sun and moon
			// =================================
			IAgVORefCrdnCollection voRefCrdnCol = voVector.getRefCrdns();

			System.out.println("RefVector's include:");

			for(int j = 0; j < voRefCrdnCol.getCount(); j++)
			{
				IAgVORefCrdn refCrdn = voRefCrdnCol.getItem(j);

				if(refCrdn.getTypeID_AsObject() == AgEGeometricElemType.E_VECTOR_ELEM)
				{
					IAgVORefCrdnVector refVector = (IAgVORefCrdnVector)refCrdn;

					String refVecName = refVector.getName();
					System.out.println("\t" + refVecName);

					if(refVecName.equalsIgnoreCase("Facility/" + s_FAC_NAME + " Sun Vector"))
					{
						refVector.setVisible(true);
						refVector.setLabelVisible(true);
						refVector.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
						IAgDisplayTm dispTime = (IAgDisplayTm)refVector;
						dispTime.setDisplayStatusType(AgEDisplayTimesType.E_ALWAYS_ON);
					}
    				/* TODO: Find the "Moon Vector" on the Facility object
    				 * and assign the same values for this "moon" vector as
    				 * for the sun vector above.  But change the moon vector's color
    				 * to Gray via the value of 0xE0E0E0
    				*/
					else if(refVecName.equalsIgnoreCase("Facility/" + s_FAC_NAME + " Moon Vector"))
					{
						refVector.setVisible(true);
						refVector.setLabelVisible(true);
						refVector.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.LIGHT_GRAY));
						IAgDisplayTm dispTime = (IAgDisplayTm)refVector;
						dispTime.setDisplayStatusType(AgEDisplayTimesType.E_ALWAYS_ON);
					}
				}
			}

			// =================================
			// Display the available Crdns
			// =================================
			Object[] crdns = (Object[])voRefCrdnCol.getAvailableCrdns_AsObject();
			int colCount = crdns.length;
			System.out.println("Available Crdns are:");
			for(int colIndex = 0; colIndex < colCount; colIndex++)
			{
				Object[] crdnArray = (Object[])crdns[colIndex];
				
				System.out.print("\t["+colIndex+"] = ");
				for(int i=0; i<crdnArray.length; i++)
				{
					System.out.print(crdnArray[i]);
					if(i != crdnArray.length-1) 
					{
						System.out.print(", ");
					}
				}
				System.out.println();
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/*package*/ void createDataDisplay()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get Previously create AGI_BIRD_1 Object
			// ===============================================
			IAgSatellite sat = null;
			sat = (IAgSatellite)this.m_AgScenarioClass.getChildren().getItem(s_SAT_NAME);

			System.out.println("==============================");
			System.out.println(" Got Satellite/" + s_SAT_NAME);
			System.out.println("==============================");

			// ===================================================
			// 3D Show LLA Position Data
			// ===================================================
			IAgSaVO vo = sat.getVO();
    		// TODO: Get the VO's data display collection ( IAgVODataDisplayCollection ) and
    		// assign its value to a variable call ddc
			IAgVODataDisplayCollection ddc = vo.getDataDisplay();

			int ddcount = ddc.getCount();
			System.out.println("ddcount=" + ddcount);
			System.out.println("Data Displays include:");

			for(int i = 0; i < ddcount; i++)
			{
				IAgVODataDisplayElement dde = ddc.getItem(i);
				
				// You can display any of the following printed out display names
				String name = dde.getName();
				System.out.println("\t"+name);
				
				if(name.equals("LLA Position"))
				{
					dde.setIsVisible(true);
				}
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/*package*/ void createDataProviders()
	throws Throwable
	{
		// ===============================================
		// Get Previously create sat Object
		// ===============================================
		IAgStkObject satobj = this.m_AgScenarioClass.getChildren().getItem(s_SAT_NAME);

		System.out.println("==============================");
		System.out.println(" Got Satellite/" + s_SAT_NAME);
		System.out.println("==============================");

		// TODO: Get the data providers collection ( IAgDataProviderCollection ) from
		// the satobj variable and assign it to a variable call dpc
		IAgDataProviderCollection dpc = satobj.getDataProviders();

		int dpCount = dpc.getCount();
		System.out.println("dpCount=" + dpCount);

		for(int i = 0; i < dpCount; i++)
		{
			IAgDataProviderInfo dpi = dpc.getItem(new Integer(i));
			String name = dpi.getName();
			AgEDataProviderType type = dpi.getType_AsObject();
			boolean isGroup = dpi.isGroup();
			System.out.println("DataProviderInfo[ name=" + name + ", type=" + type + ", typeInfo=" + this.getHumanReadeableDpType(type) + ", isGroup=" + isGroup + " ]");

			if(name.equalsIgnoreCase("LLA State"))
			{
				if(isGroup)
				{
					IAgDataProviderGroup group = (IAgDataProviderGroup)dpi;
					IAgDataProviders prvs = group.getGroup();

					int llaDpcCount = prvs.getCount();
					System.out.println("llaDpcCount=" + llaDpcCount);

					IAgDataProviderInfo dpilla = prvs.getItem("Fixed");
					String llaname = dpilla.getName();
					AgEDataProviderType llatype = dpilla.getType_AsObject();
					boolean llaisGroup = dpilla.isGroup();
					System.out.println("DataProviderInfo for LLA State[ name=" + llaname + ", type=" + llatype + ", typeInfo=" + this.getHumanReadeableDpType(llatype) + ", isGroup=" + llaisGroup
					+ " ]");

					IAgDataPrvTimeVar var = (IAgDataPrvTimeVar)dpilla;
					IAgDrResult result = var.exec(this.m_StartTime, this.m_StopTime, 60);

					IAgDrDataSetCollection dsc = result.getDataSets();
					int dscCount = dsc.getCount();
					System.out.println("dscCount=" + dscCount);

					String elementNames[] = new String[dscCount];
					int elementTypes[] = new int[dscCount];
					int elementUnits[] = new int[dscCount];
					int valueCounts[] = new int[dscCount];
					Object values[] = new Object[dscCount];
					Object units[] = new Object[dscCount];

					for(int dscIndex = 0; dscIndex < dscCount; dscIndex++)
					{
						IAgDrDataSet dataset = dsc.getItem(dscIndex);

						elementNames[dscIndex] = dataset.getElementName();
						elementTypes[dscIndex] = dataset.getElementType();
						elementUnits[dscIndex] = dataset.getUnitType();
						valueCounts[dscIndex] = dataset.getCount();
						units[dscIndex] = (Object[])dataset.getInternalUnitValues_AsObject();
						values[dscIndex] = (Object[])dataset.getValues_AsObject();
					}

					for(int dscIndex = 0; dscIndex < dscCount; dscIndex++)
					{
						System.out.print(elementNames[dscIndex]);
						System.out.print("[ ");
						System.out.print(getHumanReadeableElemType(AgEDataPrvElementType.getFromValue(elementTypes[dscIndex])));
						System.out.print(", ");
						System.out.print(elementUnits[dscIndex]);
						System.out.print(", ");
						System.out.print(valueCounts[dscIndex]);
						System.out.print(" ]:");

						Object value = values[dscIndex];
						Object[] vars = (Object[])value;

						for(int dsIndex = 0; dsIndex < valueCounts[dscIndex]; dsIndex++)
						{
							System.out.print("  ");
							System.out.print(vars[dsIndex].toString());
						}
						
						System.out.println();
					}

					IAgDrIntervalCollection ic = result.getIntervals();
					int icCount = ic.getCount();
					System.out.println("icCount=" + icCount);

					IAgDrInterval interval = ic.getItem(0);
					Object startTime = interval.getStartTime_AsObject();
					Object stopTime = interval.getStopTime_AsObject();

					System.out.println("\tstartTime=" + startTime + ", stopTime=" + stopTime);
				}
			}
		}
	}

	/*package*/ void saveAndUnloadScenario()
	throws Throwable
	{
		String workingDirectory = System.getProperty("user.dir");
		String platFilePathSep = System.getProperty("file.separator");
		StringBuffer filePath = new StringBuffer();
		filePath.append(workingDirectory);
		filePath.append(platFilePathSep);
		filePath.append("AGI_JAVA_OMTUTEND");

		this.m_AgStkObjectRootClass.saveScenarioAs(filePath.toString());
		this.m_AgStkObjectRootClass.closeScenario();
	}

	/*package*/ String getHumanReadeableElemType(AgEDataPrvElementType elemType)
	{
		String humanElemType = null;

		switch(elemType)
		{
			case E_REAL:
			{
				humanElemType = "Real";
				break;
			}
			case E_INT:
			{
				humanElemType = "Int";
				break;
			}
			case E_CHAR:
			{
				humanElemType = "Char";
				break;
			}
			case E_CHAR_OR_REAL:
			{
				humanElemType = "CharOrReal";
				break;
			}
			default:
			{
				humanElemType = "unreadable";
				break;
			}
		}

		return humanElemType;
	}

	/*package*/ String getHumanReadeableDpType(AgEDataProviderType dpType)
	{
		String humanDpType = null;

		switch(dpType)
		{
			case E_DR_DUP_TIME:
			{
				humanDpType = "Duplicate Time";
				break;
			}
			case E_DR_DYN_IGNORE:
			{
				humanDpType = "Dynamic Ignore";
				break;
			}
			case E_DR_FIXED:
			{
				humanDpType = "Fixed";
				break;
			}
			case E_DR_INTVL:
			{
				humanDpType = "Interval";
				break;
			}
			case E_DR_INTVL_DEFINED:
			{
				humanDpType = "Interval Defined";
				break;
			}
			case E_DR_STAND_ALONE:
			{
				humanDpType = "StandAlone";
				break;
			}
			case E_DR_TIME_VAR:
			{
				humanDpType = "Time Variable";
				break;
			}
			default:
			{
				humanDpType = "unreadable";
				break;
			}
		}

		return humanDpType;
	}
}