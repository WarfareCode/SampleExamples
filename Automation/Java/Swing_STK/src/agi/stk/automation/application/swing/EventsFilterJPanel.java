package agi.stk.automation.application.swing;

// Java API
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;

// STK Java API
import agi.stkobjects.*;

public class EventsFilterJPanel 
extends JPanel
implements ActionListener
{
	private static final long serialVersionUID = 1L;

	private JRadioButton 			m_NoEventsJRadioButton;
	private JRadioButton 			m_AllEventsJRadioButton;
	private JRadioButton 			m_SelectEventsJRadioButton;

	private SelectionFilterJPanel 	m_SelectionFilterJPanel;
	
	private JButton 	m_UpdateJButton;
	
	public EventsFilterJPanel()
	{
		this.setLayout(new BorderLayout());

		JPanel radiosJPanel = new JPanel();
		radiosJPanel.setLayout(new GridLayout(3,1));
		
		this.m_NoEventsJRadioButton = new JRadioButton();
		this.m_NoEventsJRadioButton.setText("No Events");
		this.m_NoEventsJRadioButton.addActionListener(this);
		radiosJPanel.add(this.m_NoEventsJRadioButton);
		
		this.m_AllEventsJRadioButton = new JRadioButton();
		this.m_AllEventsJRadioButton.setText("All Events");
		this.m_AllEventsJRadioButton.addActionListener(this);
		radiosJPanel.add(this.m_AllEventsJRadioButton);

		this.m_SelectEventsJRadioButton = new JRadioButton();
		this.m_SelectEventsJRadioButton.setText("Select Events");
		this.m_SelectEventsJRadioButton.addActionListener(this);
		radiosJPanel.add(this.m_SelectEventsJRadioButton);

		this.add(radiosJPanel, BorderLayout.NORTH);
		
		ButtonGroup bg = new ButtonGroup();
		bg.add(this.m_NoEventsJRadioButton);
		bg.add(this.m_AllEventsJRadioButton);
		bg.add(this.m_SelectEventsJRadioButton);

		this.m_SelectionFilterJPanel = new SelectionFilterJPanel();
		this.add(this.m_SelectionFilterJPanel, BorderLayout.CENTER);
		
		this.m_UpdateJButton = new JButton();
		this.m_UpdateJButton.setText("Update");
		this.add(this.m_UpdateJButton, BorderLayout.SOUTH);

		// Init the state
		this.m_AllEventsJRadioButton.setSelected(true);
		this.m_SelectionFilterJPanel.setEnabled(false);
	}
	
	public AgENotificationFilterMask getFilter()
	{
		AgENotificationFilterMask mask = AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_NO_EVENTS;

		if(this.m_NoEventsJRadioButton.isSelected())
		{
			mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_NO_EVENTS);
		}
		else if(this.m_AllEventsJRadioButton.isSelected())
		{
			mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_ENABLE_ALL_EVENTS);
		}
		else if(this.m_SelectEventsJRadioButton.isSelected())
		{
			if(this.m_SelectionFilterJPanel.m_ShowAnimationEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_ANIMATION_EVENTS);
			}

			if(this.m_SelectionFilterJPanel.m_ShowScenarioEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_SCENARIO_EVENTS);
			}

			if(this.m_SelectionFilterJPanel.m_ShowLoggingEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_LOGGING_EVENTS);
			}

			if(this.m_SelectionFilterJPanel.m_ShowObjectEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_OBJECT_EVENTS);
			}

			if(this.m_SelectionFilterJPanel.m_ShowObjectChangedEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_OBJECT_CHANGED_EVENTS);
			}

			if(this.m_SelectionFilterJPanel.m_ShowObjectRenameEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_OBJECT_RENAME_EVENTS);
			}

			if(this.m_SelectionFilterJPanel.m_ShowPercentCompleteEventsJCheckBox.isSelected())
			{
				mask.add(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_PERCENT_COMPLETE_EVENTS);
			}
		}
		return mask;
	}
	
	public void setFilter(AgENotificationFilterMask mask)
	{
		if(mask.equals(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_NO_EVENTS))
		{
			this.m_NoEventsJRadioButton.setSelected(true);
			this.m_SelectionFilterJPanel.setEnabled(false);
		}
		else if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_ENABLE_ALL_EVENTS))
		{
			this.m_AllEventsJRadioButton.setSelected(true);
			this.m_SelectionFilterJPanel.setEnabled(false);
		}
		else
		{
			this.m_SelectEventsJRadioButton.setSelected(true);
			this.m_SelectionFilterJPanel.setEnabled(true);
			
			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_ANIMATION_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowAnimationEventsJCheckBox.setSelected(true);
			}

			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_SCENARIO_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowScenarioEventsJCheckBox.setSelected(true);
			}

			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_LOGGING_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowLoggingEventsJCheckBox.setSelected(true);
			}
			
			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_OBJECT_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowObjectEventsJCheckBox.setSelected(true);
			}

			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_OBJECT_CHANGED_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowObjectChangedEventsJCheckBox.setSelected(true);
			}

			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_OBJECT_RENAME_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowObjectRenameEventsJCheckBox.setSelected(true);
			}

			if(mask.contains(AgENotificationFilterMask.E_NOTIFICATION_FILTER_MASK_PERCENT_COMPLETE_EVENTS))
			{
				this.m_SelectionFilterJPanel.m_ShowPercentCompleteEventsJCheckBox.setSelected(true);
			}
		}
	}

	public boolean isUpdateJButton(Object src)
	{
		return m_UpdateJButton.equals(src);
	}
	
	public void addActionListener(ActionListener l)
	{
		this.m_UpdateJButton.addActionListener(l);
	}

	public void removeActionListener(ActionListener l)
	{
		this.m_UpdateJButton.removeActionListener(l);
	}

	public void actionPerformed(ActionEvent e)
	{
		try
		{
			if(this.m_NoEventsJRadioButton.isSelected())
			{
				this.m_SelectionFilterJPanel.setEnabled(false);
			}
			else if(this.m_AllEventsJRadioButton.isSelected())
			{
				this.m_SelectionFilterJPanel.setEnabled(false);
			}
			else if(this.m_SelectEventsJRadioButton.isSelected())
			{
				this.m_SelectionFilterJPanel.setEnabled(true);
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	
	private class SelectionFilterJPanel
	extends JPanel
	{
		private static final long serialVersionUID = -1207586507002338558L;

		private JCheckBox 	m_ShowAnimationEventsJCheckBox;
		private JCheckBox 	m_ShowScenarioEventsJCheckBox;
		private JCheckBox 	m_ShowLoggingEventsJCheckBox;
		private JCheckBox 	m_ShowObjectEventsJCheckBox;
		private JCheckBox 	m_ShowObjectChangedEventsJCheckBox;
		private JCheckBox 	m_ShowObjectRenameEventsJCheckBox;
		private JCheckBox 	m_ShowPercentCompleteEventsJCheckBox;

		public SelectionFilterJPanel()
		{
			this.setLayout(new GridLayout(7,1));
			this.setBorder(new TitledBorder("Events"));
			
			this.m_ShowAnimationEventsJCheckBox = new JCheckBox();
			this.m_ShowAnimationEventsJCheckBox.setText("Animation - Such as Rewind, Play Forward, Play Backward, Pause, Step Forward, Step Backward, etc.");
			this.add(this.m_ShowAnimationEventsJCheckBox);

			this.m_ShowScenarioEventsJCheckBox = new JCheckBox();
			this.m_ShowScenarioEventsJCheckBox.setText("Scenario - Such as Close, Load, New, Save, Save As, etc.");
			this.add(this.m_ShowScenarioEventsJCheckBox);

			this.m_ShowLoggingEventsJCheckBox = new JCheckBox();
			this.m_ShowLoggingEventsJCheckBox.setText("Logging - Such as message viewer messages that contain text with errors, warnings, info statements, debug statements, etc.");
			this.add(this.m_ShowLoggingEventsJCheckBox);

			this.m_ShowObjectEventsJCheckBox = new JCheckBox();
			this.m_ShowObjectEventsJCheckBox.setText("Object - Such as add, delete, etc");
			this.add(this.m_ShowObjectEventsJCheckBox);

			this.m_ShowObjectChangedEventsJCheckBox = new JCheckBox();
			this.m_ShowObjectChangedEventsJCheckBox.setText("Object Changed - Such as instance state changed");
			this.add(this.m_ShowObjectChangedEventsJCheckBox);

			this.m_ShowObjectRenameEventsJCheckBox = new JCheckBox();
			this.m_ShowObjectRenameEventsJCheckBox.setText("Object Rename - Such as instance name has been changed");
			this.add(this.m_ShowObjectRenameEventsJCheckBox);

			this.m_ShowPercentCompleteEventsJCheckBox = new JCheckBox();
			this.m_ShowPercentCompleteEventsJCheckBox.setText("Percent Complete");
			this.add(this.m_ShowPercentCompleteEventsJCheckBox);
		}
		
		public void setEnabled(boolean enabled)
		{
			this.m_ShowAnimationEventsJCheckBox.setEnabled(enabled);
			this.m_ShowScenarioEventsJCheckBox.setEnabled(enabled);
			this.m_ShowLoggingEventsJCheckBox.setEnabled(enabled);
			this.m_ShowObjectEventsJCheckBox.setEnabled(enabled);
			this.m_ShowObjectChangedEventsJCheckBox.setEnabled(enabled);
			this.m_ShowObjectRenameEventsJCheckBox.setEnabled(enabled);
			this.m_ShowPercentCompleteEventsJCheckBox.setEnabled(enabled);
		}
	}
}
