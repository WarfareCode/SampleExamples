#pragma once

class CStkObject
{
public:
	CStkObject(IAgStkObjectPtr object);
	~CStkObject(void);

	IAgDrResultPtr Position(double animTime, SAFEARRAY *llaNames);
	IAgDrResultPtr Acceleration(double animTime, SAFEARRAY *xyzNames);
	IAgDrResultPtr Velocity(double animTime, SAFEARRAY *xyzNames);
	IAgDrResultPtr Attitude(double animTime, SAFEARRAY *yprNames);

private:
	IAgStkObjectPtr m_object;
	IAgDataPrvTimeVarPtr m_position,
		                 m_acceleration,
						 m_velocity,
						 m_attitude;
	_bstr_t m_isDIS;
	_bstr_t m_entityID;
	_bstr_t m_entityType;
};
