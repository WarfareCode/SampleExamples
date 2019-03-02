package utils.helpers;

// AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkvgt.*;
import agi.stkgraphics.*;
import agi.stkobjects.*;

public class ViewHelper
{
	/**
	 * Changes the view of a scene such that the camera's field of view encompasses the specified bounding sphere.
	 * @param scene
	 * @param root
	 * @param centralBody
	 * @param sphere
	 * @throws AgCoreException 
	 */
	public static void viewBoundingSphere
	(IAgStkObjectRoot root, IAgStkGraphicsScene scene, String centralBody, IAgStkGraphicsBoundingSphere sphere) 
	throws AgCoreException
	{
		viewBoundingSphere(root, scene, centralBody, sphere, -90, 30);
	}

	/**
	 * 
	 * @param scene
	 * @param root
	 * @param centralBody
	 * @param sphere
	 * @param azimuthAngle
	 * @param elevationAngle
	 * @throws AgCoreException
	 */
	public static void viewBoundingSphere
	(IAgStkObjectRoot root, IAgStkGraphicsScene scene, String centralBody, IAgStkGraphicsBoundingSphere sphere, double azimuthAngle, double elevationAngle) 
    throws AgCoreException
    {
		IAgCrdnProvider provider = root.getCentralBodies().getItem(centralBody).getVgt();
        IAgCrdnPoint referencePoint = STKVgtHelper.createPoint(provider, AgECrdnPointType.E_CRDN_POINT_TYPE_FIXED_IN_SYSTEM);

        Object[] centerArray = (Object[])sphere.getCenter_AsObject();
        IAgCrdnPointFixedInSystem system = (IAgCrdnPointFixedInSystem)referencePoint;
        
        IAgPosition pos = system.getFixedPoint();
        double x = ((Double)centerArray[0]).doubleValue();
        double y = ((Double)centerArray[1]).doubleValue();
        double z = ((Double)centerArray[2]).doubleValue();
        pos.assignCartesian(x, y, z);

        IAgCrdnSystem fixedSystem = root.getVgtRoot().getWellKnownSystems().getEarth().getFixed();
        IAgCrdnSystemRefTo ref = system.getReference();
        ref.setSystem(fixedSystem);

        IAgPosition boundingSphereCenter = root.getConversionUtility().newPositionOnEarth();
        boundingSphereCenter.assignCartesian(x,y,z);

        IAgCrdnAxes boundingSphereAxes = (IAgCrdnAxes)STKVgtHelper.createAxes(root, centralBody, boundingSphereCenter);

        double r = scene.getCamera().getDistancePerRadius() * sphere.getRadius();

        String displayUnit = root.getUnitPreferences().getCurrentUnitAbbrv("AngleUnit");
        String internalUnit = "rad";
        double elevationAngleInRad = root.getConversionUtility().convertQuantity("AngleUnit", displayUnit, internalUnit, elevationAngle);
        double azimuthAngleInRad = root.getConversionUtility().convertQuantity("AngleUnit", displayUnit, internalUnit, azimuthAngle);

        double phi = elevationAngleInRad;
        double theta = azimuthAngleInRad;

        Object[] offset = new Object[] { new Double(r * Math.cos(phi) * Math.cos(theta)), new Double(r * Math.cos(phi) * Math.sin(theta)), new Double(r * Math.sin(phi)) };

        scene.getCamera().viewOffset(boundingSphereAxes, referencePoint, offset);
    }

	/**
	 * Change the view of a scene such that the camera's field of view encompasses the specified extent.
	 * @param scene
	 * @param root
	 * @param centralBody
	 * @param extent - Extent as an Array of doubles in the order west, south, east, north.
	 * @param azimuthAngle
	 * @param elevationAngle
	 * @throws AgCoreException 
	 */
	public static void viewExtent(IAgStkObjectRoot root, IAgStkGraphicsScene scene, String centralBody, Object[] extent, double azimuthAngle, double elevationAngle) 
	throws AgCoreException
	{
		double west, south, east, north;
		west = ((Double)extent[0]).doubleValue();
		south = ((Double)extent[1]).doubleValue();
		east = ((Double)extent[2]).doubleValue();
		north = ((Double)extent[3]).doubleValue();

		viewExtent(root, scene, centralBody, west, south, east, north, azimuthAngle, elevationAngle);
	}

	public static void viewExtent(IAgStkObjectRoot root, IAgStkGraphicsScene scene, String centralBody, double west, double south, double east, double north, double azimuthAngle, double elevationAngle) 
	throws AgCoreException
	{
		scene.getCamera().viewRectangularExtent(centralBody, west, south, east, north);

		IAgCartesian3Vector offset = root.getConversionUtility().newCartesian3Vector();
		double r = scene.getCamera().getDistance();

        String displayUnit = root.getUnitPreferences().getCurrentUnitAbbrv("AngleUnit");
        String internalUnit = "rad";
        double elevationAngleInRad = root.getConversionUtility().convertQuantity("AngleUnit", displayUnit, internalUnit, elevationAngle);
        double azimuthAngleInRad = root.getConversionUtility().convertQuantity("AngleUnit", displayUnit, internalUnit, azimuthAngle);

        double phi = elevationAngleInRad;
        double theta = azimuthAngleInRad;
		
		offset.set(r * Math.cos(phi) * Math.cos(theta), r * Math.cos(phi) * Math.sin(theta), r * Math.sin(phi));

		Object[] refPts = (Object[])scene.getCamera().getReferencePoint_AsObject();
		double pt0 = ((Double)refPts[0]).doubleValue() + offset.getX();
		double pt1 = ((Double)refPts[1]).doubleValue() + offset.getY();
		double pt2 = ((Double)refPts[2]).doubleValue() + offset.getZ();
		Object[] newCameraPosition = new Object[] { new Double(pt0), new Double(pt1), new Double(pt2) };

		scene.getCamera().setPosition(newCameraPosition);
	}
}