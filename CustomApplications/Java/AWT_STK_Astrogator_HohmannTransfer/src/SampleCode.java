//AGI Java API
import agi.core.*;
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
		this.m_AgStkObjectRootClass.newScenario("HohmannTransfer");

		this.m_AgScenarioClass = (AgScenarioClass)this.m_AgStkObjectRootClass.getCurrentScenario();
	}
	
	/*package*/ void step2_createSatellite()
	throws AgCoreException
	{
		IAgStkObjectCollection children = this.m_AgScenarioClass.getChildren();
		this.m_IAgSatellite = (IAgSatellite)children._new(AgESTKObjectType.E_SATELLITE, "Satellite1");
		this.m_IAgSatellite.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_ASTROGATOR);
        this.m_IAgVADriverMCS = (IAgVADriverMCS)this.m_IAgSatellite.getPropagator();
        this.m_IAgVADriverMCS.getMainSequence().removeAll();
	}

	/*package*/ void step3_defineInitialState()
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

	/*package*/ void step4_propagateTheParkingOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
        IAgVAMCSPropagate propagate = (IAgVAMCSPropagate)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Propagate", "-");
        propagate.setPropagatorName("Earth Point Mass");
        IAgVAStoppingConditionCollection scc = propagate.getStoppingConditions();
        IAgVAStoppingConditionElement scelem = scc.getItem("Duration");
        IAgVAStoppingCondition sc = (IAgVAStoppingCondition)scelem.getProperties();
        sc.setTrip(new Double(7200));
	}	

	/*package*/ void step5_maneuverIntoTheTransferEllipse()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		IAgVAMCSManeuver maneuver = (IAgVAMCSManeuver)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "DV1", "-");
        maneuver.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
        IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)maneuver.getManeuver();
        impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);
        IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.getAttitudeControl();
        thrustVector.getDeltaVVector().assignCartesian(2.421, 0, 0);
        impulsive.setUpdateMass(true);
	}	

	/*package*/ void step6_propagateTheTransferOrbitToApogee()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		IAgVAMCSPropagate propagate = (IAgVAMCSPropagate)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Transfer Ellipse", "-");
        propagate.setPropagatorName("Earth Point Mass");
        IAgVAStoppingConditionCollection scc = propagate.getStoppingConditions();
        scc.add("Apoapsis");
        scc.remove("Duration");
	}	

	/*package*/ void step7_maneuverIntoTheOuterOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		IAgVAMCSManeuver maneuver = (IAgVAMCSManeuver)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_MANEUVER, "DV2", "-");
        maneuver.setManeuverType(AgEVAManeuverType.E_VAMANEUVER_TYPE_IMPULSIVE);
        IAgVAManeuverImpulsive impulsive = (IAgVAManeuverImpulsive)maneuver.getManeuver();
        impulsive.setAttitudeControlType(AgEVAAttitudeControl.E_VAATTITUDE_CONTROL_THRUST_VECTOR);
        IAgVAAttitudeControlImpulsiveThrustVector thrustVector = (IAgVAAttitudeControlImpulsiveThrustVector)impulsive.getAttitudeControl();
        thrustVector.getDeltaVVector().assignCartesian(1.465, 0, 0);
        impulsive.setUpdateMass(true);
	}	

	/*package*/ void step8_propagateTheOuterOrbit()
	throws AgCoreException
	{
		IAgVAMCSSegmentCollection ms = this.m_IAgVADriverMCS.getMainSequence();
		IAgVAMCSPropagate propagate = (IAgVAMCSPropagate)ms.insert(AgEVASegmentType.E_VASEGMENT_TYPE_PROPAGATE, "Outer Orbit", "-");
        propagate.setPropagatorName("Earth Point Mass");
        IAgVAStoppingConditionCollection scc = propagate.getStoppingConditions();
        IAgVAStoppingConditionElement scelem = scc.getItem("Duration");
        IAgVAStoppingCondition sc = (IAgVAStoppingCondition)scelem.getProperties();
        sc.setTrip(new Double(86400));
	}	

	/*package*/ void step9_runMCS()
	throws AgCoreException
	{
		this.m_IAgVADriverMCS.runMCS();
	}	

	/*package*/ String step10_getResults()
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

	/*package*/ void step11_closeScenario()
	throws AgCoreException
	{
		this.m_AgStkObjectRootClass.closeScenario();
	}	
}