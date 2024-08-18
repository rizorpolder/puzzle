using System;

[Serializable]
public struct ShortDataTime
{
	public static ShortDataTime UtcNow => new ShortDataTime(DateTime.UtcNow);

	/// <summary>
	/// Second
	/// </summary>
	public int s;

	/// <summary>
	/// Minute
	/// </summary>
	public int m;

	/// <summary>
	/// Hour
	/// </summary>
	public int h;

	/// <summary>
	/// Day
	/// </summary>
	public int d;

	public ShortDataTime(DateTime dateTime)
	{
		TimeSpan timeSpan = dateTime - GetStartDateTime();
		s = timeSpan.Seconds;
		m = timeSpan.Minutes;
		h = timeSpan.Hours;
		d = timeSpan.Days;
	}

	public ShortDataTime(int s, int m, int h, int d)
	{
		this.s = s;
		this.m = m;
		this.h = h;
		this.d = d;
	}

	public string Print()
	{
		return "(" + d + ")" + "(" + h + ")" + "(" + m + ")" + "(" + s + ")";
	}

	public bool IsEmpty()
	{
		return s == 0 && m == 0 && h == 0 && d == 0;
	}

	private static DateTime GetStartDateTime()
	{
		return new DateTime(2022, 1, 1, 1, 0, 0, 0);
	}

	public DateTime ToDateTime()
	{
		DateTime dateTime = GetStartDateTime().AddDays(d).AddHours(h).AddMinutes(m).AddSeconds(s);
		return dateTime;
	}

	public TimeSpan ToTimeSpan()
	{
		return new TimeSpan(d, h, m, s);
	}

	public ShortDataTime AddMinutes(int minutes)
	{
		return new ShortDataTime(ToDateTime().AddMinutes(minutes));
	}

	public override string ToString()
	{
		return ToDateTime().ToString();
	}
}