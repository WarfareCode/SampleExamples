package agi.stk.plugin.accessconstraints.common;

import agi.core.*;
import agi.stk.attr.*;

public class JavaPropertiesExample 
implements IJavaPropertiesExample
{
	// Base
	public Object getJavaVendor()		{return AgSystemPropertiesHelper.getJavaVendor();}
	public Object getJavaVendorURL()	{return AgSystemPropertiesHelper.getJavaVendorURL();}
	public Object getJavaVersion()		{return AgSystemPropertiesHelper.getJavaVersion();}

	// Spec
	public Object getJavaSpecName()		{return AgSystemPropertiesHelper.getJavaSpecificationName();}
	public Object getJavaSpecVendor()	{return AgSystemPropertiesHelper.getJavaSpecificationVendor();}
	public Object getJavaSpecVersion()	{return AgSystemPropertiesHelper.getJavaSpecificationVersion();}
	
	// Compiler
	public Object getJavaClsVersion()	{return AgSystemPropertiesHelper.getJavaClassVersion();}
	public Object getJavaCompiler()		{return AgSystemPropertiesHelper.getJavaCompiler();}

	// Dirs
	public Object getJavaHome()			{return AgSystemPropertiesHelper.getJavaHome();}
	public Object getJavaClassPath()	{return AgSystemPropertiesHelper.getJavaClassPath();}
	public Object getJavaLibraryPath()	{return AgSystemPropertiesHelper.getJavaLibraryPath();}
	public Object getJavaExtDirs()		{return AgSystemPropertiesHelper.getJavaExtDirs();}
	public Object getJavaIoTmpDir()		{return AgSystemPropertiesHelper.getJavaIoTmpDir();}

	// VM
	public Object getJavaVmName()		{return AgSystemPropertiesHelper.getJavaVmName();}
	public Object getJavaVmVendor()		{return AgSystemPropertiesHelper.getJavaVmVendor();}
	public Object getJavaVmVersion()	{return AgSystemPropertiesHelper.getJavaVmVersion();}
	public Object getJavaVmSpecName()	{return AgSystemPropertiesHelper.getJavaVmSpecificationName();}
	public Object getJavaVmSpecVendor()	{return AgSystemPropertiesHelper.getJavaVmSpecificationVendor();}
	public Object getJavaVmSpecVersion(){return AgSystemPropertiesHelper.getJavaVmSpecificationVersion();}

	//User
	public Object getJavaUserName()		{return AgSystemPropertiesHelper.getUserName();}
	public Object getJavaUserDir()		{return AgSystemPropertiesHelper.getUserDir();}
	public Object getJavaUserHome()		{return AgSystemPropertiesHelper.getUserHome();}

	// Separator
	public Object getJavaLineSep()		{return AgSystemPropertiesHelper.getLineSeparator();}
	public Object getJavaPathSep()		{return AgSystemPropertiesHelper.getPathSeparator();}
	public Object getJavaFileSep()		{return AgSystemPropertiesHelper.getFileSeparator();}

	// OS
	public Object getJavaOsName()		{return AgSystemPropertiesHelper.getOsName();}
	public Object getJavaOsArch()		{return AgSystemPropertiesHelper.getOsArch();}
	public Object getJavaOsVersion()	{return AgSystemPropertiesHelper.getOsVersion();}

	protected void getJavaProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Java Properties", 
		"The properties of the Java Runtime", 
		javaPropScope);
	
		builder.addStringDispatchProperty
		(javaPropScope, 
		"Vendor", 
		"The vendor", 
		"JavaVendor", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaPropScope, 
		"Vendor URL", 
		"The vendor url", 
		"JavaVendorURL", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaPropScope, 
		"Version", 
		"The version", 
		"JavaVersion", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		getJavaSpecProperties(builder, javaPropScope);
		getJavaCompilerProperties(builder, javaPropScope);
		getJavaConfigProperties(builder, javaPropScope);
		getJavaVmProperties(builder, javaPropScope);
		getJavaUserProperties(builder, javaPropScope);
		getJavaOsProperties(builder, javaPropScope);
		getJavaSepProperties(builder, javaPropScope);
	}

	private void getJavaSpecProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaSpecPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Specification", 
		"The virtual machine properties", 
		javaSpecPropScope);
	
		builder.addStringDispatchProperty
		(javaSpecPropScope, 
		"Name", 
		"The name",
		"JavaSpecName", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaSpecPropScope, 
		"Vendor", 
		"The vendor", 
		"JavaSpecVendor", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaSpecPropScope, 
		"Version", 
		"The version", 
		"JavaSpecVersion", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}

	private void getJavaCompilerProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaCompilerPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Compiler", 
		"The Compiler properties", 
		javaCompilerPropScope);
	
		builder.addStringDispatchProperty
		(javaCompilerPropScope, 
		"Version", 
		"The compiler version",
		"JavaCompiler", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaCompilerPropScope, 
		"Class Version", 
		"The compiled class bytecode version", 
		"JavaClsVersion", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}

	private void getJavaConfigProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaConfigPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Configuartion", 
		"The configuration properties", 
		javaConfigPropScope);
	
		builder.addStringDispatchProperty
		(javaConfigPropScope, 
		"Java Home", 
		"The JAVA_HOME",
		"JavaHome", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaConfigPropScope, 
		"Classpath", 
		"The CLASSPATH setting", 
		"JavaClassPath", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaConfigPropScope, 
		"Path", 
		"The PATH or LD_LIBRARY_PATH setting", 
		"JavaLibraryPath", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaConfigPropScope, 
		"Ext Directories", 
		"The extension directories", 
		"JavaExtDirs", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaConfigPropScope, 
		"IO Temp Directory", 
		"The temporary directory", 
		"JavaIoTmpDir", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}

	private void getJavaVmProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaVmPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Virtual Machine", 
		"The virtual machine properties", 
		javaVmPropScope);
	
		builder.addStringDispatchProperty
		(javaVmPropScope, 
		"Name", 
		"The name",
		"JavaVmName", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaVmPropScope, 
		"Vendor", 
		"The vendor", 
		"JavaVmVendor", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaVmPropScope, 
		"Version", 
		"The version", 
		"JavaVmVersion", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
		
		getJavaVmSpecProperties(builder, javaVmPropScope);
	}

	private void getJavaVmSpecProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaVmSpecPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Specification", 
		"The virtual machine properties", 
		javaVmSpecPropScope);
	
		builder.addStringDispatchProperty
		(javaVmSpecPropScope, 
		"Name", 
		"The name",
		"JavaVmSpecName", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaVmSpecPropScope, 
		"Vendor", 
		"The vendor", 
		"JavaVmSpecVendor", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaVmSpecPropScope, 
		"Version", 
		"The version", 
		"JavaVmSpecVersion", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}

	private void getJavaUserProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaUserPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"User", 
		"The user properties of the Java Runtime", 
		javaUserPropScope);
	
		builder.addStringDispatchProperty
		(javaUserPropScope, 
		"Name", 
		"The user name",
		"JavaUserName", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaUserPropScope, 
		"Current Directory", 
		"The user current directory",
		"JavaUserDir", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaUserPropScope, 
		"Home Directory", 
		"The user home directory",
		"JavaUserHome", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}

	private void getJavaOsProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaOsPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"OS", 
		"The operating system properties of the Java Runtime", 
		javaOsPropScope);
	
		builder.addStringDispatchProperty
		(javaOsPropScope, 
		"Name", 
		"The os name",
		"JavaOsName", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaOsPropScope, 
		"Architecture", 
		"The os architecture",
		"JavaOsArch", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaOsPropScope, 
		"Version", 
		"The os version",
		"JavaOsVersion", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}

	private void getJavaSepProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException 
	{
		IAgDispatch javaSepPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Separators", 
		"The separator properties of the Java Runtime", 
		javaSepPropScope);
	
		builder.addStringDispatchProperty
		(javaSepPropScope, 
		"Line", 
		"The line separator",
		"JavaLineSep", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaSepPropScope, 
		"Path", 
		"The path separator",
		"JavaPathSep", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());

		builder.addStringDispatchProperty
		(javaSepPropScope, 
		"File", 
		"The file separator",
		"JavaFileSep", // get/set will be prefixed to this name
		AgEAttrAddFlags.of(AgEAttrAddFlags.E_ADD_FLAG_READ_ONLY, AgEAttrAddFlags.E_ADD_FLAG_TRANSIENT).getValue());
	}
}
