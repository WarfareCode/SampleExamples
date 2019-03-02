package agi.stk.plugin.accessconstraints.config;

import agi.core.AgCoreException;

public interface IJavaExample 
{
	//Boolean
	public abstract Object getBooleanReadOnly();
	public abstract void setBooleanReadOnly(Object value);
	public abstract Object getBooleanReadWrite();
	public abstract void setBooleanReadWrite(Object value);
	public abstract Object getBooleanHidden();
	public abstract void setBooleanHidden(Object value);
	public abstract Object getBooleanTransient();
	public abstract void setBooleanTransient(Object value);

	//Integer
	public abstract Object getIntegerReadOnly();
	public abstract void setIntegerReadOnly(Object value);
	public abstract Object getIntegerReadWrite();
	public abstract void setIntegerReadWrite(Object value);
	public abstract Object getIntegerHidden();
	public abstract void setIntegerHidden(Object value);
	public abstract Object getIntegerTransient();
	public abstract void setIntegerTransient(Object value);

	//Long
	public abstract Object getLongReadOnly();
	public abstract void setLongReadOnly(Object value);
	public abstract Object getLongReadWrite();
	public abstract void setLongReadWrite(Object value);
	public abstract Object getLongHidden();
	public abstract void setLongHidden(Object value);
	public abstract Object getLongTransient();
	public abstract void setLongTransient(Object value);

	// Double
	public abstract Object getDoubleReadOnly();
	public abstract void setDoubleReadOnly(Object value);
	public abstract Object getDoubleReadWrite();
	public abstract void setDoubleReadWrite(Object value);
	public abstract Object getDoubleHidden();
	public abstract void setDoubleHidden(Object value);
	public abstract Object getDoubleTransient();
	public abstract void setDoubleTransient(Object value);
	public abstract Object getDoubleMin();
	public abstract void setDoubleMin(Object value);
	public abstract Object getDoubleMinMax();
	public abstract void setDoubleMinMax(Object value);

	// Quantity
	public abstract Object getQuantityReadOnly();
	public abstract void setQuantityReadOnly(Object value);
	public abstract Object getQuantityReadWrite();
	public abstract void setQuantityReadWrite(Object value);
	public abstract Object getQuantityHidden();
	public abstract void setQuantityHidden(Object value);
	public abstract Object getQuantityTransient();
	public abstract void setQuantityTransient(Object value);

	//String
	public abstract Object getStringReadOnly();
	public abstract void setStringReadOnly(Object value);
	public abstract Object getStringReadWrite();
	public abstract void setStringReadWrite(Object value);
	public abstract Object getStringHidden();
	public abstract void setStringHidden(Object value);
	public abstract Object getStringTransient();
	public abstract void setStringTransient(Object value);
	public Object getStringMultiLine();
	public void setStringMultiLine(Object value);
	
	// Choice
	public Object getChoiceColor() throws AgCoreException;
	public void setChoiceColor(Object value) throws AgCoreException;

	// File/Dir
	public Object getDir();
	public void setDir(Object value);
}
