//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkobjects.*;

public class SampleCode
{
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgScenarioClass			m_AgScenarioClass;
	private IAgStkObjectCollection	m_ScenarioChildren;
	private String					m_StopTime;

	/* package */SampleCode(AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass = root;
	}

	/* package */void setUnits()
	throws Throwable
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
		this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DateFormat", "UTCG");
	}

	/* package */void createScenario()
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
			this.m_AgStkObjectRootClass.closeScenario();
			this.m_AgStkObjectRootClass.newScenario("ObjModel");

			System.out.println("===========================");
			System.out.println(" Created Scenario/ObjModel ");
			System.out.println("===========================");

			IAgScenario scenario = (IAgScenario)this.m_AgStkObjectRootClass.getCurrentScenario();
			Object[] markers = (Object[])scenario.getVO().availableMarkerTypes_AsObject();
			int colCount = markers.length;
			System.out.println("Available Marker Types are:");
			for(int colIndex = 0; colIndex < colCount; colIndex++)
			{
				System.out.println("\t[" + colIndex + "] = " + markers[colIndex]);
			}

			// ================================================================
			// Get the current scenario we just created above called ObjModel
			// ================================================================
			this.m_AgScenarioClass = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
			this.m_ScenarioChildren = this.m_AgScenarioClass.getChildren();
			this.m_AgScenarioClass.setShortDescription("ObjModel scenario created via AGI Java wrapper for Object Model");
			this.m_AgScenarioClass.setLongDescription("See Short Description");

			// ==========================================
			// Get the Scenario Stop time
			// ==========================================
    		this.m_StopTime = (String)this.m_AgScenarioClass.getStopTime_AsObject();

			// ==========================================
			// Set the Scenario Animation configuration
			// ==========================================
			IAgScAnimation scAnimation = this.m_AgScenarioClass.getAnimation();

			// Animation Cycle
			scAnimation.setEnableAnimCycleTime(true);
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
			Object[] fonts = large3dFont.getAvailableFonts().getJavaObjectArray();
			System.out.println("Available Fonts Include:");
			for(int colIndex = 0; colIndex < fonts.length; colIndex++)
			{
				System.out.println("\t[" + colIndex + "] = " + fonts[colIndex]);
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */String createHomeFacility()
	throws Throwable
	{
		String objectName = null;
		
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "F16_Base";

			// ===============================================
			// Create Facility Object
			// ===============================================
			AgFacilityClass fac = (AgFacilityClass)this.m_ScenarioChildren._new(AgESTKObjectType.E_FACILITY, name);
			fac.setShortDescription("Facility/" + name + " created by Java STK Engine wrapper Obj Model sample");
			fac.setLongDescription("See Short Description");

			System.out.println("===========================");
			System.out.println(" Created Facility/F16_Base ");
			System.out.println("===========================");

			// ===============================================
			// Set the position
			// ===============================================
			IAgPosition facpos = fac.getPosition();
			IAgGeodetic facGeo = (IAgGeodetic)facpos.convertTo(AgEPositionType.E_GEODETIC);

			facGeo.setLat(new Double(36.406)); // degrees
			facGeo.setLon(new Double(-100.212)); // degrees

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
			fa2D.setLabelName("Java Facility/F16_Base"); // Change the label from default name to add Java in it
			fa2D.setMarkerColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));
			fa2D.setMarkerStyle("Circle"); // Show a circle marker instead of the Facility.bmp default.

			// ===============================================
			// Get the 3D Graphics
			// ===============================================
			IAgFaVO faVO = fac.getVO();

			// Build the 3D model path
			String separator = System.getProperty("file.separator");
			String userDir = System.getProperty("user.dir");
			StringBuffer sb = new StringBuffer();
			sb.append(userDir);
			sb.append(separator);
			sb.append("data");
			sb.append(separator);
			sb.append("airport.mdl");

			// ==================
			// Set the 3D Model
			// ==================
			IAgPtTargetVOModel mdl = faVO.getModel();
			mdl.setVisible(true);
			mdl.setModelType(AgEModelType.E_MODEL_FILE);
			mdl.setScaleValue(0);
			IAgVOModelFile mdlFile = (IAgVOModelFile)mdl.getModelData();
			mdlFile.setFilename(sb.toString());

			objectName = "Facility/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
		
		return objectName;
	}

	/* package */void createHomeFacilityVectors()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "Facility/F16_Base";

			// ===============================================
			// Get existing Facility F16_Base Object
			// ===============================================
			IAgFacility fac = (IAgFacility)this.m_ScenarioChildren.getItem("F16_Base");

			// =====================
			// Set 3D Vector info
			// =====================
			IAgFaVO faVO = fac.getVO();
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

				if(refCrdn instanceof IAgVORefCrdnVector)
				{
					IAgVORefCrdnVector refVector = (IAgVORefCrdnVector)refCrdn;

					String refVecName = refVector.getName();
					System.out.println("\t" + refVecName);

					if(refVecName.equalsIgnoreCase(name + " Sun Vector"))
					{
						refVector.setVisible(true);
						refVector.setLabelVisible(true);
						refVector.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
						IAgDisplayTm dispTime = (IAgDisplayTm)refVector;
						dispTime.setDisplayStatusType(AgEDisplayTimesType.E_ALWAYS_ON);
					}
					else if(refVecName.equalsIgnoreCase(name + " Moon Vector"))
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
			Object[][] crdns = voRefCrdnCol.getAvailableCrdns().getJavaObject2DArray();
			System.out.println("Available RefCrdn:");
			for(int rowIndex = 0; rowIndex < crdns.length; rowIndex++)
			{
				for(int colIndex = 0; colIndex < ((Object[])crdns[0]).length; colIndex++)
				{
					System.out.println("\t[" + rowIndex + "][" + colIndex + "] = " + ((Object[])crdns[rowIndex])[colIndex]);
				}
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */String createSamSite()
	throws Throwable
	{
		String objectName = null;

		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "SAM";

			// ===============================================
			// Create Facility Object
			// ===============================================
			IAgFacility fac = (IAgFacility)this.m_ScenarioChildren._new(AgESTKObjectType.E_FACILITY, name);

			System.out.println("======================");
			System.out.println(" Created Facility/SAM ");
			System.out.println("======================");

			// ===============================================
			// Set the position
			// ===============================================
			IAgPosition facpos = fac.getPosition();
			IAgGeodetic facGeo = (IAgGeodetic)facpos.convertTo(AgEPositionType.E_GEODETIC);

			facGeo.setLat(new Double(37.515255));
			facGeo.setLon(new Double(-80.809220));

			facpos.assign(facGeo);

			// ===============================================
			// Set the 2D Graphics
			// ===============================================
			IAgFaGraphics fa2D = fac.getGraphics();

			fa2D.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.PINK));
			fa2D.setInheritFromScenario(true); // Already set by default
			fa2D.setLabelVisible(true); // Set the label visible, true by default.
			fa2D.setLabelColor(AgCoreColor.HOTPINK);
			fa2D.setUseInstNameLabel(false); // Turn off using the instance name as the label
			fa2D.setLabelName("Java Facility/SAM"); // Change the label from default name to add Java in it
			fa2D.setMarkerColor(AgCoreColor.PURPLE);
			fa2D.setMarkerStyle("Square"); // Show a square marker instead of the Facility.bmp default.

			// ===============================================
			// Get the 3D Graphics
			// ===============================================
			IAgFaVO faVO = fac.getVO();

			// Build the 3D model path
			String separator = System.getProperty("file.separator");
			String userDir = System.getProperty("user.dir");
			StringBuffer sb = new StringBuffer();
			sb.append(userDir);
			sb.append(separator);
			sb.append("data");
			sb.append(separator);
			sb.append("ground-radar.mdl");

			// ===================================
			// Set the 3D Model
			// ===================================
			// The Higher quality of the model,
			// the longer loading the model takes
			// ===================================
			IAgPtTargetVOModel mdl = faVO.getModel();
			mdl.setVisible(true);
			mdl.setModelType(AgEModelType.E_MODEL_FILE);
			mdl.setScaleValue(0);
			IAgVOModelData mdlData = mdl.getModelData();
			IAgVOModelFile mdlFile = new AgVOModelFile(mdlData);
			mdlFile.setFilename(sb.toString());

			objectName = "Facility/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
		
		return objectName;
	}

	/* package */void createSamSiteRings()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "SAM";

			// ===============================================
			// Get Facility SAM Object
			// ===============================================
			IAgStkObject facObject = this.m_ScenarioChildren.getItem(name);
			IAgFacility fac = new AgFacility(facObject);

			// ======================
			// 2D Range Rings
			// ======================
			IAgFaGraphics gfx = fac.getGraphics();
			IAgGfxRangeContours rc2 = gfx.getContours();
			IAgLevelAttributeCollection lc = rc2.getLevelAttributes();

			rc2.setIsVisible(true);
			lc.removeAll(); // remove the default rings.
			lc.addLevelRange(new Double(1.0 /* km */), new Double(101.0), new Double(10.0 /* km */));

			// By defeult, there are 10 rings when adding
			// the range automatically, so ringCount should equal 10
			int ringCount = lc.getCount();
			System.out.println("ringCount=" + ringCount);

			for(int i = 0; i < ringCount; i++)
			{
				// How to modify ring level 1
				IAgLevelAttribute rl = lc.getItem(i);
				rl.setLabelVisible(true);
				rl.setLineWidth(AgELineWidth.E3);
				rl.setUserText("Ring Level " + i);
				rl.setUserTextVisible(true);

				switch(i)
				{
					case 0:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
						break;
					case 1:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.ORANGE));
						break;
					case 2:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
						break;
					case 3:
						rl.setColor(AgCoreColor.MEDIUMSPRINGGREEN);
						break;
					case 4:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));
						break;
					case 5:
						rl.setColor(AgCoreColor.TEAL);
						break;
					case 6:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLUE));
						break;
					case 7:
						rl.setColor(AgCoreColor.PURPLE);
						break;
					case 8:
						rl.setColor(AgCoreColor.VIOLET);
						break;
					case 9:
						rl.setColor(AgCoreColor.HOTPINK);
						break;
					default:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
						break;
				}
			}

			// ==================================================
			// Add Range Rings around the Facility on 3D window
			// We just need to turn the 3D range contours on b/c it will
			// inherit all the rings from the 2D window (by default)
			// ==================================================
			IAgFaVO vo = fac.getVO();
			IAgVORangeContours rc3 = vo.getRangeContours();
			IAgVOBorderWall bw = rc3.getBorderWall();

			rc3.setIsVisible(true);
			bw.setUseBorderWall(true);
			bw.setUpperEdgeHeight(0);
			bw.setLowerEdgeHeight(0.1);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createSamSpinningSensor()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "SAM_Spinning_Sensor";

			// ===============================================
			// Get Previously create SAM Facility Object
			// ===============================================
			IAgStkObject facobj = this.m_ScenarioChildren.getItem("SAM");

			// ==================================================
			// Create Sensor on the Facility
			// ==================================================
			IAgStkObjectCollection facChildren = facobj.getChildren();
			IAgStkObject sensorObject = facChildren._new(AgESTKObjectType.E_SENSOR, name);
			IAgSensor sensor = new AgSensor(sensorObject);

			System.out.println("====================================");
			System.out.println(" Created Sensor/SAM_Spinning_Sensor ");
			System.out.println("====================================");

			// ====================
			// Set Sensor Pattern
			// ====================
			sensor.setPatternType(AgESnPattern.E_SN_RECTANGULAR);

			IAgSnPattern pattern = sensor.getPattern();
			IAgSnRectangularPattern rectPattern = new AgSnRectangularPattern(pattern);

			rectPattern.setVerticalHalfAngle(new Double(5.0));
			rectPattern.setHorizontalHalfAngle(new Double(45.0));

			// =====================
			// Set the Pointing
			// =====================
			sensor.setPointingType(AgESnPointing.E_SN_PT_SPINNING);

			IAgSnPointing pointing = sensor.getPointing();
			IAgSnPtSpinning spinning = new AgSnPtSpinning(pointing);

			spinning.setScanMode(AgESnScanMode.E_SN_CONTINUOUS);
			spinning.setSpinRate(500.0);
			spinning.setSpinAxisAzimuth(new Double(0.0));
			spinning.setSpinAxisElevation(new Double(90.0));
			spinning.setSpinAxisConeAngle(new Double(90.0));

			// =======================================
			// Set 2D Graphics
			// =======================================
			IAgSnGraphics gfx = sensor.getGraphics();
			float[] hsb = Color.RGBtoHSB(99, 198, 222, null);
			Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
			gfx.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(awtColor));

			// =====================
			// Set the tranlucency
			// =====================
			IAgSnVO vo = sensor.getVO();
			vo.setTranslucentLinesVisible(true);
			vo.setFillVisible(true);
			vo.setFillTranslucency(50.0);

			// =====================
			// Range Constraint
			// =====================
			IAgAccessConstraintCollection acc = sensor.getAccessConstraints();
			IAgAccessConstraint ac = acc.addConstraint(AgEAccessConstraints.E_CSTR_RANGE);
			IAgAccessCnstrMinMax acmm = new AgAccessCnstrMinMax(ac);

			// Just like the STK GUI, we need to enable the max first.
			// Then we can set the max.
			acmm.setEnableMax(true);
			acmm.setMax(new Double(100.0)); // 100 Km
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */String createSamThreatDome()
	throws Throwable
	{
		String objectName = null;
		
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "SAM_Threat_Dome";

			// ===============================================
			// Get Previously create SAM Facility Object
			// ===============================================
			IAgStkObject facobj = this.m_ScenarioChildren.getItem("SAM");

			// =====================================================
			// Visualize a threat dome by creating a sensor child
			// on the facility object
			// =====================================================
			IAgStkObjectCollection facObjects = facobj.getChildren();
			IAgStkObject threatObject = facObjects._new(AgESTKObjectType.E_SENSOR, name);
			IAgSensor threatDome = new AgSensor(threatObject);

			System.out.println("======================================");
			System.out.println(" Created Sensor/SAM_Threat_Dome ");
			System.out.println("======================================");

			// =====================
			// Set 2D Graphics
			// =====================
			IAgSnGraphics threatDomegfx = threatDome.getGraphics();
			threatDomegfx.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));

			// =====================
			// Set 3D Graphics
			// =====================
			IAgSnVO threatDomeVO = threatDome.getVO();
			threatDomeVO.setTranslucentLinesVisible(true);
			threatDomeVO.setFillVisible(true);
			threatDomeVO.setFillTranslucency(50.0);

			// =====================
			// Set Sensor Pattern
			// =====================
			threatDome.setPatternType(AgESnPattern.E_SN_SIMPLE_CONIC);
			IAgSnSimpleConicPattern simplePattern = new AgSnSimpleConicPattern(threatDome.getPattern());
			simplePattern.setConeAngle(new Double(90.0));

			// =====================
			// Set the range
			// =====================
			IAgAccessConstraintCollection threatDomeCnstCol = threatDome.getAccessConstraints();
			IAgAccessConstraint accCnstr = threatDomeCnstCol.addConstraint(AgEAccessConstraints.E_CSTR_RANGE);
			IAgAccessCnstrMinMax threatDomeRangeCon = new AgAccessCnstrMinMax(accCnstr);

			// Just like the STK GUI, we need to enable the max first.
			// Then we can set the max.
			threatDomeRangeCon.setEnableMax(true);
			threatDomeRangeCon.setMax(new Double(100.0)); // 100 Km

			objectName = "Facility/SAM";
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
		return objectName;
	}

	/* package */String createSafeAirCorridor()
	throws Throwable
	{
		String objectName = null;

		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "Safe_Air_Corridor";

			// ===============================================
			// Create Area Target Object
			// ===============================================
			IAgStkObject areaTarObj = this.m_ScenarioChildren._new(AgESTKObjectType.E_AREA_TARGET, name);
			IAgAreaTarget safeCorridor = new AgAreaTarget(areaTarObj);

			System.out.println("======================================");
			System.out.println(" Created AreaTarget/Safe_Air_Corridor ");
			System.out.println("======================================");

			// ===================================
			// Set the shape of the areat target
			// ===================================
			safeCorridor.setAreaType(AgEAreaType.E_PATTERN);
			IAgAreaTypePatternCollection areaPattern = new AgAreaTypePatternCollection(safeCorridor.getAreaTypeData());

			// Inner edge
			areaPattern.add(new Double(37.680), new Double(-81.971));
			areaPattern.add(new Double(38.081), new Double(-81.763));
			areaPattern.add(new Double(38.412), new Double(-81.158));
			areaPattern.add(new Double(38.416), new Double(-80.513));
			areaPattern.add(new Double(38.148), new Double(-79.965));
			areaPattern.add(new Double(37.779), new Double(-79.689));
			areaPattern.add(new Double(37.354), new Double(-79.663));
			areaPattern.add(new Double(36.911), new Double(-79.932));
			areaPattern.add(new Double(36.625), new Double(-80.442));

			// Outer edge
			areaPattern.add(new Double(35.696), new Double(-80.326));
			areaPattern.add(new Double(36.547), new Double(-78.985));
			areaPattern.add(new Double(37.184), new Double(-78.645));
			areaPattern.add(new Double(38.098), new Double(-78.701));
			areaPattern.add(new Double(38.672), new Double(-79.124));
			areaPattern.add(new Double(39.242), new Double(-80.332));
			areaPattern.add(new Double(39.143), new Double(-81.705));
			areaPattern.add(new Double(38.534), new Double(-82.649));

			// ===============================================
			// Set the Color of the Area Target 2D/3D window
			// ===============================================
			IAgATGraphics at2D = safeCorridor.getGraphics();
			at2D.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));

			// ===============================================
			// Set the Translucency of the Area Target
			// in the 3D/VO window
			// ===============================================
			IAgATVO atVO = safeCorridor.getVO();
			atVO.setFillInterior(true);
			atVO.setPercentTranslucencyInterior(50.0);

			// ===============================================
			// Set the Boundary Wall height of the Area Target
			// ===============================================
			IAgVOBorderWall brdWallAT = atVO.getBorderWall();
			brdWallAT.setUseBorderWall(true);
			brdWallAT.setUseLineTranslucency(true);
			brdWallAT.setUseWallTranslucency(true);
			brdWallAT.setLineTranslucency(70.0);
			brdWallAT.setWallTranslucency(25.0);
			brdWallAT.setUpperEdgeHeight(20.0);
			brdWallAT.setLowerEdgeHeight(2);

			objectName = "AreaTarget/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
		
		return objectName;
	}

	/* package */String createF16FlightPath()
	throws Throwable
	{
		String objectName = null;

		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "F16";

			// ========================
			// Create Aircraft Object
			// ========================
			IAgStkObject airObject = this.m_ScenarioChildren._new(AgESTKObjectType.E_AIRCRAFT, name);
			IAgAircraft f15 = new AgAircraft(airObject);

			System.out.println("==============================");
			System.out.println(" Created Aircraft/F16 ");
			System.out.println("==============================");

			// ==================
			// Set 2D Graphics
			// ==================
			IAgAcGraphics acgfx = f15.getGraphics();
			IAgVeGfxAttributes gfxAttr = acgfx.getAttributes();
			IAgVeGfxAttributesRoute gfxAttrRoute = new AgVeGfxAttributesRoute(gfxAttr);
			float[] hsb = Color.RGBtoHSB(0, 255, 255, null);
			Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
			gfxAttrRoute.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(awtColor));
			gfxAttrRoute.setIsVisible(true); // default already set

			// =======================
			// Build model path
			// =======================
			String separator = System.getProperty("file.separator");
			String userDir = System.getProperty("user.dir");
			StringBuffer sb = new StringBuffer();
			sb.append(userDir);
			sb.append(separator);
			sb.append("data");
			sb.append(separator);
			sb.append("f-16_falcon.mdl");

			// =======================
			// Set the f15 model
			// =======================
			IAgAcVO acVO = f15.getVO();
			IAgVeRouteVOModel mdl = acVO.getModel();
			mdl.setModelType(AgEModelType.E_MODEL_FILE);
			mdl.setScaleValue(0);
			IAgVOModelFile mdlFile = new AgVOModelFile(mdl.getModelData());
			mdlFile.setFilename(sb.toString());

			// =======================================
			// Propagate the Aircraft with Waypoints
			// =======================================
			f15.setRouteType(AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC);
			IAgVePropagator prop = f15.getRoute();
			IAgVePropagatorGreatArc gaProp = new AgVePropagatorGreatArc(prop);
			gaProp.setMethod(AgEVeWayPtCompMethod.E_DETERMINE_TIME_ACC_FROM_VEL);
			IAgCrdnEventIntervalSmartInterval interval = null;
			interval = gaProp.getEphemerisInterval();
			interval.setExplicitInterval(this.m_AgScenarioClass.getStartTime(), this.m_AgScenarioClass.getStopTime());

			final double constantVelocity = 20;

			IAgVeWaypointsElement waypoint = gaProp.getWaypoints().add();

			waypoint.setLatitude(new Double(36.406));
			waypoint.setLongitude(new Double(-100.212));
			waypoint.setAltitude(0.001);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(36.406));
			waypoint.setLongitude(new Double(-87.212));
			waypoint.setAltitude(1.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.852));
			waypoint.setLongitude(new Double(-80.941));
			waypoint.setAltitude(3.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.835));
			waypoint.setLongitude(new Double(-80.066));
			waypoint.setAltitude(3.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.368));
			waypoint.setLongitude(new Double(-79.401));
			waypoint.setAltitude(3.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.902));
			waypoint.setLongitude(new Double(-79.233));
			waypoint.setAltitude(3.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(36.678));
			waypoint.setLongitude(new Double(-79.491));
			waypoint.setAltitude(3.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(35.949));
			waypoint.setLongitude(new Double(-81.079));
			waypoint.setAltitude(3.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(35.696));
			waypoint.setLongitude(new Double(-87.128));
			waypoint.setAltitude(2.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(36.406));
			waypoint.setLongitude(new Double(-100.212));
			waypoint.setAltitude(0.001);
			waypoint.setSpeed(constantVelocity);

			gaProp.propagate();

			objectName = "Aircraft/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
		
		return objectName;
	}

	/* package */void createF16Rings()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get Previously create F16 Aircraft Object
			// ===============================================
			IAgStkObject acobj = this.m_ScenarioChildren.getItem("F16");
			IAgAircraft f16 = new AgAircraft(acobj);

			System.out.println("==============================");
			System.out.println(" Got Aircraft/F16 ");
			System.out.println("==============================");

			// =======================================
			// 2D Range Rings
			// =======================================
			IAgAcGraphics gfx = f16.getGraphics();
			IAgGfxRangeContours rc2 = gfx.getRangeContours();
			IAgLevelAttributeCollection lc = rc2.getLevelAttributes();

			rc2.setIsVisible(true);
			lc.removeAll(); // Remove default rings if any.
			lc.addLevelRange(new Double(1.0 /* km */), new Double(51.0), new Double(5.0 /* km */));

			// By defeult, there are 10 rings when adding
			// the range automatically, so ringCount should equal 10
			int ringCount = lc.getCount();
			System.out.println("ringCount=" + ringCount);

			for(int i = 0; i < ringCount; i++)
			{
				// How to modify ring level 1
				IAgLevelAttribute rl = lc.getItem(i);
				rl.setLabelVisible(true);
				rl.setLineWidth(AgELineWidth.E1);
				rl.setUserText("Ring Level " + i);
				rl.setUserTextVisible(true);

				switch(i)
				{
					case 0:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
						break;
					case 1:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.ORANGE));
						break;
					case 2:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));
						break;
					case 3:
						rl.setColor(AgCoreColor.MEDIUMSPRINGGREEN);
						break;
					case 4:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));
						break;
					case 5:
						rl.setColor(AgCoreColor.TEAL);
						break;
					case 6:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLUE));
						break;
					case 7:
						rl.setColor(AgCoreColor.PURPLE);
						break;
					case 8:
						rl.setColor(AgCoreColor.VIOLET);
						break;
					case 9:
						rl.setColor(AgCoreColor.HOTPINK);
						break;
					default:
						rl.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.WHITE));
						break;
				}
			}

			// ===================================================
			// 3D Range Rings
			// ===================================================
			// We just need to turn the 3D range contours on
			// b/c it will inherit all the rings from the 2D
			// window (by default)
			// ===================================================
			IAgAcVO vo = f16.getVO();
			IAgVORangeContours rc3 = vo.getRangeContours();
			IAgVOBorderWall bw = rc3.getBorderWall();

			rc3.setIsVisible(true);
			bw.setUseBorderWall(true);
			bw.setUpperEdgeHeight(3.045);
			bw.setLowerEdgeHeight(3.051);
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createF16Vectors()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get Previously create F16 Aircraft Object
			// ===============================================
			IAgStkObject acobj = this.m_ScenarioChildren.getItem("F16");
			IAgAircraft f16 = new AgAircraft(acobj);

			System.out.println("==============================");
			System.out.println(" Got Aircraft/F16 ");
			System.out.println("==============================");

			// ===================================================
			// 3D Body Axes Vector
			// ===================================================
			IAgAcVO vo = f16.getVO();
			IAgVOVector vec = vo.getVector();

			// ------------------------------
			// Vectors scaling so they are
			// not bigger than the 3D model
			// ------------------------------
			vec.setAngleSizeScale(1.000);
			vec.setScaleRelativeToModel(true);
			vec.setVectorSizeScale(1.000);

			// --------------------
			// Set the body axes
			// --------------------
			IAgVORefCrdnCollection refCrdns = vec.getRefCrdns();
			for(int j = 0; j < refCrdns.getCount(); j++)
			{
				IAgVORefCrdn refCrdn = refCrdns.getItem(j);

				if(refCrdn.getTypeID_AsObject().equals(AgEGeometricElemType.E_AXES_ELEM))
				{
					AgVORefCrdnAxes refAxes = new AgVORefCrdnAxes(refCrdn);

					if(refAxes.getName().equalsIgnoreCase("Aircraft/F16 Body Axes"))
					{
						refAxes.setVisible(true);
						refAxes.setLabelVisible(true);
						float[] hsb = Color.RGBtoHSB(238, 204, 204, null);
						Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
						refAxes.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(awtColor));

						IAgDisplayTm dispTime = new AgDisplayTm(refAxes);
						dispTime.setDisplayStatusType(AgEDisplayTimesType.E_ALWAYS_ON);
					}
				}
			}

			// ---------------------------------
			// Print out other available Crdns
			// ---------------------------------
			Object crdns = refCrdns.getAvailableCrdns_AsObject();
			if(crdns instanceof Object[][])
			{
				System.out.println("Available RefCrdn:");
				Object[][] crdnsArray = (Object[][])crdns;
				int rowCount = crdnsArray.length;
				int colCount = crdnsArray[0].length;
				for(int rowIndex = 0; rowIndex < rowCount; rowIndex++)
				{
					for(int colIndex = 0; colIndex < colCount; colIndex++)
					{
						System.out.println("\t[" + rowIndex + "][" + colIndex + "] = " + crdnsArray[rowIndex][colIndex]);
					}
				}
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createF16DropDownLines()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get Previously create F16 Aircraft Object
			// ===============================================
			IAgStkObject acobj = this.m_ScenarioChildren.getItem("F16");
			IAgAircraft f16 = new AgAircraft(acobj);

			System.out.println("==============================");
			System.out.println(" Got Aircraft/F16 ");
			System.out.println("==============================");

			// =======================================
			// Add drop lines to aircraft
			// =======================================
			IAgAcVO vo = f16.getVO();
			IAgVeVORouteDropLines dl = vo.getDropLines();

			// ---------------------------------------
			// Position drop lines
			// ---------------------------------------
			IAgVeVODropLinePosItemCollection dlpc = dl.getPosition();

			int dlpcount = dlpc.getCount();
			System.out.println("dlpcount=" + dlpcount);

			for(int i = 0; i < dlpcount; i++)
			{
				IAgVeVODropLinePosItem posi = dlpc.getItem(i);

				if(posi.getType_AsObject().equals(AgEVeVODropLineType.E_DROP_LINE_WGS84ELLIPSOID))
				{
					posi.setIsVisible(true);
					posi.setUse2DColor(false);
					posi.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));
					posi.setLineWidth(AgELineWidth.E3);
				}
				else if(posi.getType_AsObject().equals(AgEVeVODropLineType.E_DROP_LINE_TERRAIN))
				{
					// Do Nothing Now, but you could if you want
					// too...just like in the WGS84 above.
					posi.setIsVisible(true);
				}
				else if(posi.getType_AsObject().equals(AgEVeVODropLineType.E_DROP_LINE_MEAN_SEA_LEVEL))
				{
					// Do Nothing Now, but you could if you want
					// too...just like in the WGS84 above.
					posi.setIsVisible(true);
				}
				else
				{
					posi.setIsVisible(true);
				}
			}

			// ---------------------------------------
			// Path drop lines
			// ---------------------------------------
			IAgVeVODropLinePathItemCollection dlc = dl.getRoute();

			int dlcount = dlc.getCount();
			System.out.println("dlcount=" + dlcount);

			for(int i = 0; i < dlcount; i++)
			{
				IAgVeVODropLinePathItem pi = dlc.getItem(i);

				if(pi.getType_AsObject().equals(AgEVeVODropLineType.E_DROP_LINE_WGS84ELLIPSOID))
				{
					pi.setIsVisible(true);
					pi.setUse2DColor(false);
					pi.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));
					pi.setLineWidth(AgELineWidth.E3);
					pi.setInterval(0.030);
				}
				else if(pi.getType_AsObject().equals(AgEVeVODropLineType.E_DROP_LINE_TERRAIN))
				{
					// Do Nothing Now, but you could if you want
					// too...just like in the WGS84 above.
					pi.setIsVisible(true);
				}
				else
				{
					pi.setIsVisible(true);
				}
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createF16DataDisplay()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get Previously create F16 Aircraft Object
			// ===============================================
			IAgStkObject acobj = this.m_ScenarioChildren.getItem("F16");
			IAgAircraft f16 = new AgAircraft(acobj);

			System.out.println("==============================");
			System.out.println(" Got Aircraft/F16 ");
			System.out.println("==============================");

			// ===================================================
			// 3D Show LLA Position Data
			// ===================================================
			IAgAcVO vo = f16.getVO();
			IAgVODataDisplayCollection ddc = vo.getDataDisplay();

			int ddcount = ddc.getCount();
			System.out.println("ddcount=" + ddcount);

			for(int i = 0; i < ddcount; i++)
			{
				IAgVODataDisplayElement dde = ddc.getItem(i);

				if((dde.getName()).equals("LLA Position"))
				{
					dde.setIsVisible(true);
				}
				// You could display other data, such as Velocity Heading
				else if((dde.getName()).equals("Velocity Heading"))
				{
					dde.setIsVisible(false);
				}
			}
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createTargetedSensor()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "SAM_Targeted_Sensor";

			// ==================================================
			// Create Sensor on the Facility
			// ==================================================
			IAgStkObject facobj = this.m_ScenarioChildren.getItem("SAM");
			IAgStkObjectCollection facChildren = facobj.getChildren();
			IAgStkObject sensorObject = facChildren._new(AgESTKObjectType.E_SENSOR, name);
			IAgSensor sensor = new AgSensor(sensorObject);

			System.out.println("====================================");
			System.out.println(" Created Sensor/SAM_Targeted_Sensor ");
			System.out.println("====================================");

			// ====================
			// Set Sensor Pattern
			// ====================
			sensor.setPatternType(AgESnPattern.E_SN_RECTANGULAR);

			IAgSnPattern pattern = sensor.getPattern();
			IAgSnRectangularPattern rectPattern = new AgSnRectangularPattern(pattern);

			rectPattern.setVerticalHalfAngle(new Double(5.0));
			rectPattern.setHorizontalHalfAngle(new Double(5.0));

			// =====================
			// Set the Pointing
			// =====================
			sensor.setPointingType(AgESnPointing.E_SN_PT_TARGETED);

			IAgSnPointing pointing = sensor.getPointing();
			IAgSnPtTargeted targeted = new AgSnPtTargeted(pointing);

			// F16 needs to be created before we can set this
			IAgSnTargetCollection targets = targeted.getTargets();
			targets.add("Aircraft/F16");
			targeted.setEnableAccessTimes(true);

			// =======================================
			// Set 2D Graphics
			// =======================================
			IAgSnGraphics gfx = sensor.getGraphics();
			gfx.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.YELLOW));

			// =====================
			// Set the tranlucency
			// =====================
			IAgSnVO vo = sensor.getVO();
			vo.setTranslucentLinesVisible(true);
			vo.setFillVisible(true);
			vo.setFillTranslucency(50.0);

			// =====================
			// Range Constraint
			// =====================
			IAgAccessConstraintCollection acc = sensor.getAccessConstraints();
			IAgAccessConstraint ac = acc.addConstraint(AgEAccessConstraints.E_CSTR_RANGE);
			IAgAccessCnstrMinMax acmm = new AgAccessCnstrMinMax(ac);

			// Just like the STK GUI, we need to enable the max first.
			// Then we can set the max.
			acmm.setEnableMax(true);
			acmm.setMax(new Double(150.0)); // 150 Km
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */String createGroundVehicle()
	throws Throwable
	{
		String objectName = null;

		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "Hummer";

			// ========================
			// Create Ground Vehicle
			// ========================
			IAgStkObject gvObject = this.m_ScenarioChildren._new(AgESTKObjectType.E_GROUND_VEHICLE, name);
			IAgGroundVehicle gv = new AgGroundVehicle(gvObject);

			System.out.println("==============================");
			System.out.println(" Created GroundVehicle/Hummer ");
			System.out.println("==============================");

			// ==================
			// Set 2D Graphics
			// ==================
			IAgGvGraphics gvgfx = gv.getGraphics();
			IAgVeGfxAttributes gfxAttr = gvgfx.getAttributes();
			IAgVeGfxAttributesRoute gfxAttrRoute = new AgVeGfxAttributesRoute(gfxAttr);

			float[] hsb = Color.RGBtoHSB(0, 255, 255, null);
			Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
			gfxAttrRoute.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(awtColor));

			// =====================
			// Set 3D Vector info
			// =====================
			IAgGvVO gvVO = gv.getVO();
			IAgVOVector voVector = gvVO.getVector();

			voVector.setAngleSizeScale(1.000);
			voVector.setScaleRelativeToModel(true);
			voVector.setVectorSizeScale(1.000);

			// =======================
			// Build model path
			// =======================
			String separator = System.getProperty("file.separator");
			String userDir = System.getProperty("user.dir");
			StringBuffer sb = new StringBuffer();
			sb.append(userDir);
			sb.append(separator);
			sb.append("data");
			sb.append(separator);
			sb.append("humvee.mdl");

			// =======================
			// Set the GV model
			// =======================
			IAgVeRouteVOModel mdl = gvVO.getModel();
			mdl.setModelType(AgEModelType.E_MODEL_FILE);
			mdl.setScaleValue(0);
			IAgVOModelFile mdlFile = new AgVOModelFile(mdl.getModelData());
			mdlFile.setFilename(sb.toString());

			// =======================================
			// Propagate the Ground Vehicle with Waypoints
			// =======================================
			gv.setRouteType(AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC);
			IAgVePropagator prop = gv.getRoute();
			IAgVePropagatorGreatArc gaProp = new AgVePropagatorGreatArc(prop);
			gaProp.setMethod(AgEVeWayPtCompMethod.E_DETERMINE_TIME_ACC_FROM_VEL);
			IAgCrdnEventIntervalSmartInterval interval = null;
			interval = gaProp.getEphemerisInterval();
			interval.setExplicitInterval(this.m_AgScenarioClass.getStartTime(), this.m_AgScenarioClass.getStopTime());

			final double constantVelocity = 1;

			IAgVeWaypointsElement waypoint = gaProp.getWaypoints().add();

			waypoint.setLatitude(new Double(36.495));
			waypoint.setLongitude(new Double(-81.506));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.211));
			waypoint.setLongitude(new Double(-81.474));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.798));
			waypoint.setLongitude(new Double(-81.581));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.137));
			waypoint.setLongitude(new Double(-81.175));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.277));
			waypoint.setLongitude(new Double(-80.930));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.236));
			waypoint.setLongitude(new Double(-80.532));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(38.109));
			waypoint.setLongitude(new Double(-80.192));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.949));
			waypoint.setLongitude(new Double(-80.006));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.777));
			waypoint.setLongitude(new Double(-79.943));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.295));
			waypoint.setLongitude(new Double(-79.871));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(36.915));
			waypoint.setLongitude(new Double(-80.175));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = gaProp.getWaypoints().add();
			waypoint.setLatitude(new Double(37.273));
			waypoint.setLongitude(new Double(-80.553));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			gaProp.propagate();

			// ==========================
			// Get Route supported types
			// ==========================
			Object routes = gv.getRouteSupportedTypes_AsObject();
			if(routes instanceof Object[][])
			{
				System.out.println("Route Supported Types:");
				Object[][] routesArray = (Object[][])routes;
				int rowCount = routesArray.length;
				int colCount = routesArray[0].length;
				for(int rowIndex = 0; rowIndex < rowCount; rowIndex++)
				{
					for(int colIndex = 0; colIndex < colCount; colIndex++)
					{
						System.out.println("\t[" + rowIndex + "][" + colIndex + "] = " + routesArray[rowIndex][colIndex]);
					}
				}
			}
			
			objectName = "GroundVehicle/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}

		return objectName;
	}

	/* package */String createSatellite()
	throws Throwable
	{
		String objectName = null;

		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "GPS";

			// ========================
			// Create Satellite Object
			// ========================
			IAgStkObject satObject = this.m_ScenarioChildren._new(AgESTKObjectType.E_SATELLITE, name);
			IAgSatellite gpsSat = new AgSatellite(satObject);

			System.out.println("=======================");
			System.out.println(" Created Satellite/GPS ");
			System.out.println("=======================");

			// ===============================================
			// Set 2D ( Map ) graphics
			// ===============================================
			IAgSaGraphics sa2D = gpsSat.getGraphics();

			// ===============================================
			// Set 2D ( Map ) Attributes
			// ===============================================
			int attType = sa2D.getAttributesType();
			System.out.println("Attribute Type == " + attType);

			// ============================
			// Set Basic Attributes
			// ============================
			IAgVeGfxAttributes gfxAtt = sa2D.getAttributes();
			IAgVeGfxAttributesBasic gfxAttBasic = new AgVeGfxAttributesBasic(gfxAtt);
			gfxAttBasic.setIsVisible(true);
			gfxAttBasic.setMarkerStyle("Plus");
			float[] hsb = Color.RGBtoHSB(170, 170, 170, null);
			Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
			gfxAttBasic.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(awtColor));

			// ===============================================
			// Set Propagator type to SGP4
			// ===============================================
			gpsSat.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_SGP4);
			IAgVePropagator prop = gpsSat.getPropagator();
			IAgVePropagatorSGP4 sgp4 = new AgVePropagatorSGP4(prop);

			// ===============================================
			// Configure the propagator
			// ===============================================
			IAgCrdnEventIntervalSmartInterval interval = null;
			interval = sgp4.getEphemerisInterval();
			interval.setExplicitInterval(this.m_AgScenarioClass.getStartTime(), this.m_AgScenarioClass.getStopTime());

			// ===============================================
			// Set a segment of the propagator
			// ===============================================
			IAgVeSGP4SegmentCollection segments = sgp4.getSegments();
			IAgVeSGP4Segment segment = segments.addSeg();

			segment.setSSCNum("10684");
			segment.setClassification("U");
			segment.setIntlDesignator("78020A");
			segment.setEpoch(5277.07422);
			segment.setBStar(0.0001);
			segment.setRevNumber(18667);
			segment.setInclination(new Double(0.0));
			segment.setRAAN(new Double(282.9926));
			segment.setEccentricity(0.00);
			segment.setArgOfPerigee(new Double(172.9453));
			segment.setMeanAnomaly(new Double(187.1708));
			segment.setMeanMotion(new Double(8.25299291666667E-03));

			// ===============================================
			// Propagate
			// ===============================================
			sgp4.propagate();

			objectName = "Satellite/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}

		return objectName;
	}

	/* package */String createShip()
	throws Throwable
	{
		String objectName = null;

		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "AEGIS";

			// ========================
			// Create Ship
			// ========================
			IAgStkObject shipObject = this.m_ScenarioChildren._new(AgESTKObjectType.E_SHIP, name);
			IAgShip ship = new AgShip(shipObject);

			System.out.println("=======================");
			System.out.println(" Created Ship/AEGIS ");
			System.out.println("=======================");

			// ===============================================
			// Set 2D Graphics
			// ===============================================
			IAgShGraphics ship2D = ship.getGraphics();
			IAgVeGfxAttributes gfxAtt = ship2D.getAttributes();
			ship2D.setAttributesType(AgEVeGfxAttributes.E_ATTRIBUTES_BASIC);
			IAgVeGfxAttributesBasic gfxAttBasic = new AgVeGfxAttributesBasic(gfxAtt);
			float[] hsb = Color.RGBtoHSB(187, 187, 187, null);
			Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
			gfxAttBasic.setColor(AgAwtColorTranslator.fromAWTtoCoreColor(awtColor));

			// ===============================================
			// Set 3D Graphics
			// ===============================================
			IAgShVO ship3D = ship.getVO();
			IAgVeRouteVOModel mdl = ship3D.getModel();

			mdl.setModelType(AgEModelType.E_MODEL_FILE);
			mdl.setScaleValue(0);

			IAgVOModelFile mdlFile = new AgVOModelFile(mdl.getModelData());

			String separator = System.getProperty("file.separator");
			String userDir = System.getProperty("user.dir");

			StringBuffer sb = new StringBuffer();
			sb.append(userDir);
			sb.append(separator);
			sb.append("data");
			sb.append(separator);
			sb.append("aegis-destroyer.mdl");

			mdlFile.setFilename(sb.toString());

			// =======================================
			// Propagate the Ship with Waypoints
			// =======================================
			ship.setRouteType(AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC);

			IAgVePropagator prop = ship.getRoute();
			IAgVePropagatorGreatArc greatArc = new AgVePropagatorGreatArc(prop);

			greatArc.setMethod(AgEVeWayPtCompMethod.E_DETERMINE_TIME_ACC_FROM_VEL);
			IAgCrdnEventIntervalSmartInterval interval = null;
			interval = greatArc.getEphemerisInterval();
			interval.setExplicitInterval(this.m_AgScenarioClass.getStartTime(), this.m_AgScenarioClass.getStopTime());

			final double constantVelocity = 20;

			IAgVeWaypointsElement waypoint = greatArc.getWaypoints().add();

			waypoint.setLatitude(new Double(36.514));
			waypoint.setLongitude(new Double(-70.505));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(36.594));
			waypoint.setLongitude(new Double(-72.505));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(36.487));
			waypoint.setLongitude(new Double(-73.879));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(37.044));
			waypoint.setLongitude(new Double(-74.796));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(37.095));
			waypoint.setLongitude(new Double(-75.607));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(37.094));
			waypoint.setLongitude(new Double(-76.129));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(37.497));
			waypoint.setLongitude(new Double(-76.209));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(37.961));
			waypoint.setLongitude(new Double(-76.212));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(38.131));
			waypoint.setLongitude(new Double(-76.370));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			waypoint = greatArc.getWaypoints().add();
			waypoint.setLatitude(new Double(38.128));
			waypoint.setLongitude(new Double(-76.663));
			waypoint.setAltitude(0.0);
			waypoint.setSpeed(constantVelocity);

			greatArc.propagate();

			objectName = "Ship/" + name;
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
		return objectName;
	}

	/* package */void createMto()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			String name = "MultiTrack";

			// ========================
			// Create Mto Object
			// ========================
			IAgStkObject mtoObject = this.m_ScenarioChildren._new(AgESTKObjectType.E_MTO, name);
			IAgMto mto = new AgMto(mtoObject);

			// ===============================================
			// Configure the default track properties
			// ===============================================
			mto.getDefaultTrack().setInterpolate(true);

			// ===============================================
			// Set Unit Preferences for date representation
			// temporarily to EpSec for MTO, then set it
			// back to UTCG at the end.
			// ===============================================
			this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DateFormat", "EpSec");

			// ===============================================
			// Configure static track data
			// ===============================================
			double minLat = 40.0;
			double minLon = -84.0;
			double maxLat = 41.0;
			double maxLon = -82.0;

			Object[] timearray = new Object[] {new Double(0.0), new Double(3600.0), new Double(7200.0), new Double(14400.0), new Double(28800.0)};

			Object[] altarray = new Object[] {new Double(250.0), new Double(250.0), new Double(250.0), new Double(250.0), new Double(250.0)};

			// ===============================================
			// Use track recycling to optimize performance
			// ===============================================
			IAgMtoTrackCollection basictracks = new AgMtoTrackCollection(mto.getTracks());
			IAgMtoGfxTrackCollection gfxtracks = new AgMtoGfxTrackCollection(mto.getGraphics().getTracks());
			basictracks.setRecycling(true);
			gfxtracks.setRecycling(true);

			// ===============================================
			// Create tracks
			// ===============================================
			float[] hsb = Color.RGBtoHSB(0, 170, 0, null);
			Color awtColor = Color.getHSBColor(hsb[0], hsb[1], hsb[2]);
			AgCoreColor trackcolor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);
			for(int i = 0; i < 5; ++i)
			{
				Object[] latarray = new Object[] {new Double(minLat), new Double(minLat), new Double(maxLat), new Double(maxLat), new Double(minLat)};
				Object[] lonarray = new Object[] {new Double(minLon), new Double(maxLon), new Double(maxLon), new Double(minLon), new Double(minLon)};

				IAgMtoTrack basictrack = new AgMtoTrack(mto.getTracks().addTrack(i, timearray, latarray, lonarray, altarray));
				IAgMtoGfxTrack gfxtrack = new AgMtoGfxTrack(mto.getGraphics().getTracks().getTrackFromId(basictrack.getId()));

				basictrack.setName(String.valueOf(i));
				gfxtrack.setColor(trackcolor);

				minLon = maxLon;
				maxLon += 2.0;
				trackcolor.setValue(trackcolor.getValue() + 0x1100);
			}
			this.m_AgStkObjectRootClass.getUnitPreferences().setCurrentUnit("DateFormat", "UTCG");
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createSamF16Access()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			IAgStkObject f16Obj = this.m_ScenarioChildren.getItem("F16");
			IAgStkObject samObj = this.m_ScenarioChildren.getItem("SAM");

			String f16path = f16Obj.getPath();
			IAgStkAccess f16access = samObj.getAccess(f16path);

			f16access.computeAccess();
		}
		finally
		{
			this.m_AgStkObjectRootClass.endUpdate();
			this.m_AgStkObjectRootClass.rewind();
		}
	}

	/* package */void createDataProviders()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.beginUpdate();

			// ===============================================
			// Get Previously create F16 Aircraft Object
			// ===============================================
			IAgStkObject acobj = this.m_ScenarioChildren.getItem("F16");

			System.out.println("==============================");
			System.out.println(" Got Aircraft/F16 ");
			System.out.println("==============================");

			IAgDataProviderCollection dpc = acobj.getDataProviders();

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
						IAgDataProviderGroup group = new AgDataProviderGroupClass(dpi);
						IAgDataProviders prvs = group.getGroup();

						int llaDpcCount = prvs.getCount();
						System.out.println("llaDpcCount=" + llaDpcCount);

						IAgDataProviderInfo dpilla = prvs.getItem("Fixed");
						String llaname = dpilla.getName();
						AgEDataProviderType llatype = dpilla.getType_AsObject();
						boolean llaisGroup = dpilla.isGroup();
						System.out.println("DataProviderInfo for LLA State[ name=" + llaname + ", type=" + llatype + ", typeInfo=" + this.getHumanReadeableDpType(llatype) + ", isGroup=" + llaisGroup
						+ " ]");

						String startTime = (String)this.m_AgScenarioClass.getStartTime_AsObject();
						String stopTime = (String)this.m_AgScenarioClass.getStopTime_AsObject();
						double timeStep = 60;

						IAgDataPrvTimeVar var = new AgDataPrvTimeVarClass(dpilla);
						IAgDrResult result = var.exec(startTime, stopTime, timeStep);

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
							units[dscIndex] = dataset.getInternalUnitValues().getJavaObjectArray();
							values[dscIndex] = dataset.getValues().getJavaObjectArray();
						}

						for(int dscIndex = 0; dscIndex < dscCount; dscIndex++)
						{
							StringBuffer sb = new StringBuffer();

							sb.append(elementNames[dscIndex]);
							sb.append("[ ");
							sb.append(getHumanReadeableElemType(AgEDataPrvElementType.getFromValue(elementTypes[dscIndex])));
							sb.append(", ");
							sb.append(elementUnits[dscIndex]);
							sb.append(", ");
							sb.append(valueCounts[dscIndex]);
							sb.append(" ]:");

							Object[] valuesArray = (Object[])values[dscIndex];

							for(int dsIndex = 0; dsIndex < valueCounts[dscIndex]; dsIndex++)
							{
								sb.append("  ");
								sb.append(valuesArray[dsIndex]);
							}

							System.out.println(sb.toString());
						}

						IAgDrIntervalCollection ic = result.getIntervals();
						int icCount = ic.getCount();
						System.out.println("icCount=" + icCount);

						IAgDrInterval interval = ic.getItem(0);
						Object strStartTime = interval.getStartTime_AsObject();
						Object strStopTime = interval.getStopTime_AsObject();

						System.out.println("\tstartTime=" + strStartTime + ", stopTime=" + strStopTime);
					}
				}
			}
		}
		finally
		{
		}
	}

	public void reset()
	throws Throwable
	{
		try
		{
			this.m_AgStkObjectRootClass.closeScenario();
		}
		finally
		{
		}
	}

	/*package*/ void viewObject(String objectName)
	{
		try
		{
			if(objectName != null)
			{
				if(!objectName.equalsIgnoreCase(""))
				{
					StringBuffer sb = new StringBuffer();
					
					sb.append("VO * ViewFromTo Normal From ");
					sb.append(objectName);
					sb.append(" To " );
					sb.append(objectName);
					
					this.m_AgStkObjectRootClass.executeCommand(sb.toString());
				}
			}
		}
		catch( Throwable t )
		{
			t.printStackTrace();
		}
	}

	private String getHumanReadeableElemType(AgEDataPrvElementType elemType)
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

	private String getHumanReadeableDpType(AgEDataProviderType dpType)
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