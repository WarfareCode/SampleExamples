import java.util.EventListener;

public interface TabCloseListener
extends EventListener
{
	void tabClosed(TabCloseEvent evt);
}
