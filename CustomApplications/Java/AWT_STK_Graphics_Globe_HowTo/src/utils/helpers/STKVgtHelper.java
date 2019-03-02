package utils.helpers;

// Java API
import java.text.*;

// AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkobjects.*;

public class STKVgtHelper
{
	static int	_counter	= 0;

	public static String getTransientName(int type)
	{
		return MessageFormat.format("{0}_{1}", new Object[]{ new Integer(type).toString(), new Integer(_counter++)});
	}

	public static IAgCrdnPoint createPoint(IAgCrdnProvider provider, AgECrdnPointType type)
	throws AgCoreException
	{
		String transientName = getTransientName(STKVgtTypes.POINT);
		return provider.getPoints().getFactory().create(transientName, "", type);
	}

	public static IAgCrdnAxes createAxes(IAgCrdnProvider provider, AgECrdnAxesType type)
	throws AgCoreException
	{
		String transientName = getTransientName(STKVgtTypes.AXES);
		return provider.getAxes().getFactory().create(transientName, "", type);
	}

	public static IAgCrdnSystem createSystem(IAgCrdnProvider provider, AgECrdnSystemType type)
	throws AgCoreException
	{
		String transientName = getTransientName(STKVgtTypes.SYSTEM);
		return provider.getSystems().getFactory().create(transientName, "", type);
	}

	public static IAgCrdnAxesFixed createAxes(IAgStkObjectRoot root, String centralBodyName, IAgPosition pos)
	throws AgCoreException
	{
		IAgCrdnProvider provider = root.getCentralBodies().getItem(centralBodyName).getVgt();
		IAgCrdnPointFixedInSystem fixed = (IAgCrdnPointFixedInSystem)STKVgtHelper.createPoint(provider, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);

		Object[] xyz = (Object[])pos.queryCartesianArray_AsObject();
		fixed.getFixedPoint().assignCartesian(((Double)xyz[0]).doubleValue(), ((Double)xyz[1]).doubleValue(), ((Double)xyz[2]).doubleValue());
		fixed.getReference().setSystem(provider.getWellKnownSystems().getEarth().getFixed());

		// Create a topocentric axes
		IAgCrdnAxesOnSurface axes = (IAgCrdnAxesOnSurface)STKVgtHelper.createAxes(provider, AgECrdnAxesType.E_CRDN_AXES_TYPE_ON_SURFACE);
		axes.getReferencePoint().setPoint((IAgCrdnPoint)fixed);
		axes.getCentralBody().setPath(centralBodyName);

		IAgCrdnAxesFixed eastNorthUp = (IAgCrdnAxesFixed)STKVgtHelper.createAxes(provider, AgECrdnAxesType.E_CRDN_AXES_TYPE_FIXED);
		eastNorthUp.getReferenceAxes().setAxes((IAgCrdnAxes)axes);
		eastNorthUp.getFixedOrientation().assignEulerAngles(AgEEulerOrientationSequence.E321, new Double(90), new Integer(0), new Integer(0));

		return eastNorthUp;
	}

	public static IAgCrdnSystem createSystem(IAgStkObjectRoot root, String centralBodyName, IAgPosition pos, IAgCrdnAxesFixed axes)
	throws AgCoreException
	{
		IAgCrdnProvider provider = root.getCentralBodies().getItem(centralBodyName).getVgt();

		IAgCrdnPointFixedInSystem point = (IAgCrdnPointFixedInSystem)STKVgtHelper.createPoint(provider, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);
		Object[] xyz = (Object[])pos.queryCartesianArray_AsObject();
		point.getFixedPoint().assignCartesian(((Double)xyz[0]).doubleValue(), ((Double)xyz[1]).doubleValue(), ((Double)xyz[2]).doubleValue());
		point.getReference().setSystem(provider.getSystems().getItem("Fixed"));

		IAgCrdnSystemAssembled system = (IAgCrdnSystemAssembled)STKVgtHelper.createSystem(provider, AgECrdnSystemType.E_CRDN_SYSTEM_TYPE_ASSEMBLED);
		system.getOriginPoint().setPoint((IAgCrdnPoint)point);
		system.getReferenceAxes().setAxes((IAgCrdnAxes)axes);
		return (IAgCrdnSystem)system;
	}
}