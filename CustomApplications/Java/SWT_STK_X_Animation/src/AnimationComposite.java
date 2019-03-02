// Eclipse API
import org.eclipse.swt.SWT;
import org.eclipse.swt.events.SelectionListener;
import org.eclipse.swt.layout.FillLayout;
import org.eclipse.swt.widgets.Composite;

public final class AnimationComposite
extends Composite
{
	/*package*/ AnimationButton	m_AnimPlayForwardButton;
	/*package*/ AnimationButton	m_AnimPlayBackwardButton;
	/*package*/ AnimationButton	m_AnimPauseButton;
	/*package*/ AnimationButton	m_AnimStepForwardButton;
	/*package*/ AnimationButton	m_AnimStepBackwardButton;
	/*package*/ AnimationButton	m_AnimFasterButton;
	/*package*/ AnimationButton	m_AnimSlowerButton;
	/*package*/ AnimationButton	m_AnimRewindButton;

	public AnimationComposite(Composite parent, int style)
	{
		super(parent, style);

		this.setLayout(new FillLayout(SWT.HORIZONTAL));

		this.m_AnimRewindButton = new AnimationButton(this, "Rewind", "AgAnimationRewind.gif");
		this.m_AnimStepBackwardButton = new AnimationButton(this, "Step Backward", "AgAnimationStepBackward.gif");
		this.m_AnimPlayBackwardButton = new AnimationButton(this, "Play Backward", "AgAnimationPlayBackward.gif");
		this.m_AnimPauseButton = new AnimationButton(this, "Pause", "AgAnimationPause.gif");
		this.m_AnimPlayForwardButton = new AnimationButton(this, "Play Forward", "AgAnimationPlayForward.gif");
		this.m_AnimStepForwardButton = new AnimationButton(this, "Step Forward", "AgAnimationStepForward.gif");
		this.m_AnimSlowerButton = new AnimationButton(this, "Slower", "AgAnimationSlower.gif");
		this.m_AnimFasterButton = new AnimationButton(this, "Faster", "AgAnimationFaster.gif");
	}

	/* package */void addSelectionListener(SelectionListener sl)
	{
		this.m_AnimRewindButton.addSelectionListener(sl);
		this.m_AnimSlowerButton.addSelectionListener(sl);
		this.m_AnimStepBackwardButton.addSelectionListener(sl);
		this.m_AnimPlayBackwardButton.addSelectionListener(sl);
		this.m_AnimPauseButton.addSelectionListener(sl);
		this.m_AnimPlayForwardButton.addSelectionListener(sl);
		this.m_AnimStepForwardButton.addSelectionListener(sl);
		this.m_AnimFasterButton.addSelectionListener(sl);
	}

	/* package */void removeSelectionListener(SelectionListener sl)
	{
		this.m_AnimRewindButton.removeSelectionListener(sl);
		this.m_AnimSlowerButton.removeSelectionListener(sl);
		this.m_AnimStepBackwardButton.removeSelectionListener(sl);
		this.m_AnimPlayBackwardButton.removeSelectionListener(sl);
		this.m_AnimPauseButton.removeSelectionListener(sl);
		this.m_AnimPlayForwardButton.removeSelectionListener(sl);
		this.m_AnimStepForwardButton.removeSelectionListener(sl);
		this.m_AnimFasterButton.removeSelectionListener(sl);
	}
}
