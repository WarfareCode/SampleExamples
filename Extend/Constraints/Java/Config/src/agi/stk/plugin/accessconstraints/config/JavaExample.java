package agi.stk.plugin.accessconstraints.config;

import java.util.logging.*;
import java.awt.*;

import agi.core.*;
import agi.stk.attr.*;
import agi.stk.plugin.util.*;
import agi.stk.plugin.accessconstraints.*;
import agi.stk.plugin.accessconstraints.common.*;

public class JavaExample 
extends JavaPropertiesExample
implements IJavaExample, 
		   IAgAccessConstraintPlugin,
		   IAgUtPluginConfig
{
	/**
	* The logger name for all log calls in this class.
	*/
	private static final String s_LoggerName = JavaExample.class.getName();

	/**
	* The Logger for this class type.
	*/
	private static final Logger s_Logger = Logger.getLogger( s_LoggerName );
	
	private String			 m_DisplayName;
	private IAgUtPluginSite  m_IAgUtPluginSite;
	private IAgDispatch		 m_ConfigScope;

	//Boolean
	private Boolean m_BooleanReadOnly;
	private Boolean m_BooleanReadWrite;
	private Boolean m_BooleanHidden;
	private Boolean m_BooleanTransient;

	//Integer
	private Integer m_IntegerReadOnly;
	private Integer m_IntegerReadWrite;
	private Integer m_IntegerHidden;
	private Integer m_IntegerTransient;
	private Integer m_IntegerMin;
	private Integer m_IntegerMinMax;
	
	//Long
	private Long m_LongReadOnly;
	private Long m_LongReadWrite;
	private Long m_LongHidden;
	private Long m_LongTransient;

	//Double
	private Double m_DoubleReadOnly;
	private Double m_DoubleReadWrite;
	private Double m_DoubleHidden;
	private Double m_DoubleTransient;
	private Double m_DoubleMin;
	private Double m_DoubleMinMax;

	//Quantity
	private Double m_QuantityReadOnly;
	private Double m_QuantityReadWrite;
	private Double m_QuantityHidden;
	private Double m_QuantityTransient;

	//String
	private String m_StringReadOnly;
	private String m_StringReadWrite;
	private String m_StringHidden;
	private String m_StringTransient;
	private String m_StringMultiLine;
	
	//Choices
	private AgVariant[] m_StringColors;
	private Color m_ChoiceColor;

	private AgVariant[] m_StringPets;
	private String m_ChoicePet;

	//File/Dir
	private String m_Dir;
	
	public JavaExample()
	throws AgCoreException
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "<init>", "ENTER" );

		this.m_DisplayName = "Java.Config";
		
		this.m_BooleanReadOnly 	= Boolean.TRUE;
		this.m_BooleanReadWrite	= Boolean.FALSE;
		this.m_BooleanHidden 	= Boolean.TRUE;
		this.m_BooleanTransient = Boolean.FALSE;

		this.m_IntegerReadOnly 	= new Integer(1234);
		this.m_IntegerReadWrite = new Integer(5678);
		this.m_IntegerHidden 	= new Integer(987654321);
		this.m_IntegerTransient = new Integer(1234567890);
		this.m_IntegerMin 		= new Integer(20);
		this.m_IntegerMinMax 	= new Integer(15);
		
		this.m_LongReadOnly 	= new Long(1234);
		this.m_LongReadWrite 	= new Long(5678);
		this.m_LongHidden 		= new Long(987654321);
		this.m_LongTransient 	= new Long(1234567890);

		this.m_DoubleReadOnly 	= new Double(0.1234);
		this.m_DoubleReadWrite 	= new Double(5.6789);
		this.m_DoubleHidden 	= new Double(1.234567890);
		this.m_DoubleTransient 	= new Double(9.876543210);
		this.m_DoubleMin		= new Double(Math.PI * 3);
		this.m_DoubleMinMax 	= new Double(Math.PI * 1.5);
		
		this.m_QuantityReadOnly 	= new Double(123.45);
		this.m_QuantityReadWrite 	= new Double(567.89);
		this.m_QuantityHidden 		= new Double(1.234567890);
		this.m_QuantityTransient 	= new Double(9.876543210);
		
		this.m_StringReadOnly 	= "You can't change me! I'm read only.";
		this.m_StringReadWrite 	= "Change me please! I'm readable and writable.";
		this.m_StringHidden 	= "I'm hidden, use me to save information that you don't want the user to change.";
		this.m_StringTransient 	= "I'm transient, you can only change information in me during runtime, but I won't save the information and won't remember during the next reload of the scenario.";
		this.m_StringMultiLine 	= "Hi, I'm a line!\n And I'm a new line!\nI'm also a new line!";
		
		this.m_StringColors = new AgVariant[]
           	{new AgVariant("Red"), 
        		new AgVariant("Orange"), 
		        	new AgVariant("Yellow"), 
		               	new AgVariant("Green"), 
		               		new AgVariant("Blue"), 
		               			new AgVariant("No Color")};
		this.m_ChoiceColor = Color.RED;
		
		this.m_StringPets = new AgVariant[]
		    {new AgVariant("Dog"),
				new AgVariant("Cat"),
		        	new AgVariant("Bird"),
		             	new AgVariant("Goldfish"),
		               		new AgVariant("Hamster"),
		               			new AgVariant("No pet")};

		this.m_ChoicePet = this.m_StringPets[0].getString();

		this.m_Dir 		= System.getProperty("java.io.tmpdir");
		
		s_Logger.logp(Level.FINEST, s_LoggerName, "<init>", "EXIT" );
	}

	//===================================
	//  IJavaExample
	//===================================
	//Boolean
	public Object getBooleanReadOnly()
	{		
		s_Logger.logp(Level.FINEST, s_LoggerName, "getBooleanReadOnly", "m_BooleanReadOnly={0}", this.m_BooleanReadOnly );
		return this.m_BooleanReadOnly;
	}
	public void setBooleanReadOnly(Object value)
	{		
		this.m_BooleanReadOnly = (Boolean)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setBooleanReadOnly", "m_BooleanReadOnly={0}", this.m_BooleanReadOnly );
	}
	public Object getBooleanReadWrite()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getBooleanReadWrite", "m_BooleanReadWrite={0}", this.m_BooleanReadWrite);
		return this.m_BooleanReadWrite;
	}
	public void setBooleanReadWrite(Object value)
	{
		this.m_BooleanReadWrite = (Boolean)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setBooleanReadWrite", "m_BooleanReadWrite={0}", this.m_BooleanReadWrite);
	}
	public Object getBooleanHidden()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getBooleanHidden", "m_BooleanHidden={0}", this.m_BooleanHidden);
		return this.m_BooleanHidden;
	}
	public void setBooleanHidden(Object value)
	{
		this.m_BooleanHidden = (Boolean)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setBooleanHidden", "m_BooleanHidden={0}", this.m_BooleanHidden);
	}
	public Object getBooleanTransient()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getBooleanTransient", "m_BooleanTransient={0}", this.m_BooleanTransient);
		return this.m_BooleanTransient;
	}
	public void setBooleanTransient(Object value)
	{
		this.m_BooleanTransient = (Boolean)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setBooleanTransient", "m_BooleanTransient={0}", this.m_BooleanTransient);
	}
	//Integer
	public Object getIntegerReadOnly()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getIntegerReadOnly", "m_IntegerReadOnly={0}", this.m_IntegerReadOnly);
		return this.m_IntegerReadOnly;
	}
	public void setIntegerReadOnly(Object value)
	{
		this.m_IntegerReadOnly = (Integer)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setIntegerReadOnly", "m_IntegerReadOnly={0}", this.m_IntegerReadOnly);
	}
	public Object getIntegerReadWrite()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getIntegerReadWrite", "m_IntegerReadWrite={0}", this.m_IntegerReadWrite);
		return this.m_IntegerReadWrite;
	}
	public void setIntegerReadWrite(Object value)
	{
		this.m_IntegerReadWrite = (Integer)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setIntegerReadWrite", "m_IntegerReadWrite={0}", this.m_IntegerReadWrite);
	}
	public Object getIntegerHidden()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getIntegerHidden", "m_IntegerHidden={0}", this.m_IntegerHidden);
		return this.m_IntegerHidden;
	}
	public void setIntegerHidden(Object value)
	{
		this.m_IntegerHidden = (Integer)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setIntegerHidden", "m_IntegerHidden={0}", this.m_IntegerHidden);
	}
	public Object getIntegerTransient()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getIntegerTransient", "m_IntegerTransient={0}", this.m_IntegerTransient);
		return this.m_IntegerTransient;
	}
	public void setIntegerTransient(Object value)
	{
		this.m_IntegerTransient = (Integer)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setIntegerTransient", "m_IntegerTransient={0}", this.m_IntegerTransient);
	}
	public Object getIntegerMin()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getIntegerMin", "m_IntegerMin={0}", this.m_IntegerMin);
		return this.m_IntegerMin;
	}
	public void setIntegerMin(Object value)
	{
		this.m_IntegerMin = (Integer)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setIntegerMin", "m_IntegerMin={0}", this.m_IntegerMin);
	}
	public Object getIntegerMinMax()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getIntegerMinMax", "m_IntegerMinMax={0}", this.m_IntegerMinMax);
		return this.m_IntegerMinMax;
	}
	public void setIntegerMinMax(Object value)
	{
		this.m_IntegerMinMax = (Integer)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setIntegerMinMax", "m_IntegerMinMax={0}", this.m_IntegerMinMax);
	}
	// Long
	public Object getLongReadOnly() 				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getLongReadOnly", "m_LongReadOnly={0}", this.m_LongReadOnly);
		return this.m_LongReadOnly;
	}
	public void setLongReadOnly(Object value)
	{
		this.m_LongReadOnly = (Long)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setLongReadOnly", "m_LongReadOnly={0}", this.m_LongReadOnly);
	}
	public Object getLongReadWrite()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getLongReadWrite", "m_LongReadWrite={0}", this.m_LongReadWrite);
		return this.m_LongReadWrite;
	}
	public void setLongReadWrite(Object value) 		
	{
		this.m_LongReadWrite = (Long)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setLongReadWrite", "m_LongReadWrite={0}", this.m_LongReadWrite);
	}
	public Object getLongHidden()					
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getLongHidden", "m_LongHidden={0}", this.m_LongHidden);
		return this.m_LongHidden;
	}
	public void setLongHidden(Object value)			
	{
		this.m_LongHidden = (Long)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setLongHidden", "m_LongHidden={0}", this.m_LongHidden);
	}
	public Object getLongTransient()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getLongTransient", "m_LongTransient={0}", this.m_LongTransient);
		return this.m_LongTransient;
	}
	public void setLongTransient(Object value)		
	{
		this.m_LongTransient = (Long)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setLongTransient", "m_LongTransient={0}", this.m_LongTransient);
	}
	// Double
	public Object getDoubleReadOnly() 				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDoubleReadOnly", "m_DoubleReadOnly={0}", this.m_DoubleReadOnly);
		return this.m_DoubleReadOnly;
	}
	public void setDoubleReadOnly(Object value)
	{
		this.m_DoubleReadOnly = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDoubleReadOnly", "m_DoubleReadOnly={0}", this.m_DoubleReadOnly);
	}
	public Object getDoubleReadWrite()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDoubleReadWrite", "m_DoubleReadWrite={0}", this.m_DoubleReadWrite);
		return this.m_DoubleReadWrite;
	}
	public void setDoubleReadWrite(Object value)	
	{
		this.m_DoubleReadWrite = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDoubleReadWrite", "m_DoubleReadWrite={0}", this.m_DoubleReadWrite);
	}
	public Object getDoubleHidden()					
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDoubleHidden", "m_DoubleHidden={0}", this.m_DoubleHidden);
		return this.m_DoubleHidden;
	}
	public void setDoubleHidden(Object value)		
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDoubleHidden", "m_DoubleHidden={0}", this.m_DoubleHidden);
		this.m_DoubleHidden = (Double)value;
	}
	public Object getDoubleTransient()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDoubleTransient", "m_DoubleTransient={0}", this.m_DoubleTransient);
		return this.m_DoubleTransient;
	}
	public void setDoubleTransient(Object value)
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDoubleTransient", "m_DoubleTransient={0}", this.m_DoubleTransient);
		this.m_DoubleTransient = (Double)value;
	}
	public Object getDoubleMin()					
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDoubleMin", "m_DoubleMin={0}", this.m_DoubleMin);
		return this.m_DoubleMin;
	}
	public void setDoubleMin(Object value)			
	{
		this.m_DoubleMin = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDoubleMin", "m_DoubleMin={0}", this.m_DoubleMin);
	}
	public Object getDoubleMinMax()					
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDoubleMinMax", "m_DoubleMinMax={0}", this.m_DoubleMinMax);
		return this.m_DoubleMinMax;
	}
	public void setDoubleMinMax(Object value)		
	{
		this.m_DoubleMinMax = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDoubleMinMax", "m_DoubleMinMax={0}", this.m_DoubleMinMax);
	}
	//Quantity
	public Object getQuantityReadOnly() 			
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getQuantityReadOnly", "m_QuantityReadOnly={0}", this.m_QuantityReadOnly);
		return this.m_QuantityReadOnly;
	}
	public void setQuantityReadOnly(Object value)
	{
		this.m_QuantityReadOnly = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setQuantityReadOnly", "m_QuantityReadOnly={0}", this.m_QuantityReadOnly);
	}
	public Object getQuantityReadWrite()			
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getQuantityReadWrite", "m_QuantityReadWrite={0}", this.m_QuantityReadWrite);
		return this.m_QuantityReadWrite;
	}
	public void setQuantityReadWrite(Object value) 	
	{
		this.m_QuantityReadWrite = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setQuantityReadWrite", "m_QuantityReadWrite={0}", this.m_QuantityReadWrite);
	}
	public Object getQuantityHidden()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getQuantityHidden", "m_QuantityHidden={0}", this.m_QuantityHidden);
		return this.m_QuantityHidden;
	}
	public void setQuantityHidden(Object value)		
	{
		this.m_QuantityHidden = (Double)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setQuantityHidden", "m_QuantityHidden={0}", this.m_QuantityHidden);
	}
	public Object getQuantityTransient()			
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getQuantityTransient", "m_QuantityTransient={0}", this.m_QuantityTransient);
		return this.m_QuantityTransient;
	}
	public void setQuantityTransient(Object value)	
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "setQuantityTransient", "m_QuantityTransient={0}", this.m_QuantityTransient);
		this.m_QuantityTransient = (Double)value;
	}
	// String
	public Object getStringReadOnly() 				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getStringReadOnly", "m_StringReadOnly={0}", this.m_StringReadOnly);
		return this.m_StringReadOnly;
	}
	public void setStringReadOnly(Object value)
	{
		this.m_StringReadOnly = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setStringReadOnly", "m_StringReadOnly={0}", this.m_StringReadOnly);
	}
	public Object getStringReadWrite()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getStringReadWrite", "m_StringReadWrite={0}", this.m_StringReadWrite);
		return this.m_StringReadWrite;
	}
	public void setStringReadWrite(Object value) 	
	{
		this.m_StringReadWrite = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setStringReadWrite", "m_StringReadWrite={0}", this.m_StringReadWrite);
	}
	public Object getStringHidden() 				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getStringHidden", "m_StringHidden={0}", this.m_StringHidden);
		return this.m_StringHidden;
	}
	public void setStringHidden(Object value) 		
	{
		this.m_StringHidden = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setStringHidden", "m_StringHidden={0}", this.m_StringHidden);
	}
	public Object getStringTransient()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getStringTransient", "m_StringTransient={0}", this.m_StringTransient);
		return this.m_StringTransient;
	}
	public void setStringTransient(Object value)	
	{
		this.m_StringTransient = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setStringTransient", "m_StringTransient={0}", this.m_StringTransient);
	}
	public Object getStringMultiLine()				
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getStringMultiLine", "m_StringMultiLine={0}", this.m_StringMultiLine);
		return this.m_StringMultiLine;
	}
	public void setStringMultiLine(Object value)	
	{
		this.m_StringMultiLine = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setStringMultiLine", "m_StringMultiLine={0}", this.m_StringMultiLine);
	}
	//File
	public Object getDir()					
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDir", "m_Dir={0}", this.m_Dir);
		return this.m_Dir;
	}
	public void setDir(Object value) 		
	{
		this.m_Dir = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDir", "m_Dir={0}", this.m_Dir);
	}
	//Choices
	public Object getChoiceColor()
	throws AgCoreException 
	{
		String colorString = colorToString(this.m_ChoiceColor);
		s_Logger.logp(Level.FINEST, s_LoggerName, "getChoiceColor", "colorString={0}", colorString);
		return colorString;
	}
	
	public void setChoiceColor(Object value)
	throws AgCoreException
	{
		String colorString = (String)value;
		s_Logger.logp(Level.FINEST, s_LoggerName, "setChoiceColor", "colorString={0}", colorString);
		this.m_ChoiceColor = stringToColor(colorString);
	}

	private String colorToString(Color choiceColor) 
	throws AgCoreException
	{
		if(choiceColor == null) return this.m_StringColors[5].getString();
		else if(choiceColor == Color.RED) return this.m_StringColors[0].getString();
		else if(choiceColor == Color.ORANGE) return this.m_StringColors[1].getString();
		else if(choiceColor == Color.YELLOW) return this.m_StringColors[2].getString();
		else if(choiceColor == Color.GREEN) return this.m_StringColors[3].getString();
		else if(choiceColor == Color.BLUE) return this.m_StringColors[4].getString();
		else return this.m_StringColors[5].getString();
	}

	private Color stringToColor(String value) 
	throws AgCoreException
	{
		if(value.equalsIgnoreCase(this.m_StringColors[0].getString())) return Color.RED;
		else if(value.equalsIgnoreCase(this.m_StringColors[1].getString())) return Color.ORANGE;
		else if(value.equalsIgnoreCase(this.m_StringColors[2].getString())) return Color.YELLOW;
		else if(value.equalsIgnoreCase(this.m_StringColors[3].getString())) return Color.GREEN;
		else if(value.equalsIgnoreCase(this.m_StringColors[4].getString())) return Color.BLUE;
		else if(value.equalsIgnoreCase(this.m_StringColors[5].getString())) return null;
		else return null;
	}

	public Object getChoicePet()
	throws AgCoreException 
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getChoicePet", "m_ChoicePet={0}", this.m_ChoicePet);
		return this.m_ChoicePet;
	}
	
	public void setChoicePet(Object value)
	throws AgCoreException
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "setChoicePet", "m_ChoicePet={0}", this.m_ChoicePet);
		this.m_ChoicePet = (String)value;
	}
	public Object getChoicePetChoices()
	throws AgCoreException
	{
		AgSafeArray pets = null;
		pets = new AgSafeArray(AgVariantTypes.VT_VARIANT, this.m_StringPets.length);
		pets.setObjectArray(this.m_StringPets);
		return pets;
	}

	//===================================
	//  IAgUtPluginConfig
	//===================================
	public IAgDispatch getPluginConfig(IAgAttrBuilder builder)
	throws AgCoreException 
	{
		if(this.m_ConfigScope == null)
		{
			this.m_ConfigScope = builder.newScope();
			getJavaProperties(builder, this.m_ConfigScope);
			getDispatchProperties(builder, this.m_ConfigScope);
		}

		return this.m_ConfigScope;
	}

	private void getDispatchProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException
	{
		IAgDispatch localScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Dispatch Properties", 
		"The dispatch property examples for a plugin", 
		localScope);

		getDispatchBooleanProperties(builder, localScope);
		getDispatchIntegerProperties(builder, localScope);
		getDispatchLongProperties(builder, localScope);
		getDispatchDoubleProperties(builder, localScope);
		getDispatchQuantityProperties(builder, localScope);
		getDispatchStringProperties(builder, localScope);
		getDispatchChoicesProperties(builder, localScope);
		getDispatchFileDirProperties(builder, localScope);
	}

	private void getDispatchBooleanProperties(IAgAttrBuilder builder, IAgDispatch parentScope) 
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"Boolean Properties", 
		"The boolean properties of the plugin", 
		localScope);

		builder.addBoolDispatchProperty
		(localScope, 
		"Boolean Read Only", 
		"Boolean property that is read only", 
		"BooleanReadOnly", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY.getValue());

		builder.addBoolDispatchProperty
		(localScope, 
		"Boolean Read Write", 
		"Boolean property that is readable and writable", 
		"BooleanReadWrite", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addBoolDispatchProperty
		(localScope, 
		"Boolean Hidden",
		"This Boolean is not visible in the user interface property page, but will be saved in the plugin configuration", 
		"BooleanHidden",
		AgEAttrAddFlags.E_ADD_FLAG_HIDDEN.getValue());

		builder.addBoolDispatchProperty
		(localScope, 
		"Boolean Transient",
		"This Boolean value will not be saved in the plugin configuration", 
		"BooleanTransient",
		AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT.getValue());
	}
	
	private void getDispatchIntegerProperties(IAgAttrBuilder builder, IAgDispatch parentScope) 
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"Integer Properties", 
		"The integer properties of the plugin", 
		localScope);

		builder.addIntDispatchProperty
		(localScope, 
		"Integer Read Only", 
		"Integer property that is read only", 
		"IntegerReadOnly", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY.getValue());

		builder.addIntDispatchProperty
		(localScope, 
		"Integer Read Write", 
		"Integer property that is readable and writable", 
		"IntegerReadWrite", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addIntDispatchProperty
		(localScope, 
		"Integer Hidden",
		"This Integer is not visible in the user interface property page, but will be saved in the plugin configuration", 
		"IntegerHidden",
		AgEAttrAddFlags.E_ADD_FLAG_HIDDEN.getValue());

		builder.addIntDispatchProperty
		(localScope, 
		"Integer Transient",
		"This Integer value will not be saved in the plugin configuration", 
		"IntegerTransient",
		AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT.getValue());
		
		builder.addIntMinDispatchProperty
		(localScope,
		"Integer Min",
		"This Integer value will have a minimum value that is allowed to be set by the user",
		"IntegerMin", 
		10,
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addIntMinMaxDispatchProperty
		(localScope,
		"Integer MinMax",
		"This Integer value will have a minimum value and a maximum value that is allowed to be set by the user",
		"IntegerMinMax", 
		10,
		20,
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());
	}

	private void getDispatchLongProperties(IAgAttrBuilder builder, IAgDispatch parentScope) 
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"Long Properties", 
		"The long properties of the plugin", 
		localScope);

		builder.addLongDispatchProperty
		(localScope, 
		"Long Read Only", 
		"Long property that is read only", 
		"LongReadOnly", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY.getValue());

		builder.addLongDispatchProperty
		(localScope, 
		"Long Read Write", 
		"Long property that is readable and writable", 
		"LongReadWrite", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addLongDispatchProperty
		(localScope, 
		"Long Hidden",
		"This Long is not visible in the user interface property page, but will be saved in the plugin configuration", 
		"LongHidden",
		AgEAttrAddFlags.E_ADD_FLAG_HIDDEN.getValue());

		builder.addLongDispatchProperty
		(localScope, 
		"Long Transient",
		"This Long value will not be saved in the plugin configuration", 
		"LongTransient",
		AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT.getValue());
	}
	
	private void getDispatchDoubleProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"Double Properties", 
		"The double properties of the plugin", 
		localScope);

		builder.addDoubleDispatchProperty
		(localScope, 
		"Double Read Only", 
		"Double property that is read only", 
		"DoubleReadOnly", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY.getValue());

		builder.addDoubleDispatchProperty
		(localScope, 
		"Double Read Write", 
		"Double property that is readable and writable", 
		"DoubleReadWrite", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addDoubleDispatchProperty
		(localScope, 
		"Double Hidden",
		"This Double is not visible in the user interface property page, but will be saved in the plugin configuration", 
		"DoubleHidden",
		AgEAttrAddFlags.E_ADD_FLAG_HIDDEN.getValue());

		builder.addDoubleDispatchProperty
		(localScope, 
		"Double Transient",
		"This Double value will not be saved in the plugin configuration", 
		"DoubleTransient",
		AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT.getValue());

		builder.addDoubleMinDispatchProperty
		(localScope, 
		"Double Min",
		"This Double value has a minimum value that can be set by the user", 
		"DoubleMin",
		Math.PI,
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addDoubleMinMaxDispatchProperty
		(localScope, 
		"Double MinMax",
		"This Double value has minimum and maximum values that can be set by the user", 
		"DoubleMinMax",
		Math.PI,
		Math.PI * 2,
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());
}

	private void getDispatchQuantityProperties(IAgAttrBuilder builder, IAgDispatch parentScope) 
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"Quantity Properties", 
		"The quantity properties of the plugin", 
		localScope);
		
		builder.addQuantityDispatchProperty2
		(localScope,
		"Quantity Read Only", 
		"Quantity property that is read only", 
		"QuantityReadOnly", // get/set will be prefixed to this name
		"DistanceUnit",
		"Meters",
		"Meters",
		AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY.getValue());

		builder.addQuantityDispatchProperty2
		(localScope,
		"Quantity Read Write", 
		"Quantity property that is readable and writable", 
		"QuantityReadWrite", // get/set will be prefixed to this name
		"DistanceUnit",
		"Meters",
		"Meters",
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());
		
		builder.addQuantityDispatchProperty2
		(localScope, 
		"Quantity Hidden",
		"This Quantity is not visible in the user interface property page, but will be saved in the plugin configuration", 
		"QuantityHidden",
		"DistanceUnit",
		"Meters",
		"Meters",
		AgEAttrAddFlags.E_ADD_FLAG_HIDDEN.getValue());

		builder.addQuantityDispatchProperty2
		(localScope, 
		"Quantity Transient",
		"This Quantity will not be saved in the plugin configuration", 
		"QuantityTransient",
		"DistanceUnit",
		"Meters",
		"Meters",
		AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT.getValue());
	}
	
	private void getDispatchStringProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException
	{
		IAgDispatch localScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"String Properties", 
		"The string properties of the plugin", 
		localScope);

		builder.addStringDispatchProperty
		(localScope, 
		"String Read Only", 
		"This string is readonly", 
		"StringReadOnly",
		AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY.getValue());

		builder.addStringDispatchProperty
		(localScope, 
		"String Read Write",
		"This string is readable and writable", 
		"StringReadWrite",
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());

		builder.addStringDispatchProperty
		(localScope, 
		"String Hidden",
		"This string is not visible in the user interface property page, but will be saved in the plugin configuration", 
		"StringHidden",
		AgEAttrAddFlags.E_ADD_FLAG_HIDDEN.getValue());

		builder.addStringDispatchProperty
		(localScope, 
		"String Transient",
		"This string value will not be saved in the plugin configuration", 
		"StringTransient",
		AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT.getValue());
		
		builder.addMultiLineStringDispatchProperty
		(localScope, 
		"String MultiLine",
		"This string is multiLine",
		"StringMultiLine",
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());
	}	

	private void getDispatchChoicesProperties(IAgAttrBuilder builder, IAgDispatch parentScope) 
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"Choice Properties", 
		"The choice properties of the plugin", 
		localScope);

		AgSafeArray colors = null;
		colors = new AgSafeArray(AgVariantTypes.VT_VARIANT, this.m_StringColors.length);
		colors.setObjectArray(this.m_StringColors);
		
		builder.addChoicesDispatchProperty
		(localScope, 
		"Choice Color", 
		"Choice property that is a color", 
		"ChoiceColor", // get/set will be prefixed to this name
		colors);

		builder.addChoicesFuncDispatchProperty
		(localScope, 
		"Choice Pets", 
		"Choice property that is a pet", 
		"ChoicePet", // get/set will be prefixed to this name
		"ChoicePetChoices");
	}
	
	private void getDispatchFileDirProperties(IAgAttrBuilder builder, IAgDispatch parentScope) 
	throws AgCoreException 
	{
		IAgDispatch localScope = builder.newScope();

		builder.addScopeDispatchProperty
		(parentScope, 
		"File/Dir Properties", 
		"The file/dir properties of the plugin", 
		localScope);

		builder.addDirectoryDispatchProperty
		(localScope, 
		"Directory", 
		"Directory property that is a scenario file", 
		"Dir", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue());
	}

	public void verifyPluginConfig(IAgUtPluginConfigVerifyResult apcvr)
	throws AgCoreException 
	{
		boolean	result	= true;
		String	message = "Ok";

		apcvr.setResult(result);
		apcvr.setMessage(message);
	}

	//===================================
	//  IAgAccessConstraintPlugin
	//===================================
	public String getDisplayName() 
	throws AgCoreException 
	{ 
		return this.m_DisplayName;
	}

	public void register(IAgAccessConstraintPluginResultRegister result)
	throws AgCoreException 
	{
		try
		{
			result.setTargetDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.addTarget(AgEAccessConstraintObjectType.E_SENSOR);

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_FACILITY);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Facility to Sensor)");
			
			result.setBaseObjectType(AgEAccessConstraintObjectType.E_TARGET);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Target to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_AIRCRAFT);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Aircraft to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_GROUND_VEHICLE);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(GroundVehicle to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_LAUNCH_VEHICLE);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(LaunchVehicle to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_MISSILE);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Missile to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_PLANET);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Planet to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_SATELLITE);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Satellite to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_SHIP);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Ship to Sensor)");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_STAR);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Star to Sensor)");
		}
		catch(AgCoreException t)
		{
			t.printStackTrace();
			throw t;
		}
	}

	public boolean init(IAgUtPluginSite pluginSite) 
	throws AgCoreException 
	{
		try
		{
			// This sample is to show configuration only, 
			// it does not compute any access constraint
			this.m_IAgUtPluginSite = pluginSite;

			if(this.m_IAgUtPluginSite != null)
			{
				Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": init()");
			}
		}
		catch(AgCoreException t)
		{
			t.printStackTrace();
			throw t;
		}

		return true;
	}

	public boolean preCompute(IAgAccessConstraintPluginResultPreCompute result)
	throws AgCoreException 
	{
		try
		{
			// This sample is to show configuration only, 
			// it does not compute any access constraint
			if(this.m_IAgUtPluginSite != null)
			{
				Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": preCompute()");
			}
		}
		catch(AgCoreException t)
		{
			t.printStackTrace();
			throw t;
		}
		return true;
	}

	public boolean evaluate(IAgAccessConstraintPluginResultEval result,
			IAgAccessConstraintPluginObjectData baseObj,
			IAgAccessConstraintPluginObjectData targetObj) 
	throws AgCoreException 
	{
		try
		{
			// This sample is to show configuration only, 
			// it does not compute any access constraint
			if(result != null)
			{
				result.setValue(1.0);
			}
			Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": evaluate()");
		}
		catch(AgCoreException t)
		{
			Message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, this.m_DisplayName+": evaluate()" + t.getMessage());
			t.printStackTrace();
			throw t;
		}
		return true;
	}

	public boolean postCompute(IAgAccessConstraintPluginResultPostCompute result)
	throws AgCoreException 
	{
		try
		{
			// This sample is to show configuration only, 
			// it does not compute any access constraint
			if(this.m_IAgUtPluginSite != null)
			{
				Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": postCompute()");
			}
		}
		catch(AgCoreException t)
		{
			t.printStackTrace();
			throw t;
		}
		return true;
	}

	public void free() 
	throws AgCoreException 
	{
		try
		{
			// This sample is to show configuration only, 
			// it does not compute any access constraint
			if(this.m_IAgUtPluginSite != null)
			{
				Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": free()");
			}
			this.m_IAgUtPluginSite = null;
		}
		catch(AgCoreException t)
		{
			t.printStackTrace();
			throw t;
		}
	}
	
	/**
	 * Logs a message to the STK Message Viewer
	 * @param severity One of the final static members of AgEUtLogMsgType.
	 * @param msg The message to display in the message viewer.
	 * @throws AgCoreException If an error occurred while logging the message. 
	 */
	private void Message(AgEUtLogMsgType severity, String msg)
	throws AgCoreException
	{
		if(this.m_IAgUtPluginSite != null)
		{
			this.m_IAgUtPluginSite.message(severity, msg);
		}
		else
		{
			if(severity.equals(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM) ||
				severity.equals(AgEUtLogMsgType.E_UT_LOG_MSG_WARNING))
			{
				System.err.println(msg);
			}
			else
			{
				System.out.println(msg);
			}
		}
	}
}
