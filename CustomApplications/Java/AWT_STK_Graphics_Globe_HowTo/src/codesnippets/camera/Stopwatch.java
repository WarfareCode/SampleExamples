package codesnippets.camera;

import org.joda.time.Period;
import org.joda.time.PeriodType;

/**
 * Provides a set of methods and properties that you can use to accurately measure elapsed time.
 */
public class Stopwatch
{
	/**
	 * Gets the frequency of the timer as the number of ticks per second. This field is read-only.
	 */
	public static final long	FREQUENCY	= 1000000000L;

	private long				startTime;
	private long				elapsed;
	private boolean				running;

	/**
	 * Initializes a new instance of the Stopwatch class.
	 */
	public Stopwatch()
	{
	}

	/**
	 * Gets the total elapsed time measured by the current instance.
	 * 
	 * @return A time span representing the total elapsed time measured by the current instance.
	 */
	public Period getElapsed()
	{
		return new Period(getElapsedMilliseconds(), PeriodType.dayTime());
	}

	/**
	 * Starts, or resumes, measuring elapsed time for an interval.
	 */
	public void start()
	{
		if(!running)
		{
			startTime = System.currentTimeMillis();
			running = true;
		}
	}

	/**
	 * Stops measuring elapsed time for an interval.
	 */
	public void stop()
	{
		if(running)
		{
			elapsed += System.currentTimeMillis() - startTime;
			running = false;
		}
	}

	/**
	 * Stops time interval measurement and resets the elapsed time to zero.
	 */
	public void reset()
	{
		running = false;
		elapsed = 0;
		startTime = 0;
	}

	/**
	 * Gets a value indicating whether the Stopwatch timer is running.
	 * 
	 * @return true if the Stopwatch instance is currently running and measuring elapsed time for an interval; otherwise, false.
	 */
	public boolean isRunning()
	{
		return running;
	}

	/**
	 * Gets the total elapsed time measured by the current instance, in milliseconds.
	 * 
	 * @return A long representing the total number of milliseconds measured by the current instance.
	 */
	public long getElapsedMilliseconds()
	{
		long result = elapsed;
		if(running)
		{
			result += System.currentTimeMillis() - startTime;
		}
		return result;
	}

	/**
	 * Gets the current number of ticks in the timer mechanism.
	 * 
	 * @return A long representing the tick counter value of the underlying timer mechanism.
	 */
	public static long getTimestamp()
	{
		return System.currentTimeMillis();
	}
}
