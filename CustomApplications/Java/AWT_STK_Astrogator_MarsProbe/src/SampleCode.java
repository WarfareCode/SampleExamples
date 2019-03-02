//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkobjects.*;
import agi.stkobjects.astrogator.*;

public class SampleCode
{
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private IAgVADriverMCS			m_AgVADriverMCS;

	/* package */SampleCode(AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass = root;
	}
	

	/*package*/ void createScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
		this.m_AgStkObjectRootClass.newScenario("MarsProbe");

		String startTime = "1 Mar 1997 00:00:00.000";
		String stopTime = "1 Mar 1998 00:00:00.000";
		String epoch = "1 Mar 1997 00:00:00.000";
		
		IAgScenario scen = (IAgScenario)this.m_AgStkObjectRootClass.getCurrentScenario();

		scen.setStartTime(startTime);
		scen.setStopTime(stopTime);
		scen.setEpoch(epoch);

		scen.getAnimation().setAnimStepValue(3600);
		scen.getGraphics().setPlanetOrbitsVisible(true);
		scen.getGraphics().setSubPlanetPointsVisible(false);
		scen.getGraphics().setSubPlanetLabelsVisible(false);

		IAgScAnimation anim = scen.getAnimation();
		anim.setStartTime(startTime);
		
		this.m_AgStkObjectRootClass.rewind();
	}

	/*package*/ void definePlanets()
	throws AgCoreException
	{
		AgScenarioClass scen = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
		IAgStkObjectCollection children = scen.getChildren();
		
        IAgPlanet earth = (IAgPlanet)children._new(AgESTKObjectType.E_PLANET, "Planet1");
        earth.setPositionSource(AgEPlPositionSourceType.E_POS_CENTRAL_BODY);
        IAgPlPosCentralBody earthcb = (IAgPlPosCentralBody)earth.getPositionSourceData();
        earthcb.setCentralBody("Earth");
        earthcb.setEphemSource(AgEEphemSourceType.E_EPHEM_JPLDE);

        IAgPlanet mars = (IAgPlanet)children._new(AgESTKObjectType.E_PLANET, "Planet2");
        mars.setPositionSource(AgEPlPositionSourceType.E_POS_CENTRAL_BODY);
        IAgPlPosCentralBody marscb = (IAgPlPosCentralBody)mars.getPositionSourceData();
        marscb.setCentralBody("Mars");
        marscb.setEphemSource(AgEEphemSourceType.E_EPHEM_JPLDE);
	}

	/*package*/ void setupGraphics()
	throws AgCoreException
	{
		AgScenarioClass scen = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
		IAgStkObjectCollection children = scen.getChildren();

		IAgSatellite sat = (IAgSatellite)children._new(AgESTKObjectType.E_SATELLITE, "Satellite1");
        sat.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_ASTROGATOR);
        this.m_AgVADriverMCS = (IAgVADriverMCS)sat.getPropagator();
        
        sat.getGraphics().getPassData().getOrbit().setLeadDataType(AgELeadTrailData.E_DATA_ALL);

        this.m_AgStkObjectRootClass.executeCommand("VO * CentralBody Sun 1");
        this.m_AgStkObjectRootClass.executeCommand("VO * Celestial Moon Label Off WindowID 1");
        this.m_AgStkObjectRootClass.executeCommand("VO * Grids Space ShowEcliptic On WindowID 1");           
        this.m_AgStkObjectRootClass.executeCommand("Window3D * ViewVolume MaxVisibleDist 2e15 WindowID 1");
        this.m_AgStkObjectRootClass.executeCommand("VO * View Top WindowID 1");
        this.m_AgStkObjectRootClass.executeCommand("VO * View North WindowID 1");

        this.m_AgStkObjectRootClass.executeCommand("VO * CentralBody Mars 2");
        this.m_AgStkObjectRootClass.executeCommand("VO * Celestial Moon Label Off WindowID 2");
        this.m_AgStkObjectRootClass.executeCommand("VO * Grids Space ShowEcliptic On WindowID 2");           
        this.m_AgStkObjectRootClass.executeCommand("Window3D * ViewVolume MaxVisibleDist 2e15 WindowID 2");
	}

	/*package*/ void defineInitialState()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection mcsSegs = this.m_AgVADriverMCS.getMainSequence();
		mcsSegs.removeAll();

		IAgVAMCSInitialState initState = null;
		initState = (IAgVAMCSInitialState)mcsSegs.insert(AgEVASegmentType.E_VASEGMENT_TYPE_INITIAL_STATE, "InitialState1", "-");
		
        initState.setCoordSystemName("CentralBody/Sun J2000");
        initState.setElementType(AgEVAElementType.E_VAELEMENT_TYPE_KEPLERIAN);
        initState.setOrbitEpoch("1 Mar 1997 00:00:00.000");
        
        IAgVAElementKeplerian kep = (IAgVAElementKeplerian)initState.getElement();
        kep.setSemiMajorAxis(193216365.381);
        kep.setEccentricity(0.236386);
        kep.setInclination(new Double(23.455));
        kep.setRAAN(new Double(0.258));
        kep.setArgOfPeriapsis(new Double(71.347));
        kep.setTrueAnomaly(new Double(85.152));
	}

	/*package*/ void propgateInterplanetaryTrajectory()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection mcsSegs = this.m_AgVADriverMCS.getMainSequence();

		AgVAMCSPropagateClass propagate = null;
		propagate = (AgVAMCSPropagateClass)mcsSegs.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Propagate", "-");
        propagate.setPropagatorName("Heliocentric");
        
        propagate.getProperties().setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.ORANGE));
        propagate.setEnableMaxPropagationTime(false);
        
        IAgVAStoppingConditionCollection stops = propagate.getStoppingConditions();
        IAgVAStoppingConditionElement elem = stops.add("Periapsis");
        IAgVAStoppingCondition cond = (IAgVAStoppingCondition)elem.getProperties();
        cond.setCentralBodyName("Earth");
        
        stops.remove("Duration");

        this.m_AgVADriverMCS.runMCS();
	}

	/*package*/ String stopNearMars()
	throws AgCoreException
	{
        StringBuffer data = new StringBuffer();
        
		IAgVAMCSSegmentCollection mcsSegs = this.m_AgVADriverMCS.getMainSequence();
		AgVAMCSPropagateClass propagate = (AgVAMCSPropagateClass)mcsSegs.getItem("Propagate");

        IAgVAStoppingConditionCollection stops = propagate.getStoppingConditions();
        IAgVAStoppingConditionElement elem = stops.add("Periapsis");
        IAgVAStoppingCondition cond = (IAgVAStoppingCondition)elem.getProperties();
        cond.setCentralBodyName("Mars");

        this.m_AgVADriverMCS.runMCS();

        propagate.getProperties().setDisplayCoordinateSystem("CentralBody/Mars Inertial");
        IAgDrResult result = propagate.getExecSummary();

        IAgDrIntervalCollection intervals = result.getIntervals();
        for (int i = 0; i < intervals.getCount(); i++)
        {
            IAgDrInterval interval = intervals.getItem(i);
            IAgDrDataSetCollection datasets = interval.getDataSets();
            for (int j = 0; j < datasets.getCount(); j++)
            {
                IAgDrDataSet dataset = datasets.getItem(j);

                Object values = dataset.getValues_AsObject();
                if(values instanceof Object[])
                {
	                Object[] elements = (Object[])values;
	                for( int k = 0; k < elements.length; k++)
	                {
	                    data.append(elements[k] + "\r\n");
	                }
                }
            }
        }
        
        return data.toString();
	}

	/*package*/ void createMarsPointMassPropagator()
	throws AgCoreException
	{
		IAgScenario scen = (IAgScenario)this.m_AgStkObjectRootClass.getCurrentScenario();
        IAgComponentDirectory compdir = scen.getComponentDirectory();
        IAgComponentInfoCollection gatorcomps = compdir.getComponents(AgEComponent.E_COMPONENT_ASTROGATOR);
        IAgComponentInfoCollection propcomps = gatorcomps.getFolder("Propagators");
        IAgCloneable cloneable = (IAgCloneable)propcomps.getItem("Earth Point Mass");
        IAgComponentInfo clonedinfo = new AgComponentInfo(cloneable.cloneObject());
        clonedinfo.setName("Mars Point Mass");
        IAgVANumericalPropagatorWrapper numprop = null;
        numprop = new AgVANumericalPropagatorWrapper(clonedinfo);
        numprop.setCentralBodyName("Mars");
	}

	/*package*/ void targetACircularOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection mcsSegs = this.m_AgVADriverMCS.getMainSequence();

		IAgVAMCSTargetSequence ts = null;
		ts = (IAgVAMCSTargetSequence)mcsSegs.insert(AgEVASegmentType.E_VASEGMENT_TYPE_TARGET_SEQUENCE, "TargetSequence", "-");

        IAgVAMCSSegmentCollection tsSegs = ts.getSegments();
        AgVAMCSManeuverClass mcsman = null;
		mcsman = (AgVAMCSManeuverClass)tsSegs.insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "Maneuver1", "-");
        mcsman.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
        IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)mcsman.getManeuver();
        impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);

        mcsman.enableControlParameter(AgEVAControlManeuver.E_VACONTROL_MANEUVER_IMPULSIVE_CARTESIAN_X);
        mcsman.enableControlParameter(AgEVAControlManeuver.E_VACONTROL_MANEUVER_IMPULSIVE_CARTESIAN_Z);
        IAgVACalcObjectCollection cocol = mcsman.getResults();
        IAgVAStateCalcEccentricity eccentricity = null;
        eccentricity = (IAgVAStateCalcEccentricity)cocol.add("Keplerian Elems/Eccentricity");
        eccentricity.setCentralBodyName("Mars");
        IAgVAProfileCollection profcol = ts.getProfiles();
        IAgVAProfileDifferentialCorrector dc = null;
        dc = (IAgVAProfileDifferentialCorrector)profcol.getItem("Differential Corrector");
        IAgVADCControlCollection ccol = dc.getControlParameters();
        for (int i = 0; i < ccol.getCount(); i++)
        {
            ccol.getItem(i).setEnable(true);
        }
        IAgVADCResultCollection rcol = dc.getResults();
        IAgVADCResult result = rcol.getItem(0);
        result.setEnable(true);
        result.setTolerance(new Double(0.01));
        dc.setMaxIterations(50);
        dc.setMode(AgEVAProfileMode.E_VAPROFILE_MODE_ITERATE);
        ts.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);
	}

	/*package*/ void propagateOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection mcsSegs = this.m_AgVADriverMCS.getMainSequence();

		IAgVAMCSPropagate prop2 = null;
		prop2 = (IAgVAMCSPropagate)mcsSegs.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Propagate2", "-");
        
		IAgVAStoppingCondition periapsis = null;
		periapsis = (IAgVAStoppingCondition)prop2.getStoppingConditions().add("Periapsis").getProperties();
        prop2.getStoppingConditions().remove("Duration");
        periapsis.setCentralBodyName("Mars");
        periapsis.setRepeatCount(2);
        prop2.setPropagatorName("Mars Point Mass");
        this.m_AgVADriverMCS.runMCS();
	}

	public void closeScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
	}	
}