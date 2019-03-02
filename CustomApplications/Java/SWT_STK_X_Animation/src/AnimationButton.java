// Java API
import java.io.*;

// Eclipse API
import org.eclipse.swt.*;
import org.eclipse.swt.events.*;
import org.eclipse.swt.graphics.*;
import org.eclipse.swt.layout.*;
import org.eclipse.swt.widgets.*;

// AGI Java API
import agi.stk.core.images.animation.*;

// AnimationButton really is a composite that contains a Button
// See the following FAQ for Button derivation
// http://www.eclipse.org/swt/faq.php#subclassing
/* package */final class AnimationButton
extends Composite
{
	private Button	m_Button;
	private Image	m_Image;

	/* package */AnimationButton(Composite parent, String name, String imageName)
	{
		super(parent, SWT.NONE);

		this.setLayout(new FillLayout());

		InputStream is = AgAnimationImageHelper.class.getResourceAsStream(imageName);
		this.m_Image = new Image(this.getDisplay(), is);

		this.m_Button = new Button(this, SWT.PUSH);
		this.m_Button.setToolTipText(name);
		this.m_Button.setImage(this.m_Image);
		this.m_Button.setEnabled(true);
		this.m_Button.addDisposeListener(new DisposeListener()
		{
			public void widgetDisposed(DisposeEvent de)
			{
				m_Image.dispose();
			}
		});
	}

	/* package */void addSelectionListener(SelectionListener sl)
	{
		this.m_Button.addSelectionListener(sl);
	}

	/* package */void removeSelectionListener(SelectionListener sl)
	{
		this.m_Button.removeSelectionListener(sl);
	}
}