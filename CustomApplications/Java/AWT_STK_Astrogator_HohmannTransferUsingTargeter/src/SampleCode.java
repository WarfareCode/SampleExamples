//Java API
import java.awt.*;

//AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.stkutil.*;
import agi.stkobjects.*;
import agi.stkobjects.astrogator.*;

public class SampleCode
{
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgScenarioClass			m_AgScenarioClass;
	private IAgSatellite			m_IAgSatellite;
	private IAgVADriverMCS			m_IAgVADriverMCS;

	/* package */SampleCode(AgStkObjectRootClass root)
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass = root;
	}

	/*package*/ void step1_createScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
		this.m_AgStkObjectRootClass.newScenario("HohmannTransferUsingTargeter");
		this.m_AgScenarioClass = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();

		IAgStkObjectCollection children = this.m_AgScenarioClass.getChildren();
		this.m_IAgSatellite = (IAgSatellite)children._new(AgESTKObjectType.E_SATELLITE, "Satellite1");
		
		this.m_IAgSatellite.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_ASTROGATOR);
        this.m_IAgVADriverMCS = (IAgVADriverMCS)this.m_IAgSatellite.getPropagator();
        this.m_IAgVADriverMCS.getMainSequence().removeAll();
        this.m_IAgVADriverMCS.getOptions().setDrawTrajectoryIn3D(true);
	}
	
	/*package*/ void step2_defineInitialState()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();

		IAgVAMCSInitialState initialState = (IAgVAMCSInitialState)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_INITIAL_STATE, "Inner Orbit", "-");
        initialState.setElementType(AgEVAElementType.E_VAELEMENT_TYPE_KEPLERIAN);
        
        IAgVAElementKeplerian modKep = (IAgVAElementKeplerian)initialState.getElement();
        modKep.setPeriapsisRadiusSize(6700);
        modKep.setArgOfPeriapsis(new Double(0));
        modKep.setEccentricity(0);
        modKep.setInclination(new Double(0));
        modKep.setRAAN(new Double(0));
        modKep.setTrueAnomaly(new Double(0));
	}	

	/*package*/ void step3_propagateTheParkingOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();

		IAgVAMCSPropagate propagate = (IAgVAMCSPropagate)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Propagate", "-");
        propagate.setPropagatorName("Earth Point Mass");
        ((IAgVAMCSSegment)propagate).getProperties().setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.BLUE));
        
        IAgVAStoppingConditionCollection scc = propagate.getStoppingConditions();
        IAgVAStoppingConditionElement scelem = scc.getItem("Duration");
        IAgVAStoppingCondition sc = (IAgVAStoppingCondition)scelem.getProperties();
        sc.setTrip(new Double(7200));
	}	

	/*package*/ void step4_TargetSequenceContainingManeuverIntoTheTransferEllipse()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		IAgVAMCSTargetSequence ts = (IAgVAMCSTargetSequence)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_TARGET_SEQUENCE, "Start Transfer", "-");
        ts.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);

		IAgVAMCSSegmentCollection segments = ts.getSegments();
		IAgVAMCSManeuver dv1 = (IAgVAMCSManeuver)segments.insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "DV1", "-");
        dv1.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
        IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)dv1.getManeuver();
        
        impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);
        IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.getAttitudeControl();
        thrustVector.setThrustAxesName("Satellite/Satellite1 VNC(Earth)");

        dv1.enableControlParameter(AgEVAControlManeuver.E_VACONTROL_MANEUVER_IMPULSIVE_CARTESIAN_X);
        ((IAgVAMCSSegment)dv1).getResults().add("Keplerian Elems/Radius of Apoapsis");

        IAgVAProfileCollection profiles = ts.getProfiles();

        IAgVAProfileDifferentialCorrector dc = (IAgVAProfileDifferentialCorrector)profiles.getItem("Differential Corrector");
        dc.setMaxIterations(50);
        dc.setEnableDisplayStatus(true);
        dc.setMode(AgEVAProfileMode.E_VAPROFILE_MODE_ITERATE);

        IAgVADCControlCollection controls = dc.getControlParameters();
        IAgVADCControl xControlParam = controls.getControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X");
        xControlParam.setEnable(true);
        xControlParam.setMaxStep(new Double(0.3));
        
        IAgVADCResultCollection results = dc.getResults();
        IAgVADCResult roaResult = results.getResultByPaths("DV1", "Radius Of Apoapsis");
        roaResult.setEnable(true);
        roaResult.setDesiredValue(new Double(42238));
        roaResult.setTolerance(new Double(0.1));
	}	

	/*package*/ void step5_propagateTheTransferOrbitToApogee()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();

		IAgVAMCSPropagate propagate = (IAgVAMCSPropagate)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Transfer Ellipse", "-");
        propagate.setPropagatorName("Earth Point Mass");
        ((IAgVAMCSSegment)propagate).getProperties().setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.RED));

        IAgVAStoppingConditionCollection scc = propagate.getStoppingConditions();
        scc.add("Apoapsis");
        scc.remove("Duration");
	}	

	/*package*/ void step6_TargetSequenceContainerManeuverIntoTheOuterOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		
        IAgVAMCSTargetSequence ts = (IAgVAMCSTargetSequence)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_TARGET_SEQUENCE, "Finish Transfer", "-");
        ts.setAction(AgEVATargetSeqAction.E_VATARGET_SEQ_ACTION_RUN_ACTIVE_PROFILES);
		IAgVAMCSSegmentCollection segments = ts.getSegments();

		IAgVAMCSManeuver dv2 = (IAgVAMCSManeuver)segments.insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "DV2", "-");
        dv2.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
        IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)dv2.getManeuver();

        impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);
        IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.getAttitudeControl();
        thrustVector.setThrustAxesName("Satellite/Satellite1 VNC(Earth)");

        dv2.enableControlParameter(AgEVAControlManeuver.E_VACONTROL_MANEUVER_IMPULSIVE_CARTESIAN_X);

        ((IAgVAMCSSegment)dv2).getResults().add("Keplerian Elems/Eccentricity");

        IAgVAProfileCollection profiles = ts.getProfiles();

        IAgVAProfileDifferentialCorrector dc = (IAgVAProfileDifferentialCorrector)profiles.getItem("Differential Corrector");
        dc.setEnableDisplayStatus(true);
        dc.setMode(AgEVAProfileMode.E_VAPROFILE_MODE_ITERATE);
        
        IAgVADCControlCollection controls = dc.getControlParameters();
        IAgVADCControl xControlParam = controls.getControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X");
        xControlParam.setEnable(true);
        xControlParam.setMaxStep(new Double(0.3));

        IAgVADCResultCollection results = dc.getResults();
        IAgVADCResult eccResult = results.getResultByPaths("DV2", "Eccentricity");
        eccResult.setEnable(true);
        eccResult.setDesiredValue(new Double(0));
	}	

	/*package*/ void step7_propagateTheOuterOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		
		IAgVAMCSPropagate propagate = (IAgVAMCSPropagate)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Outer Orbit", "-");
        propagate.setPropagatorName("Earth Point Mass");
        ((IAgVAMCSSegment)propagate).getProperties().setColor(AgAwtColorTranslator.fromAWTtoCoreColor(Color.GREEN));

        IAgVAStoppingConditionCollection scc = propagate.getStoppingConditions();
        IAgVAStoppingConditionElement scelem = scc.getItem("Duration");
        IAgVAStoppingCondition sc = (IAgVAStoppingCondition)scelem.getProperties();
        sc.setTrip(new Double(86400));
	}	

	/*package*/ String step8_runMCS()
	throws AgCoreException
	{
		this.m_IAgVADriverMCS.runMCS();

		StringBuffer sb = new StringBuffer();

		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
        
		IAgVAMCSTargetSequence startTransfer = (IAgVAMCSTargetSequence)ms.getItem("Start Transfer");
        IAgVAProfileCollection startprofiles = startTransfer.getProfiles();
        IAgVAProfileDifferentialCorrector startDC = (IAgVAProfileDifferentialCorrector)startprofiles.getItem("Differential Corrector");
        IAgVADCControlCollection sdccc = startDC.getControlParameters();
        IAgVADCControl scontrol = sdccc.getControlByPaths("DV1", "ImpulsiveMnvr.Cartesian.X");
        sb.append(scontrol.getFinalValue_AsObject()+"\r\n");
        IAgVAMCSSegmentCollection startSegments = startTransfer.getSegments();
        IAgVAMCSManeuver dv1 = (IAgVAMCSManeuver)startSegments.getItem("DV1");
        IAgVAManeuverImpulsive dv1Impulsive = (IAgVAManeuverImpulsive)dv1.getManeuver();
        IAgVAAttitudeControlImpulsiveThrustVector dv1ThrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)dv1Impulsive.getAttitudeControl();
        IAgCartesian cartesian = (IAgCartesian)dv1ThrustVector.getDeltaVVector().convertTo(AgEPositionType.E_CARTESIAN);
        sb.append(cartesian.getX()+"\r\n");
        startTransfer.applyProfiles();
        cartesian = (IAgCartesian)dv1ThrustVector.getDeltaVVector().convertTo(AgEPositionType.E_CARTESIAN);
        sb.append(cartesian.getX()+"\r\n");

        IAgVAMCSTargetSequence finishTransfer = (IAgVAMCSTargetSequence)ms.getItem("Finish Transfer");
        IAgVAProfileCollection finishprofiles = finishTransfer.getProfiles();
        IAgVAProfileDifferentialCorrector finishDC = (IAgVAProfileDifferentialCorrector)finishprofiles.getItem("Differential Corrector");
        IAgVADCControlCollection fdccc = finishDC.getControlParameters();
        IAgVADCControl fcontrol = fdccc.getControlByPaths("DV2", "ImpulsiveMnvr.Cartesian.X");
        sb.append(fcontrol.getFinalValue_AsObject()+"\r\n");
        IAgVAMCSSegmentCollection finishSegments = finishTransfer.getSegments();

        IAgVAMCSManeuver dv2 = (IAgVAMCSManeuver)finishSegments.getItem("DV2");
        IAgVAManeuverImpulsive dv2Impulsive = (IAgVAManeuverImpulsive)dv2.getManeuver();
        IAgVAAttitudeControlImpulsiveThrustVector dv2ThrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)dv2Impulsive.getAttitudeControl();
        cartesian = (IAgCartesian)dv2ThrustVector.getDeltaVVector().convertTo(AgEPositionType.E_CARTESIAN);
        sb.append(cartesian.getX()+"\r\n");
        finishTransfer.applyProfiles();
        
        cartesian = (IAgCartesian)dv2ThrustVector.getDeltaVVector().convertTo(AgEPositionType.E_CARTESIAN);
        sb.append(cartesian.getX()+"\r\n");
        
        return sb.toString();
	}

	/*package*/ String step9_getResults()
	throws AgCoreException
	{
        StringBuffer reportData = new StringBuffer();
        IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
        IAgVAMCSSegment segment = ms.getItem("Outer Orbit");
        IAgVAMCSPropagate outerOrbit = (IAgVAMCSPropagate)segment;
        IAgDrResult result = ((IAgVAMCSSegment)outerOrbit).getExecSummary();
        if(result.getCategory_AsObject() == AgEDrCategories.E_DR_CAT_INTERVAL_LIST)
        {
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
		                for(int k=0; k < elements.length; k++)
		                {
		                    reportData.append(elements[k]);
		                    reportData.append("\r\n");
		                }
	                }
	            }
	        }
        }
        else
        {
        	reportData.append("Not implemented for AgEDrCatgories."+result.getCategory());
        }
        return reportData.toString();
	}	

	/*package*/ void step10_closeScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
	}	
}