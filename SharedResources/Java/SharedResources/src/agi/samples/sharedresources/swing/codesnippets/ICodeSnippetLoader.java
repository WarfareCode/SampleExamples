package agi.samples.sharedresources.swing.codesnippets;

// Java API
import javax.swing.*;

public interface ICodeSnippetLoader
{
	public abstract String getRootName();
	
	public abstract Class<?> getImageHelperClass();
	
	public abstract void load(JTree tree);
}
