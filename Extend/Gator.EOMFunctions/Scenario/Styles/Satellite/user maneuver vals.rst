stk.v.9.0
WrittenBy    STK_v9.1.0

BEGIN ReportStyle

BEGIN ClassId
	Class		Satellite
END ClassId

BEGIN Header
	StyleType		0
	Date		Yes
	Name		Yes
	DescShort		No
	DescLong		No
	YLog10		No
	Y2Log10		No
	VerticalGridLines		No
	HorizontalGridLines		No
	AnnotationType		Spaced
	NumAnnotations		3
	NumAngularAnnotations		5
	AnnotationRotation		1
	BackgroundColor		#ffffff
	ForegroundColor		#000000
	ViewableDuration		3600.000000
	RealTimeMode		No
	DayLinesStatus		1
	LegendStatus		1
	LegendLocation		1

BEGIN PostProcessor
	Destination	0
	Use	0
	Destination	1
	Use	0
	Destination	2
	Use	0
	Destination	3
	Use	0
END PostProcessor
	NumSections		1
END Header

BEGIN Section
	Name		Section 1
	ClassName		Satellite
	NameInTitle		No
	ExpandMethod		0
	PropMask		2
	ShowIntervals		No
	NumIntervals		0
	NumLines		1

BEGIN Line
	Name		Line 1
	NumElements		5

BEGIN Element
	Name		Time
	IsIndepVar		Yes
	IndepVarName		Time
	Title		Time
	NameInTitle		No
	Service		Astrogator
	Type		UserValues
	Element		Time
	SumAllowedMask		23
	SummaryOnly		No
	DataType		0
	UnitType		2
	LineStyle		0
	LineWidth		0
	LineColor		#000000
	PointStyle		0
	PointSize		0
	PointColor		#000000
	FillPattern		0
	FillColor		#000000
	PropMask		0
	UseScenUnits		Yes
END Element

BEGIN Element
	Name		Astrogator Values-UserValues-EffectiveImpulse
	IsIndepVar		No
	IndepVarName		Time
	Title		EffectiveImpulse
	NameInTitle		Yes
	Service		Astrogator
	Type		UserValues
	Element		EffectiveImpulse
	SumAllowedMask		23
	SummaryOnly		No
	DataType		0
	UnitType		6
	LineStyle		0
	LineWidth		0
	LineColor		#000000
	PointStyle		0
	PointSize		0
	PointColor		#000000
	FillPattern		0
	FillColor		#000000
	PropMask		0
	UseScenUnits		Yes
END Element

BEGIN Element
	Name		Astrogator Values-UserValues-IntegratedDeltaVx
	IsIndepVar		No
	IndepVarName		Time
	Title		IntegratedDeltaVx
	NameInTitle		Yes
	Service		Astrogator
	Type		UserValues
	Element		IntegratedDeltaVx
	SumAllowedMask		23
	SummaryOnly		No
	DataType		0
	UnitType		6
	LineStyle		0
	LineWidth		0
	LineColor		#000000
	PointStyle		0
	PointSize		0
	PointColor		#000000
	FillPattern		0
	FillColor		#000000
	PropMask		0
	UseScenUnits		Yes
END Element

BEGIN Element
	Name		Astrogator Values-UserValues-IntegratedDeltaVy
	IsIndepVar		No
	IndepVarName		Time
	Title		IntegratedDeltaVy
	NameInTitle		Yes
	Service		Astrogator
	Type		UserValues
	Element		IntegratedDeltaVy
	SumAllowedMask		23
	SummaryOnly		No
	DataType		0
	UnitType		6
	LineStyle		0
	LineWidth		0
	LineColor		#000000
	PointStyle		0
	PointSize		0
	PointColor		#000000
	FillPattern		0
	FillColor		#000000
	PropMask		0
	UseScenUnits		Yes
END Element

BEGIN Element
	Name		Astrogator Values-UserValues-IntegratedDeltaVz
	IsIndepVar		No
	IndepVarName		Time
	Title		IntegratedDeltaVz
	NameInTitle		Yes
	Service		Astrogator
	Type		UserValues
	Element		IntegratedDeltaVz
	SumAllowedMask		23
	SummaryOnly		No
	DataType		0
	UnitType		6
	LineStyle		0
	LineWidth		0
	LineColor		#000000
	PointStyle		0
	PointSize		0
	PointColor		#000000
	FillPattern		0
	FillColor		#000000
	PropMask		0
	UseScenUnits		Yes
END Element
END Line
END Section

BEGIN LineAnnotations
END LineAnnotations
END ReportStyle

