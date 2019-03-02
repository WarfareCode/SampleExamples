package agi.samples.sharedresources.swing.codesnippets;

// Java API
import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;
import java.io.*;

// Sample API
import agi.samples.sharedresources.codesnippets.*;

public class CodeSnippetViewJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private JEditorPane	m_JEditorPane;
	private JScrollPane	m_JScrollPane;

	private JLabel		m_SourceLinkJLabel;
	private JLabel		m_SourceLinkTextJLabel;

	private JCheckBox	m_ShowImportsJCheckBox;

	private CodeSnippet	m_CodeSnippet;

	public CodeSnippetViewJPanel()
	{
		super();
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout(new BorderLayout());

		this.setBorder(new BevelBorder(BevelBorder.LOWERED));
		
		this.m_JEditorPane = new JEditorPane();
		this.m_JEditorPane.setEditable(false);
		this.m_JEditorPane.setBackground(Color.WHITE);
		this.m_JEditorPane.setContentType("text/java");
		this.m_JEditorPane.setBorder(new BevelBorder(BevelBorder.LOWERED));

		this.m_JScrollPane = new JScrollPane(this.m_JEditorPane);
		// this.m_JScrollPane.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS);
		this.add(this.m_JScrollPane, BorderLayout.CENTER);

		JPanel p = new JPanel();
		p.setLayout(new BorderLayout());
		p.setBorder(new BevelBorder(BevelBorder.RAISED));
		this.add(p, BorderLayout.NORTH);

		this.m_SourceLinkJLabel = new JLabel();
		this.m_SourceLinkJLabel.setText("Source File:");
		this.m_SourceLinkJLabel.setBorder(new EmptyBorder(0,5,0,5));
		p.add(this.m_SourceLinkJLabel, BorderLayout.WEST);

		this.m_SourceLinkTextJLabel = new JLabel();
		this.m_SourceLinkTextJLabel.setText("");
		this.m_SourceLinkTextJLabel.setCursor(new Cursor(Cursor.HAND_CURSOR));
		CompoundBorder b = new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0,5,0,5));
		this.m_SourceLinkTextJLabel.setBorder(b);
		p.add(this.m_SourceLinkTextJLabel, BorderLayout.CENTER);

		this.m_ShowImportsJCheckBox = new JCheckBox();
		this.m_ShowImportsJCheckBox.setText("Show imports");
		this.m_ShowImportsJCheckBox.setBorder(new EmptyBorder(0,5,0,5));
		p.add(this.m_ShowImportsJCheckBox, BorderLayout.EAST);
	}

	public JLabel getSourceLinkTextJLabel()
	{
		return this.m_SourceLinkTextJLabel;
	}

	public JCheckBox getShowImportsJCheckBox()
	{
		return this.m_ShowImportsJCheckBox;
	}

	public void displayCodeSnippet(CodeSnippet codeSnippet, boolean showImports)
	throws IOException
	{
		this.m_CodeSnippet = codeSnippet;
		if(this.m_CodeSnippet == null)
		{
			this.m_JEditorPane.setText("");
			this.m_SourceLinkTextJLabel.setText("");
		}
		else
		{
			StringBuffer codeSnippetText = new StringBuffer();
			if(showImports)
			{
				codeSnippetText.append(codeSnippet.getImports());
			}
			codeSnippetText.append(this.m_CodeSnippet.getCode());
			this.m_JEditorPane.setText(codeSnippetText.toString());
			this.m_JEditorPane.setCaretPosition(0);

			String fileName = this.m_CodeSnippet.getFileName();
			String sourceLinkText = "<html><u>" + fileName + "</u></html>";
			this.m_SourceLinkTextJLabel.setText(sourceLinkText);
		}
	}
}
