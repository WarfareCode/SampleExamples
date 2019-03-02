package agi.stk.plugin.accessconstraints.common;

public interface IJavaPropertiesExample 
{
	// Base
	public Object getJavaVendor();
	public Object getJavaVendorURL();
	public Object getJavaVersion();

	// Spec
	public Object getJavaSpecName();
	public Object getJavaSpecVendor();
	public Object getJavaSpecVersion();
	
	// Compiler
	public Object getJavaClsVersion();
	public Object getJavaCompiler();

	// Dirs
	public Object getJavaHome();
	public Object getJavaClassPath();
	public Object getJavaLibraryPath();
	public Object getJavaExtDirs();
	public Object getJavaIoTmpDir();

	// VM
	public Object getJavaVmName();
	public Object getJavaVmVendor();
	public Object getJavaVmVersion();
	public Object getJavaVmSpecName();
	public Object getJavaVmSpecVendor();
	public Object getJavaVmSpecVersion();

	//User
	public Object getJavaUserName();
	public Object getJavaUserDir();
	public Object getJavaUserHome();

	// Separator
	public Object getJavaLineSep();
	public Object getJavaPathSep();
	public Object getJavaFileSep();

	// OS
	public Object getJavaOsName();
	public Object getJavaOsArch();
	public Object getJavaOsVersion();
}
